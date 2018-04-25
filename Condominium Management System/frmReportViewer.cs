using Microsoft.Reporting.WinForms;
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
    public partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            if (this.Tag.ToString() == "HouseReport")
            {
                //show house list report
                
            }
            else if (this.Tag.ToString() == "OwnerReport")
            {
                //show owner list report
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT dbo.tblOwner.ID, dbo.tblOwner.Fullname, dbo.tblGender.TItle AS Gender, dbo.tblOwner.CompleteAddress, dbo.tblOwner.PhoneNumber, dbo.tblOwner.MobileNumber, dbo.tblMaritalStatus.TItle AS MaritalStatus, dbo.tblOwner.BirthDate FROM   dbo.tblOwner INNER JOIN dbo.tblGender ON dbo.tblOwner.GenderID = dbo.tblGender.ID INNER JOIN dbo.tblMaritalStatus ON dbo.tblOwner.MaritalStatusID = dbo.tblMaritalStatus.ID", connection);

                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                reportViewer1.Reset();

                this.reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Value = ds.Tables[0];
                reportDataSource.Name = "DataSet1";

                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                this.reportViewer1.LocalReport.ReportPath = "E:\\Programming Camp\\Desktop Application Development Training\\Condominium Management System\\Condominium Management System\\rptOwnerList.rdlc";
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "rptOwnerList.rdlc";

                this.reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
