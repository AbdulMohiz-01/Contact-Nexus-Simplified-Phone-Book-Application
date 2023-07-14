using Microsoft.Data.SqlClient;
using System.CodeDom.Compiler;
using System.Data;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        private bool isUpdateSelected = default;
        public Form1()
        {
            InitializeComponent();
        }


        private void newButton_Click(object sender, EventArgs e)
        {
            phTextBox.Text = "";
            nameTextBox.Text = "";
            emailTextBox.Text = "";
            addressTextBox.Text = "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (isUpdateSelected)
            {
                //generate update query
                ExecuteQuery("UPDATE SET name = '" + nameTextBox.Text + "', email = '" + emailTextBox.Text + "', address = '" + addressTextBox.Text + "', phoneNumber = '" + phTextBox.Text + "' WHERE phone = '" + phTextBox.Text + "'");
            }
            else
            {
                ExecuteQuery("INSERT INTO PhoneBook VALUES ('" + phTextBox.Text + "', '" + nameTextBox.Text + "', '" + emailTextBox.Text + "', '" + addressTextBox.Text + "')");

            }
            MessageBox.Show("Data Saved Successfully");
            isUpdateSelected = false;

        }
        private void ExecuteQuery(string query)
        {
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=phonebook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            RenderData();
        }
        private void RenderData()
        {
            string query = "Select phoneNumber,name,email,address FROM PhoneBook";
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=phonebook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data = new();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RenderData();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            phTextBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            nameTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            emailTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            addressTextBox.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            isUpdateSelected = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string number = (string)dataGridView1.CurrentRow.Cells[0].Value;
            ExecuteQuery("DELETE FROM PhoneBook WHERE phoneNumber = '" + number + "'");
        }
    }
}