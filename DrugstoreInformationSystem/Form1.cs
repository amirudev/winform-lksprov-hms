using System.Data.SqlClient;

namespace DrugstoreInformationSystem
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-EN0V4OG;Initial Catalog=LKS_DIS;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userId = -1;
            String name = "";
            String userType = "";
            bool isLoginSuccess = false;

            String username = textBox1.Text;
            String password = textBox2.Text;

            String queryUserInfo = "SELECT Id_User, Tipe_User, Nama_User FROM Tbl_User WHERE Username = @Username AND Password = @Password";
            SqlCommand command = new SqlCommand(queryUserInfo, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                userId = reader.GetInt32(0);
                userType = reader.GetString(1);
                name = reader.GetString(2);
                isLoginSuccess = true;
            } else
            {
                connection.Close();
                MessageBox.Show("Username / Password Salah");
            }

            reader.Close();

            if (isLoginSuccess)
            {
                DateTime currentTime = DateTime.Now;

                String query = "INSERT INTO Tbl_Log (Aktifitas, Id_User, waktu) VALUES (@Activity, @IdUser, @Timestamp)";

                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@Activity", "Log In");
                sqlCommand.Parameters.AddWithValue("@IdUser", userId);
                sqlCommand.Parameters.AddWithValue("@Timestamp", currentTime);

                sqlCommand.ExecuteNonQuery();
                connection.Close();

                this.Hide();

                if (userType == "Admin")
                {
                    AdminNavigationForm adminNavigationForm = new AdminNavigationForm();
                    adminNavigationForm.ShowDialog();
                } else if (userType == "Apoteker") {
                    PharmacistFormulaForm pharmacistFormulaForm = new PharmacistFormulaForm();
                    pharmacistFormulaForm.ShowDialog();
                } else if (userType == "Kasir")
                {
                    CashierTransactionForm cashierTransactionForm = new CashierTransactionForm(name: name, userId: userId);
                    cashierTransactionForm.ShowDialog();
                }
            }
        }
    }
}