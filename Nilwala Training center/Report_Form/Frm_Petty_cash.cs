using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using System.Data.Sql;
using System.Configuration;
using System.Drawing.Printing;
using Nilwala_Training_center;
using Nilwala_Training_center.Report;

namespace Inventory_Control_System
{
    public partial class Frm_Petty_cash : Form
    {
        public Frm_Petty_cash()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string ReSelectQ = "";

        public void Select_Users()
        {
            try
            {

                #region Select_Users by petty............................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Pety_Reson = @"SELECT  UserCode, DisplayOn FROM  UserProfile";
                SqlCommand cmd1 = new SqlCommand(Pety_Reson, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr1.Read())
                {
                    Cmb_Payer.Items.Add(dr1[1].ToString());
                    Cmb_Reciever.Items.Add(dr1[1].ToString());
                }

                #endregion
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

       
        public void Load_Reason()
        {
            try
            {
                #region Petty cash reson............................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Pety_Reson = @"SELECT     PettyID, Reason, Active_Deactive FROM         Reason_for_pettyCash WHERE     (Active_Deactive = '1')";
                SqlCommand cmd1 = new SqlCommand(Pety_Reson, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                //  string Cnt = "";
                Cmb_category.Items.Clear();

                while (dr1.Read())
                {
                    Cmb_category.Items.Add(dr1[1].ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Select_PettyCash_Reson_ID()
        {
            try
            {

                #region select the _PettyCash_Reson_ID---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT     PettyID FROM Reason_for_pettyCash WHERE (Active_Deactive = '1') AND Reason='" + Cmb_category.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                //txtBank.Items.Clear();

                if (dr.Read() == true)
                {
                    Catogery_ID.Text = dr[0].ToString();
                }

                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    dr.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Concat_SQL_Quary()
        {
            try
            {
                #region Concat SQL...........................................................



                ReSelectQ = @" SELECT PettyCashID, Reason, Discription, Receiver, Payer, Received_Amount, Paid_amount, Balance, UserID, ptDate
                                  FROM Petty_Cash WHERE 1=1";

                //-----------------------------------------------------------------------------------

                if (By_Category.SelectedIndex == 1)
                {
                    ReSelectQ += " AND Reason='" + Cmb_category.Text + "' ";
                }

                //-----------------------------------------------------------------------------------

                if (By_Receiver.SelectedIndex == 1)
                {
                    ReSelectQ += " AND Receiver='" + Cmb_Reciever.Text + "'";
                }

                //-----------------------------------------------------------------------------------

                if (By_Payer.SelectedIndex == 1)
                {
                    ReSelectQ += " AND Payer='" + Cmb_Payer.Text + "'";
                }

                //-----------------------------------------------------------------------------------

                if (by_Pettycash_ID.SelectedIndex == 1)
                {
                    ReSelectQ = @" SELECT PettyCashID, Reason, Discription, Receiver, Payer, Received_Amount, Paid_amount, Balance, UserID, ptDate
                                  FROM Petty_Cash WHERE 1=1 AND PettyCashID ='" + Cmb_PettyCash_ID.Text + "'";
                }

                //-----------------------------------------------------------------------------------

                ReSelectQ.Replace("1=1 AND ", "");
                ReSelectQ.Replace(" WHERE 1=1 ", "");

                string time = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                if (by_Pettycash_ID.SelectedIndex != 1)
                {
                    ReSelectQ += "AND ptDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + time + "' ";
                }

                if (by_Pettycash_ID.SelectedIndex == 1)
                {
                    ReSelectQ += "";
                }

                // MessageBox.Show(ReSelectQ);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Select_PettyCash_IDs()
        {
            try
            {
                #region select Select_PettyCash_IDs---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT DISTINCT PettyCashID FROM Petty_Cash ORDER BY PettyCashID ASC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                Cmb_PettyCash_ID.Items.Clear();

                while (dr.Read() == true)
                {
                    Cmb_PettyCash_ID.Items.Add(dr[0].ToString());
                }

                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    dr.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Frm_Petty_cash_Load(object sender, EventArgs e)
        {
            Load_Reason();

            LgUser.Text = Logged_User_Details.UserID;
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
        }

        private void Rbt_Cat_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void Cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_PettyCash_Reson_ID();
        }

        string Balancerem = "";
        private void button1_Click(object sender, EventArgs e)
        {
            string totalTables = "";


            //select the print datatables number----------------
            SqlConnection conx = new SqlConnection(IMS);
            conx.Open();

            string ReSelecttableNumbers = @"SELECT RptNumbers FROM RptNumbers";
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


            #region check B4 insert....................................................

            if (By_Category.SelectedIndex == 1 && Cmb_category.Text=="")
            {
                MessageBox.Show("Please select Item Category","error selection Category",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (By_Receiver.SelectedIndex == 1 && Cmb_Reciever.Text == "")
            {
                MessageBox.Show("Please select Item Reciever", "error selection Reciever", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (By_Payer.SelectedIndex == 1 && Cmb_Payer.Text == "")
            {
                MessageBox.Show("Please select Item Payer", "error selection Payer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (by_Pettycash_ID.SelectedIndex == 1 && Cmb_PettyCash_ID.Text == "")
            {
                MessageBox.Show("Please select Item PettyCash ID", "error selection PettyCash ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion

            #region selected by---------------------------------------------------

            string Selected_By = "";

            if (By_Category.SelectedIndex == 0)
            {
                Selected_By += "Categoty=All, ";
            }

            if (By_Category.SelectedIndex == 1)
            {
                Selected_By += "Categoty="+Cmb_category.Text+", ";
            }
            //.............................................................

            if (By_Receiver.SelectedIndex == 0)
            {
                Selected_By += "Receiver=All, ";
            }

            if (By_Receiver.SelectedIndex == 1)
            {
                Selected_By += "Receiver=" + Cmb_Reciever.Text + ", ";
            }
            //.............................................................

            if (By_Payer.SelectedIndex == 0)
            {
                Selected_By += "Payer=All, ";
            }

            if (By_Payer.SelectedIndex == 1)
            {
                Selected_By += "Payer=" + Cmb_Payer.Text + ", ";
            }
            //.............................................................

            if (by_Pettycash_ID.SelectedIndex == 0)
            {
                Selected_By += "Petty cash ID=All";
            }

            if (by_Pettycash_ID.SelectedIndex == 1)
            {
                Selected_By += "Petty cash ID=" + Cmb_PettyCash_ID.Text;
            }
            //.............................................................
            #endregion

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            My_Data_SET ds;

            
                #region Category vice---------------------------------------------------------

            try
            {
                    //create SQL quary.........................................
                SqlConnection conx1 = new SqlConnection(IMS);
                conx1.Open();

                string BalanctRemai = @"select top 1 Current_Bal from PettyCash_Balance order by Bal_ID desc";
                SqlCommand cmdx1 = new SqlCommand(BalanctRemai, conx1);
                SqlDataReader drx1 = cmdx1.ExecuteReader(CommandBehavior.CloseConnection);

                if (drx1.Read() == true)
                {
                    Balancerem = drx1[0].ToString();
                }


                    Concat_SQL_Quary();

                

                     Petty_Cash_Category_Vice rpt = new Petty_Cash_Category_Vice();

                    TextObject From;
                    TextObject To;
                    TextObject user;
                    TextObject sel_by, baltot;

                    if (rpt.ReportDefinition.ReportObjects["Text14"] != null)
                    {
                        From = (TextObject)rpt.ReportDefinition.ReportObjects["Text14"];
                        From.Text = PickerDateFrom.Text;

                    }

                    if (rpt.ReportDefinition.ReportObjects["Text15"] != null)
                    {
                        To = (TextObject)rpt.ReportDefinition.ReportObjects["Text15"];
                        To.Text = PickerDateTo.Text;
                   
                    }

                //select user...................................
                    if (rpt.ReportDefinition.ReportObjects["Text16"] != null)
                    {
                        user = (TextObject)rpt.ReportDefinition.ReportObjects["Text16"];
                        user.Text = LgUser.Text;
                    }

                //select selecteb by-----------------------------------
                    if (rpt.ReportDefinition.ReportObjects["Text20"] != null)
                    {
                        sel_by = (TextObject)rpt.ReportDefinition.ReportObjects["Text20"];
                        sel_by.Text = Selected_By;
                    }


                //end........................

                    if (rpt.ReportDefinition.ReportObjects["Text25"] != null)
                    {
                        baltot = (TextObject)rpt.ReportDefinition.ReportObjects["Text25"];
                        baltot.Text = Balancerem;
                    }


                    SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                    ds = new My_Data_SET();
                    dscmd.Fill(ds);

                   
                    rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                    CrystalReVie_Profit.ReportSource = rpt;
                    CrystalReVie_Profit.Refresh();
                    con1.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("please check category vise details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

                #endregion


        }

        private void By_Receiver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (By_Receiver.SelectedIndex == 0)
            {
                Cmb_Reciever.Enabled = false;
                Cmb_Reciever.SelectedIndex = -1;
            }

            if (By_Receiver.SelectedIndex == 1)
            {
                Cmb_Reciever.Enabled = true;
               
            }
        }

        private void By_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (By_Category.SelectedIndex == 0)
            {
                Cmb_category.Enabled = false;
                Cmb_category.SelectedIndex = -1;
            }

            if (By_Category.SelectedIndex == 1)
            {
                Cmb_category.Enabled = true;
            }
        }

        private void By_Payer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (By_Payer.SelectedIndex == 0)
            {
                Cmb_Payer.Enabled = false;
                Cmb_Payer.SelectedIndex = -1;
            }

            if (By_Payer.SelectedIndex == 1)
            {
                Cmb_Payer.Enabled = true;
            }
        }

        private void by_Pettycash_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (by_Pettycash_ID.SelectedIndex == 0)
            {
                Cmb_PettyCash_ID.Enabled = false;
                Cmb_PettyCash_ID.SelectedIndex = -1;
            }

            if (by_Pettycash_ID.SelectedIndex == 1)
            {
                Cmb_PettyCash_ID.Enabled = true;
            }
        }

        private void Cmb_Reciever_Click(object sender, EventArgs e)
        {
            Cmb_Reciever.Items.Clear();
            
            Select_Users();
        }

        private void Cmb_Payer_Click(object sender, EventArgs e)
        {
            Cmb_Payer.Items.Clear();

            Select_Users();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Concat_SQL_Quary();
        }

        private void Cmb_PettyCash_ID_Click(object sender, EventArgs e)
        {
            Select_PettyCash_IDs();
        }
    }
}
