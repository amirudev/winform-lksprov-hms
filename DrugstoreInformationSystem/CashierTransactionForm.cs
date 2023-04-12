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
    public partial class CashierTransactionForm : Form
    {
        String name;
        int userId;
        long updatedTotal = 0;

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EN0V4OG;Initial Catalog=LKS_DIS;Integrated Security=True");

        public CashierTransactionForm(string name, int userId)
        {
            InitializeComponent();
            this.name = name;
            this.userId = userId;
        }

        void LoadFormulaList(String filterKeyword = "")
        {
            String query = "SELECT * FROM Tbl_Resep";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();

            dataAdapter.Fill(table);

            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = table;

            //dataGridView1.DataSource = bindingSource;
            dataGridView1.DataSource = table;

            //if (filterKeyword != null)
            //{
            //    bindingSource.Filter = "No_Resep LIKE '%" + filterKeyword + "%'";
            //}
        }

        void UpdateFormulaTotal()
        { 
            String queryGetTempSubtotal = "SELECT SUM(Jumlah_ObatDibeli) as Jumlah FROM Tbl_Resep";

            SqlCommand commandGetTempSubtotal = new SqlCommand(queryGetTempSubtotal, connection);

            SqlDataReader dataReader = commandGetTempSubtotal.ExecuteReader();
            if (dataReader.Read())
            {
                updatedTotal = dataReader.GetInt64(0);
                label12.Text = this.updatedTotal.ToString();
            }

            dataReader.Close();
        }

        private void CashierTransactionForm_Load(object sender, EventArgs e)
        {
            connection.Open();

            UpdateFormulaTotal();
            LoadFormulaList();

            label10.Text = this.name;
            label12.Text = updatedTotal.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String queryPostTransaction = "INSERT INTO Tbl_Resep (No_Resep, Tgl_Resep, Nama_Dokter, Nama_Pasien, Nama_ObatDibeli, Jumlah_ObatDibeli) " +
                "VALUES (@FormulaNumber, @FormulaDate, @DoctorName, @PatientName, @PurchasedDrugName, @PurchaseDrugAmount)";
            SqlCommand commandPostTransaction = new SqlCommand(queryPostTransaction, connection);

            int thisDrugTotal = int.Parse(textBox8.Text) * int.Parse(textBox5.Text);
            // Post data to table
            commandPostTransaction.Parameters.AddWithValue("@FormulaNumber", textBox1.Text);
            commandPostTransaction.Parameters.AddWithValue("@FormulaDate", dateTimePicker1.Text);
            commandPostTransaction.Parameters.AddWithValue("@DoctorName", textBox2.Text);
            commandPostTransaction.Parameters.AddWithValue("@PatientName", textBox3.Text);
            commandPostTransaction.Parameters.AddWithValue("@PurchasedDrugName", textBox4.Text);
            commandPostTransaction.Parameters.AddWithValue("@PurchaseDrugAmount", int.Parse(textBox8.Text) * int.Parse(textBox5.Text));

            commandPostTransaction.ExecuteNonQuery();

            // Get Latest Subtotal 

            LoadFormulaList();
            UpdateFormulaTotal();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Enabled = false; textBox2.Enabled = false; textBox3.Enabled = false;
            } else
            {
                textBox1.Enabled = true; textBox2.Enabled = true; textBox3.Enabled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int change = int.Parse(textBox6.Text) - int.Parse(label12.Text);

                if (change > 0)
                {
                    label15.BackColor = Color.DarkGreen;
                    label15.Text = change.ToString();
                } else
                {
                    label15.BackColor = Color.Yellow;
                    label15.Text = "Uang masih kurang";
                }
            } catch {
                label15.BackColor = Color.Red;
                label15.Text = "Error: Mohon Masukkan Angka";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String queryPostTransaction = "INSERT INTO Tbl_Transaksi ( No_Transaksi, Tgl_Transaksi, Total_Bayar, Id_User, Id_Obat, Id_Resep) " +
                "VALUES (@No_Transaksi, @Tgl_Transaksi, @Total_Bayar, @Id_User, @Id_Obat, @Id_Resep)";
            SqlCommand commandPostTransaction = new SqlCommand(queryPostTransaction, connection);

            if (textBox6.Text != "")
            {
                long thisDrugTotal = int.Parse(textBox6.Text) - int.Parse(label12.Text);
                // Post data to table
                commandPostTransaction.Parameters.AddWithValue("@No_Transaksi", DateTime.Now.Ticks.ToString());
                commandPostTransaction.Parameters.AddWithValue("@Tgl_Transaksi", DateTime.Now.Date);
                commandPostTransaction.Parameters.AddWithValue("@Total_Bayar", this.updatedTotal);
                commandPostTransaction.Parameters.AddWithValue("@Id_User", this.userId);
                commandPostTransaction.Parameters.AddWithValue("@Id_Obat", this.userId);
                commandPostTransaction.Parameters.AddWithValue("@Id_Res `q`                                                                                                             ep", textBox1.Text);

                DialogResult result = MessageBox.Show("Pembayaran Rp" + updatedTotal + " dilakukan dengan pembayaran Rp" + textBox6.Text + ", Kembalian Rp" + thisDrugTotal.ToString(), "Payment Confirmation", MessageBoxButtons.YesNo);
                commandPostTransaction.ExecuteNonQuery();
            } else
            {
                MessageBox.Show("Masukkan angka pada form bayar");
            }
        }
    }
}
