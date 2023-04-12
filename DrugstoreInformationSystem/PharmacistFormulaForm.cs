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
    public partial class PharmacistFormulaForm : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EN0V4OG;Initial Catalog=LKS_DIS;Integrated Security=True");
        public PharmacistFormulaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        void GetTransactionList()
        {
            String query = "SELECT * FROM Tbl_Transaksi";
            SqlCommand command = new SqlCommand(query, connection);
            
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();

            dataAdapter.Fill(table);

            dataGridView1.DataSource = table;
        }

        private void PharmacistFormulaForm_Load(object sender, EventArgs e)
        {
            GetTransactionList();
        }
    }
}
