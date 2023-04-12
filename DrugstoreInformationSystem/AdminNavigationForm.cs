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
    public partial class AdminNavigationForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-EN0V4OG;Initial Catalog=LKS_DIS;Integrated Security=True");

        public AdminNavigationForm()
        {
            InitializeComponent();
        }

        void GetLogList()
        {
            // SqlCommand c = new SqlCommand("EXEC ListEmp_SP", conn);
            String query = "SELECT * FROM Tbl_Log";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            
            DataTable dt = new DataTable();
            sd.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void AdminNavigationForm_Load(object sender, EventArgs e)
        {
            GetLogList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserManagement userManagement = new UserManagement();
            this.Hide();
            userManagement.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrugManagement drugManagement = new DrugManagement();
            this.Hide();
            drugManagement.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportManagement reportManagement = new ReportManagement();
            this.Hide();
            reportManagement.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }
    }
}
