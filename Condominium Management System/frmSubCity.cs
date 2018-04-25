using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Condominium_Management_System
{
    public partial class frmSubCity : Form
    {
        public frmSubCity()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            string query = "INSERT INTO tblSubCity(RegionID, Title) VALUES(" + cboRegion.SelectedValue + ",'" + txtTitle.Text + "')";

            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            MessageBox.Show("Saved");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            string query = "UPDATE tblSubCity SET RegionID = " + cboRegion.SelectedValue + ", Title='" + txtTitle.Text + "' WHERE ID = " + txtID.Text;

            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            MessageBox.Show("Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            string query = "DELETE tblSubCity WHERE ID = " + txtID.Text;

            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            MessageBox.Show("Deleted");
        }

        private void frmSubCity_Load(object sender, EventArgs e)
        {
            LoadRegionCombo();
            LoadSubCityListView();
        }

        private void LoadRegionCombo()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            string query = "SELECT ID, Title FROM tblRegion";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Title");

            while (reader.Read())
            {
                table.Rows.Add(reader.GetInt32(0), reader.GetString(1));
            }

            cboRegion.DataSource = table;
            cboRegion.ValueMember = "ID";
            cboRegion.DisplayMember = "Title";
        }

        private void LoadSubCityListView()
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            connection.Open();

            string query = "SELECT ID, RegionID, Title FROM tblSubCity";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader.GetInt32(0).ToString());
                item.SubItems.Add(reader.GetSqlInt32(1).ToString());
                item.SubItems.Add(reader.GetString(2));

                listViewSubCity.Items.Add(item);
            }
        }

        private void listViewSubCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewSubCity.SelectedIndices.Count > 0)
            {
                txtID.Text = listViewSubCity.Items[listViewSubCity.SelectedIndices[0]].SubItems[0].Text;
            }
        }
    }
}
