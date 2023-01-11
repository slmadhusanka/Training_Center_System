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

namespace Nilwala_Training_center.Payments
{
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();

            slectBank();
            slectAccName();
            loadlistview();
            loadsetOff();
            getCreateStockCode();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


        private void Deposit_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

           
            slectBank();
            slectAccName();
            loadlistview();
            loadsetOff();
            getCreateStockCode();
        }

        public void slectBank()
        {
            #region load Bank in CMB

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = "select BankID,BankName from Bank_Category  ";
            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader();

            cmbBankdeposit.Items.Clear();
            //cmbBankdeposit.Items.Add("<New>");

            while (dr.Read())
            {
                cmbBankdeposit.Items.Add(dr[1].ToString());
            }

            cmd1.Dispose();
            dr.Close();

            if (con1.State == ConnectionState.Open)
            {

                con1.Close();
            }
            #endregion
        }

        public void Select_BankID()
        {
            #region select the bank---------------------

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "SELECT BankID FROM Bank_Category WHERE BankName='" + cmbBankdeposit.Text + "'";
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

        public void Select_Acc_Numbers()
        {
            #region Select_Acc_Numbers---------------------

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "SELECT Account_No FROM Bank_Registor WHERE Bank_Status='1' AND Bank_ID='"+txtBankIdCredit.Text+"'";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================

            //txtBank.Items.Clear();

            cmbAccName.Items.Clear();

            while (dr.Read() == true)
            {
                cmbAccName.Items.Add(dr[0].ToString());
            }

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                dr.Close();
            }

            #endregion
        }

        public void slectAccName()
        {
            #region load AccName in CMB

            String name = cmbAccName.Text;
            String id = cmbAccName.Text;


            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = "SELECT Account_No FROM Bank_Registor WHERE  Bank_Status='1'";
            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader();

            cmbAccName.Items.Clear();
            //cmbBankdeposit.Items.Add("<New>");


            while (dr.Read())
            {
                cmbAccName.Items.Add(dr[0].ToString());
            }
            cmd1.Dispose();
            dr.Close();

            if (con1.State == ConnectionState.Open)
            {

                con1.Close();
            }
            #endregion
        }

        public void loadlistview()
        {
            #region load data to listview....

           // MessageBox.Show(DateTime.Now.ToShortDateString());

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String addinvoice = "select InvoiceID,CkNumber,mentionDate,Amount,CkStatus,Bank from InvoiceCheckDetails where CkStatus='Active' and mentionDate <='" + DateTime.Now.ToShortDateString() + "'";
            SqlCommand cmd = new SqlCommand(addinvoice, cnn);
            SqlDataReader dr = cmd.ExecuteReader();

            listView1.Items.Clear();

            while (dr.Read() == true)
            {
               // MessageBox.Show(DateTime.Now.ToShortDateString());
                ListViewItem li;
                li = new ListViewItem(dr[0].ToString());
                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(dr[2].ToString());
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[5].ToString());

                listView1.Items.Add(li);
                // label8.Text = (dr[5].ToString());
            }
            #endregion
        }

        String AutoId;
        string Set_off_Number;
        string New_Set_off_Number;

        public void loadsetOff()
        {
            #region load setoff value
            SqlConnection cnnoff = new SqlConnection(IMS);
            cnnoff.Open();
            String setoffvaluetop = "select top 1 Remain_Balance,InvoiceAutoID,DOC_Num from Set_Off_Details  order by AutoID desc";
            SqlCommand cmmset = new SqlCommand(setoffvaluetop, cnnoff);
            SqlDataReader drset = cmmset.ExecuteReader();
            while (drset.Read())
            {
                lblremainig.Text = drset[0].ToString();
                lblCurrently.Text = drset[0].ToString();
                AutoId = drset[1].ToString();

                Set_off_Number = drset[2].ToString();

                string OrderNumOnly = Set_off_Number.Substring(3);

                Set_off_Number = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                New_Set_off_Number = "STF" + Set_off_Number;
            }


            #endregion
        }

        public void getCreateStockCode()
        {
            #region New Document Code auto generate...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();

               // SELECT DoC_ID FROM Bank_Doc_details
                //=====================================================================================================================
                string sql = "SELECT DoC_ID FROM Bank_Doc_details";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    lbldepositID.Text = "DBD10000001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 DoC_ID FROM Bank_Doc_details order by DoC_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    if (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        lbldepositID.Text = "DBD" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }

        public void countcolumnValue()
        {
            #region count column Value ...............................
            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listView2.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[2].Text), 2);
            }
            tot_chk.Text = Convert.ToString(gtotal);
            #endregion
        }

        public void cashChequetot()
        {
            #region total cash &cheque................................

            String chequeCashtot = (Double.Parse(tot_Cash.Text) + Double.Parse(tot_chk.Text)).ToString();
            tot_dep.Text = chequeCashtot;
            #endregion
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            try
            {
                #region value pass other listview...............

                string asd = listView1.SelectedItems[0].SubItems[1].Text;
                for (int i = 0; i <= listView2.Items.Count - 1; i++)
                {

                    if (listView2.Items[i].SubItems[0].Text == asd)
                    {
                        MessageBox.Show("Details alredy..!", "Message");
                        return;

                    }



                }
                //---------------------------------------------------------------------------------


                ListViewItem li;

                li = new ListViewItem(listView1.SelectedItems[0].SubItems[1].Text);
                li.SubItems.Add(dateTimePicker1.Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[3].Text);
                li.SubItems.Add(listView1.SelectedItems[0].SubItems[4].Text);


                listView2.Items.Add(li);
                countcolumnValue();


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            cmbBankdeposit.Focus();

        }

        //String getdoid, getbalance;
        //public void selectbankwiseBalance()
        //{
        //    #region select bank wise Balance........


        //    SqlConnection cnn1 = new SqlConnection(IMS);
        //    cnn1.Open();
        //    String selecttopDoc1 = "SELECT top 1 DoC_ID from Bank_Doc_details where Bank_Name='" + txtBankIdCredit.Text + "' and  Acc_Num='" + cmbAccName.Text + "'order by DoC_ID desc";
        //    SqlCommand cmm1 = new SqlCommand(selecttopDoc1, cnn1);
        //    SqlDataReader dr12 = cmm1.ExecuteReader();

        //    while (dr12.Read())
        //    {

        //        getdoid = (dr12[0].ToString());
        //        //MessageBox.Show(getdoid);
        //    }
        //    dr12.Close();

        //    SqlConnection cnn2 = new SqlConnection(IMS);
        //    cnn2.Open();
        //    String selecttopDoc2 = "select top 1 Balance from Bank_Balance where DoC_ID='" + getdoid + "' order by Bal_Auto_ID desc";
        //    SqlCommand cmm2 = new SqlCommand(selecttopDoc2, cnn2);
        //    SqlDataReader dr2 = cmm2.ExecuteReader();
        //    if (dr2.Read())
        //    {
        //        getbalance = (dr2[0].ToString());
        //        //MessageBox.Show(getbalance);
        //    }
        //    else
        //    {
        //        MessageBox.Show("cannot read this bank from the system. please add bank correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //    dr2.Close();


        //    #endregion

        //}

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView2.SelectedItems[0].Remove();
            countcolumnValue();
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtAmount.Text == "")
            {
                return;
            }

            if (e.KeyValue == 13)
            {
                simpleButton1.Focus();
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                txtAmount.Text = "0.0";
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region Remaining Balance Calculation.............................

                if (txtAmount.Text == "")
                {
                    return;
                }
                loadsetOff();
                //Value check--------------------------------------------------------------------------

                if (Double.Parse(lblremainig.Text) < Double.Parse(txtAmount.Text))
                {
                    MessageBox.Show(" value is grater than to Remaining Balance", "Message");
                    txtAmount.Focus();
                    txtAmount.Text = "0.0";
                    return;

                }


                //CurrentlybalanceMinus------------------------------------------------------------------------------------

                String Currentlybalance = (Double.Parse(lblremainig.Text) - Double.Parse(txtAmount.Text)).ToString();
                lblCurrently.Text = Currentlybalance;
                tot_Cash.Text = txtAmount.Text;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void cmbBankdeposit_Click(object sender, EventArgs e)
        {
            slectBank();

           // Select_Acc_Numbers();
        }

        private void cmbBankdeposit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmbAccName.Focus();
            }
        }

        private void cmbBankdeposit_MouseClick(object sender, MouseEventArgs e)
        {
           // Select_BankID();

           // Select_Acc_Numbers();
        }

        private void cmbBankdeposit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_BankID();

            Select_Acc_Numbers();
        }

        private void cmbAccName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                simpleButton1.Focus();
            }
        }

        private void tot_Cash_TextChanged(object sender, EventArgs e)
        {
            cashChequetot();
        }

        private void tot_chk_TextChanged(object sender, EventArgs e)
        {
            cashChequetot();
        }

        Double balanceaddCash;
        String add1;
        String emp;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                #region Save button Code......................................
                #region check empty........................

                if (cmbAccName.Text == "")
                {
                    MessageBox.Show("Please select Account Number..", "Message");
                    return;
                }
                if (cmbBankdeposit.Text == "")
                {
                    MessageBox.Show("Please select Bank Name..", "Message");
                    return;
                }
                #endregion

                getCreateStockCode();

                DialogResult dgResult = MessageBox.Show("Are you sure you want to complete the details?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dgResult == DialogResult.Yes)
                {


                    if (Double.Parse(tot_chk.Text) > 0.0)
                    {
                        #region insert cheque to bank balance table
                        //if (listView2.Items.Count == 0)
                        //{
                        //    MessageBox.Show("Please Fill List view..", "Message");
                        //    return;
                        //}
                        try
                        {
                            #region select bank wise Balance........


                            //SqlConnection cnn1 = new SqlConnection(IMS);
                            //cnn1.Open();
                            //String selecttopDoc1 = "SELECT top 1 DoC_ID from Bank_Doc_details where Bank_Name='" + txtBankIdCredit.Text + "' and  Acc_Num='" + cmbAccName.Text + "'order by DoC_ID desc";
                            //SqlCommand cmm1 = new SqlCommand(selecttopDoc1, cnn1);
                            //SqlDataReader dr12 = cmm1.ExecuteReader();

                            //while (dr12.Read())
                            //{

                            //    getdoid = (dr12[0].ToString());
                            //    //MessageBox.Show(getdoid);
                            //}
                            //dr12.Close();

                            //SqlConnection cnn2 = new SqlConnection(IMS);
                            //cnn2.Open();
                            //String selecttopDoc2 = "select top 1 Balance from Bank_Balance where DoC_ID='" + getdoid + "' order by Bal_Auto_ID desc";
                            //SqlCommand cmm2 = new SqlCommand(selecttopDoc2, cnn2);
                            //SqlDataReader dr2 = cmm2.ExecuteReader();
                            //if (dr2.Read())
                            //{
                            //    getbalance = (dr2[0].ToString());
                            //    MessageBox.Show(getbalance);
                            //}
                            //else
                            //{
                            //    MessageBox.Show("cannot read this bank from the system. please add bank correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            //}
                            //dr2.Close();


                            #endregion

                            //double Calc_Bal = Convert.ToDouble(getbalance);


                            for (int i = 0; i <= listView2.Items.Count - 1; i++)
                            {



                                //======================================================================================================


                                //Calc_Bal = Calc_Bal + Convert.ToDouble(listView2.Items[i].SubItems[2].Text);



                                //SqlConnection Conn = new SqlConnection(IMS);
                                //Conn.Open();
                                //MessageBox.Show("topBalanceadd");
                                //string sql = @"insert into Bank_Balance ([DoC_ID],[Amount_Status],[Debit_Amount],[Credit_Amount],[Balance],[Add_User],[Time_Stamp]) values(@DoC_ID,@Amount_Status,@Debit_Amount,@Credit_Amount,@Balance,@Add_User,@Time_Stamp)";
                                //SqlCommand cmd = new SqlCommand(sql, Conn);
                                //cmd.Parameters.AddWithValue("DoC_ID", lbldepositID.Text);
                                //cmd.Parameters.AddWithValue("Amount_Status", "Cheque Deposit");
                                //cmd.Parameters.AddWithValue("Debit_Amount", (listView2.Items[i].SubItems[2].Text));
                                //cmd.Parameters.AddWithValue("Credit_Amount", 0.0);
                                //cmd.Parameters.AddWithValue("Balance", Calc_Bal);
                                //cmd.Parameters.AddWithValue("Add_User", LgUser.Text);
                                //cmd.Parameters.AddWithValue("Time_Stamp", listView2.Items[i].SubItems[1].Text);


                                //cmd.ExecuteNonQuery();





                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        #endregion

                    }

                    if (Double.Parse(txtAmount.Text) > 0.0)
                    {
                        #region insert cash in to Bank balance table........

                        //#region select bank wise Balance........


                        //SqlConnection cnn1 = new SqlConnection(IMS);
                        //cnn1.Open();
                        //String selecttopDoc1 = "SELECT top 1 DoC_ID from Bank_Doc_details where Bank_Name='" + txtBankIdCredit.Text + "' and  Acc_Num='" + cmbAccName.Text + "'order by DoC_ID desc";
                        //SqlCommand cmm1 = new SqlCommand(selecttopDoc1, cnn1);
                        //SqlDataReader dr12 = cmm1.ExecuteReader();

                        //while (dr12.Read())
                        //{

                        //    getdoid = (dr12[0].ToString());
                        //    // MessageBox.Show(getdoid);
                        //}
                        //dr12.Close();

                        //SqlConnection cnn2 = new SqlConnection(IMS);
                        //cnn2.Open();
                        //String selecttopDoc2 = "select top 1 Balance from Bank_Balance where DoC_ID='" + getdoid + "' order by Bal_Auto_ID desc";
                        //SqlCommand cmm2 = new SqlCommand(selecttopDoc2, cnn2);
                        //SqlDataReader dr2 = cmm2.ExecuteReader();
                        //if (dr2.Read())
                        //{
                        //    getbalance = (dr2[0].ToString());
                        //    // MessageBox.Show(getbalance);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("cannot read this bank from the system. please add bank correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //}
                        //dr2.Close();


                        //#endregion
                        ////=================================================================================

                        //balanceaddCash = (Double.Parse(getbalance) + (Double.Parse(tot_Cash.Text)));
                        //topBalanceadd = balanceaddCash.ToString();
                        //// MessageBox.Show(topBalanceadd);

                        //SqlConnection cnn4 = new SqlConnection(IMS);
                        //cnn4.Open();
                        //String newtop = "select top 1 '" + lbldepositID.Text + "',Balance  from Bank_Balance order by Bal_Auto_ID desc";
                        //SqlCommand cmm4 = new SqlCommand(newtop, cnn4);
                        //SqlDataReader dr4 = cmm4.ExecuteReader();
                        //while (dr4.Read())
                        //{
                        //    add1 = (dr4[1].ToString());
                        //    //MessageBox.Show(add1);
                        //}

                        //emp = (Double.Parse(add1) + (Double.Parse(tot_Cash.Text))).ToString();
                        ////MessageBox.Show(emp);
                       
                        //    SqlConnection Conn = new SqlConnection(IMS);
                        //    Conn.Open();
                        //    string sql = @"insert into Bank_Balance ([DoC_ID],[Amount_Status],[Debit_Amount],[Credit_Amount],[Balance],[Add_User],[Time_Stamp]) values('" + lbldepositID.Text + "','" + "Cash" + "','" + txtAmount.Text + "','" + "0.0" + "','" + emp + "','" + LgUser.Text + "','" + dateTimePicker1.Text + "')";
                        //    SqlCommand cmd = new SqlCommand(sql, Conn);
                        //    cmd.ExecuteNonQuery();

                        try
                        {

                        SqlConnection cnn5 = new SqlConnection(IMS);
                        cnn5.Open();
                        String AddSetoFF = @"INSERT INTO [Set_Off_Details]([DOC_Num],[DOC_ID],[Status],[InvoiceAutoID],[Setoff_Balance],[Invoiced_Tot],[Bank_amount],[Remain_Balance],[LgUser],[Set_Off_Date]) values('" + New_Set_off_Number + "','" + lbldepositID.Text + "','" + "Deposit" + "','" + AutoId + "','" + lblremainig.Text + "','" + "0.00" + "','" + txtAmount.Text + "','" + lblCurrently.Text + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmm5 = new SqlCommand(AddSetoFF, cnn5);
                        cmm5.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        #endregion
                    }

                    try
                    {
                        #region insert chequ/cash details in to Bank doc Details............
                        //MessageBox.Show("insert chequ/cash details in to Bank doc Details............");
                        SqlConnection Conn1 = new SqlConnection(IMS);
                        Conn1.Open();
                        string bankdoc = @"insert into Bank_Doc_details ([DoC_ID],[Banked_Date],[Bank_Name],[Acc_Num],Doc_Ref_ID)values('" + lbldepositID.Text + "','" + dateTimePicker1.Text + "','" + txtBankIdCredit.Text + "','" + cmbAccName.Text + "','" + lbldepositID.Text + "')";
                        SqlCommand cmd1 = new SqlCommand(bankdoc, Conn1);
                        cmd1.ExecuteNonQuery();
                        #endregion

                        
                        #region insert data into Banked Cheque Details......

                        if (listView2.Items.Count != 0)
                        {
                            for (int i = 0; i <= listView2.Items.Count - 1; i++)
                            {
                                ListViewItem li;
                                li = new ListViewItem(listView1.SelectedItems[0].SubItems[0].Text);
                            }

                            SqlConnection Conn2 = new SqlConnection(IMS);
                            Conn2.Open();
                            string Banked_Cheque_Details = @"insert into Banked_Cheque_Details ([Bank_Doc_ID],[Ck_Invoice_Num],[Ck_Number],[Bank],[Status])values('" + lbldepositID.Text + "','" + listView1.SelectedItems[0].SubItems[0].Text + "','" + listView1.SelectedItems[0].SubItems[1].Text + "','" + txtBankIdCredit.Text + "','" + "diposit" + "')";
                            SqlCommand cmd2 = new SqlCommand(Banked_Cheque_Details, Conn2);
                            cmd2.ExecuteNonQuery();
                        }

                        #endregion
                        

                        #region update InvoiceCheckDetails details...........
                        {
                            for (int i = 0; i <= listView2.Items.Count - 1; i++)
                            {

                                SqlConnection Conn3 = new SqlConnection(IMS);
                                Conn3.Open();
                                string Banked_Cheque_Details = @"update  InvoiceCheckDetails set CkStatus='" + "Deposited" + "' where CkNumber='" + listView2.Items[i].SubItems[0].Text + "'and Bank='" + listView2.Items[i].SubItems[3].Text + "'";
                                SqlCommand cmd3 = new SqlCommand(Banked_Cheque_Details, Conn3);
                                cmd3.ExecuteNonQuery();
                            }

                        }
                        #endregion
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    MessageBox.Show("Successfull");

                   //  frmDepositDetails sm = new frmDepositDetails();
                  //  sm.diposiID = lbldepositID.Text;
                    // sm.Cash = tot_Cash.Text;
                    // sm.Cheque = tot_chk.Text;
                  //  sm.Show();


                    #region Clear....................................................

                    loadlistview();
                    listView2.Items.Clear();
                    cmbAccName.SelectedIndex = -1;
                    cmbBankdeposit.SelectedIndex = -1;
                    txtAmount.Text = "0.0";
                    lblCurrently.Text = "0.0";
                    loadsetOff();
                    getCreateStockCode();
                    loadlistview();
                    tot_Cash.Text = "0.00";
                    tot_chk.Text = "0.00";
                    tot_dep.Text = "0.00";


                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
    }
}
