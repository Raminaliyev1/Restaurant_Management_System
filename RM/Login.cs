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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=HV01\SQLEXPRESS;Initial Catalog=LocalDB;Integrated Security=True");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            con.Open();
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            try
            {
                SqlCommand cmd = new SqlCommand("select Username,Password from usertab where Username='" + txtUsername.Text + "' and Password='" + txtPassword.Text + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    username = txtUsername.Text;
                    password = txtPassword.Text;

                    Main basliq = new Main();
                    basliq.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login Details","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtPassword.Clear();

                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }
            finally 
            { 
                con.Close();
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Dou you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();

            txtPassword.Focus();
        }
    }
}
