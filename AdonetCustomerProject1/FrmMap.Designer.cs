namespace AdonetCustomerProject1
{
    partial class FrmMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCityForm = new System.Windows.Forms.Button();
            this.btnCustomerForm = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCityForm
            // 
            this.btnCityForm.Location = new System.Drawing.Point(37, 63);
            this.btnCityForm.Name = "btnCityForm";
            this.btnCityForm.Size = new System.Drawing.Size(202, 67);
            this.btnCityForm.TabIndex = 0;
            this.btnCityForm.Text = "City";
            this.btnCityForm.UseVisualStyleBackColor = true;
            this.btnCityForm.Click += new System.EventHandler(this.btnCityForm_Click);
            // 
            // btnCustomerForm
            // 
            this.btnCustomerForm.Location = new System.Drawing.Point(37, 189);
            this.btnCustomerForm.Name = "btnCustomerForm";
            this.btnCustomerForm.Size = new System.Drawing.Size(202, 67);
            this.btnCustomerForm.TabIndex = 1;
            this.btnCustomerForm.Text = "Customer";
            this.btnCustomerForm.UseVisualStyleBackColor = true;
            this.btnCustomerForm.Click += new System.EventHandler(this.btnCustomerForm_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(37, 311);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(202, 67);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // FrmMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCustomerForm);
            this.Controls.Add(this.btnCityForm);
            this.Name = "FrmMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forms";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCityForm;
        private System.Windows.Forms.Button btnCustomerForm;
        private System.Windows.Forms.Button btnExit;
    }
}