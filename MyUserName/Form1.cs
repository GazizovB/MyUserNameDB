using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUserName
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\бауыржан\мой документы\visual studio 2015\Projects\MyUserName\MyUserName\UserDB.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Users]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(sqlReader["Id"] + "\t " + Convert.ToString(sqlReader["Name"]) + "\t " + Convert.ToString(sqlReader["Login"]) + "\t " + Convert.ToString(sqlReader["Password"])+"\t "+ Convert.ToString(sqlReader["Address"])+"\t "+ sqlReader["MobilePhone"]+"\t "/*+ Convert.ToString(sqlReader["SignAdmin"])*/);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            //    sqlConnection.Close();
            Close();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Users] (Name, Login, Password, Address, MobilePhone, SignAdmin)VALUES(@Name, @Login, @Password, @Address, @MobilePhone, @SignAdmin)", sqlConnection);

            command.Parameters.AddWithValue("Name", textBox1.Text);
            command.Parameters.AddWithValue("Login", textBox2.Text);
            command.Parameters.AddWithValue("Password", textBox7.Text);
            command.Parameters.AddWithValue("Address", textBox8.Text);
            command.Parameters.AddWithValue("MobilePhone", textBox9.Text);
            command.Parameters.AddWithValue("SignAdmin", textBox10.Text);

            await command.ExecuteNonQueryAsync();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Users]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(sqlReader["Id"] + "\t " + Convert.ToString(sqlReader["Name"]) + "\t " + Convert.ToString(sqlReader["Login"]) + "\t " + Convert.ToString(sqlReader["Password"]) + "\t " + Convert.ToString(sqlReader["Address"]) + "\t " + sqlReader["MobilePhone"] + "\t " + Convert.ToString(sqlReader["SignAdmin"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Users] SET [Login]=@Login, [Password]=@Password WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox5.Text);
                command.Parameters.AddWithValue("Login", textBox4.Text);
                command.Parameters.AddWithValue("Password", textBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Users] WHERE [Id] = @Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox6.Text);

                await command.ExecuteNonQueryAsync();
            }
        }


    }
}
