using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    public partial class Main : Form
    {
        /*[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nHeightEllipse

            );*/
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //btnOrder.Region = Region.FromHrgn(CreateRoundRectRgn(50, 50, btnOrder.Width, btnOrder.Height, 100, 100));
            //btnBill.Region = Region.FromHrgn(CreateRoundRectRgn(50, 50, btnOrder.Width, btnOrder.Height, 100, 100));
            //btnStaff.Region = Region.FromHrgn(CreateRoundRectRgn(50, 50, btnOrder.Width, btnOrder.Height, 100, 100));

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Food food = new Food();
            food.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Waiter waiter = new Waiter();
            waiter.Show();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            Payment payment = new Payment();
            payment.Show();
        }
    }
}
