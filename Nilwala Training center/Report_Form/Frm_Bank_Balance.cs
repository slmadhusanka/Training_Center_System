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
    public partial class Frm_Bank_Balance : Form
    {
        public Frm_Bank_Balance()
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

                //  string Cnt = "";


                Cmb_User.Items.Clear();

                while (dr1.Read())
                {
                    Cmb_User.Items.Add(dr1[0].ToString());

                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Load_Bank_Name()
        {
           
            #region Load_Bank_Name ...........................

            try
            {

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string Bank_Name = @"SELECT  BankID, BankName FROM  Bank_Category";
            SqlCommand cmd1 = new SqlCommand(Bank_Name, con1);
            SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

            Cmb_Bank.Items.Clear();
            Cmb_Bank_Account_Vice.SelectedIndex = -1;

            while (dr1.Read())
            {
                Cmb_Bank.Items.Add(dr1[1].ToString());
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check Bank Details Selection method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Load_Bank_ID_Acc_Name()
        {
            #region Load_Bank_ID_Acc_Name ...........................

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Bank_Name = @"SELECT BankID FROM  Bank_Category WHERE BankName='"+Cmb_Bank.Text+"'";
                SqlCommand cmd1 = new SqlCommand(Bank_Name, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr1.Read())
                {
                    Bank_ID.Text= dr1[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check Bank_ID Details Selection method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Load_Bank_ACC_Numbers()
        {
            #region Load_Bank_ACC_Numbers ...........................

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Bank_Name = @"SELECT Acc_Num FROM  Bank_Doc_details WHERE Bank_Name='" + Bank_ID.Text + "'";
                SqlCommand cmd1 = new SqlCommand(Bank_Name, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);


                Cmb_Bank_Account_Vice.Items.Clear();
                while (dr1.Read())
                {
                    Cmb_Bank_Account_Vice.Items.Add( dr1[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check Bank Account Vice Details Selection method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Load_Bank_Document_ID()
        {
            #region Load_Bank_Document_ID ...........................

            try
            {

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                //Bank_Doc_details.DoC_ID
                string Bank_Name = @"SELECT DoC_ID FROM Bank_Doc_details";
                SqlCommand cmd1 = new SqlCommand(Bank_Name, con1);
                SqlDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                Cmb_Document_ID.Items.Clear();

                while (dr1.Read())
                {
                    Cmb_Document_ID.Items.Add(dr1[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check Bank Document ID Details Selection method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Concat_SQL_Quary()
        {
            try
            {

                #region Concat SQL...........................................................



            ReSelectQ = @"   SELECT Bank_Doc_details.DoC_ID, Bank_Category.BankName,Bank_Doc_details.Acc_Num, Set_Off_Details.Bank_amount, Bank_Doc_details.Banked_Date, Set_Off_Details.LgUser
                             FROM Bank_Doc_details INNER JOIN
                             Set_Off_Details ON Bank_Doc_details.DoC_ID = Set_Off_Details.DOC_ID INNER JOIN
                             Bank_Category ON Bank_Doc_details.Bank_Name = Bank_Category.BankID WHERE 1=1 ";

                //-----------------------------------------------------------------------------------

                if (By_Bank.SelectedIndex == 1)
                {
                    ReSelectQ += " AND Bank_Category.BankName='" + Cmb_Bank.Text + "' ";
                }

                //-----------------------------------------------------------------------------------

                if (By_Account_Vice.SelectedIndex == 1)
                {
                    ReSelectQ += " AND Bank_Doc_details.Acc_Num='" + Cmb_Bank_Account_Vice.Text + "'";
                }

                //-----------------------------------------------------------------------------------

                if (By_User.SelectedIndex == 1)
                {
                    ReSelectQ += " AND Set_Off_Details.LgUser='" + Cmb_User.Text + "'";
                }

                //-----------------------------------------------------------------------------------

                if (by_Document_ID.SelectedIndex == 1)
                {
                    ReSelectQ = @" SELECT Bank_Doc_details.DoC_ID, Bank_Category.BankName,Bank_Doc_details.Acc_Num, Set_Off_Details.Bank_amount, Bank_Doc_details.Banked_Date, Set_Off_Details.LgUser
                             FROM Bank_Doc_details INNER JOIN
                             Set_Off_Details ON Bank_Doc_details.DoC_ID = Set_Off_Details.DOC_ID INNER JOIN
                             Bank_Category ON Bank_Doc_details.Bank_Name = Bank_Category.BankID WHERE 1=1 AND Bank_Doc_details.DoC_ID ='" + Cmb_Document_ID.Text + "'";
                }

                //-----------------------------------------------------------------------------------

                ReSelectQ.Replace("1=1 AND ", "");
                ReSelectQ.Replace(" WHERE 1=1 ", "");

                if (by_Document_ID.SelectedIndex != 1)
                {
                    ReSelectQ += "AND Bank_Doc_details.Banked_Date BETWEEN '" + PickerDateFrom.Text + "' AND '" + PickerDateTo.Text + "' ";
                }

                if (by_Document_ID.SelectedIndex == 1)
                {
                    ReSelectQ += "";
                }

                //MessageBox.Show(ReSelectQ);

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
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

                //if (By_Category.SelectedIndex == 1 && Cmb_category.Text == "")
                //{
                //    MessageBox.Show("Please select Item Category", "error selection Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //if (By_Receiver.SelectedIndex == 1 && Cmb_Reciever.Text == "")
                //{
                //    MessageBox.Show("Please select Item Reciever", "error selection Reciever", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //if (By_Payer.SelectedIndex == 1 && Cmb_Payer.Text == "")
                //{
                //    MessageBox.Show("Please select Item Payer", "error selection Payer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //if (by_Pettycash_ID.SelectedIndex == 1 && Cmb_PettyCash_ID.Text == "")
                //{
                //    MessageBox.Show("Please select Item PettyCash ID", "error selection PettyCash ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                #endregion

                #region selected by---------------------------------------------------

                string Selected_By = "";

                if (By_Bank.SelectedIndex == 0)
                {
                    Selected_By += "Bank=All, ";
                }

                if (By_Bank.SelectedIndex == 1)
                {
                    Selected_By += "Bank=" + Cmb_Bank.Text + ", ";
                }
                //.............................................................

                if (By_Account_Vice.SelectedIndex == 0)
                {
                    //if (By_Bank.SelectedIndex == 0)
                    //{
                    Selected_By += "Account Numbers=All, ";
                    //}

                    //if (By_Bank.SelectedIndex == 1)
                    //{
                    //    Selected_By += "Account Number is '" + Cmb_Bank_Account_Vice.Text + "', ";
                    //}


                }

                if (By_Account_Vice.SelectedIndex == 1)
                {
                    Selected_By += "Account Number(s)=" + Cmb_Bank_Account_Vice.Text + ", ";
                }
                //.............................................................

                if (By_User.SelectedIndex == 0)
                {
                    Selected_By += "User=All, ";
                }

                if (By_User.SelectedIndex == 1)
                {
                    Selected_By += "User=" + Cmb_User.Text + ", ";
                }
                //.............................................................

                if (by_Document_ID.SelectedIndex == 0)
                {
                    Selected_By += "Bank Document ID=All";
                }

                if (by_Document_ID.SelectedIndex == 1)
                {
                    Selected_By += "Bank Document ID=" + Cmb_Document_ID.Text;
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
                    Concat_SQL_Quary();


                    Rpt_Bank_Balance rpt = new Rpt_Bank_Balance();

                    TextObject From;
                    TextObject To;
                    TextObject user;
                    TextObject sel_by;

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

                    SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                    ds = new My_Data_SET();
                    dscmd.Fill(ds);


                    rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                    CrystalReVie_Bank_Bal.ReportSource = rpt;
                    CrystalReVie_Bank_Bal.Refresh();
                    con1.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("please check category vise details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void By_Bank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (By_Bank.SelectedIndex == 0)
            {
                Cmb_Bank.Enabled = false;
                Cmb_Bank.SelectedIndex = -1;
            }

            if (By_Bank.SelectedIndex == 1)
            {
                Cmb_Bank.Enabled = true;
            }
        }

        private void By_Account_Vice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (By_Account_Vice.SelectedIndex == 0)
            {
                Cmb_Bank_Account_Vice.Enabled = false;
                Cmb_Bank_Account_Vice.SelectedIndex = -1;
            }

            if (By_Account_Vice.SelectedIndex == 1)
            {
                Cmb_Bank_Account_Vice.Enabled = true;
                
            }
        }

        private void By_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (By_User.SelectedIndex == 0)
            {
                Cmb_User.Enabled = false;
                Cmb_User.SelectedIndex = -1;
            }

            if (By_User.SelectedIndex == 1)
            {
                Cmb_User.Enabled = true;
            }
        }

        private void by_Document_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (by_Document_ID.SelectedIndex == 0)
            {
                Cmb_Document_ID.Enabled = false;
                Cmb_Document_ID.SelectedIndex = -1;
               
            }

            if (by_Document_ID.SelectedIndex == 1)
            {
                Cmb_Document_ID.Enabled = true;
                Cmb_Document_ID.SelectedIndex = -1;
                Cmb_Bank.SelectedIndex = -1;
                Cmb_User.SelectedIndex = -1;
                Cmb_Bank_Account_Vice.SelectedIndex = -1;

                By_Bank.SelectedIndex = 0;
                By_Account_Vice.SelectedIndex = 0;
                By_Bank.SelectedIndex = 0;
                By_User.SelectedIndex = 0;
                
            }
        }

        private void Cmb_Bank_Click(object sender, EventArgs e)
        {
            Load_Bank_Name();
        }

        private void Cmb_Bank_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Bank_ID_Acc_Name();
        }

        private void Cmb_Bank_Account_Vice_Click(object sender, EventArgs e)
        {
            if (Bank_ID.Text != "Bank_ID")
            {
                Load_Bank_ACC_Numbers();
            }
        }

        private void Cmb_Bank_Account_Vice_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Cmb_User_Click(object sender, EventArgs e)
        {
            Select_Users();
        }

        private void Cmb_Document_ID_Click(object sender, EventArgs e)
        {
            Load_Bank_Document_ID();
        }

        private void Frm_Bank_Balance_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;
        }
    }
}
