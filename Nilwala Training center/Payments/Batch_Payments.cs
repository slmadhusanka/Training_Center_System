using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Nilwala_Training_center.Payments
{
    public partial class Batch_Payments : Form
    {
        public Batch_Payments()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string Invoiced_Last_Auto_ID = "";
        String Last_SET_OFF_BAL = "";

        Decimal totset;

        private void Batch_Payments_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            TxtCash.Focus();
            getCreateStockCode();
        }


        public void getCreateStockCode()
        {
            #region New Document Code auto generate...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select Agency_DOC_ID from Agency_Payment_DOC_Num";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    lblDocumentNo.Text = "CCP10000001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 Agency_DOC_ID FROM Agency_Payment_DOC_Num order by Agency_DOC_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        lblDocumentNo.Text = "CCP" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_1", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }


        public void Select_Bank()
        {
            try
            {

                #region select the bank---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT BankName FROM Bank_Category";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
               
                txtBank.Items.Clear();
                

                while (dr.Read() == true)
                {
                    txtBank.Items.Add(dr[0].ToString());
                }
                txtBank.Items.Add("<New>");
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    dr.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_2", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Last_SetOFF_Details()
        {
            #region last Invoice Auto ID Select ---------------------------------

            try
            {

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT TOP 1 InvoiceAutoID,Remain_Balance FROM Set_Off_Details order by AutoID DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                if (dr.Read() == true)
                {
                    Invoiced_Last_Auto_ID = dr[0].ToString();
                    Last_SET_OFF_BAL = dr[1].ToString();

                    Conn.Close();
                    dr.Close();
                }
                else
                {
                    Invoiced_Last_Auto_ID = "0";
                    Last_SET_OFF_BAL = "0";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_3", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

        public void createAcceptedAmountTotal()
        {
            try
            {

                #region createAcceptedAmountTotal

                double add = (double.Parse(TxtCash.Text) + double.Parse(txtCheque.Text) + double.Parse(txtCard.Text));
                label21.Text = add.ToString();
                label27.Text = Convert.ToString(Convert.ToDouble(label27.Text) + add);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_4", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void totadueAmount()
        {
            try
            {
                #region Total Due Amount......

                decimal gtotal = 0;
                foreach (ListViewItem lstItem in listView1.Items)
                {
                    gtotal += Math.Round(decimal.Parse(lstItem.SubItems[3].Text), 2);
                }
                label25.Text = Convert.ToString(gtotal);


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("this error came from Total Due Amount", "System Error_5", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void cleartextbox()
        {
            try
            {
                #region cleartextBox

                TxtCash.Text = "0.0";
                txtCheque.Text = "0.0";
                txtCard.Text = "0.0";
                txtBank.Enabled = false;
                txtBranch.Text = "-";
                ChecNo.Text = "-";
                Card_no.Text = "-";
                Card_no.Enabled = false;
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ChecNo.Enabled = false;



                dateTimePicker1.ResetText();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_6", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void addcash_card_check_on_listview()
        {
            try
            {

                #region addcash_card_check_on_listview
                ListViewItem li;
                li = new ListViewItem(TxtCash.Text);
                li.SubItems.Add(txtCheque.Text);
                li.SubItems.Add(txtCard.Text);

                //Select the Bank -------------------
                if (Convert.ToDouble(txtCheque.Text) != 0)
                {
                    li.SubItems.Add(txtBank.Text);
                }

                if (Convert.ToDouble(txtCheque.Text) == 0)
                {
                    li.SubItems.Add("-");
                }

                //------------------------------------------

                li.SubItems.Add(ChecNo.Text);
                li.SubItems.Add(Card_no.Text);
                li.SubItems.Add(txtBranch.Text);
                li.SubItems.Add(dateTimePicker1.Text);


                if (Convert.ToDouble(txtCheque.Text) != 0)
                {
                    li.SubItems.Add(lbl_Bank_ID.Text);

                   // MessageBox.Show(lbl_Bank_ID.Text);
                }

                if (Convert.ToDouble(txtCheque.Text) == 0)
                {
                    li.SubItems.Add("-");
                   // MessageBox.Show("-");
                }

                listView2.Items.Add(li);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_7", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        public void Customer_Total_Paymet_FrmDB()
        {
            try
            {
                #region Select Custoemr paymernt summary--------------------------

                double LastBalance = 0;
                double Debit_Balance = 0;

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

                string sql1 = "SELECT TOP (1) Balance,Debit_Balance FROM RegCusCredBalance WHERE (CusID = '" + txtSupID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                if (dr7.Read())
                {
                    LastBalance = Convert.ToDouble(dr7[0].ToString());
                    Debit_Balance = Convert.ToDouble(dr7[1].ToString());

                    //MessageBox.Show(LastBalance.ToString()+" "+Debit_Balance.ToString());
                    //MessageBox.Show(LastBalance.ToString());
                }

                txtTotalRem.Text = Convert.ToString(Convert.ToDouble(label27.Text) - Convert.ToDouble(label29.Text) + Debit_Balance);

                cmd1.Dispose();
                dr7.Close();
                Conn.Close();



                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_7", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void ListView4Item_Count()
        {
            #region if GRN paymet list view empty, apply button dissable

            if (listView4.Items.Count == 0)
            {
                button1.Enabled = false;

            }

            else
                button1.Enabled = true;

            #endregion
        }

        public void TotalPayamount()
        {
            try
            {
                #region Total Pay amount
                decimal gtotal = 0;
                foreach (ListViewItem lstItem in listView4.Items)
                {
                    gtotal += Math.Round(decimal.Parse(lstItem.SubItems[4].Text), 2);
                }
                label29.Text = Convert.ToString(gtotal);


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_8", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void Customer_Final_Balance_Update()
        {
            try
            {
                #region Customer_Final_Balance_Update--------------------------

                double LastBalance = 0;
                double New_Bal = 0;

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

                string sql1 = "SELECT TOP (1) Balance FROM RegCusCredBalance WHERE (CusID = '" + txtSupID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                if (dr7.Read())
                {
                    LastBalance = Convert.ToDouble(dr7[0].ToString());
                }

                #region New Vendor Previos Remainder is Possitive Value----------------------------

                //balance calc-----------------------------
                double Calc_Bal = LastBalance - Convert.ToDouble(label27.Text);

                //if there is some remaining credits
                if (Calc_Bal >= 0)
                {
                    New_Bal = Calc_Bal;
                }
                //if cedite over and some balace on our hand....
                if (Calc_Bal < 0)
                {
                    New_Bal = 0;
                }

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string Vend_DebitPaymet = @"INSERT INTO RegCusCredBalance(CusID, DocNumber, Credit_Amount, Debit_Amount, Debit_Balance, Balance, Date) 
                                        VALUES  ('" + txtSupID.Text + "','" + lblDocumentNo.Text + "','0','" + label27.Text + "','" + txtTotalRem.Text + "','" + New_Bal + "','" + DateTime.Now.ToString() + "')";

                SqlCommand cmd21 = new SqlCommand(Vend_DebitPaymet, con1);
                cmd21.ExecuteNonQuery();

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion

                cmd1.Dispose();
                dr7.Close();
                Conn.Close();



                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_8", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void getCreate_Bank_Catogory_Code()
        {
            #region getCreate_Agency_Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                //  string sql = "select OrderID from CurrentStockItems";
                string sql = "SELECT     BankID, BankName FROM Bank_Category";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    lblBanklDauto.Text = "BNK1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    // string sql1 = " SELECT TOP 1 OrderID FROM CurrentStockItems order by OrderID DESC";
                    string sql1 = " SELECT top 1 BankID, BankName FROM Bank_Category order by BankID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        lblBanklDauto.Text = "BNK" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void txtBank_Click(object sender, EventArgs e)
        {
            Select_Bank();
        }


        string BankID;
        private void txtBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtBank.SelectedIndex == -1)
            {
                // chbDeactive.Hide();
                return;
            }
            if (txtBank.SelectedItem.ToString() == "<New>")
            {
                PnlBankName.Visible = true;
                txtbankName.Focus();
                getCreate_Bank_Catogory_Code();
                lbl_Bank_ID.Text = "";


            }

            #region select the bank ID pass to Label---------------------

            SqlConnection Conn1 = new SqlConnection(IMS);
            Conn1.Open();


            //=====================================================================================================================
            string sql1 = "SELECT BankName,BankID FROM Bank_Category where BankName='" + txtBank.Text + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, Conn1);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                if (txtBank.Text != "<New>")
                {

                    lbl_Bank_ID.Text = dr1[1].ToString();
                }
                if (txtBank.Text == "<New>")
                {
                    
                    return;
                }
            }

            #endregion


            try
            {

                #region select the bank ID---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT BankName,BankID FROM Bank_Category where BankName='" + txtBank.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================



                while (dr.Read() == true)
                {
                    //txtBank.Items.Add(dr[0].ToString());
                    BankID = (dr[1].ToString());
                }

                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    dr.Close();
                }

                #endregion

                if (txtBank.SelectedIndex == -1)
                {
                    txtBank.Focus();
                    return;
                }
                if (txtBank.SelectedIndex != -1)
                {
                    ChecNo.Focus();

                }

              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_9", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void txtBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtBranch.Focus();
            }
        }

        private void txtBranch_Leave(object sender, EventArgs e)
        {
            if (txtBranch.Text == "")
            {
                txtBranch.Text = "-";
            }
        }

        private void txtBranch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                dateTimePicker1.Focus();
            }
        }

        private void TxtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCheque.Focus();
            }
        }

        private void TxtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void TxtCash_Leave(object sender, EventArgs e)
        {
            if (TxtCash.Text == "")
            {
                TxtCash.Text = "0.00";
            }
        }

        private void TxtCash_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void txtCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txtCheque.Text == "" || Convert.ToDouble(txtCheque.Text) == 0)
                {
                    txtCard.Focus();
                }

                if (Convert.ToDouble(txtCheque.Text) > 0)
                {
                    Select_Bank();
                    txtBank.Focus();
                    txtBank.DroppedDown = true;
                }
            }
        }

        private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtCheque_Leave(object sender, EventArgs e)
        {
            if (txtCheque.Text == "" || double.Parse(txtCheque.Text) == 0)
            {
                // cleartextbox();
                txtCheque.Text = "0.0";
                txtBank.Enabled = false;
                txtBranch.Enabled = false;
                ChecNo.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
        }

        private void txtCheque_TextChanged(object sender, EventArgs e)
        {
            if (txtCheque.Text == "")
            {
                return;
            }

            if (double.Parse(txtCheque.Text) > 0)
            {
                txtBank.Enabled = true;
                txtBranch.Enabled = true;
                ChecNo.Enabled = true;
                dateTimePicker1.Enabled = true;
                button3.Enabled = true;

            }
        }

        private void txtCard_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                #region textbox value pass to listview,cleartextbox,createAcceptTotal

                if (txtCard.Text == "")
                {
                    txtCard.Text = "0.0";
                }

                if (e.KeyValue == 13)
                {
                    // check cash,sheque,card AMOUNT-----------------------------------------------------------------------------------------------
                    if ((double.Parse(TxtCash.Text) + double.Parse(txtCheque.Text) + double.Parse(txtCard.Text)) > 0)
                    {
                        //  -----------------------------------------------------------------------------------------------------------------------------

                        //IF CARD AMOUNT 0 VALUES PASS LIST VIEW ------------------------------------------------------------------------------------------
                        if (double.Parse(txtCard.Text) == 0)
                        {
                            DialogResult re = MessageBox.Show("Do you want to add accepted values to makes the payment", "WarningException", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (re == DialogResult.Yes)
                            {

                                #region bank and check details OK or Not

                                if (Convert.ToDouble(txtCard.Text) != 0)
                                {
                                    if (Card_no.Text == "-")
                                    {
                                        MessageBox.Show("Please fill the cheque Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtCard.Focus();
                                        return;
                                    }
                                }//end txtcard

                                if (Convert.ToDouble(txtCheque.Text) != 0)
                                {
                                    if (txtBank.Text == "-")
                                    {
                                        MessageBox.Show("Please fill the Bank Name !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtBank.Focus();
                                        return;
                                    }

                                    if (ChecNo.Text == "-")
                                    {
                                        MessageBox.Show("Please fill the Cheque number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        ChecNo.Focus();
                                        return;
                                    }
                                }//end txtcheque details

                                #endregion


                                if (txtCard.Text == "")
                                {
                                    txtCard.Text = "0.0";
                                    return;
                                }
                                createAcceptedAmountTotal();
                                // addcash_card_check_on_listview();

                                #region addcash_card_check_on_listview

                                ListViewItem li;
                                li = new ListViewItem(TxtCash.Text);
                                li.SubItems.Add(txtCheque.Text);
                                li.SubItems.Add(txtCard.Text);

                                //Select the Bank -------------------
                                if (Convert.ToDouble(txtCheque.Text) != 0)
                                {
                                    li.SubItems.Add(txtBank.Text);
                                }

                                if (Convert.ToDouble(txtCheque.Text) == 0)
                                {
                                    li.SubItems.Add("-");
                                }

                                //------------------------------------------

                                li.SubItems.Add(ChecNo.Text);
                                li.SubItems.Add(Card_no.Text);
                                li.SubItems.Add(txtBranch.Text);
                                li.SubItems.Add(dateTimePicker1.Text);
                                
                                if (Convert.ToDouble(txtCheque.Text) != 0)
                                {
                                    li.SubItems.Add(lbl_Bank_ID.Text);

                                   // MessageBox.Show(lbl_Bank_ID.Text);
                                }

                                if (Convert.ToDouble(txtCheque.Text) == 0)
                                {
                                    li.SubItems.Add("-");
                                   // MessageBox.Show("-");
                                }

                                listView2.Items.Add(li);
                                #endregion

                                cleartextbox();
                                button3.Focus();
                            }
                        }
                        else
                        {
                            Card_no.Enabled = true;
                            Card_no.Focus();
                        }
                        //  -----------------------------------------------------------------------------------------------------------------------------

                    }
                    else
                    {
                        DialogResult rest = MessageBox.Show("Please Enter Value..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (rest == DialogResult.OK)
                        {
                            TxtCash.Focus();
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_10", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void ChecNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtBranch.Focus();
            }
        }

        private void ChecNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void ChecNo_Leave(object sender, EventArgs e)
        {

            if (ChecNo.Text == "")
            {
                ChecNo.Text = "-";
            }
        }

        private void Card_no_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                #region textbox value pass to listview,cleartextbox,createAcceptTotal

                if (e.KeyValue == 13)
                {
                    DialogResult re = MessageBox.Show("Do you want to add accepted values to makes the payment", "WarningException", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (re == DialogResult.Yes)
                    {
                        #region bank and check details OK or Not

                        if (Convert.ToDouble(txtCard.Text) != 0)
                        {
                            if (Card_no.Text == "-")
                            {
                                MessageBox.Show("Please fill the cheque Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtCard.Focus();
                                return;
                            }
                        }//end txtcard

                        if (Convert.ToDouble(txtCheque.Text) != 0)
                        {
                            if (txtBank.Text == "-")
                            {
                                MessageBox.Show("Please fill the Bank Name !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBank.Focus();
                                return;
                            }

                            if (ChecNo.Text == "-")
                            {
                                MessageBox.Show("Please fill the Cheque number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ChecNo.Focus();
                                return;
                            }
                        }//end txtcheque details

                        #endregion


                        createAcceptedAmountTotal();
                        addcash_card_check_on_listview();
                        cleartextbox();
                        button3.Focus();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_11", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Card_no_Leave(object sender, EventArgs e)
        {

            if (Card_no.Text == "")
            {
                Card_no.Text = "-";
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCard.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                #region textbox value pass to listview,cleartextbox,createAcceptTotal

                if (txtCard.Text == "")
                {
                    txtCard.Text = "0.0";
                }

                if (txtCard.Text == "")
                {
                    txtCard.Text = "0.0";
                    return;
                }

                if (TxtCash.Text == "" && txtCard.Text == "" && txtCheque.Text == "0.0" || TxtCash.Text == "0.0" && txtCard.Text == "0.0" && txtCheque.Text == "0.0")
                {
                    MessageBox.Show("Please Enter Amount....", "Message");
                    TxtCash.Focus();
                    return;

                }




                #region bank and check details OK or Not

                if (Convert.ToDouble(txtCard.Text) != 0)
                {
                    if (Card_no.Text == "-")
                    {
                        MessageBox.Show("Please fill the cheque Number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCard.Focus();
                        return;
                    }
                }//end txtcard

                if (Convert.ToDouble(txtCheque.Text) != 0)
                {
                    if (txtBank.Text == "-")
                    {
                        MessageBox.Show("Please fill the Bank Name !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBank.Focus();
                        return;
                    }

                    if (ChecNo.Text == "-")
                    {
                        MessageBox.Show("Please fill the Cheque number !!", "Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ChecNo.Focus();
                        return;
                    }
                }//end txtcheque details

                #endregion




                // check cash,sheque,card AMOUNT-----------------------------------------------------------------------------------------------
                if ((double.Parse(TxtCash.Text) + double.Parse(txtCheque.Text) + double.Parse(txtCard.Text)) > 0)
                {
                    //  -----------------------------------------------------------------------------------------------------------------------------

                    //IF CARD AMOUNT 0 VALUES PASS LIST VIEW ------------------------------------------------------------------------------------------
                    if (double.Parse(txtCard.Text) == 0)
                    {
                        DialogResult re = MessageBox.Show("Do you want to add accepted values to makes the payment", "WarningException", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (re == DialogResult.Yes)
                        {



                            createAcceptedAmountTotal();
                            // addcash_card_check_on_listview();

                            #region addcash_card_check_on_listview

                            ListViewItem li;
                            li = new ListViewItem(TxtCash.Text);
                            li.SubItems.Add(txtCheque.Text);
                            li.SubItems.Add(txtCard.Text);

                            //Select the Bank -------------------
                            if (Convert.ToDouble(txtCheque.Text) != 0)
                            {
                                li.SubItems.Add(txtBank.Text);
                            }

                            if (Convert.ToDouble(txtCheque.Text) == 0)
                            {
                                li.SubItems.Add("-");
                            }

                            //------------------------------------------

                            li.SubItems.Add(ChecNo.Text);
                            li.SubItems.Add(Card_no.Text);
                            li.SubItems.Add(txtBranch.Text);
                            li.SubItems.Add(dateTimePicker1.Text);
                            li.SubItems.Add(lbl_Bank_ID.Text);

                            listView2.Items.Add(li);
                            #endregion
                            cleartextbox();
                            txtBank.SelectedIndex = -1;
                            button3.Focus();
                        }
                    }
                    else
                    {
                        Card_no.Enabled = true;
                        Card_no.Focus();
                    }
                    //  -----------------------------------------------------------------------------------------------------------------------------

                }
                else
                {
                    DialogResult rest = MessageBox.Show("Please Enter Value..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (rest == DialogResult.OK)
                    {
                        TxtCash.Focus();
                    }
                }


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_12", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            double cash = Convert.ToDouble(listView2.SelectedItems[0].SubItems[0].Text);
            double chek = Convert.ToDouble(listView2.SelectedItems[0].SubItems[1].Text);
            double card = Convert.ToDouble(listView2.SelectedItems[0].SubItems[2].Text);

            label27.Text = Convert.ToString(Convert.ToDouble(label27.Text) - (cash + chek + card));

            listView2.SelectedItems[0].Remove();
            txtBank.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                PnlAgencySerch.Visible = true;

                #region select the customer when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT Ajecy_ID, Agency_Name, Agency_Address, Agency_Email, Agency_Per_01_Name, Agency_Per_01_Mob, Agency_Per_02_Name, Agency_Per_02_Mob
                                        FROM Agency_Details";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                listViewAgency.Items.Clear();

                while (dr.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());

                    listViewAgency.Items.Add(li);
                    

                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_13", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlAgencySerch.Visible = false;
        }

        private void textBox19_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {

                PnlAgencySerch.Visible = true;

                #region select the customer when select the button

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = @"SELECT Ajecy_ID, Agency_Name, Agency_Address, Agency_Email, Agency_Per_01_Name, Agency_Per_01_Mob, Agency_Per_02_Name, Agency_Per_02_Mob
                                        FROM Agency_Details WHERE Ajecy_ID LIKE '%"+textBox19.Text+"%' OR Agency_Name LIKE '%"+textBox19.Text+"%'";

                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                listViewAgency.Items.Clear();

                while (dr.Read() == true)
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());

                    listViewAgency.Items.Add(li);


                }

                if (con1.State == ConnectionState.Open)
                {
                    con1.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_14", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listViewAgency_DoubleClick(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            label29.Text = "0.00";

               

                try
                {
                     #region Agency details fill in Textbox.......
                    ListViewItem lst = listViewAgency.SelectedItems[0];

                            txtSupID.Text = lst.SubItems[0].Text;
                            Txtsup_name.Text = lst.SubItems[1].Text;

                            PnlAgencySerch.Visible = false;

              
                #endregion

                     #region add value in list view


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectvalue = @" SELECT     DOC_ID, Time_Stamp, Credit_Amount, Remain_amount
                                        FROM         Trainee_Registration_DOC_Details
                                        WHERE     (DOC_Status = '1' AND Remain_amount >0 AND  Agency_ID='" + txtSupID.Text + "')";

                SqlCommand cmd = new SqlCommand(selectvalue, con1);
                SqlDataReader sd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                listView1.Items.Clear();
                while (sd.Read() == true)
                {

                    ListViewItem li;
                    li = new ListViewItem(sd[0].ToString());
                    li.SubItems.Add(sd[1].ToString());
                    li.SubItems.Add(sd[2].ToString());
                    li.SubItems.Add(sd[3].ToString());
                    listView1.Items.Add(li);


                }

                totadueAmount();

                Customer_Total_Paymet_FrmDB();

                #endregion

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error_15", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
           
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //from Balance on list view  pass to Negotiated Amount textbox--------------------------------------------------------------  
            for (int a = 0; a <= listView4.Items.Count - 1; a++)
            {
                if (listView1.SelectedItems[0].SubItems[0].Text == listView4.Items[a].SubItems[0].Text)
                {
                    MessageBox.Show("This already in the payment table. please try another one or remove it and try again", "Duplicate Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ListViewItem item = listView1.SelectedItems[0];
            txtnegotiaAmount.Text = item.SubItems[3].Text;
            txtnegotiaAmount.Enabled = true;
            txtnegotiaAmount.Focus();
            //--------------------------------------------------------------------------------------------------------------------------
           
        }

        private void txtnegotiaAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtnegotiaAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                #region value pass from listview1 to listview2 with calculation

                if (e.KeyValue == 13)
                {
                    //from Balance on list view check Graterthan Negotiated Amount textbox--------------------------------------------------------------  

                    if (txtnegotiaAmount.Text == "")
                    {
                        return;
                    }

                    ListViewItem item1 = listView1.SelectedItems[0];



                    if ((double.Parse(item1.SubItems[3].Text) >= (double.Parse(txtnegotiaAmount.Text))))
                    {

                        //-----------------------------------------------
                        if (Convert.ToDouble(txtnegotiaAmount.Text) == 0)
                        {
                            MessageBox.Show("Negotiate amount cannot be Zero or empty!", "Error negotiate amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtnegotiaAmount.Focus();
                            return;
                        }//close if
                        //--------------------------------------------


                        //check negotiated value & Remain total--------------------------------------------------------------  
                        if ((double.Parse(txtTotalRem.Text) >= (double.Parse(txtnegotiaAmount.Text))))
                        {

                            //string min2 = (double.Parse(txtTotalRem.Text) - double.Parse(txtnegotiaAmount.Text)).ToString();
                            //txtTotalRem.Text = min2;
                            //storeValue = label27.Text;



                            ListViewItem item = listView1.SelectedItems[0];
                            ListViewItem li;
                            li = new ListViewItem(item.SubItems[0].Text);
                            li.SubItems.Add(dateTimePicker2.Text);
                            li.SubItems.Add(item.SubItems[2]);

                            //
                            string pastBalan = (double.Parse(item.SubItems[3].Text) - double.Parse(txtnegotiaAmount.Text)).ToString();
                            li.SubItems.Add(pastBalan);
                            li.SubItems.Add(txtnegotiaAmount.Text);

                            //=====================================================================

                            listView4.Items.Add(li);

                            txtnegotiaAmount.Text = "0.0";
                            txtnegotiaAmount.Enabled = false;

                            TotalPayamount();

                            MessageBox.Show("Added to the payment Details", "Added Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);




                            if (txtnegotiaAmount.Text == "")
                            {
                                txtnegotiaAmount.Text = "0.00";
                            }

                        } //check negotiated value & Remain total

                        else
                        {
                            MessageBox.Show("given money less than you type price", "WarningException", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtnegotiaAmount.Text = "0.0";
                            return;

                        }

                    }//from Balance on list view check Graterthan Negotiated Amount textbox
                    else
                    {
                        MessageBox.Show("You cannot pay more than that Invoice Have", "WarningException", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtnegotiaAmount.Text = "0.0";
                        return;

                    }
                }//end mail if
                #endregion

                ListView4Item_Count();
            }
            catch (Exception ex)
            {
                MessageBox.Show("value pass from listview1 to listview2 with calculation", "System Error_16", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void label27_TextChanged(object sender, EventArgs e)
        {
            Customer_Total_Paymet_FrmDB();
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {

            listView4.SelectedItems[0].Remove();

            TotalPayamount();

            ListView4Item_Count();
        }

        private void label29_TextChanged(object sender, EventArgs e)
        {
            Customer_Total_Paymet_FrmDB();
        }









        string Set_off_Number = "";
        string New_Set_off_Number;
        String setid = "";

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
                    setid = "STF1001";

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

                        setid = "STF" + no;

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
       
        public void Last_SetOFF_Details2()
        {

            #region last Invoice Auto ID Select ---------------------------------

            try
            {

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT TOP 1 InvoiceAutoID FROM Set_Off_Details order by AutoID DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                if (dr.Read() == true)
                {
                    Invoiced_Last_Auto_ID = dr[0].ToString();

                    Conn.Close();
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }



        Double totCash = 0;
        Double totCheque = 0;
        Double totcreditcard = 0;


        //Decimal totset;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region value insert to Agency_Payment_Details table  in DB

                DialogResult result = MessageBox.Show("Do you want to complete the Invoice Payment details..?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                 if (result == DialogResult.Yes)
                 {
                     getCreateStockCode();

                     #region insert DOC code to the db...................................

                     SqlConnection con2 = new SqlConnection(IMS);
                     con2.Open();
                     string add1 = "INSERT INTO Agency_Payment_DOC_Num (Agency_DOC_ID, Status) values('" + lblDocumentNo.Text + "','1')";
                     SqlCommand cmd1 = new SqlCommand(add1, con2);

                     cmd1.ExecuteNonQuery();

                     #endregion

                     for (int i = 0; i <= listView2.Items.Count - 1; i++)
                     {
                         totCash += Double.Parse(listView2.Items[i].SubItems[0].Text);
                         totCheque += Double.Parse(listView2.Items[i].SubItems[1].Text);
                         totcreditcard += Double.Parse(listView2.Items[i].SubItems[2].Text);

                       //  MessageBox.Show(Convert.ToString(totCash));

                         #region Agency paymet details Insert------------------------------------------------

                         SqlConnection con1 = new SqlConnection(IMS);
                         con1.Open();
                         string add = "INSERT INTO Agency_Payment_Details (DOC_Number,Cash_Amount,Cheque_Amount,Cheque_No,Branch,Card_Amount,Card_No )values(@Docu_No,@Cash_Amount,@Cheque_Amount,@Cheque_No,@Branch,@Card_Amount,@Card_No )";
                         SqlCommand cmd = new SqlCommand(add, con1);

                         cmd.Parameters.AddWithValue("Docu_No", lblDocumentNo.Text);
                        // MessageBox.Show(lblDocumentNo.Text);
                         cmd.Parameters.AddWithValue("Cash_Amount", listView2.Items[i].SubItems[0].Text);
                        // MessageBox.Show(listView2.Items[i].SubItems[0].Text);
                         cmd.Parameters.AddWithValue("Cheque_Amount", listView2.Items[i].SubItems[1].Text);
                        // MessageBox.Show(listView2.Items[i].SubItems[1].Text);

                         // cmd.Parameters.AddWithValue("Bank", listView2.Items[i].SubItems[3].Text);
                         cmd.Parameters.AddWithValue("Cheque_No", listView2.Items[i].SubItems[4].Text);
                        // MessageBox.Show(listView2.Items[i].SubItems[4].Text);
                         cmd.Parameters.AddWithValue("Branch", listView2.Items[i].SubItems[6].Text);
                        // MessageBox.Show(listView2.Items[i].SubItems[6].Text);
                         // cmd.Parameters.AddWithValue("Cheque_date", listView2.Items[i].SubItems[7].Text);
                         cmd.Parameters.AddWithValue("Card_Amount", listView2.Items[i].SubItems[2].Text);
                        // MessageBox.Show(listView2.Items[i].SubItems[2].Text);
                         cmd.Parameters.AddWithValue("Card_No", listView2.Items[i].SubItems[5].Text);
                        // MessageBox.Show(listView2.Items[i].SubItems[5].Text);


                         cmd.ExecuteNonQuery();

                         if (con1.State == ConnectionState.Open)
                         {
                             con1.Close();
                         }

                         #endregion

                         #region insert Chq Details==================================================

                         if (Convert.ToDouble(listView2.Items[i].SubItems[1].Text) != 0)
                         {
                             SqlConnection con6 = new SqlConnection(IMS);
                             con6.Open();
                             string InsertCheckPaymetDetails = "INSERT INTO InvoiceCheckDetails(InvoiceID,CkStatus,CkNumber,Bank,Amount,CurrentDate,MentionDate) VALUES(@InvoiceID1,@CkStatus,@CkNumber,@Bank,@Amount,@CurrentDate,@MentionDate)";
                             SqlCommand cmd6 = new SqlCommand(InsertCheckPaymetDetails, con6);

                             cmd6.Parameters.AddWithValue("InvoiceID1", lblDocumentNo.Text);
                             cmd6.Parameters.AddWithValue("CkStatus", "Active");
                             cmd6.Parameters.AddWithValue("CkNumber", listView2.Items[i].SubItems[4].Text);

                           //  MessageBox.Show(listView2.Items[i].SubItems[8].Text);
                             cmd6.Parameters.AddWithValue("Bank", listView2.Items[i].SubItems[8].Text);
                             //cmd6.Parameters.AddWithValue("Bank", BankID);
                             cmd6.Parameters.AddWithValue("Amount", listView2.Items[i].SubItems[1].Text);
                             cmd6.Parameters.AddWithValue("CurrentDate", DateTime.Today.ToShortDateString());
                             cmd6.Parameters.AddWithValue("MentionDate", listView2.Items[i].SubItems[7].Text);

                             cmd6.ExecuteNonQuery();


                             if (con6.State == ConnectionState.Open)
                             {
                                 cmd6.Dispose();
                                 con6.Close();
                             }
                         }

                         #endregion

                     }

                     SqlConnection cnn = new SqlConnection(IMS);
                     cnn.Open();
                     string invoPyDetails = @"insert into InvoicePaymentDetails(InvoiceID, SubTotal, VATpresentage, GrandTotal, PayCash, PayCheck, PayCrditCard,  PAyCredits,  InvoiceDate)
                                            values('" + lblDocumentNo.Text + "','" + label27.Text + "','" + "0.0" + "','" + label27.Text + "','" + totCash + "','" + totCheque + "','" + totcreditcard + "','" + "0.0" + "','" + dateTimePicker2.Text + "')";

                     SqlCommand cmm = new SqlCommand(invoPyDetails, cnn);
                     cmm.ExecuteNonQuery();
                     cnn.Close();
                 }

                #endregion

                 #region value insert to Agency_Payment_Doc_Details table  in DB.........................

                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();

                    for (int a = 0; a <= listView4.Items.Count - 1; a++)
                    {
                        string add2 = "INSERT INTO Agency_Payment_Doc_Details (Docu_No,GRN_No,Date,Paid_amount,UserID,DOC_Status) values(@Docu_No,@GRN_No,@Date,@Paid_amount,@UserID,@DOC_Status)";
                        SqlCommand cmd2 = new SqlCommand(add2, con);
                        cmd2.Parameters.AddWithValue("Docu_No", lblDocumentNo.Text);
                        cmd2.Parameters.AddWithValue("GRN_No", listView4.Items[a].SubItems[0].Text);
                        cmd2.Parameters.AddWithValue("Date", dateTimePicker2.Text);
                        cmd2.Parameters.AddWithValue("Paid_amount", listView4.Items[a].SubItems[4].Text);
                        cmd2.Parameters.AddWithValue("UserID", LgUser.Text);
                        cmd2.Parameters.AddWithValue("DOC_Status", "1");
                        cmd2.ExecuteNonQuery();

                        // update pay balance==================================================
                        SqlConnection con3 = new SqlConnection(IMS);
                        con3.Open();
                        string upadatevalue = "UPDATE Trainee_Registration_DOC_Details SET Remain_amount='" + listView4.Items[a].SubItems[3].Text + "'  where DOC_ID='" + listView4.Items[a].SubItems[0].Text + "'";
                        SqlCommand cmd = new SqlCommand(upadatevalue, con3);


                        cmd.ExecuteNonQuery();
                    }

                

                    Customer_Final_Balance_Update();





                //insert to setoff Table============================================================================


                    #region insert to setoff Table============================================================================


                    Create_SetOff_code();
                    Last_Invoice_Auto_ID_Select();


                    String textid = "";
                    string cashset = "";
                    string chequeset = "";
                    string cardsetof = "";
                    string TopRemain = "";



                    SqlConnection consetof1 = new SqlConnection(IMS);
                    consetof1.Open();
                    string selectsettext = "select DOC_Num from Set_Off_Details";
                    SqlCommand cmmset = new SqlCommand(selectsettext, consetof1);
                    SqlDataReader drset = cmmset.ExecuteReader();
                    if (drset.Read())
                    {

                        //MessageBox.Show("Read");
                        #region Not Empty...........................................................

                        SqlConnection consetof3 = new SqlConnection(IMS);
                        consetof3.Open();
                        string selectsettext3 = "select top 1 AutoID,Remain_Balance from Set_Off_Details order by AutoID desc";
                        SqlCommand cmmset3 = new SqlCommand(selectsettext3, consetof3);
                        SqlDataReader drse3 = cmmset3.ExecuteReader();
                        if (drse3.Read())
                        {
                            TopRemain = drse3[1].ToString();
                        }



                        decimal cashtot = 0;
                        decimal chequetot = 0;
                        decimal cardtota = 0;

                        foreach (ListViewItem lstItem in listView2.Items)
                        {
                            cashtot += decimal.Parse(lstItem.SubItems[0].Text);
                            chequetot += decimal.Parse(lstItem.SubItems[1].Text);
                            cardtota += decimal.Parse(lstItem.SubItems[2].Text);
                        }

                       // totset = cashtot + chequetot + cardtota;



                        decimal totalwithremaing = Convert.ToDecimal(TopRemain) + Convert.ToDecimal(cashtot);




                        SqlConnection consetof2 = new SqlConnection(IMS);
                        consetof2.Open();
                        String insertNoy = @"insert into Set_Off_Details(DOC_Num,DOC_ID, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
                                                            values('" + setid + "','" + lblDocumentNo.Text + "','" + " Invoice Payment " + "','" + Invoiced_Last_Auto_ID + "','" + TopRemain + "','" + cashtot + "','" + "0.00" + "','" + totalwithremaing + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmmset1 = new SqlCommand(insertNoy, consetof2);
                        cmmset1.ExecuteNonQuery();

                        #endregion

                    }

                    else
                    {

                        //MessageBox.Show("Not Read");
                        #region not Read......................................................................

                       


                        decimal cashtot = 0;
                        decimal chequetot = 0;
                        decimal cardtota = 0;

                        foreach (ListViewItem lstItem in listView2.Items)
                        {
                            cashtot += decimal.Parse(lstItem.SubItems[0].Text);
                            chequetot += decimal.Parse(lstItem.SubItems[1].Text);
                            cardtota += decimal.Parse(lstItem.SubItems[2].Text);
                        }


                       // totset = cashtot + chequetot + cardtota;
                        


                        SqlConnection consetof2 = new SqlConnection(IMS);
                        consetof2.Open();
                        String insertNoy = @"insert into Set_Off_Details(DOC_Num,DOC_ID, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
                                              values('" + setid + "','" + lblDocumentNo.Text + "','" + " Invoice Payment " + "','" + Invoiced_Last_Auto_ID + "','" + "0.00" + "','" + cashtot + "','" + "0.00" + "','" + cashtot + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmmset1 = new SqlCommand(insertNoy, consetof2);
                        cmmset1.ExecuteNonQuery();

                        #endregion
                    }


                    #endregion


              //end insert to setoff Table============================================================================
                   
                  MessageBox.Show("Successfully updated Customer credit payment values", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);



                  Report_Form.FRMBatchcreditPayment grnfm = new Report_Form.FRMBatchcreditPayment();
                  grnfm.InvoiceCreditBalance = lblDocumentNo.Text;
                  grnfm.Show();

                    listView1.Items.Clear();
                    listView2.Items.Clear();
                    listView4.Items.Clear();
                    label25.Text = "0.0";
                    label21.Text = "0.0";
                    txtTotalRem.Text = "0.00";
                    txtBank.Enabled = false;
                    txtBranch.Enabled = false;
                    ChecNo.Enabled = false;
                    Card_no.Enabled = false;
                    txtSupID.Clear();
                    Txtsup_name.Clear();
                    TxtCash.Focus();
                    getCreateStockCode();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                #endregion

                    label27.Text = "0.00";
                    label29.Text = "0.00";
                    button1.Enabled = false;


                //=======================================================
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_17", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }




        }

        private void Btn_Save_New_Bank_Click(object sender, EventArgs e)
        {

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();        
            string sql = "SELECT     BankID, BankName FROM Bank_Category";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                String BanKNa = dr[1].ToString();

                if (dr[1].ToString()==txtbankName.Text)
                {
                    MessageBox.Show("Please Enter Another Bank Name");
                    txtbankName.Focus();
                    return;
                }
            }



            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String AddBankCate = "Insert into Bank_Category(BankID, BankName, Status) values('" + lblBanklDauto.Text + "','" + txtbankName.Text + "','1')";
            SqlCommand cmm = new SqlCommand(AddBankCate, cnn);
            cmm.ExecuteNonQuery();

            MessageBox.Show("Insert Successful...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Select_Bank();
            lblBanklDauto.Text = "";
            getCreate_Bank_Catogory_Code();
            txtbankName.Text = "";
            PnlBankName.Visible = false;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Select_Bank();
            lblBanklDauto.Text = "";
            txtbankName.Text = "";
            PnlBankName.Visible = false;
            getCreate_Bank_Catogory_Code();
            txtBank.SelectedIndex = -1;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblDocumentNo_Click(object sender, EventArgs e)
        {
            //Report_Form.FRMBatchcreditPayment grnfm = new Report_Form.FRMBatchcreditPayment();
            //grnfm.InvoiceCreditBalance = "CCP10000019";
            //grnfm.Show();

        }













        
        
        private void button2_Click(object sender, EventArgs e)
        {
        #region insert to setoff Table============================================================================


            Create_SetOff_code();
            Last_Invoice_Auto_ID_Select();


            String textid = "";
            string cashset = "";
            string chequeset = "";
            string cardsetof = "";
            string TopRemain = "";


            
            SqlConnection consetof1 = new SqlConnection(IMS);
            consetof1.Open();
            string selectsettext = "select DOC_Num from Set_Off_Details";
            SqlCommand cmmset = new SqlCommand(selectsettext, consetof1);
            SqlDataReader drset = cmmset.ExecuteReader();
            if (drset.Read())
            {
                #region Not Empty...........................................................

                SqlConnection consetof3 = new SqlConnection(IMS);
                consetof3.Open();
                string selectsettext3 = "select top 1 AutoID,Remain_Balance from Set_Off_Details order by AutoID desc";
                SqlCommand cmmset3 = new SqlCommand(selectsettext3, consetof3);
                SqlDataReader drse3 = cmmset3.ExecuteReader();
                if (drse3.Read())
                {
                    TopRemain = drse3[1].ToString();
                }



                decimal cashtot = 0;
                decimal chequetot = 0;
                decimal cardtota = 0;

                foreach (ListViewItem lstItem in listView2.Items)
                {
                    cashtot += decimal.Parse(lstItem.SubItems[0].Text);
                    chequetot += decimal.Parse(lstItem.SubItems[1].Text);
                    cardtota += decimal.Parse(lstItem.SubItems[2].Text);
                }

                totset = cashtot + chequetot + cardtota;



                decimal totalwithremaing = Convert.ToDecimal(TopRemain) + Convert.ToDecimal(totset);
                



                SqlConnection consetof2 = new SqlConnection(IMS);
                consetof2.Open();
                String insertNoy = @"insert into Set_Off_Details(DOC_Num,DOC_ID, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
                                                            values('" + setid + "','" + lblDocumentNo.Text + "','" + " paid Credit" + "','" + Invoiced_Last_Auto_ID + "','" + TopRemain + "','" + totset + "','" + "0.00" + "','" + totalwithremaing + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "')";
                SqlCommand cmmset1 = new SqlCommand(insertNoy, consetof2);
                cmmset1.ExecuteNonQuery();

                #endregion

            }

            if (!drset.Read())
            {
                #region not Read......................................................................

                

               
                decimal cashtot = 0;
                decimal chequetot = 0;
                decimal cardtota = 0;
                
                foreach (ListViewItem lstItem in listView2.Items)
                {
                    cashtot += decimal.Parse(lstItem.SubItems[0].Text);
                    chequetot += decimal.Parse(lstItem.SubItems[1].Text);
                    cardtota += decimal.Parse(lstItem.SubItems[2].Text);
                }

                totset = cashtot + chequetot + cardtota;
               // MessageBox.Show(totset.ToString());






                SqlConnection consetof2 = new SqlConnection(IMS);
                consetof2.Open();
                String insertNoy = @"insert into Set_Off_Details(DOC_Num,DOC_ID, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
                                                            values('" + setid + "','" + lblDocumentNo.Text + "','" + " paid Credit" + "','" + Invoiced_Last_Auto_ID + "','" + "0.00" + "','" + "0.00" + "','" + "0.00" + "','" + totset + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "')";
                SqlCommand cmmset1 = new SqlCommand(insertNoy, consetof2);
                cmmset1.ExecuteNonQuery();

#endregion
            }
            

//            

            MessageBox.Show("Successfully updated Customer credit payment values", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

        #endregion
        }

    }
}
