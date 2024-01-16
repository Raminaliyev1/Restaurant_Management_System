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
    public partial class Waiter : Form
    {
        public Waiter()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=HV01\SQLEXPRESS;Initial Catalog=restaurantDB;Integrated Security=True");

        private List<StaffsDTO> staffs = new List<StaffsDTO>();

        private void Waiter_Load(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from stafftab", con);

                var datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    var staff = new StaffsDTO();

                    staff.Id = Convert.ToInt16(datareader["Id"].ToString());
                    staff.Name = datareader["Name"].ToString();
                    staff.Age = Convert.ToInt16(datareader["Age"].ToString());

                    staffs.Add(staff);
                }

                dgv1.DataSource = staffs;

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
                SqlCommand cmd = new SqlCommand("Insert into stafftab values(@Id,'" + tbName.Text + "',@Age)", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));
                cmd.Parameters.AddWithValue("@Age", int.Parse(tbAge.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Added");

                con.Close();


                staffs.Add(new StaffsDTO()
                {
                    Id = int.Parse(tbID.Text),
                    Name = tbName.Text,
                    Age = Convert.ToInt16(tbAge.Text)
                });

                tbID.Clear();
                tbAge.Clear();
                tbName.Clear();
                tbID.Focus();

                //dgv1.DataSource = staffs;

                
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update stafftab Set Name = '" + tbName.Text + "', Age = @Age where Id = @Id", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));
                cmd.Parameters.AddWithValue("@Age", int.Parse(tbAge.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Updated");

                con.Close();

                foreach (var staff in staffs)
                {
                    if (staff.Id == int.Parse(tbID.Text))
                    {
                        staff.Name = tbName.Text;
                        staff.Age = Convert.ToInt16(tbAge.Text);
                    }
                }
                dgv1.DataSource = staffs;
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
                SqlCommand cmd = new SqlCommand("Delete stafftab where Id = @Id", con);

                cmd.Parameters.AddWithValue("@Id", int.Parse(tbID.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Deleted");

                con.Close();

                tbID.Clear();
                tbAge.Clear();
                tbName.Clear();
                tbID.Focus();

                //dgv1.DataSource = staffs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Close(); }
        }
    }
}
