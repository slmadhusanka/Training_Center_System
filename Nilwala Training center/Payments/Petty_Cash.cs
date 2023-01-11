using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.IO;

namespace Nilwala_Training_center.Payments
{
    public partial class Petty_Cash : Form
    {
        public Petty_Cash()
        {
            InitializeComponent();

            slectCus(cmbPayer);

            loadbalance();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string addition;

        double Pre_Balance = 0;
        double Current_Bal1 = 0;
        double Current_tot = 0;

        string Invoiced_Last_Auto_ID = "";
        string Auto_ID = "";

        String getdoid;
        String getbalance;

        string Bank_DOC_ID;

        public void Petty_Cash_Code()
        {
            #region petty cash ID generate--------------------------------------------

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select PettyCashID from Petty_Cash";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                PettyCash_ID.Text = "PTY1001";
                lblPeetyIDCredit.Text = "PTY1001";
                lblRecivedPettyCashID.Text = "PTY1001";
                cmd.Dispose();
                dr.Close();
            }
            else
            {
                cmd.Dispose();
                dr.Close();

                // string sql1 = "select MAX(VenderID) from VenderDetails";
                string sql1 = "select TOP 1 PettyCashID from Petty_Cash ORDER BY PettyCashID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string VenNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                    PettyCash_ID.Text = "PTY" + no;
                    lblPeetyIDCredit.Text = "PTY" + no;
                    lblRecivedPettyCashID.Text = "PTY" + no;

                }
                cmd1.Dispose();
                dr7.Close();
            }
            Conn.Close();

            #endregion
        }

