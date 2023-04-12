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
    public partial class ReportManagement : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-EN0V4OG;Initial Catalog=LKS_DIS;Integrated Security=True");
        public ReportManagement()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            String query = "SELECT Tgl_Transaksi, SUM(Total_Bayar) AS TotalProfit FROM Tbl_Transaksi WHERE Tgl_Transaksi BETWEEN @startDate AND @endDate GROUP BY Tgl_Transaksi";
            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue("@startDate", "");
            command.Parameters.AddWithValue("@endDate", "");

            SqlDataReader reader= command.ExecuteReader();
            while (reader.Read())
            {
                //MessageBox.Show(reader.GetDa);
            }

            sqlConnection.Open();
            command.BeginExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
