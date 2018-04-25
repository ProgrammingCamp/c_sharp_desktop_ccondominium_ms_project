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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            tblUser user = new Condominium_Management_System.tblUser();
            user.Fullname = txtFullname.Text;
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Text;

            entity.tblUsers.Add(user);
            entity.SaveChanges();

            MessageBox.Show("User Saved");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int userID = int.Parse(listViewUser.Items[listViewUser.SelectedIndices[0]].SubItems[0].Text);

            tblUser oldUser = entity.tblUsers.Where(x => x.ID == userID).FirstOrDefault();

            tblUser newUser = new Condominium_Management_System.tblUser();
            newUser.ID = userID;
            newUser.Fullname = txtFullname.Text;
            newUser.Username = txtUsername.Text;
            newUser.Password = txtPassword.Text;

            entity.Entry(oldUser).CurrentValues.SetValues(newUser);
            entity.SaveChanges();

            MessageBox.Show("User Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int userID = int.Parse(listViewUser.Items[listViewUser.SelectedIndices[0]].SubItems[0].Text);

            tblUser oldUser = entity.tblUsers.Where(x => x.ID == userID).FirstOrDefault();
            entity.tblUsers.Remove(oldUser);
            entity.SaveChanges();

            MessageBox.Show("User Deleted");
        }

        private void listViewUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewUser.SelectedIndices.Count > 0)
            {
                txtFullname.Text = listViewUser.Items[listViewUser.SelectedIndices[0]].SubItems[1].Text;
                txtUsername.Text = listViewUser.Items[listViewUser.SelectedIndices[0]].SubItems[2].Text;
                txtPassword.Text = listViewUser.Items[listViewUser.SelectedIndices[0]].SubItems[3].Text;
            }
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblUser> users = entity.tblUsers.ToList();

            foreach (tblUser user in users)
            {
                ListViewItem item = new ListViewItem(user.ID.ToString());
                item.SubItems.Add(user.Fullname);
                item.SubItems.Add(user.Username);
                item.SubItems.Add(user.Password);

                listViewUser.Items.Add(item);
            }
        }
    }
}
