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
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnOpenRegion_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOpenSubCity_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOpenWoreda_Click(object sender, EventArgs e)
        {
            
        }

        private void regionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegion frm = new frmRegion();
            frm.ShowDialog();
        }

        private void subCityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubCity frm = new frmSubCity();
            frm.ShowDialog();
        }

        private void woredaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWoreda frm = new frmWoreda();
            frm.ShowDialog();
        }

        private void manageHouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHouse frm = new Condominium_Management_System.frmHouse();
            frm.Tag = this.Tag;
            frm.ShowDialog();
        }

        private void manageUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUser frm = new Condominium_Management_System.frmUser();
            frm.ShowDialog();
        }

        private void manageRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRole frm = new Condominium_Management_System.frmRole();
            frm.ShowDialog();
        }

        private void managerUserRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserRole frm = new Condominium_Management_System.frmUserRole();
            frm.ShowDialog();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            SecurityPolicy policy = new Condominium_Management_System.SecurityPolicy();
            bool IsAllowed = policy.IsInRole(this.Tag.ToString(), "Administrator");

            if(IsAllowed == true)
            {
                securityToolStripMenuItem.Enabled = true;
            }
            else
            {
                securityToolStripMenuItem.Enabled = false;
            }
        }

        private void homeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportViewer frm = new Condominium_Management_System.frmReportViewer();
            frm.Tag = "HouseReport";
            frm.ShowDialog();
        }

        private void ownerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportViewer frm = new Condominium_Management_System.frmReportViewer();
            frm.Tag = "OwnerReport";
            frm.ShowDialog();
        }
    }
}
