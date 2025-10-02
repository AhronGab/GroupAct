using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void enterbttn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter the username.");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter the password.");
                return;
            }

            // Corrected connection string with spaces after semicolons
            string connectionString = "Data Source=DESKTOP-OCND0IM\\SQLEXPRESS; Initial Catalog=asplogin; Integrated Security=True; TrustServerCertificate=True";

            string sqlQuery = "SELECT COUNT(*) FROM login WHERE username = @username AND password = @password";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", textBox2.Text.Trim());

                        await con.OpenAsync();

                        int userCount = (int)await cmd.ExecuteScalarAsync();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Login Successful");

                            // Create an instance of the new form (e.g., MainForm)
                            Form2 mainForm = new Form2();

                            // Show the new form
                            mainForm.Show();

                            // Hide the current login form
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DEBUG - Error: " + ex.Message);
            }
        }

        private void resetbttn_Click(object sender, EventArgs e)
        {
            // Clear the username textbox
            textBox1.Text = "";

            // Clear the password textbox
            textBox2.Text = "";
        }
    }
}