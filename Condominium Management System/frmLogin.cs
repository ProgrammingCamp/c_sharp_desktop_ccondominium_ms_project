using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Condominium_Management_System
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            tblUser user = entity.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text).FirstOrDefault();

            if(user != null)
            {
                frmHome frm = new Condominium_Management_System.frmHome();
                frm.Tag = txtUsername.Text;
                frm.ShowDialog();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
