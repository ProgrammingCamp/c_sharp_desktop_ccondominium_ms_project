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
    public partial class frmHouse : Form
    {
        public frmHouse()
        {
            InitializeComponent();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();

                tblHouse house = new tblHouse();
                house.RegionID = int.Parse(cboRegion.SelectedValue.ToString());
                house.SubCityID = int.Parse(cboSubCity.SelectedValue.ToString());
                house.WoredaID = int.Parse(cboWoreda.SelectedValue.ToString());

                house.BlockNumber = txtBlockNumber.Text;
                house.FloorNumber = int.Parse(txtFloorNumber.Text);
                house.HouseNumber = txtHouseNumber.Text;

                house.GovernmentTransferedDate = dtpGovernmentTrasferedDate.Value;
                house.SiteName = txtSiteName.Text;

                entity.tblHouses.Add(house);
                entity.SaveChanges();

                MessageBox.Show("Saved");
            }
            catch (Exception)
            {
                MessageBox.Show("Save Failed");
            }
            
        }

        private void frmHouse_Load(object sender, EventArgs e)
        {
            LoadRegionCombo();
            LoadSubCityCombo();
            LoadWoredaCombo();

            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblHouse> houses = entity.tblHouses.ToList();

            foreach (tblHouse house in houses)
            {
                ListViewItem item = new ListViewItem(house.ID.ToString());
                item.SubItems.Add(house.tblWoreda.TItle);
                item.SubItems.Add(house.BlockNumber);
                item.SubItems.Add(house.FloorNumber.ToString());
                item.SubItems.Add(house.HouseNumber);
                item.SubItems.Add(house.GovernmentTransferedDate.ToString());
                item.SubItems.Add(house.SiteName);

                listViewHouse.Items.Add(item);
            }

            SecurityPolicy policy = new Condominium_Management_System.SecurityPolicy();
            bool IsAllowed = policy.IsInRole(this.Tag.ToString(), "Office Clerk");
            if(IsAllowed == true)
            {
                toolStripButtonNew.Enabled = true;
                toolStripButtonSave.Enabled = true;
                toolStripButtonUpdate.Enabled = true;
                toolStripButtonDelete.Enabled = true;
            }
            else
            {
                toolStripButtonNew.Enabled = false;
                toolStripButtonSave.Enabled = false;
                toolStripButtonUpdate.Enabled = false;
                toolStripButtonDelete.Enabled = false;
            }

        }

        private void LoadRegionCombo()
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblRegion> regions = entity.tblRegions.ToList();

            cboRegion.DataSource = regions;
            cboRegion.ValueMember = "ID";
            cboRegion.DisplayMember = "Title";
        }

        private void LoadSubCityCombo()
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblSubCity> subcities = entity.tblSubCities.ToList();

            cboSubCity.DataSource = subcities;
            cboSubCity.ValueMember = "ID";
            cboSubCity.DisplayMember = "Title";
        }

        private void LoadWoredaCombo()
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            List<tblWoreda> woredas = entity.tblWoredas.ToList();

            cboWoreda.DataSource = woredas;
            cboWoreda.ValueMember = "ID";
            cboWoreda.DisplayMember = "Title";
        }

        private void listViewHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            if (listViewHouse.SelectedIndices.Count > 0)
            {
                int houseId = int.Parse(listViewHouse.Items[listViewHouse.SelectedIndices[0]].SubItems[0].Text);
                tblHouse house = entity.tblHouses.Where(x => x.ID == houseId).FirstOrDefault();

                cboRegion.Text = house.tblRegion.TItle;
                cboSubCity.Text = house.tblSubCity.TItle;
                cboWoreda.Text = house.tblWoreda.TItle;

                txtBlockNumber.Text = house.BlockNumber;
                txtFloorNumber.Text = house.FloorNumber.ToString();
                txtHouseNumber.Text = house.HouseNumber;

                dtpGovernmentTrasferedDate.Value = house.GovernmentTransferedDate;
                txtSiteName.Text = house.SiteName;
            }
        }

        private void toolStripButtonUpdate_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int houseId = int.Parse(listViewHouse.Items[listViewHouse.SelectedIndices[0]].SubItems[0].Text);
            tblHouse oldHouse = entity.tblHouses.Where(x => x.ID == houseId).FirstOrDefault();

            tblHouse newHouse = new tblHouse();
            newHouse.ID = houseId;
            newHouse.RegionID = int.Parse(cboRegion.SelectedValue.ToString());
            newHouse.SubCityID = int.Parse(cboSubCity.SelectedValue.ToString());
            newHouse.WoredaID = int.Parse(cboWoreda.SelectedValue.ToString());

            newHouse.BlockNumber = txtBlockNumber.Text;
            newHouse.FloorNumber = int.Parse(txtFloorNumber.Text);
            newHouse.HouseNumber = txtHouseNumber.Text;

            newHouse.GovernmentTransferedDate = dtpGovernmentTrasferedDate.Value;
            newHouse.SiteName = txtSiteName.Text;

            entity.Entry(oldHouse).CurrentValues.SetValues(newHouse);
            entity.SaveChanges();

            MessageBox.Show("Updated");
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int houseId = int.Parse(listViewHouse.Items[listViewHouse.SelectedIndices[0]].SubItems[0].Text);
            tblHouse oldHouse = entity.tblHouses.Where(x => x.ID == houseId).FirstOrDefault();

            entity.tblHouses.Remove(oldHouse);
            entity.SaveChanges();

            MessageBox.Show("Deleted");
        }
    }
}