        public void Available_Cash_Total()
        {
            #region Calculate the previous remaining balance------------------------------

            try
            {


                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT TOP 1 Remain_Balance FROM Set_Off_Details order by AutoID DESC";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    #region If never set off the DB----------------------------------

                    Pre_Balance = 0.00;

                    cmd.Dispose();
                    dr.Close();

                    string Cal_Current_Cash = @"SELECT SUM(IPD.PayCash) AS TotalCash
                                                FROM InvoicePaymentDetails IPD INNER JOIN Agency_Payment_DOC_Num APDN ON IPD.InvoiceID=APDN.Agency_DOC_ID WHERE APDN.Status='1'";

                    SqlCommand cmdy = new SqlCommand(Cal_Current_Cash, Conn);
                    SqlDataReader dry = cmdy.ExecuteReader();

                    if (dry.Read() == true)//if have a value
                    {
                        if (dry[0].ToString() == "")//value empty....
                        {
                            Current_Bal1 = 0;
                        }

                        if (dry[0].ToString() != "")//value not empty......
                        {
                            Current_Bal1 = Convert.ToDouble(dry[0].ToString());
                        }

                        // MessageBox.Show(Current_Bal1.ToString());
                    }
                    dry.Close();


                    #endregion
                }

                else// if dr have read (alreay set off..............)
                {
                    dr.Close();

                    #region set off -------------------------------

                    //select the frevious total paments (cash)-------------

                    string sqlx = @"SELECT TOP 1 Remain_Balance,InvoiceAutoID,Set_Off_Date,LgUser FROM Set_Off_Details order by AutoID DESC";
                    SqlCommand cmdx = new SqlCommand(sqlx, Conn);
                    SqlDataReader drx = cmdx.ExecuteReader();



                    if (drx.Read() == true)
                    {

                        Pre_Balance = Convert.ToDouble(drx[0].ToString());
                        Auto_ID = drx[1].ToString();

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
                        if (dry[0].ToString() == "")
                        {
                            Current_Bal1 = 0.00;
                            New_Cash_Balance.Text = Current_Bal1.ToString();

                            Current_tot = Current_Bal1 + Pre_Balance;
                            New_Balance.Text = Convert.ToString(Convert.ToDecimal(Current_tot));
                            Pre_Bal.Text = Convert.ToString(Pre_Balance);
                        }

                        if (dry[0].ToString() != "")
                        {
                            Current_Bal1 = Convert.ToDouble(dry[0].ToString());
                            New_Cash_Balance.Text = dry[0].ToString();

                            Current_tot = Current_Bal1 + Pre_Balance;
                            New_Balance.Text = Convert.ToString(Convert.ToDecimal(Current_tot));
                            Pre_Bal.Text = Convert.ToString(Pre_Balance);

                        }


                    }

                    else
                    {
                        Current_Bal1 = 0.00;
                    }

                    dry.Close();



                    #endregion

                }
                Conn.Close();
                dr.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show("This error from current set off Balance selceting method. Please contact yor Administrator.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        public void Set_Off_OR_Not_Message()
        {
            #region Set_Off_OR_Not_Message................................................

            if (Convert.ToDouble(New_Cash_Balance.Text) > 0)
            {
                btnSet_OFF.Enabled = true;

                St_Off_Msg.Text = "Please SET OFF the Balance.";
                St_Off_Msg.ForeColor = System.Drawing.Color.Red;

                Grp_Bx_Main.Enabled = false;
            }

            if (Convert.ToDouble(New_Cash_Balance.Text) == 0)
            {
                btnSet_OFF.Enabled = false;

                St_Off_Msg.Text = "You can Add cash to perrt cash now.";
                St_Off_Msg.ForeColor = System.Drawing.Color.ForestGreen;

                Grp_Bx_Main.Enabled = true;
            }

            #endregion
        }

        public void loadReason(ComboBox Cmname)
        {
            #region load reasonin CMB.....................

            try
            {
               
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String insertReason = "SELECT * FROM Reason_for_pettyCash ";
                SqlCommand cmd1 = new SqlCommand(insertReason, cnn);
                SqlDataReader dr = cmd1.ExecuteReader();

                Cmname.Items.Clear();
                Cmname.Items.Add("<New>");
                while (dr.Read())
                {
                    Cmname.Items.Add(dr[1]);
                }
                cmd1.Dispose();
                dr.Close();

                if (cnn.State == ConnectionState.Open)
                {

                    cnn.Close();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Tab2Desable(string Result)
        {
            #region Eneble_Dissable======================

            try
            {
               

                if (Result == "Des")
                {
                    PetID.Enabled = false;

                    txttab2Desceip.Enabled = false;
                    PaidAmount.Enabled = false;
                    RecievedAmount.Enabled = false;
                    BalanceAmount.Enabled = false;
                    PayedTo.Enabled = false;
                    comboBox2.Enabled = false;
                    RecivedBy.Enabled = false;

                }

                if (Result == "Enb")
                {
                    PetID.Enabled = true;
                    txttab2Desceip.Enabled = true;
                    PaidAmount.Enabled = true;
                    RecievedAmount.Enabled = true;
                    BalanceAmount.Enabled = true;
                    PayedTo.Enabled = true;
                    comboBox2.Enabled = true;
                    RecivedBy.Enabled = true;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void slectCus(ComboBox customer)
        {
            try
            {
                #region load customer in CMB...............

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string CusSelectAll = "select DisplayOn from UserProfile WHERE AtiveDeactive='1' ";
                SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
                SqlDataReader dr = cmd1.ExecuteReader();

                customer.Items.Clear();

                while (dr.Read())
                {
                    customer.Items.Add(dr[0]);

                }
                cmd1.Dispose();
                dr.Close();

                if (con1.State == ConnectionState.Open)
                {

                    con1.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void SET_OFF_Details_Show_or_Not()
        {
            #region show all details from set_off........................................

            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;

            Pre_Bal.Visible = false;
            New_Cash_Balance.Visible = false;


            St_Off_Msg.Visible = false;
            btnSet_OFF.Visible = false;

            if (Rbt_Frm_Main_cash.Checked == true)
            {
                label35.Visible = true;
                label36.Visible = true;
                label37.Visible = true;
                label38.Visible = true;

                Pre_Bal.Visible = true;
                New_Cash_Balance.Visible = true;

                St_Off_Msg.Visible = true;
                btnSet_OFF.Visible = true;
            }



            #endregion
        }

        public void getCreateReasoncode()
        {
            #region New getCreateReasoncode...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select PettyID from Reason_for_pettyCash";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    lblRealD.Text = "PCI1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 PettyID FROM Reason_for_pettyCash order by PettyID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();



                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        lblRealD.Text = "PCI" + no;

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

        public void calc_total_Amount()
        {
            #region Calculate the total amount------------------------------------

            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listView1.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[4].Text), 2);
            }
            TotAmount.Text = Convert.ToString(gtotal);

            #endregion
        }

        public void clear()
        {
            #region textbox,listview,radio =clear/false/...............................

            txtpaidamount.Text = "0.0";
            //txtpaidamount.Enabled = false;
            // txtreceived.Text = "0.0";
            // txtRicever.Text = "";
            cmbPayer.Items.Clear();
            cmbReson.Items.Clear();
            //cmbReson.Enabled = false;
            // cmbPayer.Enabled = false;
            // button1.Enabled = false;
            // rbtPaidMoney.Checked = false;
            //  rntRecrviedMoney.Checked = false;
            txtDiscrip.Text = "";
            //txtDiscrip.Enabled = false;
            // txtreceived.Enabled = false;
            // txtRicever.Enabled = false;


            #endregion
        }

        public void ListView1Item_Count()
        {
            #region if  view empty, apply button dissable

            if (listView1.Items.Count == 0)
            {
                Btn_Save.Enabled = false;

            }

            else
                Btn_Save.Enabled = true;

            #endregion
        }

        public void loadbalance()
        {
            try
            {
                #region loadbalance in to lable
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "select Current_Bal from PettyCash_Balance";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    Lbl_Available_Balance.Text = "0.00";

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    SqlConnection Conn1 = new SqlConnection(IMS);
                    Conn1.Open();
                    string sql1 = " SELECT TOP 1 Current_Bal FROM PettyCash_Balance order by [Bal_ID] DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn1);
                    SqlDataReader dr7 = cmd1.ExecuteReader();
                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        Lbl_Available_Balance.Text = no;

                    }
                    cmd1.Dispose();
                    dr7.Close();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        String AutoId;
        string Set_off_Number;
        string New_Set_off_Number;

        public void loadsetOff()
        {
            #region load setoff value................

            SqlConnection cnnoff = new SqlConnection(IMS);
            cnnoff.Open();
            String setoffvaluetop = "select top 1 DOC_Num from Set_Off_Details  order by AutoID desc";
            SqlCommand cmmset = new SqlCommand(setoffvaluetop, cnnoff);
            SqlDataReader drset = cmmset.ExecuteReader();
            while (drset.Read())
            {
                Set_off_Number = drset[0].ToString();

                string OrderNumOnly = Set_off_Number.Substring(3);

                Set_off_Number = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                New_Set_off_Number = "STF" + Set_off_Number;
            }


            #endregion
        }

        public void TotalPayamount()
        {
            #region Total Pay amount
            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listView2.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[6].Text), 2);
            }
            Lbl_Tot_list.Text = Convert.ToString(gtotal);

            Remaining_Balance.Text = Convert.ToString(Convert.ToDouble(New_Balance.Text) - Convert.ToDouble(Lbl_Tot_list.Text));

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

                cmbBankNameCredit.Items.Clear();

                while (dr.Read() == true)
                {
                    cmbBankNameCredit.Items.Add(dr[0].ToString());
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

        public void Current_Bal()
        {
            if (RecievedAmount.Text == "")
            {
                return;
            }

            BalanceAmount.Text = Convert.ToString(Convert.ToDouble(PaidAmount.Text) - Convert.ToDouble(RecievedAmount.Text));
        }

        public void Select_BankID()
        {
            try
            {
                #region select the bank---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT BankID FROM Bank_Category WHERE BankName='" + cmbBankNameCredit.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                //txtBank.Items.Clear();

                if (dr.Read() == true)
                {
                    txtBankIdCredit.Text = dr[0].ToString();
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

        public void clearTab2()
        {
            DateFilter.ResetText();
            //dateTimePicker2_tab2.Value = dateTimePicker2_tab2.Value.Date;
            // txtBankname.Clear();
            txttab2Desceip.Clear();
            // txttab2ChequeNo.Clear();
            RecievedAmount.Text = "0.0";
        }

        private void Petty_Cash_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            Petty_Cash_Code();

            Available_Cash_Total();
            Set_Off_OR_Not_Message();

            loadReason(comboBox2);
            Tab2Desable("Des");

            slectCus(RecivedBy);
            slectCus(PayedTo);

            SET_OFF_Details_Show_or_Not();

        }

        private void cmbReson_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtDiscrip.Focus();
            }
        }

        private void cmbReson_MouseClick(object sender, MouseEventArgs e)
        {
            loadReason(cmbReson);
        }

        private void cmbReson_SelectedIndexChanged(object sender, EventArgs e)
        {
            // visibal label and add new reason-----------------------------------------------------------------

            if (cmbReson.SelectedItem.ToString() == "")
            {
                return;
            }

            
            if (cmbReson.SelectedItem.ToString() != "<New>")
            {
                PnlVendorSerch.Visible = false;
            }
            if (cmbReson.SelectedItem.ToString() != "")
            {
                txtDiscrip.Focus();
            }

            if (cmbReson.SelectedItem.ToString() == "<New>")
            {

                PnlVendorSerch.Visible = true;
               
                textBox1.Focus();
     

            }
            //----------------------------------------------------------------------------------------------------
        }

        private void PnlVendorSerch_MouseUp(object sender, MouseEventArgs e)
        {
            PnlVendorSerch.Visible = false;
        }

        String textmulti;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult addcheck = MessageBox.Show("Are you sure about this Reason", "New Reason Update", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (addcheck == DialogResult.OK)
                {
                    #region insert new reason in DB

                    getCreateReasoncode();

                    SqlConnection cnn = new SqlConnection(IMS);
                    cnn.Open();
                    string insernewid = "insert into [Reason_for_pettyCash]([PettyID],[Reason],[Active_Deactive])values('" + lblRealD.Text + "','" + textBox1.Text + "','" + 1 + "') ";
                    SqlCommand cmm = new SqlCommand(insernewid, cnn);
                    cmm.ExecuteNonQuery();

                    textmulti = textBox1.Text;
                    PnlVendorSerch.Visible = false;


                    cmbReson.Items.Clear();
                    loadReason(cmbReson);
                    cmbReson.DroppedDown = true;
                    #endregion
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button4.Focus();
            }
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {

            PnlVendorSerch.Visible = false;


            cmbReson.Items.Clear();
            loadReason(cmbReson);
        }

        private void txtDiscrip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmbPayer.DroppedDown = true;
                cmbPayer.Focus();
            }
        }

        private void cmbPayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtpaidamount.Focus();
            }
        }

        string user_Name = "";

        private void cmbPayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                #region user load in CMB ...............................................

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                String LaodCus = "select UserCode from UserProfile where UserCode='" + cmbPayer.Text + "' ";
                SqlCommand cmd = new SqlCommand(LaodCus, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                if (cmbPayer.SelectedIndex.ToString() != "")
                {
                    txtpaidamount.Focus();
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void txtpaidamount_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                button1.Focus();
            }
        }

        private void txtpaidamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region type only dcimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            #endregion
        }

        private void txtpaidamount_Leave(object sender, EventArgs e)
        {
            if (txtpaidamount.Text == "")
            {
                txtpaidamount.Text = "0.0";
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region reduce,Addition,check empty details--------------------------------------------------------------------

                //string min = (Double.Parse(Lbl_Available_Balance.Text) - Double.Parse(txtpaidamount.Text)).ToString();
                //Lbl_Available_Balance.Text = min;

                //check empty details----------------------------------------------------------------------------------

                if (cmbPayer.Text == "")
                {
                    MessageBox.Show("Please Fill Payer Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPayer.Focus();
                    return;
                }
                if (((Double.Parse(txtpaidamount.Text)) <= 0.0))
                {
                    MessageBox.Show("Please Fill Paid Amount Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpaidamount.Focus();
                    return;
                }
                if (cmbReson.Text == "")
                {
                    MessageBox.Show("Please Fill Reason Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbReson.Focus();
                    return;
                }
                if (txtDiscrip.Text == "")
                {
                    MessageBox.Show("Please Fill Discription Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDiscrip.Focus();
                    return;
                }

                //-----------------------------------------------------------------------------------------------------

                #endregion----------------------------------

                #region insert data in to list view
                ListViewItem li;

                li = new ListViewItem(dateTimePicker1.Text);
                li.SubItems.Add(cmbReson.Text);

                li.SubItems.Add(txtDiscrip.Text);
                //li.SubItems.Add(txtRicever.Text);
                li.SubItems.Add(cmbPayer.Text);
                //li.SubItems.Add(txtreceived.Text);
                li.SubItems.Add(txtpaidamount.Text);
                // li.SubItems.Add(Lbl_Available_Balance.Text);

                listView1.Items.Add(li);

                calc_total_Amount();

                clear();

                //  loadReason(cmbReson);
                slectCus(cmbPayer);

                ListView1Item_Count();


                Btn_Save.Enabled = true;
                Btn_Cancel.Enabled = true;

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                #region reduce,Addition,check empty details--------------------------------------------------------------------

                //string min = (Double.Parse(Lbl_Available_Balance.Text) - Double.Parse(txtpaidamount.Text)).ToString();
                //Lbl_Available_Balance.Text = min;

                //check empty details----------------------------------------------------------------------------------

                if (cmbPayer.Text == "")
                {
                    MessageBox.Show("Please Fill Payer Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPayer.Focus();
                    return;
                }
                if (((Double.Parse(txtpaidamount.Text)) <= 0.0))
                {
                    MessageBox.Show("Please Fill Paid Amount Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpaidamount.Focus();
                    return;
                }
                if (cmbReson.Text == "")
                {
                    MessageBox.Show("Please Fill Reason Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbReson.Focus();
                    return;
                }
                if (txtDiscrip.Text == "")
                {
                    MessageBox.Show("Please Fill Discription Details...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDiscrip.Focus();
                    return;
                }

                //-----------------------------------------------------------------------------------------------------

                #endregion----------------------------------

                #region insert data in to list view
                ListViewItem li;

                li = new ListViewItem(dateTimePicker1.Text);
                li.SubItems.Add(cmbReson.Text);

                li.SubItems.Add(txtDiscrip.Text);
                //li.SubItems.Add(txtRicever.Text);
                li.SubItems.Add(cmbPayer.Text);
                //li.SubItems.Add(txtreceived.Text);
                li.SubItems.Add(txtpaidamount.Text);
                // li.SubItems.Add(Lbl_Available_Balance.Text);

                listView1.Items.Add(li);

                calc_total_Amount();

                clear();

                //  loadReason(cmbReson);
                slectCus(cmbPayer);

                ListView1Item_Count();


                Btn_Save.Enabled = true;
                Btn_Cancel.Enabled = true;

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            #region remove row and reset available balance

            //ListViewItem item = listView1.SelectedItems[0];
            //if (item.SubItems[5].Text != "")
            //{
            //    String add = (Double.Parse(Lbl_Available_Balance.Text) - Double.Parse(item.SubItems[5].Text)).ToString();
            //    Lbl_Available_Balance.Text = add;

            //}

            //if (item.SubItems[6].Text != "")
            //{
            //    String add2 = (Double.Parse(Lbl_Available_Balance.Text) + Double.Parse(item.SubItems[6].Text)).ToString();
            //    Lbl_Available_Balance.Text = add2;

            //}


            #endregion

            listView1.SelectedItems[0].Remove();

            ListView1Item_Count();

            calc_total_Amount();
        }

        String TopBal = "";
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {

                #region data insert to DB----------------------------------------------



                DialogResult dgResult = MessageBox.Show("Are you sure you want to complete the details?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dgResult == DialogResult.Yes)
                {
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {

                        SqlConnection conz = new SqlConnection(IMS);
                        conz.Open();
                        String selectPetid = "select top 1 Balance from Petty_Cash order by peety_ID desc";
                        SqlCommand cmm1 = new SqlCommand(selectPetid, conz);
                        SqlDataReader dr1 = cmm1.ExecuteReader();
                        if (dr1.Read())
                        {
                            TopBal = dr1[0].ToString();
                        }

                        Decimal sum2 = (Convert.ToDecimal(TopBal)) - Convert.ToDecimal(listView1.Items[i].SubItems[4].Text);




                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        string insert1 = @"INSERT INTO [Petty_Cash] ([ptDate],[PettyCashID],[Reason] ,[Discription],[Receiver] ,[Payer],[Received_Amount] ,[Paid_amount] ,[Balance],[UserID]) VALUES(@Datept,@PettyCashID,@Reason,@Discription,@Receiver,@Payer,@Received_Amount,@Paid_amount,@Balance,@User1)";
                        SqlCommand cmm = new SqlCommand(insert1, cnn);

                        cmm.Parameters.AddWithValue("Datept", Convert.ToDateTime(listView1.Items[i].SubItems[0].Text));
                        cmm.Parameters.AddWithValue("PettyCashID", PettyCash_ID.Text);
                        cmm.Parameters.AddWithValue("Reason", listView1.Items[i].SubItems[1].Text);
                        cmm.Parameters.AddWithValue("Discription", listView1.Items[i].SubItems[2].Text);
                        cmm.Parameters.AddWithValue("Receiver", '-');
                        cmm.Parameters.AddWithValue("Payer", listView1.Items[i].SubItems[3].Text);
                        cmm.Parameters.AddWithValue("Received_Amount", '0');
                        cmm.Parameters.AddWithValue("Paid_amount", listView1.Items[i].SubItems[4].Text);
                       // cmm.Parameters.AddWithValue("Balance", listView1.Items[i].SubItems[4].Text);
                        cmm.Parameters.AddWithValue("Balance", sum2);
                        cmm.Parameters.AddWithValue("User1", LgUser.Text);

                        cmm.ExecuteNonQuery();

                        if (cnn.State == ConnectionState.Open)
                        {
                            cnn.Close();
                        }

                    }

                    #region Select the petty cash last Balance--------------------------------------------

                    SqlConnection Conn = new SqlConnection(IMS);
                    Conn.Open();


                    //=====================================================================================================================
                    string sql = "select Current_Bal from PettyCash_Balance";
                    SqlCommand cmd = new SqlCommand(sql, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //=====================================================================================================================
                    if (!dr.Read())
                    {

                        decimal Now_balance_Payment = -1 * Convert.ToDecimal(TotAmount.Text);

                        SqlConnection cnnz = new SqlConnection(IMS);
                        cnnz.Open();
                        string insert1z = @"INSERT INTO PettyCash_Balance (Petty_Cash_ID, Petty_Cash_Auto_ID, Update_Status, Amount, Current_Bal,Current_Date) VALUES('" + PettyCash_ID.Text + "','00','Credited','" + TotAmount.Text + "','" + Now_balance_Payment + "','"+DateTime.Now.ToShortDateString()+"')";
                        SqlCommand cmmz = new SqlCommand(insert1z, cnnz);

                        cmmz.ExecuteNonQuery();

                    }
                    else
                    {


                        cmd.Dispose();
                        dr.Close();

                        string sql1 = "select TOP 1 Current_Bal from PettyCash_Balance ORDER BY Bal_ID DESC";
                        SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                        SqlDataReader dr7 = cmd1.ExecuteReader();

                        double pre_balance = 0;
                        double current_Bal = 0;

                        if (dr7.Read() == true)
                        {
                            pre_balance = Convert.ToDouble(dr7[0].ToString());
                        }


                        current_Bal = pre_balance - Convert.ToDouble(TotAmount.Text);



                        cmd1.Dispose();
                        dr7.Close();

                        SqlConnection cnnz = new SqlConnection(IMS);
                        cnnz.Open();
                        string insert1z = @"INSERT INTO PettyCash_Balance (Petty_Cash_ID, Petty_Cash_Auto_ID, Update_Status, Amount, Current_Bal,[Current_Date]) VALUES('" + PettyCash_ID.Text + "','00','Credited','" + TotAmount.Text + "','" + current_Bal + "','" + DateTime.Now.ToShortDateString() + "')";
                        SqlCommand cmmz = new SqlCommand(insert1z, cnnz);
                        cmmz.ExecuteNonQuery();



                        if (cnnz.State == ConnectionState.Open)
                        {
                            cnnz.Close();

                        }

                    }
                    Conn.Close();

                    #endregion



                    MessageBox.Show("Insert Successfull...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    listView1.Items.Clear();

                    TotAmount.Text = "0.00";
                    calc_total_Amount();
                    loadbalance();

                    // Btn_Save.Enabled = false;

                    ListView1Item_Count();

                    getCreateReasoncode();

                }
                #endregion
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_addTab2_Click(object sender, EventArgs e)
        {

            try
            {
                #region filter petty cash-----------------------------------------------------------

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string selectvalue = @" SELECT     ptDate, PettyCashID, Reason, Discription, Payer, Paid_amount, peety_ID,Received_Amount,Receiver
                                        FROM         Petty_Cash
                                        WHERE     (ptDate = '" + DateFilter.Text + "')";

                SqlCommand cmd = new SqlCommand(selectvalue, con1);
                SqlDataReader sd = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ListV_Recieve.Items.Clear();

                while (sd.Read() == true)
                {

                    ListViewItem li;
                    li = new ListViewItem(sd[0].ToString());
                    li.SubItems.Add(sd[1].ToString());
                    li.SubItems.Add(sd[2].ToString());
                    li.SubItems.Add(sd[3].ToString());
                    li.SubItems.Add(sd[4].ToString());
                    li.SubItems.Add(sd[5].ToString());
                    li.SubItems.Add(sd[6].ToString());
                    li.SubItems.Add(sd[7].ToString());
                    li.SubItems.Add(sd[8].ToString());

                    ListV_Recieve.Items.Add(li);


                }



                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void btn_addTab2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    #region filter petty cash-----------------------------------------------------------

                    SqlConnection con1 = new SqlConnection(IMS);
                    con1.Open();

                    string selectvalue = @" SELECT     ptDate, PettyCashID, Reason, Discription, Payer, Paid_amount, peety_ID,Received_Amount,Receiver
                                        FROM         Petty_Cash
                                        WHERE     (ptDate = '" + DateFilter.Text + "')";

                    SqlCommand cmd = new SqlCommand(selectvalue, con1);
                    SqlDataReader sd = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    ListV_Recieve.Items.Clear();

                    while (sd.Read() == true)
                    {

                        ListViewItem li;
                        li = new ListViewItem(sd[0].ToString());
                        li.SubItems.Add(sd[1].ToString());
                        li.SubItems.Add(sd[2].ToString());
                        li.SubItems.Add(sd[3].ToString());
                        li.SubItems.Add(sd[4].ToString());
                        li.SubItems.Add(sd[5].ToString());
                        li.SubItems.Add(sd[6].ToString());
                        li.SubItems.Add(sd[7].ToString());
                        li.SubItems.Add(sd[8].ToString());

                        ListV_Recieve.Items.Add(li);


                    }



                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void ListV_Recieve_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    ListViewItem item = ListV_Recieve.SelectedItems[0];

                    //ListViewItem li;
                    PetID.Text = item.SubItems[1].Text;
                    comboBox2.Text = item.SubItems[2].Text;
                    txttab2Desceip.Text = item.SubItems[3].Text;
                    PayedTo.Text = item.SubItems[4].Text;
                    PaidAmount.Text = item.SubItems[5].Text;
                    Auto_Pet_ID.Text = item.SubItems[6].Text;
                    RecievedAmount.Text = item.SubItems[7].Text;
                    RecivedBy.Text = item.SubItems[8].Text;

                    slectCus(RecivedBy);
                    slectCus(PayedTo);


                    if (btnTab2save.Enabled == false)
                    {
                        btnTab2save.Enabled = true;
                    }

                    Current_Bal();

                    if (ListV_Recieve.Items.Count != 0)
                    {
                        Tab2Desable("Enb");

                    }

                    if (ListV_Recieve.Items.Count == 0)
                    {
                        Tab2Desable("Des");
                        btnTab2save.Enabled = false;
                    }
                    PayedTo.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void ListV_Recieve_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadReason(comboBox2);

            ListViewItem item = ListV_Recieve.SelectedItems[0];

            //ListViewItem li;
            PetID.Text = item.SubItems[1].Text;
            comboBox2.Text = item.SubItems[2].Text;
            txttab2Desceip.Text = item.SubItems[3].Text;
            PayedTo.Text = item.SubItems[4].Text;
            PaidAmount.Text = item.SubItems[5].Text;
            Auto_Pet_ID.Text = item.SubItems[6].Text;
            RecievedAmount.Text = item.SubItems[7].Text;
            RecivedBy.Text = item.SubItems[8].Text;

            slectCus(RecivedBy);
            slectCus(PayedTo);


            if (btnTab2save.Enabled == false)
            {
                btnTab2save.Enabled = true;
            }

            Current_Bal();

            if (ListV_Recieve.Items.Count != 0)
            {
                Tab2Desable("Enb");

            }

            if (ListV_Recieve.Items.Count == 0)
            {
                Tab2Desable("Des");
                btnTab2save.Enabled = false;
            }
            txttab2Desceip.Focus();
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
           // loadReason(comboBox2);
        }

        private void txttab2Desceip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                PayedTo.DroppedDown = true;
                PayedTo.Focus();
            }
        }

        private void PayedTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                RecivedBy.DroppedDown = true;
                RecivedBy.Focus();
            }
        }

        private void PayedTo_MouseClick(object sender, MouseEventArgs e)
        {
            slectCus(PayedTo);
        }

        private void RecivedBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (RecivedBy.SelectedIndex >= 0)
                {
                    RecievedAmount.Focus();
                }
                //RecievedAmount.Focus();


            }
        }

        private void RecivedBy_MouseClick(object sender, MouseEventArgs e)
        {
            slectCus(RecivedBy);
        }

        private void RecievedAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnTab2save.Focus();
            }
        }

        private void RecievedAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region type only dcimal
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            #endregion
        }

        private void RecievedAmount_Leave(object sender, EventArgs e)
        {
            if (RecievedAmount.Text == "")
            {
                RecievedAmount.Text = "0.0";
            }

            if (RecieveAmount.Text != "")
            {

                if ((Convert.ToDouble(PaidAmount.Text) - Convert.ToDouble(RecievedAmount.Text)) < 0)
                {


                    MessageBox.Show("Amount is greater than the paid amount. Please enter valid amount.", "Error Data", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    RecievedAmount.Text = "0.00";
                    RecievedAmount.Focus();
                    return;
                }

                Current_Bal();
            }

        }

        private void RecievedAmount_TextChanged(object sender, EventArgs e)
        {
            if (RecieveAmount.Text.Trim() == "")
            {
                // MessageBox.Show("Err");
                return;
            }


            //decimal x;
            //bool xyz=decimal.TryParse(RecieveAmount.Text,out x);



            Current_Bal();
        }

        private void btnTab2save_Click(object sender, EventArgs e)
        {

            #region data oftab2 insert to DB----------------------------------------------

            DialogResult checkadd = MessageBox.Show("Are you sure save data?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (checkadd == DialogResult.OK)
            {
                try
                {

                    //        for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    //        {

                    //            SqlConnection cnntab2 = new SqlConnection(IMS);
                    //            cnntab2.Open();
                    //            String addvaluetabtform = ("INSERT INTO <TABLENAME>(Date,BankName,Description,ChequeNo,Amount)Values(@Date,@BankName,@Description,@ChequeNo,@Amount)  ");
                    //            SqlCommand cmd = new SqlCommand(addvaluetabtform, cnntab2);

                    //            cmd.Parameters.AddWithValue("Date", Convert.ToDateTime(listView1.Items[i].SubItems[0].Text));
                    //            cmd.Parameters.AddWithValue("BankName", listView1.Items[i].SubItems[1].Text);
                    //            cmd.Parameters.AddWithValue("Description", listView1.Items[i].SubItems[1].Text);
                    //            cmd.Parameters.AddWithValue("ChequeNo", listView1.Items[i].SubItems[1].Text);
                    //            cmd.Parameters.AddWithValue("Amount", listView1.Items[i].SubItems[1].Text);

                    //            cmd.ExecuteNonQuery();


                    //            if (cnntab2.State == ConnectionState.Open)
                    //            {
                    //                cnntab2.Close();
                    //            }

                    //        }
                    //        listView2.Items.Clear();
                    //        btnTab2save.Enabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                //}
                //else
                //{
                //    return;
                //}

                if (RecivedBy.Text == "")
                {
                    MessageBox.Show("Please Select the Receiver's Name..", "Error Select Receiver", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RecivedBy.Focus();
                    return;
                }

                if (RecivedBy.Text == "")
                {
                    MessageBox.Show("Please Select the Receiver's Name..", "Error Select Receiver", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RecivedBy.Focus();
                    return;
                }
                //PayedTo
                try
                {
                    String TopBal="";
                    Decimal TopBalTot = 0;

                    SqlConnection conz = new SqlConnection(IMS);
                    conz.Open();
                    String selectPetid = "select top 1 Balance from Petty_Cash order by peety_ID desc";
                    SqlCommand cmm1 = new SqlCommand(selectPetid, conz);
                    SqlDataReader dr1 = cmm1.ExecuteReader();
                    if(dr1.Read())
                    {
                         TopBal = dr1[0].ToString();
                    }

                    //                                      PettyCashID, ptDate, Reason, Discription, Receiver, Payer, Received_Amount, Paid_amount, Balance, UserID

                    TopBalTot =(Convert.ToDecimal(TopBal)) + (Convert.ToDecimal(RecievedAmount.Text));
                    String NewDiscri=txttab2Desceip.Text +"   - "+PetID.Text;

                   // MessageBox.Show(NewDiscri);

                   // MessageBox.Show(TopBalTot.ToString());

                    SqlConnection conx = new SqlConnection(IMS);
                    conx.Open();
                    string InsertUpdate = "insert into Petty_Cash values ('" + lblRecivedPettyCashID.Text + "','" + DateTime.Now.ToString() + "','" + comboBox2.Text + "','" + NewDiscri + "','" + RecivedBy.Text + "','" + PayedTo.Text + "','" + RecievedAmount.Text + "','" + "0.0" + "','" + TopBalTot + "','" + LgUser.Text + "')";
                    SqlCommand cmm = new SqlCommand(InsertUpdate,conx);
                    cmm.ExecuteNonQuery();




                    //SqlConnection conz = new SqlConnection(IMS);
                    //conz.Open();

                    //string UpdateDate = @"UPDATE  Petty_Cash SET Reason='" + comboBox2.Text + "',Discription='" + txttab2Desceip.Text + "', Payer='" + PayedTo.Text + "',Receiver='" + RecivedBy.Text + "',  Received_Amount='" + RecievedAmount.Text + "', Balance='" + BalanceAmount.Text + "' WHERE (PettyCashID='" + PetID.Text + "' AND peety_ID='" + Auto_Pet_ID.Text + "')";
                    //SqlCommand cmd1 = new SqlCommand(UpdateDate, conz);
                    //cmd1.ExecuteNonQuery();



                    if (conz.State == ConnectionState.Open)
                    {
                        conz.Close();
                    }

                    //==============================================
                    SqlConnection Conn = new SqlConnection(IMS);
                    Conn.Open();

                    string sql1 = "select TOP 1 Current_Bal from PettyCash_Balance ORDER BY Bal_ID DESC";
                    SqlCommand cmd1x = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1x.ExecuteReader();

                    double pre_balance = 0;
                    double current_Bal = 0;

                    if (dr7.Read() == true)
                    {
                        pre_balance = Convert.ToDouble(dr7[0].ToString());
                    }


                    current_Bal = pre_balance + Convert.ToDouble(RecievedAmount.Text);

                    SqlConnection cnnz = new SqlConnection(IMS);
                    cnnz.Open();
                    string insert1z = @"INSERT INTO PettyCash_Balance (Petty_Cash_ID, Petty_Cash_Auto_ID, Update_Status, Amount, Current_Bal,[Current_Date]) VALUES('" + PetID.Text + "','" + Auto_Pet_ID.Text + "','Updated','" + RecievedAmount.Text + "','" + current_Bal + "','"+DateTime.Now.ToShortDateString()+"')";
                    SqlCommand cmmz = new SqlCommand(insert1z, cnnz);
                    cmmz.ExecuteNonQuery();



                    if (cnnz.State == ConnectionState.Open)
                    {
                        cnnz.Close();

                    }



                    MessageBox.Show("Update successfully!..", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ListV_Recieve.Items.Clear();

                    clearTab2();

                    Tab2Desable("Des");

                    btnTab2save.Enabled = false;

                    //--------------------------------------
                    Petty_Cash_Code();

                    loadReason(comboBox2);
                    slectCus(RecivedBy);
                    slectCus(PayedTo);

                    calc_total_Amount();
                    loadbalance();
                    PetID.Text = "PetID";
                    BalanceAmount.Text = "0.0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }

            #endregion

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PetID.Text = "ID";
            comboBox2.SelectedIndex = -1;
            txttab2Desceip.Text = "";
            PayedTo.SelectedIndex = -1;
            PaidAmount.Text = "0.00";
            RecivedBy.SelectedIndex = -1;
            RecievedAmount.Text = "0.0";
            BalanceAmount.Text = "0.00";
            Auto_Pet_ID.Text = "";
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Rbt_frm_Chk.Focus();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            Petty_Cash_Code();

            Available_Cash_Total();
            Set_Off_OR_Not_Message();

            loadReason(comboBox2);

            Tab2Desable("Des");

            slectCus(RecivedBy);
            slectCus(PayedTo);

            SET_OFF_Details_Show_or_Not();

            calc_total_Amount();
            loadbalance();

            if (Rbt_Frm_Main_cash.Checked != true)
            {
                Grp_Bx_Main.Enabled = false;
            }

            if (Rbt_frm_Chk.Checked != true)
            {
                Grp_bx_Cheque.Enabled = false;
            }

        }

        private void New_Cash_Balance_TextChanged(object sender, EventArgs e)
        {
            Set_Off_OR_Not_Message();
        }

        private void btnSet_OFF_Click(object sender, EventArgs e)
        {

            //set off
            SET_OFF petych = new SET_OFF();

            petych.LgDisplayName.Text = LgDisplayName.Text;
            petych.LgUser.Text = LgUser.Text;

            petych.Show();

            SET_OFF_Details_Show_or_Not();

            Rbt_Frm_Main_cash.Checked = false;
        }

        private void Rbt_frm_Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbt_frm_Chk.Checked == true)
            {
                Grp_bx_Cheque.Enabled = true;
                Grp_Bx_Main.Enabled = false;
            }

            if (Rbt_frm_Chk.Checked == false)
            {
                Grp_bx_Cheque.Enabled = false;
                Grp_Bx_Main.Enabled = true;
            }

            Petty_Cash_Code();
            listView2.Items.Clear();

            SET_OFF_Details_Show_or_Not();
            txtAmount.Text = "0.0";
        }

        private void Rbt_frm_Chk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmbBankNameCredit.Focus();
            }
        }

        private void Rbt_Frm_Main_cash_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbt_Frm_Main_cash.Checked == true)
            {
                Grp_bx_Cheque.Enabled = false;
                Grp_Bx_Main.Enabled = true;
            }

            if (Rbt_Frm_Main_cash.Checked == false)
            {
                Grp_bx_Cheque.Enabled = true;
                Grp_Bx_Main.Enabled = false;
            }

            Petty_Cash_Code();
            listView2.Items.Clear();

            Available_Cash_Total();
            Set_Off_OR_Not_Message();

            SET_OFF_Details_Show_or_Not();
        }

        private void Rbt_Frm_Main_cash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Main_cash_Amount.Focus();
            }
        }

        private void cmbBankNameCredit_Click(object sender, EventArgs e)
        {
            Select_Bank();

            //Select_BankID();
        }

        private void cmbBankNameCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                dtpCurrentDate.Focus();
            }
        }

        private void cmbBankNameCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_BankID();
        }

        private void dtpCurrentDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmbAcc_NameCredit.Focus();
            }
        }

        private void cmbAcc_NameCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                dtpMentionDate.Focus();
            }
        }

        private void dtpMentionDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtAmount.Focus();
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCheque_No.Focus();
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                txtAmount.Text = "0.0";
            }
        }

        private void txtCheque_No_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                dtpChangeDate.Focus();
            }
        }

        private void txtCheque_No_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dtpChangeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button2.Focus();
            }
        }

        private void Main_cash_Amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button2.Focus();
            }
        }

        private void Main_cash_Amount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //####### this Code also in the Enter key..If u did some modifications, please do it there also....

                if (Rbt_frm_Chk.Checked == true)
                {

                    #region check cheque details fields---------------------------------------------------
                    if (cmbAcc_NameCredit.Text == "")
                    {
                        MessageBox.Show("Please select Account Number", "Message");
                        cmbAcc_NameCredit.Focus();
                        return;
                    }
                    if (cmbBankNameCredit.Text == "")
                    {
                        MessageBox.Show("Please select Bank Name", "Message");
                        cmbBankNameCredit.Focus();
                        return;
                    }

                    if (txtCheque_No.Text == "")
                    {
                        MessageBox.Show("Please Enter Cheque Number", "Message");
                        txtCheque_No.Focus();
                        return;
                    }
                    if (Double.Parse(txtAmount.Text) <= (0.0))
                    {
                        MessageBox.Show("Please Enter Amount", "Message");
                        txtAmount.Focus();
                        return;
                    }
                    #endregion

                    #region value pass to list view from textbox---------------------

                    ListViewItem item = new ListViewItem(cmbBankNameCredit.Text);

                    item.SubItems.Add(cmbAcc_NameCredit.Text);
                    item.SubItems.Add(txtCheque_No.Text);
                    item.SubItems.Add(dtpCurrentDate.Text);
                    item.SubItems.Add(dtpMentionDate.Text);
                    item.SubItems.Add(dtpChangeDate.Text);
                    item.SubItems.Add(txtAmount.Text);
                    item.SubItems.Add(txtBankIdCredit.Text);

                    listView2.Items.Add(item);


                    cmbBankNameCredit.SelectedIndex = -1;
                    cmbAcc_NameCredit.SelectedIndex = -1;
                    txtAmount.Text = "0.0";
                    txtCheque_No.Text = "";
                    dtpMentionDate.ResetText();
                    dtpChangeDate.ResetText();
                    dtpCurrentDate.ResetText();
                    btnSavecredit.Enabled = true;
                    #endregion


                }

                if (Rbt_Frm_Main_cash.Checked == true)
                {
                    #region Main cash empty=================================================================================

                    if (Main_cash_Amount.Text == "" || Convert.ToDouble(Main_cash_Amount.Text) == 0)
                    {
                        MessageBox.Show("Please insert an amount", "Error Main cash", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Main_cash_Amount.Focus();
                        return;
                    }
                    if (Convert.ToDouble(Main_cash_Amount.Text) > Convert.ToDouble(New_Balance.Text))
                    {
                        MessageBox.Show("Your Amount is greater than that your current ballance", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Main_cash_Amount.Focus();
                        return;
                    }

                    if (listView2.Items.Count > 0)
                    {
                        MessageBox.Show("You Already Added from main cash. please remove it first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Main_cash_Amount.Focus();
                        return;
                    }


                    #endregion

                    #region value pass to list view from textbox---------------------

                    ListViewItem item1 = new ListViewItem("No_Bank");

                    item1.SubItems.Add("No_Account");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add(Main_cash_Amount.Text);
                    item1.SubItems.Add("No_ID");

                    listView2.Items.Add(item1);
                    Main_cash_Amount.Text = "00.00";

                    btnSavecredit.Enabled = true;
                    #endregion
                }

                TotalPayamount();

                //Set_Off_OR_Not_Message();
                //Available_Cash_Total();
                //SET_OFF_Details_Show_or_Not();

                //Default_Settings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //####### this Code also in the Enter key..If u did some modifications, please do it there also....

                if (Rbt_frm_Chk.Checked == true)
                {

                    #region check cheque details fields---------------------------------------------------
                    if (cmbAcc_NameCredit.Text == "")
                    {
                        MessageBox.Show("Please select Account Number", "Message");
                        cmbAcc_NameCredit.Focus();
                        return;
                    }
                    if (cmbBankNameCredit.Text == "")
                    {
                        MessageBox.Show("Please select Bank Name", "Message");
                        cmbBankNameCredit.Focus();
                        return;
                    }

                    if (txtCheque_No.Text == "")
                    {
                        MessageBox.Show("Please Enter Cheque Number", "Message");
                        txtCheque_No.Focus();
                        return;
                    }
                    if (Double.Parse(txtAmount.Text) <= (0.0))
                    {
                        MessageBox.Show("Please Enter Amount", "Message");
                        txtAmount.Focus();
                        return;
                    }
                    #endregion

                    #region value pass to list view from textbox---------------------

                    ListViewItem item = new ListViewItem(cmbBankNameCredit.Text);

                    item.SubItems.Add(cmbAcc_NameCredit.Text);
                    item.SubItems.Add(txtCheque_No.Text);
                    item.SubItems.Add(dtpCurrentDate.Text);
                    item.SubItems.Add(dtpMentionDate.Text);
                    item.SubItems.Add(dtpChangeDate.Text);
                    item.SubItems.Add(txtAmount.Text);
                    item.SubItems.Add(txtBankIdCredit.Text);

                    listView2.Items.Add(item);


                    cmbBankNameCredit.SelectedIndex = -1;
                    cmbAcc_NameCredit.SelectedIndex = -1;
                    txtAmount.Text = "0.0";
                    txtCheque_No.Text = "";
                    dtpMentionDate.ResetText();
                    dtpChangeDate.ResetText();
                    dtpCurrentDate.ResetText();
                    btnSavecredit.Enabled = true;
                    #endregion


                }

                if (Rbt_Frm_Main_cash.Checked == true)
                {
                    #region Main cash empty=================================================================================

                    if (Main_cash_Amount.Text == "" || Convert.ToDouble(Main_cash_Amount.Text) == 0)
                    {
                        MessageBox.Show("Please insert an amount", "Error Main cash", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Main_cash_Amount.Focus();
                        return;
                    }
                    if (Convert.ToDouble(Main_cash_Amount.Text) > Convert.ToDouble(New_Balance.Text))
                    {
                        MessageBox.Show("Your Amount is greater than that your current ballance", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Main_cash_Amount.Focus();
                        return;
                    }

                    if (listView2.Items.Count > 0)
                    {
                        MessageBox.Show("You Already Added from main cash. please remove it first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Main_cash_Amount.Focus();
                        return;
                    }


                    #endregion

                    #region value pass to list view from textbox---------------------

                    ListViewItem item1 = new ListViewItem("No_Bank");

                    item1.SubItems.Add("No_Account");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add("No");
                    item1.SubItems.Add(Main_cash_Amount.Text);
                    item1.SubItems.Add("No_ID");

                    listView2.Items.Add(item1);
                    Main_cash_Amount.Text = "00.00";

                    btnSavecredit.Enabled = true;
                    #endregion
                }

                TotalPayamount();

                //Set_Off_OR_Not_Message();
                //Available_Cash_Total();
                //SET_OFF_Details_Show_or_Not();

                //Default_Settings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Rbt_frm_Chk.Checked == true)
                {
                    #region double click row valu pass to textbox---------------------

                    ListViewItem li = listView2.SelectedItems[0];
                    cmbBankNameCredit.Text = li.SubItems[0].Text;
                    cmbAcc_NameCredit.Text = li.SubItems[1].Text;
                    txtCheque_No.Text = li.SubItems[2].Text;
                    dtpCurrentDate.Text = li.SubItems[3].Text;
                    dtpMentionDate.Text = li.SubItems[4].Text;
                    dtpChangeDate.Text = li.SubItems[5].Text;
                    txtAmount.Text = li.SubItems[6].Text;

                    listView2.SelectedItems[0].Remove();

                    #endregion
                }

                if (Rbt_Frm_Main_cash.Checked == true)
                {
                    listView2.SelectedItems[0].Remove();
                }

                TotalPayamount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }



        Double addit = 0;
        Double addit1 = 0;
        String balnce;
        String additonpeycash;
        String balancepeycash;
        String addlist;

        private void btnSavecredit_Click(object sender, EventArgs e)
        {
            string Balance_Status = "";

            //load last Invoice Auto_Number from ID..................
            Last_SetOFF_Details();
          //  Bank_DOC_ID_Generate();



            try
            {

                DialogResult dgResult = MessageBox.Show("Are you sure you want to complete the details?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dgResult == DialogResult.Yes)
                {

                    if (Rbt_frm_Chk.Checked == true)
                    {
                        Balance_Status = "Cheque_Debit";
                    }
                    if (Rbt_Frm_Main_cash.Checked == true)
                    {
                        Balance_Status = "Main_Cash_Debit";
                    }

                    if (Rbt_frm_Chk.Checked == true)
                    {

                        #region Insert Cheque details to the DB ----------------------------------------------

                        for (int i = 0; i <= listView2.Items.Count - 1; i++)
                        {
                           // Bank_DOC_ID_Generate();

                            SqlConnection cnn = new SqlConnection(IMS);
                            cnn.Open();
                            string insert1 = @"Insert into  [InvoiceCheckDetails] ( [InvoiceID] ,[CkStatus],[Bank],[Ck_Bank_acc_Number],[CkNumber] ,[CurrentDate],[MentionDate],[ChangeDate],[Amount]) values(  @InvoiceID ,@CkStatus,@Bank,@Ck_Bank_acc_Number,@CkNumber ,@CurrentDate,@MentionDate,@ChangeDate,@Amount)";
                            SqlCommand cmm = new SqlCommand(insert1, cnn);

                            cmm.Parameters.AddWithValue("InvoiceID", lblPeetyIDCredit.Text);
                            cmm.Parameters.AddWithValue("CkStatus", "Active");
                            cmm.Parameters.AddWithValue("Bank", txtBankIdCredit.Text);
                            cmm.Parameters.AddWithValue("Ck_Bank_acc_Number", listView2.Items[i].SubItems[1].Text);
                            cmm.Parameters.AddWithValue("CkNumber", listView2.Items[i].SubItems[2].Text);
                            cmm.Parameters.AddWithValue("CurrentDate", listView2.Items[i].SubItems[3].Text);
                            cmm.Parameters.AddWithValue("MentionDate", listView2.Items[i].SubItems[4].Text);
                            cmm.Parameters.AddWithValue("ChangeDate", listView2.Items[i].SubItems[5].Text);
                            cmm.Parameters.AddWithValue("Amount", listView2.Items[i].SubItems[6].Text);


                            cmm.ExecuteNonQuery();



                            if (cnn.State == ConnectionState.Open)
                            {
                                cnn.Close();
                            }

                            //Update Bank balance...................................................................

                            //selectbankwiseBalance();

                            #region select bank wise Balance (this not use in the Traininf center)........

                            //SqlConnection cnn1 = new SqlConnection(IMS);
                            //cnn1.Open();
                            //String selecttopDoc1 = "SELECT top 1 DoC_ID from Bank_Doc_details where Bank_Name='" + listView2.Items[i].SubItems[7].Text + "' and Acc_Num='" + listView2.Items[i].SubItems[1].Text + "' order by DoC_ID desc";
                            //SqlCommand cmm1 = new SqlCommand(selecttopDoc1, cnn1);
                            //SqlDataReader dr1 = cmm1.ExecuteReader();
                            //while (dr1.Read())
                            //{
                            //    getdoid = dr1[0].ToString();
                            //    MessageBox.Show(getdoid);
                            //}
                            //dr1.Close();

                            //SqlConnection cnn2 = new SqlConnection(IMS);
                            //cnn2.Open();
                            //String selecttopDoc2 = "select top 1 Balance from Bank_Balance where DoC_ID='" + getdoid + "' order by Bal_Auto_ID desc";
                            //SqlCommand cmm2 = new SqlCommand(selecttopDoc2, cnn2);
                            //SqlDataReader dr2 = cmm2.ExecuteReader();
                            //if (dr2.Read())
                            //{
                            //    getbalance = dr2[0].ToString();
                            //    MessageBox.Show(getbalance);
                            //}

                            //else
                            //{
                            //    MessageBox.Show("cannot read this bank from the system. please add bank correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            //}
                            //dr2.Close();

                            #endregion



                            #region calculate and update bank balance (this not use in the Traininf center)..............................................

                            //string topBalanceadd = Convert.ToString(Convert.ToDouble(getbalance) - Convert.ToDouble(listView2.Items[i].SubItems[6].Text));

                            //SqlConnection Conn = new SqlConnection(IMS);
                            //Conn.Open();
                            //string sql = @"insert into Bank_Balance ([DoC_ID],[Amount_Status],[Debit_Amount],[Credit_Amount],[Balance],[Add_User],[Time_Stamp]) values(@DoC_ID,@Amount_Status,@Debit_Amount,@Credit_Amount,@Balance,@Add_User,@Time_Stamp)";
                            //SqlCommand cmd = new SqlCommand(sql, Conn);

                            //cmd.Parameters.AddWithValue("DoC_ID", Bank_DOC_ID);
                            //cmd.Parameters.AddWithValue("Amount_Status", "Cheque_To_Petty_Cash");
                            //cmd.Parameters.AddWithValue("Debit_Amount", "0.0");
                            //cmd.Parameters.AddWithValue("Credit_Amount", (listView2.Items[i].SubItems[6].Text));
                            //cmd.Parameters.AddWithValue("Balance", topBalanceadd);
                            //cmd.Parameters.AddWithValue("Add_User", LgUser.Text);
                            //cmd.Parameters.AddWithValue("Time_Stamp", DateTime.Now.ToString());

                            //cmd.ExecuteNonQuery();

                            //if (Conn.State == ConnectionState.Open)
                            //{
                            //    Conn.Close();
                            //}

                            #endregion


                            #region update Bank_Doc_details table (this not use in the Traininf center)------------------------------------------------

                            //SqlConnection Conn1 = new SqlConnection(IMS);
                            //Conn1.Open();
                            //string bankdoc = @"insert into Bank_Doc_details ([DoC_ID],[Banked_Date],[Bank_Name],[Acc_Num],[Doc_Ref_ID]) values('" + Bank_DOC_ID + "','" + listView2.Items[i].SubItems[5].Text + "','" + listView2.Items[i].SubItems[7].Text + "','" + listView2.Items[i].SubItems[1].Text + "','" + lblPeetyIDCredit.Text + "')";
                            //SqlCommand cmd1x = new SqlCommand(bankdoc, Conn1);
                            //cmd1x.ExecuteNonQuery();

                            //if (Conn1.State == ConnectionState.Open)
                            //{
                            //    Conn1.Close();
                            //}

                            #endregion

                        }

                        #endregion

                    }

                    if (Rbt_Frm_Main_cash.Checked == true)
                    {
                        #region insert main cash to SET_OFF details....................................



                        try
                        {
                            // MessageBox.Show("1");
                            SqlConnection cnnselect = new SqlConnection(IMS);
                            cnnselect.Open();
                            string selecttop = "SELECT TOP 1 Current_Bal FROM [PettyCash_Balance] order by Bal_ID desc";
                            SqlCommand cmmdad = new SqlCommand(selecttop, cnnselect);
                            SqlDataReader dr = cmmdad.ExecuteReader();

                            if (dr.Read())
                            {
                                //  MessageBox.Show("dr[0].ToString()= " + dr[0].ToString());

                                addit += Convert.ToDouble(dr[0].ToString());

                                  //MessageBox.Show("Addit= "+addit.ToString());
                                 // return;

                            }
                            else
                            {
                                addit = 0;
                                //  MessageBox.Show("3");

                            }

                            //MessageBox.Show(addit.ToString());

                            ListViewItem li = listView2.Items[0];
                            addition = li.SubItems[6].Text.ToString();

                          //  MessageBox.Show("add"+addition);

                          //  MessageBox.Show("addit" + addit);

                            balnce = ((addit) + (Double.Parse(addition))).ToString();



                            //MessageBox.Show("Balance" + balnce);

                            //return;
                            //****************************************************************************
                            //**************************************************************************

                            loadsetOff();//load new set off number.........................

                            SqlConnection cnn = new SqlConnection(IMS);
                            cnn.Open();
                            string insernewid = @"insert into Set_Off_Details(DOC_Num,DOC_ID, Status, InvoiceAutoID, Setoff_Balance,Invoiced_Tot, Bank_amount, Remain_Balance, LgUser, Set_Off_Date) 
                                        values('"+New_Set_off_Number+"','" + lblPeetyIDCredit.Text + "','" + Balance_Status + "','" + Invoiced_Last_Auto_ID + "','" + New_Balance.Text + "','0','" + Lbl_Tot_list.Text + "','" + Remaining_Balance.Text + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "') ";
                            SqlCommand cmm = new SqlCommand(insernewid, cnn);
                            cmm.ExecuteNonQuery();
                            cnn.Close();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        }

                        #endregion
                    }

                    #region data insert to Petty Cash_Balance ----------------------------------------------

                    for (int i = 0; i <= listView2.Items.Count - 1; i++)
                    {

                        SqlConnection cnnselect = new SqlConnection(IMS);
                        cnnselect.Open();
                        string selecttop = "SELECT TOP 1 Current_Bal FROM [PettyCash_Balance] order by Bal_ID desc";
                        SqlCommand cmmdad = new SqlCommand(selecttop, cnnselect);
                        SqlDataReader dr = cmmdad.ExecuteReader();

                        if (dr.Read())
                        {
                            //  MessageBox.Show("dr[0].ToString()= " + dr[0].ToString());

                            addit= Convert.ToDouble(dr[0].ToString());

                            //  MessageBox.Show("Addit= "+addit.ToString());

                        }
                        else
                        {
                            addit = 0;
                            //  MessageBox.Show("3");

                        }

                        //MessageBox.Show(addit.ToString());
                        //MessageBox.Show(addit.ToString());
                        ListViewItem li = listView2.Items[0];
                        addition = li.SubItems[6].Text.ToString();
                      //  MessageBox.Show(addition);

                        balnce = ((addit) + Double.Parse(addition)).ToString();

                      //  MessageBox.Show(balnce);
                        


                        //****************************************************************************
                        //**************************************************************************




                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        string insertBalance = @"insert into [PettyCash_Balance]( [Petty_Cash_ID],[Petty_Cash_Auto_ID],[Update_Status],[Amount],[Current_Bal],[Current_Date] ) values( @Petty_Cash_ID,@Petty_Cash_Auto_ID,@Update_Status,@Amount,@Current_Bal,@Current_Date)";
                        SqlCommand cmmCre = new SqlCommand(insertBalance, cnn);

                       // MessageBox.Show(lblPeetyIDCredit.Text);

                        cmmCre.Parameters.AddWithValue("Petty_Cash_ID", lblPeetyIDCredit.Text);
                        cmmCre.Parameters.AddWithValue("Petty_Cash_Auto_ID", "00");
                        cmmCre.Parameters.AddWithValue("Update_Status", Balance_Status);
                        cmmCre.Parameters.AddWithValue("Amount", listView2.Items[i].SubItems[6].Text);
                        cmmCre.Parameters.AddWithValue("Current_Bal", balnce);
                        cmmCre.Parameters.AddWithValue("Current_Date", DateTime.Now.ToShortDateString());

                        cmmCre.ExecuteNonQuery();



                        if (cnn.State == ConnectionState.Open)
                        {
                            cnn.Close();
                        }

                    }

                    #endregion

                    //======================================================================================================

                    #region data insert to Petty_Cash ----------------------------------------------

                    for (int i = 0; i <= listView2.Items.Count - 1; i++)
                    {

                        SqlConnection cnnselect = new SqlConnection(IMS);
                        cnnselect.Open();
                        string selecttop = "select top 1 Balance from Petty_Cash order by peety_ID desc";
                        SqlCommand cmmdad = new SqlCommand(selecttop, cnnselect);
                        SqlDataReader dr = cmmdad.ExecuteReader();

                        if (dr.Read())
                        {
                            //  MessageBox.Show("dr[0].ToString()= " + dr[0].ToString());

                            addit1 = Convert.ToDouble(dr[0].ToString());

                            //  MessageBox.Show("Addit= "+addit.ToString());

                        }
                        else
                        {
                            addit1 = 0;
                            //  MessageBox.Show("3");

                        }

                        Double totBal = (addit1) + Double.Parse(listView2.Items[i].SubItems[6].Text);



                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        string insertBalance = @"INSERT INTO [Petty_Cash] ([PettyCashID],[ptDate],[Reason],[Discription] ,[Receiver] ,[Payer],[Received_Amount] ,[Paid_amount],[Balance],[UserID])VALUES(@PettyCashID,@ptDate,@Reason,@Discription ,@Receiver ,@Payer,@Received_Amount ,@Paid_amount,@Balance,@UserID)";
                        SqlCommand cmmCre = new SqlCommand(insertBalance, cnn);

                        cmmCre.Parameters.AddWithValue("PettyCashID", PettyCash_ID.Text);
                        cmmCre.Parameters.AddWithValue("ptDate", DateTime.Now.ToString());
                        cmmCre.Parameters.AddWithValue("Reason", "Debited");
                        cmmCre.Parameters.AddWithValue("Discription", Balance_Status);
                        cmmCre.Parameters.AddWithValue("Receiver", "-");
                        cmmCre.Parameters.AddWithValue("Payer", "-");
                        cmmCre.Parameters.AddWithValue("Received_Amount", Double.Parse(listView2.Items[i].SubItems[6].Text));
                        cmmCre.Parameters.AddWithValue("Paid_amount", 0.0);
                       // cmmCre.Parameters.AddWithValue("Balance", Double.Parse(listView2.Items[i].SubItems[6].Text));
                        cmmCre.Parameters.AddWithValue("Balance", totBal);
                        cmmCre.Parameters.AddWithValue("UserID", LgUser.Text);

                        cmmCre.ExecuteNonQuery();



                        if (cnn.State == ConnectionState.Open)
                        {
                            cnn.Close();
                        }
                    }



                    MessageBox.Show("Successfull add to the Database..!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Petty_Cash_Code();
                    #endregion

                    listView2.Items.Clear();
                    btnSavecredit.Enabled = false;

                    Rbt_frm_Chk.Checked = false;
                    Rbt_Frm_Main_cash.Checked = false;

                    Grp_bx_Cheque.Enabled = false;
                    Grp_Bx_Main.Enabled = false;

                    //-------------------------------------
                    Petty_Cash_Code();

                    loadReason(comboBox2);

                    Tab2Desable("Des");

                    slectCus(RecivedBy);
                    slectCus(PayedTo);

                    calc_total_Amount();
                    loadbalance();
                    //--------------------------------------- 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
         

        }

        private void cmbPayer_Click(object sender, EventArgs e)
        {
            slectCus(cmbPayer);
        }

        private void cmbAcc_NameCredit_Click(object sender, EventArgs e)
        {
            Select_BankID();
        }

        private void txtBankIdCredit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region select the bank Accounts Numbers---------------------

                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT Account_No FROM Bank_Registor WHERE  Bank_ID='" + txtBankIdCredit.Text + "' AND Account_Type='Current Account' AND Check_book='1'";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================

                cmbAcc_NameCredit.Items.Clear();

                while (dr.Read() == true)
                {
                    cmbAcc_NameCredit.Items.Add(dr[0].ToString());
                }

                if (cmbAcc_NameCredit.Items.Count == 0)
                {
                    MessageBox.Show("This Bank Do Not Issue any cheque books. please select anothe Bank", "No Cheque Book", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // txtBank.Focus();
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

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
