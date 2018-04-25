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
    public partial class frmUserRole : Form
    {
        public frmUserRole()
        {
            InitializeComponent();
        }

        private void frmUserRole_Load(object sender, EventArgs e)
        {
            LoadUserCombo();
            LoadRoleCombo();

            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblUserRole> userRoles = entity.tblUserRoles.ToList();

            foreach (tblUserRole userRole in userRoles)
            {
                ListViewItem item = new ListViewItem(userRole.ID.ToString());
                item.SubItems.Add(userRole.tblUser.Fullname);
                item.SubItems.Add(userRole.tblRole.Title);

                listViewUserRole.Items.Add(item);
            }
        }

        private void LoadUserCombo()
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblUser> users = entity.tblUsers.ToList();

            cboUser.DataSource = users;
            cboUser.ValueMember = "ID";
            cboUser.DisplayMember = "Fullname";
        }

        private void LoadRoleCombo()
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblRole> roles = entity.tblRoles.ToList();

            cboRole.DataSource = roles;
            cboRole.ValueMember = "ID";
            cboRole.DisplayMember = "Title";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            tblUserRole userRole = new Condominium_Management_System.tblUserRole();
            userRole.UserID = int.Parse(cboUser.SelectedValue.ToString());
            userRole.RoleID = int.Parse(cboRole.SelectedValue.ToString());

            entity.tblUserRoles.Add(userRole);
            entity.SaveChanges();

            MessageBox.Show("User Role Saved");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int userRoleID = int.Parse(listViewUserRole.Items[listViewUserRole.SelectedIndices[0]].SubItems[0].Text);

            tblUserRole oldUserRole = entity.tblUserRoles.Where(x => x.ID == userRoleID).FirstOrDefault();

            tblUserRole newUserRole = new Condominium_Management_System.tblUserRole();
            newUserRole.ID = userRoleID;
            newUserRole.UserID = int.Parse(cboUser.SelectedValue.ToString());
            newUserRole.RoleID = int.Parse(cboRole.SelectedValue.ToString());

            entity.Entry(oldUserRole).CurrentValues.SetValues(newUserRole);
            entity.SaveChanges();

            MessageBox.Show("User Role Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int userRoleID = int.Parse(listViewUserRole.Items[listViewUserRole.SelectedIndices[0]].SubItems[0].Text);

            tblUserRole oldUserRole = entity.tblUserRoles.Where(x => x.ID == userRoleID).FirstOrDefault();
            entity.tblUserRoles.Remove(oldUserRole);
            entity.SaveChanges();

            MessageBox.Show("User Role Deleted");
        }

        private void listViewUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewUserRole.SelectedIndices.Count > 0)
            {
                cboUser.Text = listViewUserRole.Items[listViewUserRole.SelectedIndices[0]].SubItems[1].Text;
                cboRole.Text = listViewUserRole.Items[listViewUserRole.SelectedIndices[0]].SubItems[2].Text;
            }
        }
    }
}
