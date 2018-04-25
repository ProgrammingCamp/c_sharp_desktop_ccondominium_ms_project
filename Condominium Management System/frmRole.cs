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
    public partial class frmRole : Form
    {
        public frmRole()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            tblRole role = new Condominium_Management_System.tblRole();
            role.Title = txtTitle.Text;

            entity.tblRoles.Add(role);
            entity.SaveChanges();

            MessageBox.Show("Role Saved");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int roleID = int.Parse(listViewRole.Items[listViewRole.SelectedIndices[0]].SubItems[0].Text);

            tblRole oldRole = entity.tblRoles.Where(x => x.ID == roleID).FirstOrDefault();

            tblRole newRole = new Condominium_Management_System.tblRole();
            newRole.ID = roleID;
            newRole.Title = txtTitle.Text;

            entity.Entry(oldRole).CurrentValues.SetValues(newRole);
            entity.SaveChanges();

            MessageBox.Show("Role Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int roleID = int.Parse(listViewRole.Items[listViewRole.SelectedIndices[0]].SubItems[0].Text);

            tblRole oldRole = entity.tblRoles.Where(x => x.ID == roleID).FirstOrDefault();
            entity.tblRoles.Remove(oldRole);
            entity.SaveChanges();

            MessageBox.Show("Role Deleted");
        }

        private void frmRole_Load(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblRole> roles = entity.tblRoles.ToList();

            foreach (tblRole role in roles)
            {
                ListViewItem item = new ListViewItem(role.ID.ToString());
                item.SubItems.Add(role.Title);

                listViewRole.Items.Add(item);
            }
        }

        private void listViewRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRole.SelectedIndices.Count > 0)
            {
                txtTitle.Text = listViewRole.Items[listViewRole.SelectedIndices[0]].SubItems[1].Text;
            }
        }
    }
}
