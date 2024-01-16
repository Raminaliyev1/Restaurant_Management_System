using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RM
{
    public partial class Food : Form
    {
        public Food()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=HV01\SQLEXPRESS;Initial Catalog=restaurantDB;Integrated Security=True");

        private List<OrdersDTO> orders = new List<OrdersDTO>();

        private void Food_Load(object sender, EventArgs e)
        {
            try
            {
                
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from foodtab", con);

                var datareader = cmd.ExecuteReader();

                while(datareader.Read())
                {
                    var order = new OrdersDTO();

                    order.Id = Convert.ToInt16(datareader["Id"].ToString());
                    order.Name = datareader["Name"].ToString();
                    order.Food = datareader["Food"].ToString();
                    order.Food1 = datareader["Food1"].ToString();
                    order.Price = Convert.ToInt16(datareader["Price"].ToString());

                    orders.Add(order);
                }

                dgv1.DataSource = orders;

                datareader.Close();

                /*SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(dataSet, "foodtab");
                dgv1.DataSource = dataSet.Tables["foodtab"];
                con.Close();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
            


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into foodtab values(@Id,'" + tbName.Text + "','" + tbFood.Text + "','" + tbFood1.Text + "',@Price)", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));
                cmd.Parameters.AddWithValue("@Price", int.Parse(tbPrice.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Added");

                con.Close();


                orders.Add(new OrdersDTO()
                {
                    Id = int.Parse(tbID.Text),
                    Name = tbName.Text,
                    Food = tbFood.Text,
                    Food1 = tbFood1.Text,
                    Price = Convert.ToInt16(tbPrice.Text)
                });

                tbID.Clear();
                tbName.Clear();
                tbFood.Clear();
                tbFood1.Clear();
                tbPrice.Clear();
                tbID.Focus();
                //dgv1.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }



        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update foodtab Set name = '" + tbName.Text + "', food = '" + tbFood.Text + "', food1 = '" + tbFood1.Text + "', Price = @Price where Id = @Id", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));
                cmd.Parameters.AddWithValue("@Price", int.Parse(tbPrice.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Updated");

                con.Close();

                foreach (var order in orders)
                {
                    if (order.Id == int.Parse(tbID.Text))
                    {
                        order.Name = tbName.Text;
                        order.Food = tbFood.Text;
                        order.Food1 = tbFood1.Text;
                        order.Price = Convert.ToInt16(tbPrice.Text);
                    }
                }
                dgv1.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete foodtab where Id = @Id", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Deleted");

                con.Close();

                tbID.Clear();
                tbName.Clear();
                tbFood.Clear();
                tbFood1.Clear();
                tbPrice.Clear();
                tbID.Focus();

                //dgv1.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
        }
    }
}
