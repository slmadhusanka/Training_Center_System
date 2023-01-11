using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;

namespace Nilwala_Training_center.Controling
{
    public partial class User_Control : Form
    {
        public User_Control()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        private void User_Control_Load(object sender, EventArgs e)
        {
            LgUserID.Text = Logged_User_Details.UserID;
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;

            StaffLoad();
            disablecheckbox();
        }

        public void StaffLoad()
        {
            try
            {
                #region Staff ID load in combobox...............................

                SqlConnection sd = new SqlConnection(IMS);
                sd.Open();
                String add = "Select UserCode from UserProfile where AtiveDeactive='1'";
                SqlCommand cmm = new SqlCommand(add, sd);
                SqlDataReader dr1 = cmm.ExecuteReader();

                cmbUserID.Items.Clear();

                while (dr1.Read())
                {
                    cmbUserID.Items.Add(dr1[0].ToString());
                }
                sd.Close();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_01", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Select_User_Name()
        {
            try
            {
                #region Select_User_Name to hte lable...............................

                SqlConnection sd = new SqlConnection(IMS);
                sd.Open();
                String add = "Select  DisplayOn from UserProfile where UserCode='"+cmbUserID.Text+"'";
                SqlCommand cmm = new SqlCommand(add, sd);
                SqlDataReader dr1 = cmm.ExecuteReader();

                if (dr1.Read())
                {
                    lbl_Display_Name.Text= dr1[0].ToString();
                }
                sd.Close();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_02", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void defaltsettingSelect()
        {
            try
            {
                #region select user Setting.........................

                SqlConnection sd = new SqlConnection(IMS);
                sd.Open();
                String add1 = @"SELECT New_User, New_Agency, New_Course, New_Batch, New_Bank, Trainee_Registration, Batch_Payments, Set_Off, Petty_Cash, 
                                Cash_Deposit, User_Control, User_Backup, Rpt_Main_Cash, Rpt_Petty_Cash, Rpt_Batch_Payments, Rpt_Chk_Deposit, Rpt_Bank_Details,
                                Other_Expenses,rpt_Other_Expenses,rpt_pettycashBook,rpt_MainCashbook
                                FROM User_Settings WHERE User_ID='" + cmbUserID.Text+"'";

                SqlCommand cmm1 = new SqlCommand(add1, sd);
                SqlDataReader dr = cmm1.ExecuteReader();

                while (dr.Read())
                {
                    if (dr[0].ToString() == "1")
                    {
                        rbt_New_User.Checked = true;
                    }
                    if (dr[0].ToString() == "0")
                    {
                        rbt_New_User.Checked = false;
                    }//--------------------------------------------------

                    if (dr[1].ToString() == "1")
                    {
                        rbt_New_Agency.Checked = true;
                    }
                    if (dr[1].ToString() == "0")
                    {
                        rbt_New_Agency.Checked = false;
                    }//--------------------------------------------------
                    if (dr[2].ToString() == "1")
                    {
                        rbt_New_Course.Checked = true;
                    }
                    if (dr[2].ToString() == "0")
                    {
                        rbt_New_Course.Checked = false;
                    }//--------------------------------------------------


                    if (dr[3].ToString() == "1")
                    {
                        rbt_New_Batch.Checked = true;
                    }
                    if (dr[3].ToString() == "0")
                    {
                        rbt_New_Batch.Checked = false;
                    }//--------------------------------------------------
                    if (dr[4].ToString() == "1")
                    {
                        rbt_New_Bank.Checked = true;
                    }
                    if (dr[4].ToString() == "0")
                    {
                        rbt_New_Bank.Checked = false;
                    }//--------------------------------------------------
                    if (dr[5].ToString() == "1")
                    {
                        rbt_Trainee_registration.Checked = true;
                    }
                    if (dr[5].ToString() == "0")
                    {
                        rbt_Trainee_registration.Checked = false;
                    }//--------------------------------------------------
                    if (dr[6].ToString() == "1")
                    {
                        rbt_Batch_payment.Checked = true;
                    }
                    if (dr[6].ToString() == "0")
                    {
                        rbt_Batch_payment.Checked = false;
                    }//--------------------------------------------------

                    if (dr[7].ToString() == "1")
                    {
                        rbt_Set_Off.Checked = true;
                    }
                    if (dr[7].ToString() == "0")
                    {
                        rbt_Set_Off.Checked = false;
                    }//--------------------------------------------------
                    if (dr[8].ToString() == "1")
                    {
                        rbt_Petty_cash.Checked = true;
                    }
                    if (dr[8].ToString() == "0")
                    {
                        rbt_Petty_cash.Checked = false;
                    }//--------------------------------------------------
                    if (dr[9].ToString() == "1")
                    {
                        rbt_Deposit.Checked = true;
                    }
                    if (dr[9].ToString() == "0")
                    {
                        rbt_Deposit.Checked = false;
                    }//--------------------------------------------------
                    if (dr[10].ToString() == "1")
                    {
                        rbt_User_Control.Checked = true;
                    }
                    if (dr[10].ToString() == "0")
                    {
                        rbt_User_Control.Checked = false;
                    }//--------------------------------------------------
                    if (dr[11].ToString() == "1")
                    {
                        rbt_DB_Backup.Checked = true;
                    }
                    if (dr[11].ToString() == "0")
                    {
                        rbt_DB_Backup.Checked = false;
                    }//--------------------------------------------------
                    if (dr[12].ToString() == "1")
                    {
                        rbt_Rpt_main_Cash.Checked = true;
                    }
                    if (dr[12].ToString() == "0")
                    {
                        rbt_Rpt_main_Cash.Checked = false;
                    }//--------------------------------------------------
                    if (dr[13].ToString() == "1")
                    {
                        rbt_Rpt_Petty_Cash_Details.Checked = true;
                    }
                    if (dr[13].ToString() == "0")
                    {
                        rbt_Rpt_Petty_Cash_Details.Checked = false;
                    }//--------------------------------------------------
                    if (dr[14].ToString() == "1")
                    {
                        rbt_Rpt_Batch_Paymernts.Checked = true;
                    }
                    if (dr[14].ToString() == "0")
                    {
                        rbt_Rpt_Batch_Paymernts.Checked = false;
                    }//--------------------------------------------------
                    if (dr[15].ToString() == "1")
                    {
                        rbt_Rpt_Cheque_Details.Checked = true;
                    }
                    if (dr[15].ToString() == "0")
                    {
                        rbt_Rpt_Cheque_Details.Checked = false;
                    }//--------------------------------------------------
                    if (dr[16].ToString() == "1")
                    {
                        rbt_Rpt_Bank_Betails.Checked = true;
                    }
                    if (dr[16].ToString() == "0")
                    {
                        rbt_Rpt_Bank_Betails.Checked = false;
                    }//--------------------------------------------------

                    if (dr[17].ToString() == "1")
                    {
                        rbtOtherExpenses.Checked = true;
                    }
                    if (dr[17].ToString() == "0")
                    {
                        rbtOtherExpenses.Checked = false;
                    }//--------------------------------------------------

                    if (dr[18].ToString() == "1")
                    {
                        rbt_rptOtherExpensesDet.Checked = true;
                    }
                    if (dr[18].ToString() == "0")
                    {
                        rbt_rptOtherExpensesDet.Checked = false;
                    }//--------------------------------------------------
                    if (dr[19].ToString() == "1")
                    {
                        rbt_rptPettyCashBook.Checked = true;
                    }
                    if (dr[19].ToString() == "0")
                    {
                        rbt_rptPettyCashBook.Checked = false;
                    }//--------------------------------------------------
                    if (dr[20].ToString() == "1")
                    {
                        rbt_rpt_MainCashBook.Checked = true;
                    }
                    if (dr[20].ToString() == "0")
                    {
                        rbt_rpt_MainCashBook.Checked = false;
                    }//--------------------------------------------------

                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_03", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        public void disablecheckbox()
        {
            #region  disablecheckbox.............................

            rbt_Batch_payment.Enabled = false;
            rbt_DB_Backup.Enabled = false;
            rbt_Deposit.Enabled = false;
            rbt_New_Agency.Enabled = false;
            rbt_New_Bank.Enabled = false;
            rbt_New_Batch.Enabled = false;
            rbt_New_Course.Enabled = false;
            rbt_New_User.Enabled = false;
            rbt_Petty_cash.Enabled = false;
            rbt_Rpt_Bank_Betails.Enabled = false;
            rbt_Rpt_Batch_Paymernts.Enabled = false;
            rbt_Rpt_Cheque_Details.Enabled = false;
            rbt_Rpt_main_Cash.Enabled = false;
            rbt_Rpt_Petty_Cash_Details.Enabled = false;
            rbt_Set_Off.Enabled = false;
            rbt_Trainee_registration.Enabled = false;
            rbt_User_Control.Enabled = false;
            rbt_rptPettyCashBook.Enabled = false;
            rbt_rpt_MainCashBook.Enabled = false;
            rbtOtherExpenses.Enabled = false;
            rbt_rptOtherExpensesDet.Enabled = false;
            
            #endregion
        }

        public void uncheck_Ck_BOX()
        {

            #region  uncheck_Ck_BOX.............................

            rbt_Batch_payment.Checked = false;
            rbt_DB_Backup.Checked = false;
            rbt_Deposit.Checked = false;
            rbt_New_Agency.Checked = false;
            rbt_New_Bank.Checked = false;
            rbt_New_Batch.Checked = false;
            rbt_New_Course.Checked = false;
            rbt_New_User.Checked = false;
            rbt_Petty_cash.Checked = false;
            rbt_Rpt_Bank_Betails.Checked = false;
            rbt_Rpt_Batch_Paymernts.Checked = false;
            rbt_Rpt_Cheque_Details.Checked = false;
            rbt_Rpt_main_Cash.Checked = false;
            rbt_Rpt_Petty_Cash_Details.Checked = false;
            rbt_Set_Off.Checked = false;
            rbt_Trainee_registration.Checked = false;
            rbt_User_Control.Checked = false;
            rbt_rpt_MainCashBook.Checked = false;
            rbt_rptPettyCashBook.Checked = false;
            rbtOtherExpenses.Checked = false;
            rbt_rptOtherExpensesDet.Checked = false;
            #endregion       
        }

        public void enablecheckbox()
        {
            #region  enablecheckbox.............................

            rbt_Batch_payment.Enabled = true;
            rbt_DB_Backup.Enabled = true;
            rbt_Deposit.Enabled = true;
            rbt_New_Agency.Enabled = true;
            rbt_New_Bank.Enabled = true;
            rbt_New_Batch.Enabled = true;
            rbt_New_Course.Enabled = true;
            rbt_New_User.Enabled = true;
            rbt_Rpt_Bank_Betails.Enabled = true;
            rbt_Rpt_Batch_Paymernts.Enabled = true;
            rbt_Rpt_Cheque_Details.Enabled = true;
            rbt_Rpt_main_Cash.Enabled = true;
            rbt_Rpt_Petty_Cash_Details.Enabled = true;
            rbt_Set_Off.Enabled = true;
            rbt_Trainee_registration.Enabled = true;
            rbt_User_Control.Enabled = true;
            rbt_Petty_cash.Enabled = true;
            rbt_rpt_MainCashBook.Enabled = true;
            rbt_rptPettyCashBook.Enabled = true;
            rbtOtherExpenses.Enabled = true;
            rbt_rptOtherExpensesDet.Enabled = true;

            #endregion    
        }

        private void cmbUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_User_Name();
            defaltsettingSelect();

            if (cmbUserID.SelectedIndex != -1)
            {
                btnUpdate.Enabled = true;
                BtnCancel.Enabled = true;
                enablecheckbox();
            }
            if (cmbUserID.SelectedIndex == -1)
            {
                btnUpdate.Enabled = false;
                BtnCancel.Enabled = false;
            }
        }

        string New_User, New_Agency, New_Course, New_Batch, New_Bank, Trainee_Registration, Batch_Payments, Set_Off, Petty_Cash, Cash_Deposit, User_Control1, User_Backup, Rpt_Main_Cash, Rpt_Petty_Cash, Rpt_Batch_Payments, Rpt_Chk_Deposit, Rpt_Bank_Details, OtherExpe, RptOtherexpesn, rptMainbook, RptPettycashbook;


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // try
            //{

                #region Update user setting.........................

            if (rbt_New_User.Checked == true)
            {
                New_User = "1";
            }
            if (rbt_New_User.Checked == false)
            {
                New_User = "0";
            }//--------------------------------------------------

            if (rbt_New_Agency.Checked == true)
            {
                New_Agency = "1";
            }
            if (rbt_New_Agency.Checked == false)
            {
                New_Agency = "0";
            }//--------------------------------------------------
            if (rbt_New_Course.Checked == true)
            {
                New_Course = "1";
            }
            if (rbt_New_Course.Checked == false)
            {
                New_Course = "0";
            }//--------------------------------------------------


            if (rbt_New_Batch.Checked == true)
            {
                New_Batch = "1";
            }
            if (rbt_New_Batch.Checked == false)
            {
                New_Batch = "0";
            }//--------------------------------------------------
            if (rbt_New_Bank.Checked == true)
            {
                New_Bank = "1";
            }
            if (rbt_New_Bank.Checked == false)
            {
                New_Bank = "0";
            }//--------------------------------------------------
            if (rbt_Trainee_registration.Checked == true)
            {
                Trainee_Registration = "1";
            }
            if (rbt_Trainee_registration.Checked == false)
            {
                Trainee_Registration = "0";
            }//--------------------------------------------------
            if (rbt_Batch_payment.Checked == true)
            {
                Batch_Payments = "1";
            }
            if (rbt_Batch_payment.Checked == false)
            {
                Batch_Payments = "0";
            }//--------------------------------------------------

            if (rbt_Set_Off.Checked == true)
            {
                Set_Off = "1";
            }
            if (rbt_Set_Off.Checked == false)
            {
                Set_Off = "0";
            }//--------------------------------------------------
            if (rbt_Petty_cash.Checked == true)
            {
                Petty_Cash = "1";
            }
            if (rbt_Petty_cash.Checked == false)
            {
                Petty_Cash = "0";
            }//--------------------------------------------------
            if (rbt_Deposit.Checked == true)
            {
                Cash_Deposit = "1";
            }
            if (rbt_Deposit.Checked == false)
            {
                Cash_Deposit = "0";
            }//--------------------------------------------------
            if (rbt_User_Control.Checked == true)
            {
                User_Control1 = "1";
            }
            if (rbt_User_Control.Checked == false)
            {
                User_Control1 = "0";
            }//-------------------------------------------------- , , 
            if (rbt_DB_Backup.Checked == true)
            {
                User_Backup = "1";
            }
            if (rbt_DB_Backup.Checked == false)
            {
                User_Backup = "0";
            }//--------------------------------------------------
            if (rbt_Rpt_main_Cash.Checked == true)
            {
                Rpt_Main_Cash = "1";
            }
            if (rbt_Rpt_main_Cash.Checked == false)
            {
                Rpt_Main_Cash = "0";
            }//--------------------------------------------------
            if (rbt_Rpt_Petty_Cash_Details.Checked == true)
            {
                Rpt_Petty_Cash = "1";
            }
            if (rbt_Rpt_Petty_Cash_Details.Checked == false)
            {
                Rpt_Petty_Cash = "0";
            }//--------------------------------------------------
            if (rbt_Rpt_Batch_Paymernts.Checked == true)
            {
                Rpt_Batch_Payments = "1";
            }
            if (rbt_Rpt_Batch_Paymernts.Checked == false)
            {
                Rpt_Batch_Payments = "0";
            }//--------------------------------------------------
            if (rbt_Rpt_Cheque_Details.Checked == true)
            {
                Rpt_Chk_Deposit = "1";
            }
            if (rbt_Rpt_Cheque_Details.Checked == false)
            {
                Rpt_Chk_Deposit = "0";
            }//--------------------------------------------------
            if (rbt_Rpt_Bank_Betails.Checked == true)
            {
                Rpt_Bank_Details = "1";
            }
            if (rbt_Rpt_Bank_Betails.Checked == false)
            {
                Rpt_Bank_Details = "0";
            }//--------------------------------------------------

            if (rbtOtherExpenses.Checked == true)
            {
               OtherExpe = "1";
            }
            if (rbtOtherExpenses.Checked == false)
            {
                OtherExpe = "0";
            }//--------------------------------------------------

            if (rbt_rptOtherExpensesDet.Checked == true)
            {
                RptOtherexpesn = "1";
            }
            if (rbt_rptOtherExpensesDet.Checked == false)
            {
                RptOtherexpesn = "0";
            }//--------------------------------------------------
            if (rbt_rptPettyCashBook.Checked == true)
            {
                RptPettycashbook = "1";
            }
            if (rbt_rptPettyCashBook.Checked == false)
            {
                RptPettycashbook= "0";
            }//--------------------------------------------------
            if (rbt_rpt_MainCashBook.Checked == true)
            {
                rptMainbook= "1";
            }
            if (rbt_rpt_MainCashBook.Checked == false)
            {
                rptMainbook= "0";
            }//--------------------------------------------------
           
            #endregion

            DialogResult se = MessageBox.Show("Dou you want to Edit User Setting ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (se == DialogResult.Yes)
            {
                #region pass data to DB --------------------------------------

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String update1 = @"update User_Settings set New_User='" + New_User + "', New_Agency='" + New_Agency + "', New_Course='" + New_Course + "', New_Batch='" + New_Batch + "', New_Bank='" + New_Bank + "', Trainee_Registration='" + Trainee_Registration + "', Batch_Payments='" + Batch_Payments + "', Set_Off='" + Set_Off + "', Petty_Cash='" + Petty_Cash + "', Cash_Deposit='" + Cash_Deposit + "', User_Control='" + User_Control1 + "', User_Backup='" + User_Backup + "', Rpt_Main_Cash='" + Rpt_Main_Cash + "', Rpt_Petty_Cash='" + Rpt_Petty_Cash + "', Rpt_Batch_Payments='" + Rpt_Batch_Payments + "', Rpt_Chk_Deposit='" + Rpt_Chk_Deposit + "', Rpt_Bank_Details='" + Rpt_Bank_Details + "',Other_Expenses='" + OtherExpe + "',rpt_Other_Expenses='" + RptOtherexpesn + "',rpt_pettycashBook='" + RptPettycashbook + "',rpt_MainCashbook='" + rptMainbook + "' WHERE  User_ID='" + cmbUserID.Text + "'";//

                SqlCommand cmm = new SqlCommand(update1, cnn);
                cmm.ExecuteNonQuery();
                MessageBox.Show("Update Successfully...");

                #endregion

                #region Clear checkbox and combo box................

                cmbUserID.SelectedIndex = -1;

                disablecheckbox();

                uncheck_Ck_BOX();

                #endregion
            }
           

            //}
            // catch (Exception ex)
            // {
            //     MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            // }

        }

        private void rbt_Batch_payment_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
