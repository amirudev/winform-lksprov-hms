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
    public partial class UserManagement : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EN0V4OG;Initial Catalog=LKS_DIS;Integrated Security=True");
        public UserManagement()
        {
            InitializeComponent();
        }

        void GetUserList(String filterKeyword = "")
        {
            String query = "SELECT * FROM Tbl_User";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable table = new DataTable();
            dataAdapter.Fill(table);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = table;

            dataGridView1.DataSource= bindingSource;

            if (filterKeyword != "") {
                bindingSource.Filter = "Nama_User LIKE '%" + filterKeyword + "%' OR Username LIKE '" + filterKeyword + "'";
            }
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            GetUserList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserManagement userManagement = new UserManagement();
            this.Hide();
            userManagement.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String userType, name, address, phone, username, password;

            userType = comboBox1.Text;
            name = textBox2.Text; phone = textBox3.Text; address = textBox6.Text;
            username = textBox5.Text; password = textBox4.Text;

            String query = "INSERT INTO Tbl_User (Tipe_User, Nama_User, Alamat, Telpon, Username, Password) VALUES (@UserType, @Name, @Address, @Phone, @Username, @Password)";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@UserType", userType);
            sqlCommand.Parameters.AddWithValue("@Name", name);
            sqlCommand.Parameters.AddWithValue("@Address", address);
            sqlCommand.Parameters.AddWithValue("@Phone", phone);
            sqlCommand.Parameters.AddWithValue("@Username", username);
            sqlCommand.Parameters.AddWithValue("@Password", password);

            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            GetUserList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrugManagement drugManagement = new DrugManagement();
            this.Hide();
            drugManagement.ShowDialog();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            GetUserList(textBox7.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                String name = row.Cells["Nama_User"].Value.ToString() ?? "";
                String username = row.Cells["Username"].Value.ToString() ?? "";

                textBox2.Text = name;
                textBox5.Text = username;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
