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

namespace Nilwala_Training_center.Add_New
{
    public partial class New_Bank : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        String Active = "";
        String CheckBook = "";

        string _SQL_FOR_LOAD_BANK_DETAIL = "";

        public New_Bank()
        {
            InitializeComponent();
           // getCreate_New_Bank_DOC_Code();
        }

        public void slectBank()
        {
            #region load Bank in CMB

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = "select BankID,BankName from Bank_Category  ";
            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader();

            cmbBank.Items.Clear();
            cmbBank.Items.Add("<New>");

            while (dr.Read())
            {
                cmbBank.Items.Add(dr[1].ToString());
            }
            cmd1.Dispose();
            dr.Close();

            if (con1.State == ConnectionState.Open)
            {

                con1.Close();
            }
            #endregion
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
                string sql = "SELECT BankID FROM Bank_Category";
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
                    string sql1 = " SELECT BankID FROM         Bank_Category order by BankID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    if (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        lblBanklDauto.Text = "BNK" + no;

                       // MessageBox.Show(OrderNumOnly);

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

        public void getCreate_New_Bank_DOC_Code()
        {

            #region getCreate_Agency_Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                //  string sql = "select OrderID from CurrentStockItems";
                string sql = "SELECT  Bank_ID FROM Bank_Registor";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    Bank_DOC_ID.Text = "BDC1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    // string sql1 = " SELECT TOP 1 OrderID FROM CurrentStockItems order by OrderID DESC";
                    string sql1 = " SELECT  Bank_DOC_ID FROM Bank_Registor order by Bank_DOC_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    if (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        //MessageBox.Show(no);

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        //MessageBox.Show(no);

                        Bank_DOC_ID.Text = "BDC" + no;

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

        public void Clear_all()
        {
            #region clear all.................................................

            cmbBank.SelectedIndex = -1;
            Bank_Acc_Num.Text = "";
            Bank_Acc_type.SelectedIndex = -1;

            RbNew.Checked = true;
            RbNew.Enabled = true;

            RbUp.Checked = false;
            RbUp.Enabled = false;

            CkDeactivated.Enabled = false;
            CkDeactivated.Checked = false;


            chbAllView.Checked = false;

            chcheque_Book.Enabled = false;
            chcheque_Book.Checked = false;

            #endregion
        }

        public void Enable_Item()
        {
            #region Enable_Item................................................

            cmbBank.Enabled = true;
            Bank_Acc_Num.Enabled = true;
            Bank_Acc_type.Enabled = true;

            chcheque_Book.Enabled = true;

            #endregion
        }


        public void Deable_Item()
        {
            #region Deable_Item................................................

            cmbBank.Enabled = false;
            Bank_Acc_Num.Enabled = false;
            Bank_Acc_type.Enabled = false;

            chcheque_Book.Enabled = false;

            #endregion
        }

        public void load_Quary()
        {
            #region select course load quary...............

            if (chbAllView.Checked == false)
            {
                _SQL_FOR_LOAD_BANK_DETAIL = @"SELECT BR.Bank_DOC_ID, BR.Bank_ID, BC.BankName, BR.Account_No, BR.Account_Type, 
                                              BR.Check_book, BR.Bank_Status FROM Bank_Registor BR INNER JOIN
                                              Bank_Category BC ON BR.Bank_ID = BC.BankID WHERE BR.Bank_Status='1'";
            }

            if (chbAllView.Checked == true)
            {
                _SQL_FOR_LOAD_BANK_DETAIL = @"SELECT BR.Bank_DOC_ID, BR.Bank_ID, BC.BankName, BR.Account_No, BR.Account_Type, 
                                              BR.Check_book, BR.Bank_Status FROM Bank_Registor BR INNER JOIN
                                              Bank_Category BC ON BR.Bank_ID = BC.BankID";
            }


            #endregion
        }

        public void Load_Bank_Details()
        {
            #region Load Bank Details............................

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            list_Vi_Bank_Details.Items.Clear();

            // string Course_Load = @";

            SqlCommand cmd = new SqlCommand(_SQL_FOR_LOAD_BANK_DETAIL, con1);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                

                ListViewItem li;

                li = new ListViewItem(dr[0].ToString());
                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(dr[2].ToString());
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());
                li.SubItems.Add(dr[5].ToString());
                li.SubItems.Add(dr[6].ToString());

                list_Vi_Bank_Details.Items.Add(li);
            }

            //color change to accoding to the status...........................
            for (int i = 0; i <= list_Vi_Bank_Details.Items.Count - 1; i++)
            {

                if (list_Vi_Bank_Details.Items[i].SubItems[6].Text == "0")
                {
                    list_Vi_Bank_Details.Items[i].BackColor = Color.Coral;
                }
            }


            #endregion
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chbAllView_CheckedChanged(object sender, EventArgs e)
        {
            load_Quary();
            Load_Bank_Details();

        }

        private void New_Bank_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            getCreate_New_Bank_DOC_Code();

            Clear_all();

            load_Quary();
            Load_Bank_Details();


        }

