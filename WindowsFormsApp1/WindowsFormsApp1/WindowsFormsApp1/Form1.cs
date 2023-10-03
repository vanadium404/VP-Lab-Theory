using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int age = Convert.ToInt32(textBox2.Text);
            string id = textBox3.Text;
            int contact = Convert.ToInt32(textBox4.Text);
            string address = textBox5.Text;

            SqlConnection conn = new SqlConnection(@"Server=strength\sqlexpress01;Initial Catalog=StudentDB;Integrated Security=True;");

            conn.Open();
            string qry = "Insert Into Student values('" + name + "','" + age + "','" + id + "','" + contact + "','" + address + "')";
            SqlCommand cmd = new SqlCommand(qry, conn);
            int res = cmd.ExecuteNonQuery();  // number of rows effected
            if (res == 1)
            {
                MessageBox.Show("Inserted");
            }
            else
            {
                MessageBox.Show("Not Inserted");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBox3.Text; // The ID of the record you want to delete
            string name = textBox1.Text;
            SqlConnection conn = new SqlConnection(@"Server=strength\sqlexpress01;Initial Catalog=StudentDB;Integrated Security=True;");

            try
            {
                conn.Open();

                // Use a parameterized query to delete a specific record by its ID
                string qry = "DELETE FROM Student WHERE ID = @ID AND Name=@Name";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@ID ", id);
                cmd.Parameters.AddWithValue("@Name ", name);


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record deleted successfully.");

                    // Optionally, clear the textboxes to indicate the record is deleted
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                else
                {
                    MessageBox.Show("No record found with the specified ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Close the connection when done
            }




        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = textBox3.Text;
            string name = textBox1.Text;
            SqlConnection conn = new SqlConnection(@"Server=strength\sqlexpress01;Initial Catalog=StudentDB;Integrated Security=True;");
            conn.Open();

            string qry = "Select * from Student where ID='" + id + "'AND Name ='"+ name +"'";
            SqlCommand cmd = new SqlCommand(qry, conn);

            // ConnectionLess Approach  offline Approach 
            /*  SqlDataAdapter adp = new SqlDataAdapter(cmd);
              DataTable dt = new DataTable();
              adp.Fill(dt);

              conn.Close(); // Close the connection after filling the DataTable.

              if (dt.Rows.Count > 0)
              {
                  // You can use the first row of the DataTable since you're only expecting one result.
                  textBox1.Text = dt.Rows[0]["Name"].ToString();
                  textBox2.Text = dt.Rows[0]["Age"].ToString();
                  textBox4.Text = dt.Rows[0]["Contact"].ToString();
                  textBox5.Text = dt.Rows[0]["Address"].ToString();
              }
              else
              {
                  // Handle the case where no matching record is found.
                  // You might want to clear the text boxes or show an error message.
              }
          }
            */

            // This is for Reader //

            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.HasRows) //information is avai or not
            {
                while (rd.Read())  // no count concept, coonection oreainted
                {
                    textBox1.Text = rd["Name"].ToString();
                    textBox2.Text = rd["Age"].ToString();
                    textBox3.Text = rd["id"].ToString();
                    textBox4.Text = rd["Contact"].ToString();
                    textBox5.Text = rd["Address"].ToString();


                }
            }
            conn.Close(); // this is for connectionless 

        }
    }
}
    

