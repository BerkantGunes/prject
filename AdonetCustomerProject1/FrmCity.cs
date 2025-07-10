using System;
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
    public partial class FrmCity: Form
    {
        public FrmCity()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection("Server=GUNESBERKANT\\SQLEXPRESS;initial catalog=DbCustomer;integrated security=true");

        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCity", sqlConnection); /* Sorgu satiri */
            SqlDataAdapter adapter = new SqlDataAdapter(command); /* hafizaya alip DGV'ye gondermek icin bir koprudur */
            DataTable dataTable = new DataTable(); /* Hafizaya almak icin DataTable adında sanal bir Tablo olusturduk */
            adapter.Fill(dataTable); /* Bu tablonun icine adapter'den gelen veriyi doldur. adapter'da sorgudan gelen deger var. */
            cityDGV.DataSource = dataTable; /* DataTable'dan gelen veriyi DGV'de gosterme komutu */ 
            sqlConnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("insert into TblCity (CityName,CityCountry) values (@cityName, @cityCountry)", sqlConnection); // Ekleme islemi yapacagimiz degerleri verdik. @parameter
            command.Parameters.AddWithValue("@cityName", txtCityName.Text); // parametreyi degeriyle birlikte ekledik. cityName olarak txtCityName'in textinden gelen degeri ekledik.
            command.Parameters.AddWithValue("@cityCountry", txtCityCountry.Text);
            command.ExecuteNonQuery(); // T-SQL uzerinde etkilenen komut sayilarini döndürür. Bu degisiklikleri VeriTabanına kaydeder.
            sqlConnection.Close();
            MessageBox.Show("City Added Successfully!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCity Where CityId=@cityId", sqlConnection); //@cityId parametresinden gelen degeri Sil.
            command.Parameters.AddWithValue("@cityId", txtCityId.Text); //txtCityId'den gelen degeri @cityId parametresine atadık.
            command.ExecuteNonQuery(); // Save Changes
            sqlConnection.Close();
            MessageBox.Show("City Deleted Successfully", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update TblCity Set CityName=@cityName, CityCountry=@cityCountry where CityId=@cityId", sqlConnection); // cityId'den gelen degere gore cityName ve cityCountry guncelledik.
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCountry", txtCityCountry.Text);
            command.Parameters.AddWithValue("@cityId", txtCityId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("City Updated Successfully", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCity Where CityName=@cityName", sqlConnection); //cityName göre sorgu olusturduk.
            command.Parameters.AddWithValue("@cityName", txtCityName.Text); // cityname textine girilen degere gore parametre verir.
            SqlDataAdapter adapter = new SqlDataAdapter(command); // Burdan asagisi List butonuyla aynı
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cityDGV.DataSource = dataTable;
            sqlConnection.Close();
        }
    }
}