        private void cmbBank_MouseClick(object sender, MouseEventArgs e)
        {
            slectBank();
        }

        private void cmbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBank.SelectedIndex == -1)
            {
                // chbDeactive.Hide();
                return;
            }
            if (cmbBank.SelectedItem.ToString() == "<New>")
            {
                PnlBankName.Visible = true;
                txtbankName.Focus();
                getCreate_Bank_Catogory_Code();


            }

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();

            string CusSelectAll = "select BankID,BankName from Bank_Category where BankName='" + cmbBank.Text + "'";
            SqlCommand cmd1 = new SqlCommand(CusSelectAll, con1);
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                Bank_ID.Text = dr[0].ToString();
            }
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlBankName.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String AddBankCate = "Insert into Bank_Category(BankID, BankName, Status) values('" + lblBanklDauto.Text + "','" + txtbankName.Text + "','1')";
            SqlCommand cmm = new SqlCommand(AddBankCate, cnn);
            cmm.ExecuteNonQuery();

            MessageBox.Show("Insert Successful...","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
            lblBanklDauto.Text = "";
            txtbankName.Text = "";
            PnlBankName.Visible = false;
            slectBank();
            cmbBank.DroppedDown = true;
            cmbBank.Focus();
        }


        

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

               

                #region check b4 insert..........................................

                if (cmbBank.Text == "")
                {
                    MessageBox.Show("Please enter bank name to save the document", "Bank name missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBank.Focus();
                    return;
                }

                if (Bank_Acc_Num.Text == "")
                {
                    MessageBox.Show("Please enter Account number to save the document", "Bank Account missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Bank_Acc_Num.Focus();
                    return;
                }

                if (Bank_Acc_type.Text == "")
                {
                    MessageBox.Show("Please Account type to save the document", "Account type missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Bank_Acc_type.Focus();
                    return;
                }

                #endregion

                if (RbNew.Checked == true)
                {
                    getCreate_Bank_Catogory_Code();

                    #region save details in the Bank_Registor table---------------------------------------------

                    //bank activated----------------------------------------------------------
                    //if (CkDeactivated.Checked == true)
                    //{
                    //    Active = "1";
                    //}

                    //if (CkDeactivated.Checked == false)
                    //{
                    //    Active = "0";
                    //}
                    //-----------------------------------------------------
                    //cheque book ----------------------------------------------------------

                    getCreate_New_Bank_DOC_Code();

                    if (chcheque_Book.Checked == false)
                    {
                        CheckBook = "0";
                    }
                    if (chcheque_Book.Checked == true)
                    {
                        CheckBook = "1";
                    }


                    SqlConnection CNN1 = new SqlConnection(IMS);
                    CNN1.Open();
                    String AddNewBank = @"INSERT INTO  Bank_Registor(Bank_DOC_ID, Bank_ID, Account_No, Account_Type, Check_book, CreateBy, Create_Date, Bank_Status)
                                    VALUES('" + Bank_DOC_ID.Text + "','" + Bank_ID.Text + "','" + Bank_Acc_Num.Text + "','" + Bank_Acc_type.Text + "','" + CheckBook + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "','1')";

                   // MessageBox.Show(AddNewBank);
                    SqlCommand cmm2 = new SqlCommand(AddNewBank, CNN1);
                    cmm2.ExecuteNonQuery();

                    MessageBox.Show("Insert Successful...", "Save Bank Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion

                }

                if (RbUp.Checked == true)
                {
                    #region update course or deactivated.................

                    string acti;

                    if (CkDeactivated.Checked)
                    {
                        acti = "0";
                    }
                    else
                    {
                        acti = "1";
                    }

                    if (chcheque_Book.Checked == false)
                    {
                        CheckBook = "0";
                    }
                    if (chcheque_Book.Checked == true)
                    {
                        CheckBook = "1";
                    }


                    SqlConnection con = new SqlConnection(IMS);
                    con.Open();

               //     MessageBox.Show(Bank_DOC_ID.Text);

                    string Agen_Update = @"UPDATE  Bank_Registor SET Bank_ID='" + Bank_ID.Text + "', Account_No='" + Bank_Acc_Num.Text + "', Account_Type='" + Bank_Acc_type.Text + "', Check_book='" + CheckBook + "', Lat_Update_User='" + LgUser.Text + "', Lat_Update='" + DateTime.Now.ToString() + "', Bank_Status='" + acti + "' WHERE Bank_DOC_ID='" + Bank_DOC_ID.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(Agen_Update, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated the Bank Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion
                }

                Clear_all();

                getCreate_New_Bank_DOC_Code();//load DOC ID....................

                load_Quary();//Load Quary.......................
                Load_Bank_Details();// after load the quary load bank details to the list view.............

            }

            catch (Exception ex)
            {
                MessageBox.Show("this error came from the dank details save form","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cmbAcounttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Bank_Acc_type.SelectedIndex==0)
            {
                chcheque_Book.Enabled = true;
            }

            if (Bank_Acc_type.SelectedIndex != 0)
            {
                chcheque_Book.Enabled = false;
                chcheque_Book.Checked = false;
            }
        }

        private void list_Vi_Bank_Details_DoubleClick(object sender, EventArgs e)
        {
            #region load the details from List view to the txtbox

            RbNew.Checked = false;
            RbUp.Checked = false;

            ListViewItem lst = list_Vi_Bank_Details.SelectedItems[0];

            Bank_DOC_ID.Text = lst.SubItems[0].Text;

           // MessageBox.Show(lst.SubItems[2].Text);

           // cmbBank.Items.Clear();
            Bank_ID.Text = lst.SubItems[1].Text;
            cmbBank.Text = lst.SubItems[2].Text;
            Bank_Acc_Num.Text = lst.SubItems[3].Text;
         //   MessageBox.Show(lst.SubItems[2].Text);

            Bank_Acc_type.Text = lst.SubItems[4].Text;

            //cheque book.........................
            if (lst.SubItems[5].Text == "1")
            {
                chcheque_Book.Checked = true;
            }
            if (lst.SubItems[5].Text == "0")
            {
                chcheque_Book.Checked = false;
            }

            //active deactive...............
            if (lst.SubItems[6].Text == "1")
            {
                CkDeactivated.Checked = false;
            }
            if (lst.SubItems[6].Text == "0")
            {
                CkDeactivated.Checked = true;
            }

            //##################################

            Deable_Item();

            RbNew.Checked = false;
            RbUp.Enabled = true;

            #endregion
        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {

            if (RbUp.Checked == true)
            {
                Enable_Item();

                CkDeactivated.Enabled = true;
                BtnSave.Text = "Update";
            }



        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {

            if (RbNew.Checked == true)
            {
                Clear_all();
                Enable_Item();

                BtnSave.Text = "Save";

                load_Quary();//Load Quary.......................
                Load_Bank_Details();// after load the quary load bank details to the list view.............

                getCreate_New_Bank_DOC_Code();
            }
        }

        private void chcheque_Book_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (RbNew.Checked == true)
            {
                Clear_all();
                Enable_Item();

                BtnSave.Text = "Save";

                load_Quary();//Load Quary.......................
                Load_Bank_Details();// after load the quary load bank details to the list view.............

                getCreate_New_Bank_DOC_Code();

                chcheque_Book.Enabled = false;
            }
        }

        private void Bank_Acc_Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Bank_Acc_type.Focus();
                Bank_Acc_type.DroppedDown = true;
            }
        }

        private void Bank_Acc_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BtnSave.Focus();
              //  Bank_Acc_type.DroppedDown = true;
            }
        }
    }
}
