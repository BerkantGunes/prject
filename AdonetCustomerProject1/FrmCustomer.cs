using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdonetCustomerProject1
{
    public partial class FrmCustomer: Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("Server=GUNESBERKANT\\SQLEXPRESS;initial catalog=DbCustomer;integrated security=true");

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From TblCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbCity.ValueMember = "CityId"; // listelenecek sehirlerin id degerleri
            cmbCity.DisplayMember = "CityName"; // citylerin kullaniciya gozukecek kismi yani isimleri
            cmbCity.DataSource = dataTable;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select CustomerId, CustomerName, CustomerSurname, CustomerBalance,CustomerStatus,CityName From TblCustomer Inner Join TblCity On TblCity.CityId = TblCustomer.CustomerCity", sqlConnection); 
            SqlDataAdapter adapter = new SqlDataAdapter(command); 
            DataTable dataTable = new DataTable(); 
            adapter.Fill(dataTable); 
            customerDGV.DataSource = dataTable; 
            sqlConnection.Close();
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Execute CustomerListWithCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            customerDGV.DataSource = dataTable;
            sqlConnection.Close(); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert Into TblCustomer(CustomerName,CustomerSurname,CustomerCity,CustomerBalance,CustomerStatus) values (@customerName,@customerSurname,@customerCity,@customerBalance,@customerStatus)", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            command.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
            if(rdbActive.Checked)
            {
                command.Parameters.AddWithValue("customerStatus", true);
            }
            if(rdbPassive.Checked)
            {
                command.Parameters.AddWithValue("customerStatus", false);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Customer Added Successfully");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCustomer Where CustomerId=@customerId", sqlConnection); 
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text); 
            command.ExecuteNonQuery(); // Save Changes
            sqlConnection.Close();
            MessageBox.Show("Customer Deleted Successfully", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update TblCustomer Set CustomerName=@customerName,CustomerSurname=@customerSurname,CustomerCity=@customerCity,CustomerBalance=@customerBalance,CustomerStatus=@customerStatus where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            command.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            if (rdbActive.Checked)
            {
                command.Parameters.AddWithValue("customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                command.Parameters.AddWithValue("customerStatus", false);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Customer Updated Successfully");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select CustomerId, CustomerName, CustomerSurname, CustomerBalance,CustomerStatus,CityName From TblCustomer Inner Join TblCity On TblCity.CityId = TblCustomer.CustomerCity Where CustomerName=@customerName", sqlConnection); //customerName gore arariz
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            customerDGV.DataSource = dataTable;
            sqlConnection.Close();
        }
    }
}
