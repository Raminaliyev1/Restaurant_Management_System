using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RM
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        private SqlConnection con = new SqlConnection(@"Data Source=HV01\SQLEXPRESS;Initial Catalog=restaurantDB;Integrated Security=True");

        private List<PaymentDTO> payments = new List<PaymentDTO>();
        private void Payment_Load(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from paymenttab", con);

                var datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    var payment = new PaymentDTO();

                    payment.Id = Convert.ToInt16(datareader["Id"].ToString());
                    payment.Name = datareader["Name"].ToString();
                    payment.Food = datareader["Food"].ToString();
                    payment.Food1 = datareader["Food1"].ToString();
                    payment.PaymentMethod = datareader["PaymentMethod"].ToString();
                    payment.Total = Convert.ToInt16(datareader["Total"].ToString());

                    payments.Add(payment);
                }

                dgv1.DataSource = payments;

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
                SqlCommand cmd = new SqlCommand("Insert into paymenttab values(@Id,'" + tbName.Text + "','" + tbFood.Text + "','" + tbFood1.Text + "','" + tbPaymentM.Text + "',@Total)", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));
                cmd.Parameters.AddWithValue("@Total", int.Parse(tbTotal.Text));


                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Added");

                con.Close();


                payments.Add(new PaymentDTO()
                {
                    Id = int.Parse(tbID.Text),
                    Name = tbName.Text,
                    Food = tbFood.Text,
                    Food1 = tbFood1.Text,
                    PaymentMethod = tbPaymentM.Text,
                    Total = Convert.ToInt16(tbTotal.Text)
                });

                tbID.Clear();
                tbName.Clear();
                tbFood.Clear();
                tbFood1.Clear();
                tbPaymentM.Clear();
                tbTotal.Clear();
                tbID.Focus();
                //dgv1.DataSource = payments;

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
                SqlCommand cmd = new SqlCommand("Update paymenttab Set Name = '" + tbName.Text + "', Food = '" + tbFood.Text + "', Food1 = '" + tbFood1.Text + "', PaymentMethod = '" + tbPaymentM.Text + "', Total = @Total where Id = @Id", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));
                cmd.Parameters.AddWithValue("@Total", int.Parse(tbTotal.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Updated");

                con.Close();

                foreach (var payment in payments)
                {
                    if (payment.Id == int.Parse(tbID.Text))
                    {   
                        payment.Name = tbName.Text;
                        payment.Food = tbFood.Text;
                        payment.Food1 = tbFood1.Text;
                        payment.PaymentMethod = tbPaymentM.Text;
                        payment.Total = Convert.ToInt16(tbTotal.Text);
                    }
                }
                dgv1.DataSource = payments;
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
                SqlCommand cmd = new SqlCommand("Delete paymenttab where Id = @Id", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Deleted");

                con.Close();

                tbID.Clear();
                tbName.Clear();
                tbFood.Clear();
                tbFood1.Clear();
                tbPaymentM.Clear();
                tbTotal.Clear();
                tbID.Focus();
                //dgv1.DataSource = payments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
        }

    }
}
