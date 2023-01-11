using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace Nilwala_Training_center.Payments
{
    public partial class SET_OFF : Form
    {
        public SET_OFF()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        double Pre_Balance = 0;
        double Current_Bal = 0;
        double Current_tot = 0;

        string Invoiced_Last_Auto_ID = "";
        string Auto_ID = "";

        public void Create_SetOff_code()
        {
            #region New St Off code...........................................

            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select DOC_Num from Set_Off_Details";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                if (!dr.Read())
                {
                    SetOff_ID.Text = "STF1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 DOC_Num FROM Set_Off_Details order by DOC_Num DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();



                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        SetOff_ID.Text = "STF" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("This error comes from the set off code generate method. please contact your system administrator.", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Last_Invoice_Auto_ID_Select()
        {
            #region last Invoice Auto ID Select ---------------------------------

            try
            {

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = @"SELECT TOP 1 Invoice_Auto_ID FROM InvoicePaymentDetails order by Invoice_Auto_ID DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                if (dr.Read() == true)
                {
                    Invoiced_Last_Auto_ID = dr[0].ToString();
                    //  MessageBox.Show("Auto ID = " + Invoiced_Last_Auto_ID);


                }

                else
                {
                    MessageBox.Show("You Didn't Invoice yet. Please invoiced a bill with cash", "Empty Cash Balance", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                    this.Close();
                    return;


                }

                Conn.Close();
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error comes from the last Invoice Auto ID Select mothod. please contact your administrator..", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Available_Cash_Total()
        {
            #region Calculate the previous remaining balance------------------------------

            try
            {

                //  MessageBox.Show("Available_Cash_Total");    

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT TOP 1 Remain_Balance FROM Set_Off_Details order by AutoID DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    // MessageBox.Show("NOt Read");

                    #region If never set off the DB----------------------------------

                    Pre_Balance = 0.00;

                    cmd.Dispose();
                    dr.Close();

                    string Cal_Current_Cash = @"SELECT SUM(IPD.PayCash) AS TotalCash
                                                FROM InvoicePaymentDetails IPD INNER JOIN Agency_Payment_DOC_Num APDN ON IPD.InvoiceID=APDN.Agency_DOC_ID WHERE APDN.Status='1'";

                    SqlCommand cmdy = new SqlCommand(Cal_Current_Cash, Conn);
                    SqlDataReader dry = cmdy.ExecuteReader();

                    if (dry.Read() == true)
                    {
                         // MessageBox.Show("Read Cal_Current_Cash");

                        if (dry[0].ToString() == "")
                        {
                           //   MessageBox.Show("Read Cal_Current_Cash=empty");

                            Current_Bal = 0;
                            New_Cash_Balance.Text = "0";
                            Pre_Bal.Text = "0";
                            New_Balance.Text = "0";

                            //MessageBox.Show(Pre_Balance.ToString());
                            //MessageBox.Show(Auto_ID);
                            //MessageBox.Show(Set_Off_Date.Text);
                            //MessageBox.Show(Set_Off_User.Text);
                        }

                        if (dry[0].ToString() != "")
                        {
                            // MessageBox.Show("Read Cal_Current_Cash = not empty");

                            Current_Bal = Convert.ToDouble(dry[0].ToString());
                            New_Cash_Balance.Text = dry[0].ToString();
                            Pre_Bal.Text = "0";
                            New_Balance.Text = Convert.ToString(Convert.ToDecimal(Current_Bal));
                        }
                    }
                    dry.Close();


                    Set_Off_Date.Text = "Did not Set OFF Yet";
                    Set_Off_User.Text = "Did not Set OFF Yet";


                    #endregion
                }

                else
                {
                   //  MessageBox.Show("Read");

                    dr.Close();

                    #region set off -------------------------------

                    //select the frevious total paments (cash)-------------

                    string sqlx = @"SELECT TOP 1 Remain_Balance,InvoiceAutoID,Set_Off_Date,LgUser FROM Set_Off_Details order by AutoID DESC";
                    SqlCommand cmdx = new SqlCommand(sqlx, Conn);
                    SqlDataReader drx = cmdx.ExecuteReader();

                    // if select the frevious total paments (cash)-------------

                    if (drx.Read() == true)
                    {
                       //   MessageBox.Show("drx.Read() == true");

                        Pre_Balance = Convert.ToDouble(drx[0].ToString());
                        Auto_ID = drx[1].ToString();
                        Set_Off_Date.Text = drx[2].ToString();
                        Set_Off_User.Text = drx[3].ToString();

                        //  MessageBox.Show(Pre_Balance.ToString());


                    }

                    //if not select the frevious total paments (cash)-------------
                    else
                    {
                       //  MessageBox.Show("drx.Read() == fales");

                        Pre_Balance = 0;
                        Auto_ID = "0";
                        Set_Off_Date.Text = "Not set off yet";
                        Set_Off_User.Text = "Not set off yet";



                    }


                    drx.Close();

                    //select the Current total paments (cash)-------------

                    string Cal_Current_Cash = @"SELECT SUM(IPD.PayCash) AS TotalCash
                                                FROM InvoicePaymentDetails IPD INNER JOIN Agency_Payment_DOC_Num APDN ON IPD.InvoiceID=APDN.Agency_DOC_ID 
                                                WHERE APDN.Status='1' AND IPD.Invoice_Auto_ID>'" + Auto_ID + "'";
                    SqlCommand cmdy = new SqlCommand(Cal_Current_Cash, Conn);
                    SqlDataReader dry = cmdy.ExecuteReader();

                    if (dry.Read())
                    {
                       //  MessageBox.Show("dry.Read() == true");

                        if (dry[0].ToString() == "")
                        {
                            //  MessageBox.Show("dry.Read() == true and emplty");

                           //    MessageBox.Show( "dry.Read() == true and emplty = "+New_Cash_Balance.Text );

                            Current_Bal = 0.00;
                            New_Cash_Balance.Text = Current_Bal.ToString();

                            Current_tot = Current_Bal + Pre_Balance;

                           //    MessageBox.Show(Current_tot.ToString());

                            New_Balance.Text = Convert.ToString(Convert.ToDecimal(Current_tot));
                            Pre_Bal.Text = Convert.ToString(Pre_Balance);
                        }

                        if (dry[0].ToString() != "")
                        {
                           //MessageBox.Show("dry.Read() == true and not empty");

                            Current_Bal = Convert.ToDouble(dry[0].ToString());
                            New_Cash_Balance.Text = dry[0].ToString();

                           //MessageBox.Show("dry.Read() == true and not empty = " + New_Cash_Balance.Text);

                            Current_tot = Current_Bal + Pre_Balance;
                            New_Balance.Text = Convert.ToString(Convert.ToDecimal(Current_tot));
                            Pre_Bal.Text = Convert.ToString(Pre_Balance);

                        }


                    }

                    else
                    {
                         //MessageBox.Show("dry.Read() == fales");

                        Current_Bal = 0.00;
                        New_Cash_Balance.Text = "0.00";
                        Pre_Bal.Text = "0";
                        New_Balance.Text = "0";

                        //################################################################################_____________________
                        Current_Bal = 0.00;
                        New_Cash_Balance.Text = Current_Bal.ToString();

                        Current_tot = Current_Bal + Pre_Balance;

                        //MessageBox.Show(Current_tot.ToString());

                        New_Balance.Text = Convert.ToString(Convert.ToDecimal(Current_tot));
                        Pre_Bal.Text = Convert.ToString(Pre_Balance);
                    }

                    dry.Close();



                    #endregion

                }
                Conn.Close();
                dr.Close();

                //MessageBox.Show(Pre_Balance.ToString());
                //MessageBox.Show(Auto_ID);
                //MessageBox.Show(Set_Off_Date.Text);
                //MessageBox.Show(Set_Off_User.Text);

            }

            catch (Exception ex)
            {
                MessageBox.Show("This error generrate from the available cash total method. please contact our administrator...", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void SET_OFF_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            Create_SetOff_code();
            Last_Invoice_Auto_ID_Select();
            Available_Cash_Total();
        }

        private void New_Cash_Balance_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(New_Cash_Balance.Text) > 0)
            {
                btnSet_OFF.Enabled = true;

                label7.Text = "You can SET OFF this Balance.";
                label7.ForeColor = System.Drawing.Color.ForestGreen;
            }

            if (Convert.ToDouble(New_Cash_Balance.Text) <= 0)
            {
                btnSet_OFF.Enabled = false;

                label7.Text = "This Already SET OFF. All invoices were SET OFF.";
                label7.ForeColor = System.Drawing.Color.Red;

            }
        }

        private void btnSet_OFF_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to set off the cash details?..", "Are you Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        string insernewid = @"insert into Set_Off_Details(DOC_Num,DOC_ID, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
                                        values('" + SetOff_ID.Text + "','" + SetOff_ID.Text + "','SET_OFF','" + Invoiced_Last_Auto_ID + "','" + New_Balance.Text + "','" + New_Cash_Balance.Text + "','0','" + New_Balance.Text + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "') ";
                        SqlCommand cmm = new SqlCommand(insernewid, cnn);
                        cmm.ExecuteNonQuery();
                        cnn.Close();

                        Create_SetOff_code();
                        Available_Cash_Total();

                        MessageBox.Show("Set Off Successfully..", "Successfully completed.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("This error come from save method. contact your administrator", "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
    }
}
