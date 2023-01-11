using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nilwala_Training_center.Report_Form
{
    public partial class Cusomer_Credit_Details : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string ReSelectQ = "";

        public Cusomer_Credit_Details()
        {
            InitializeComponent();
            LoarAgancy();
            LoadBatch();
        }



        public void LoarAgancy()
        {
             #region Load Agancy..........................................

            SqlConnection con = new SqlConnection(IMS);
            con.Open();

            string LoadAgency = @"select Ajecy_ID,Agency_Name from Agency_Details";
            SqlCommand cmdx = new SqlCommand(LoadAgency, con);
            SqlDataReader dr = cmdx.ExecuteReader();
                while(dr.Read())
                {
                    cmbAgancy.Items.Add(dr[1].ToString());
                }
            #endregion
        }

        public void LoadBatch()
        {
            #region Load Batch............................................

            SqlConnection cnn2 = new SqlConnection(IMS);
            cnn2.Open();
            String LoBatch = "SELECT     Batch_ID, Course_Name, Course_ID FROM         Batch_Details";
            SqlCommand cmm2 = new SqlCommand(LoBatch, cnn2);
            SqlDataReader dr2 = cmm2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox1.Items.Add(dr2[0].ToString());
            }
            #endregion
        }


        public void SqlConcat()
        {
            #region Sql Concat......................................................

            ReSelectQ = @"SELECT DISTINCT DOC_ID, Credit_Amount, Remain_amount, Total_Fee, Agency_ID, Agency_Name
                        FROM            Trainee_Registration_DOC_Details WHERE 1=1";

            if (rbtAgancyby.Checked == true)
            {
                ReSelectQ += " and Trainee_Registration_DOC_Details.Agency_ID='" + lblBankID.Text + "'";
            }
            if (rbtByBatch.Checked == true)
            {
                ReSelectQ += " and Trainee_Registration.Batch_ID='" + comboBox1.Text + "'";
            }

            ReSelectQ.Replace("1=1 AND ", "");
            ReSelectQ.Replace(" WHERE 1=1 ", "");


            #endregion
        }


        private void Cusomer_Credit_Details_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            rbtAllAgancy.Checked = true;
            rbtAllBatch.Checked = true;

        }

        private void cmbAgancy_SelectedIndexChanged(object sender, EventArgs e)
        {
             #region select ID Agancy..........................................

            SqlConnection con = new SqlConnection(IMS);
            con.Open();
            string LoadAgency = @"select Ajecy_ID,Agency_Name from Agency_Details where Agency_Name='" + cmbAgancy.Text + "'";
            SqlCommand cmdx = new SqlCommand(LoadAgency, con);
            SqlDataReader dr1 = cmdx.ExecuteReader(CommandBehavior.CloseConnection);
                if(dr1.Read())
                {
                    lblBankID.Text=dr1[0].ToString();
                }
            #endregion
        }

        private void cmbAgancy_TextChanged(object sender, EventArgs e)
        {
            #region select ID Agancy..........................................

            SqlConnection con = new SqlConnection(IMS);
            con.Open();
            string LoadAgency = @"select Ajecy_ID,Agency_Name from Agency_Details where Agency_Name='" + cmbAgancy.Text + "'";
            SqlCommand cmdx = new SqlCommand(LoadAgency, con);
            SqlDataReader dr1 = cmdx.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr1.Read())
            {
                lblBankID.Text = dr1[0].ToString();
            }
            #endregion
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(rbtByBatch.Checked==true)
            {
                if(comboBox1.Text == "")
                {
                    MessageBox.Show("Please Select Details...!", "Message");
                    return;

                }
            }
            if (rbtAgancyby.Checked == true)
            {
                if (cmbAgancy.Text == "")
                {
                    MessageBox.Show("Please Select Details...!", "Message");
                    return;

                }
            }

            string totalTables = "";


            //select the print datatables number----------------
            SqlConnection conx = new SqlConnection(IMS);
            conx.Open();

            string ReSelecttableNumbers = @"SELECT RptNumbers FROM RptNumbers_new";
            SqlCommand cmdx = new SqlCommand(ReSelecttableNumbers, conx);
            SqlDataReader drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection);

            if (drx.Read() == true)
            {
                totalTables = drx[0].ToString();
            }

            if (conx.State == ConnectionState.Open)
            {
                conx.Close();
                drx.Close();
            }
            //....................................................................................................

            Report.RPTCredit_Details rpt = new Report.RPTCredit_Details();

            TextObject agencyName, BatchID;

            #region if Agency By Checked...........................................................

            if (rbtAgancyby.Checked==true)
            {
                if (rpt.ReportDefinition.ReportObjects["Text24"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text24"];
                    agencyName.Text = cmbAgancy.Text;

                }
                if (rpt.ReportDefinition.ReportObjects["Text2"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text2"];
                    agencyName.Text = "(Agency Wise)";

                }

            }
            #endregion

            #region if All Agency  Checked...........................................................
            if (rbtAllAgancy.Checked == true)
            {
                if (rpt.ReportDefinition.ReportObjects["Text24"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text24"];
                    agencyName.Text = "";

                }
                if (rpt.ReportDefinition.ReportObjects["Text23"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text23"];
                    agencyName.Text = "";

                }
                //if (rpt.ReportDefinition.ReportObjects["Text2"] != null)
                //{
                //    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text2"];
                //    agencyName.Text = "";

                //}
            }
            #endregion

            #region if All Batch  Checked...........................................................

            if (rbtAllBatch.Checked == true)
            {
                if (rpt.ReportDefinition.ReportObjects["Text25"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text25"];
                    agencyName.Text = "";

                }
                if (rpt.ReportDefinition.ReportObjects["Text26"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text26"];
                    agencyName.Text = "";

                }
                //if (rpt.ReportDefinition.ReportObjects["Text2"] != null)
                //{
                //    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text2"];
                //    agencyName.Text = "";

                //}

            }
            #endregion

            #region if Batch By Checked...........................................................

            if (rbtByBatch.Checked == true)
            {
                if (rpt.ReportDefinition.ReportObjects["Text26"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text26"];
                    agencyName.Text = comboBox1.Text;

                }

                if (rpt.ReportDefinition.ReportObjects["Text2"] != null)
                {
                    agencyName = (TextObject)rpt.ReportDefinition.ReportObjects["Text2"];
                    agencyName.Text = "(Batch Wise)";

                }

            }
            #endregion


            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();
            SqlConcat();
            SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
            DataSet ds = new My_Data_SET_new();
            dscmd.Fill(ds);


            
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            viwerCreit_Details.ReportSource = rpt;
            viwerCreit_Details.Refresh();
            con1.Close();
        }

        private void rbtAllAgancy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAllAgancy.Checked == true)
            {
                lblBankID.Text = "..";
                cmbAgancy.SelectedIndex = -1;
                cmbAgancy.Enabled = false;
            }
            if (rbtAllAgancy.Checked == false)
            {
                
                
                cmbAgancy.Enabled = true;
            }
        }

        private void rbtAgancyby_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtAgancyby.Checked == true)
            {

                cmbAgancy.Enabled = true;
                rbtAllAgancy.Checked = false;
            }
            if (rbtAgancyby.Checked == false)
            {
                lblBankID.Text = "..";
                cmbAgancy.SelectedIndex = -1;
                cmbAgancy.Enabled = false;
                
            }
        }

        private void rbtAllBatch_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtAllBatch.Checked==true)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = -1;
                rbtByBatch.Checked = false;
            }
        }

        private void rbtByBatch_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtByBatch.Checked == true)
            {
                comboBox1.Enabled = true;
                
                rbtAllBatch.Checked = false;
            }
        }
    }
}
