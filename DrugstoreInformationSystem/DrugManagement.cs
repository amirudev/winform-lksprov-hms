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

namespace DrugstoreInformationSystem
{
    public partial class DrugManagement : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EN0V4OG;Initial Catalog=LKS_DIS;Integrated Security=True");

        public DrugManagement()
        {
            InitializeComponent();
        }

        void GetDrugList()
        {
            String query = "SELECT * FROM Tbl_Obat";
            SqlCommand sc = new SqlCommand(query, connection);
            SqlDataAdapter da = new SqlDataAdapter(sc);

            DataTable dataTable = new DataTable();
            da.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void DrugManagement_Load(object sender, EventArgs e)
        {
            GetDrugList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String drugCodeField = textBox1.Text;
            String drugNameField = textBox2.Text;
            DateTime expiryDateField = dateTimePicker1.Value;
            String amountField = textBox4.Text;
            String priceField = textBox5.Text;

            String query = "INSERT INTO Tbl_Obat (Kode_Obat, Nama_Obat, Expired_Date, Jumlah, Harga) VALUES (@DrugCode, @DrugName, @ExpiryDate, @Amount, @Price)";
            SqlCommand sc = new SqlCommand(query, connection);

            sc.Parameters.AddWithValue("@DrugCode", drugCodeField);
            sc.Parameters.AddWithValue("@DrugName", drugNameField);
            sc.Parameters.AddWithValue("@ExpiryDate", expiryDateField);
            sc.Parameters.AddWithValue("@Amount", amountField);
            sc.Parameters.AddWithValue("@Price", priceField);

            connection.Open();
            sc.ExecuteNonQuery();
            connection.Close();

            GetDrugList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportManagement reportManagement = new ReportManagement();
            this.Hide();
            reportManagement.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
    }
}
