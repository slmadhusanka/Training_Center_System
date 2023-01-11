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
    public partial class New_Agency : Form
    {
        public New_Agency()
        {
            InitializeComponent();
        }

        string _My_DB_CON = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public void getCreate_Agency_Code()
        {
            #region getCreate_Agency_Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(_My_DB_CON);
                Conn.Open();


                //=====================================================================================================================
                //  string sql = "select OrderID from CurrentStockItems";
                string sql = "SELECT Ajecy_ID FROM Agency_Details";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    Agen_ID.Text = "AGN1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    // string sql1 = " SELECT TOP 1 OrderID FROM CurrentStockItems order by OrderID DESC";
                    string sql1 = " SELECT TOP 1 Ajecy_ID FROM Agency_Details order by Ajecy_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        Agen_ID.Text = "AGN" + no;

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

        public void clearAll()
        {
            #region clear all...............

            Agen_Address.Text = "";
            Agen_Email.Text = "";
            Agen_Name.Text = "";
            Agen_P1_Name.Text = "";
            Agen_P1_Tel.Text = "";
            Agen_P2_Name.Text = "";
            Agen_P2_Tel.Text = "";

            RbNew.Checked = true;
            RbUp.Checked = false;
            RbUp.Enabled = false;

            CkDeactivated.Checked = false;
            CkDeactivated.Enabled = false;

            ckAll.Checked = false;

            #endregion
        }

        public void Desable_All()
        {
            #region Desable_All...............

            Agen_Address.Enabled = false;
            Agen_Email.Enabled = false;
            Agen_Name.Enabled = false;
            Agen_P1_Name.Enabled = false;
            Agen_P1_Tel.Enabled = false;
            Agen_P2_Name.Enabled = false;
            Agen_P2_Tel.Enabled = false;

            #endregion
        }

        public void Enable_All()
        {
            #region Enable_All...............

            Agen_Address.Enabled = true;
            Agen_Email.Enabled = true;
            Agen_Name.Enabled = true;
            Agen_P1_Name.Enabled = true;
            Agen_P1_Tel.Enabled = true;
            Agen_P2_Name.Enabled = true;
            Agen_P2_Tel.Enabled = true;

            ckAll.Checked = false;

            #endregion
        }

        public void Select_Agency()
        {
            List_View_Agency.Items.Clear();

            if (ckAll.Checked == true)
            {
                #region load_Agency details with deactivated..............................

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                string Load_Details = @"SELECT     Ajecy_ID, Agency_Name, Agency_Address, Agency_Email, Agency_Per_01_Name, Agency_Per_01_Mob, Agency_Per_02_Name, Agency_Per_02_Mob, Agency_Status
                                        FROM         Agency_Details";

                SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                List_View_Agency.Items.Clear();

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
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());

                    List_View_Agency.Items.Add(li);
                }

                #endregion
            }

            if (ckAll.Checked == false)
            {
                #region load_Agency details without deactivated..............................

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                string Load_Details = @"SELECT     Ajecy_ID, Agency_Name, Agency_Address, Agency_Email, Agency_Per_01_Name, Agency_Per_01_Mob, Agency_Per_02_Name, Agency_Per_02_Mob,Agency_Status
                                        FROM         Agency_Details
                                        WHERE     (Agency_Status = '1')";

                SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                List_View_Agency.Items.Clear();

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
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());

                    List_View_Agency.Items.Add(li);
                }

                #endregion        
            }

        }

        private void New_Agency_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            getCreate_Agency_Code();

            RbNew.Checked = true;
            RbUp.Checked = false;
            RbUp.Enabled = false;

            CkDeactivated.Checked = false;
            CkDeactivated.Enabled = false;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            PnlAgencySerch.Visible = true;
            txtSearch.Focus();


            try
            {
                Select_Agency();

            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the load agency details to the system", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          

        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlAgencySerch.Visible = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           
            
           // try
           // {
                getCreate_Agency_Code();

                #region missing things check.....................

                if (Agen_Name.Text == "")
                {
                    MessageBox.Show("Please enter Agency name to save the document","Agency name missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Agen_Name.Focus();
                    return;
                }

                if (Agen_P1_Name.Text == "")
                {
                    MessageBox.Show("Please enter contact person's name to save the document", "contact person's name missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Agen_P1_Name.Focus();
                    return;
                }

                if (Agen_P1_Tel.Text == "")
                {
                    MessageBox.Show("Please enter contact person's telephone number to save the document", "contact person's telephone number missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Agen_P1_Tel.Focus();
                    return;
                }
                #endregion

                if (RbNew.Checked == true)
                {
                    #region insert to the data base..........................................................

                    getCreate_Agency_Code();

                    SqlConnection con1 = new SqlConnection(_My_DB_CON);
                    SqlCommand cmd = con1.CreateCommand();
                    cmd.CommandText = @"EXECUTE New_Agency @Ajecy_ID, @Agency_Name, @Agency_Address,@Agency_Email, @Agency_Per_01_Name, @Agency_Per_01_Mob, 
                                    @Agency_Per_02_Name, @Agency_Per_02_Mob, @Agency_Status, 
							        @Add_User, @Add_Date, @Last_Update_By, @Update_Stamp";

                    con1.Open();

                    cmd.Parameters.Add("@Ajecy_ID", SqlDbType.NVarChar).Value = Agen_ID.Text;
                    cmd.Parameters.Add("@Agency_Name", SqlDbType.NVarChar).Value = Agen_Name.Text;
                    cmd.Parameters.Add("@Agency_Address", SqlDbType.NVarChar).Value = Agen_Address.Text;
                    cmd.Parameters.Add("@Agency_Email", SqlDbType.NVarChar).Value = Agen_Email.Text;
                    cmd.Parameters.Add("@Agency_Per_01_Name", SqlDbType.NVarChar).Value = Agen_P1_Name.Text;
                    cmd.Parameters.Add("@Agency_Per_01_Mob", SqlDbType.NVarChar).Value = Agen_P1_Tel.Text;
                    cmd.Parameters.Add("@Agency_Per_02_Name", SqlDbType.NVarChar).Value = Agen_P2_Name.Text;
                    cmd.Parameters.Add("@Agency_Per_02_Mob", SqlDbType.NVarChar).Value = Agen_P2_Tel.Text;
                    cmd.Parameters.Add("@Agency_Status", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@Add_User", SqlDbType.NVarChar).Value = LgUser.Text;
                    cmd.Parameters.Add("@Add_Date", SqlDbType.NVarChar).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("@Last_Update_By", SqlDbType.NVarChar).Value = "No";
                    cmd.Parameters.Add("@Update_Stamp", SqlDbType.NVarChar).Value = DateTime.Now.ToString();

                    cmd.ExecuteNonQuery();

                    SqlConnection con2 = new SqlConnection(_My_DB_CON);
                    con2.Open();
                    string CustomerCredit = "INSERT INTO RegCusCredBalance(CusID, DocNumber, Credit_Amount, Debit_Amount,Debit_Balance, Balance, Date) VALUES('" + Agen_ID.Text + "','" + Agen_ID.Text + "','0','0','0','0','" + DateTime.Now.ToString() + "')";
                    SqlCommand cmd2 = new SqlCommand(CustomerCredit, con1);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Insert Successfully", "Recoded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion
                }

                if (RbUp.Checked == true && CkDeactivated.Checked == false)
                {
                    #region Udate details.....................................

                    string acti;
                    if (CkDeactivated.Checked)
                    {
                        acti = "0";
                    }
                    else
                    {
                        acti = "1";
                    }

                 //   MessageBox.Show("Update");

                    SqlConnection con = new SqlConnection(_My_DB_CON);
                    con.Open();

                    string Agen_Update = @"UPDATE Agency_Details SET Agency_Status='"+acti+"', [Agency_Name] = '" + Agen_Name.Text + "',[Agency_Address] = '" + Agen_Name.Text + "',[Agency_Email] = '" + Agen_Email.Text + "',[Agency_Per_01_Name] = '" + Agen_P1_Name.Text + "',[Agency_Per_01_Mob] = '" + Agen_P1_Tel.Text + "',[Agency_Per_02_Name] = '" + Agen_P2_Name.Text + "',[Agency_Per_02_Mob] = '" + Agen_P2_Tel.Text + "',[Last_Update_By] = '" + LgUser.Text + "',[Update_Stamp] = '" + DateTime.Now.ToString() + "' WHERE Ajecy_ID='" + Agen_ID.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(Agen_Update, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated the Agency.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clearAll();
                    getCreate_Agency_Code();

                    Enable_All();

                    #endregion
                }

                if (RbUp.Checked == true && CkDeactivated.Checked == true)
                {
                    #region deactivate..

                  //  MessageBox.Show("Update");

                    SqlConnection con = new SqlConnection(_My_DB_CON);
                    con.Open();

                    string Agen_Update = @"UPDATE Agency_Details SET Agency_Status='0' ,[Last_Update_By] = '" + LgUser.Text + "',[Update_Stamp] = '" + DateTime.Now.ToString() + "' WHERE Ajecy_ID='" + Agen_ID.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(Agen_Update, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully Deactivated the Agency.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clearAll();
                    getCreate_Agency_Code();

                    Enable_All();

                    #endregion
                }

                Enable_All();
                getCreate_Agency_Code();
                clearAll();
                Agen_Name.Focus();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("This error came from the save the detail", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                #region load_Agency details..............................

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                string Load_Details = @"SELECT     Ajecy_ID, Agency_Name, Agency_Address, Agency_Email, Agency_Per_01_Name, Agency_Per_01_Mob, Agency_Per_02_Name, Agency_Per_02_Mob
                                        FROM         Agency_Details
                                        WHERE     (Agency_Status = '1') AND (Agency_Name LIKE '%" + txtSearch.Text + "%' OR Ajecy_ID LIKE '%" + txtSearch.Text + "%' OR Agency_Address LIKE '%" + txtSearch.Text + "%' OR Agency_Email LIKE '%" + txtSearch.Text + "%')";

                SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                List_View_Agency.Items.Clear();

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
                    li.SubItems.Add(dr[7].ToString());

                    List_View_Agency.Items.Add(li);
                }

                #endregion
            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the lload agency details to the system", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void List_View_Agency_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                #region select to txt boxes...............

                ListViewItem itmes = List_View_Agency.SelectedItems[0];

                Agen_ID.Text = itmes.SubItems[0].Text;
                Agen_Name.Text = itmes.SubItems[1].Text;
                Agen_Address.Text = itmes.SubItems[2].Text;
                Agen_Email.Text = itmes.SubItems[3].Text;
                
                Agen_P1_Name.Text = itmes.SubItems[4].Text;
                Agen_P1_Tel.Text = itmes.SubItems[5].Text;
                Agen_P2_Name.Text = itmes.SubItems[6].Text;
                Agen_P2_Tel.Text = itmes.SubItems[7].Text;

                if (itmes.SubItems[8].Text == "1")
                {
                    CkDeactivated.Checked = false;
                }

                if (itmes.SubItems[8].Text == "0")
                {
                    CkDeactivated.Checked = true;
                }

                PnlAgencySerch.Visible = false;

                Desable_All();

                RbNew.Checked = false;
                RbNew.Enabled = true;

                RbUp.Checked = false;
                RbUp.Enabled = true;

             


                ckAll.Checked = false;

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the select the agency details to the txtboxes","error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {
            if (RbUp.Checked == true)
            {
                Enable_All();

                CkDeactivated.Enabled = true;
                //CkDeactivated.Checked = false;

                BtnSave.Text = "Update";

            }
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            if (RbNew.Checked == true)
            {
              
                clearAll();

                getCreate_Agency_Code();

                BtnSave.Text = "Save";
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Select_Agency();
            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the lload agency details to the system with deactivated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (RbNew.Checked == true)
            {

                clearAll();

                getCreate_Agency_Code();

                BtnSave.Text = "Save";
            }
        }

        private void Agen_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Agen_Address.Focus();
            }
        }

        private void Agen_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Agen_Email.Focus();
            }
        }

        private void Agen_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Agen_P1_Name.Focus();
            }
        }

        private void Agen_P1_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Agen_P1_Tel.Focus();
            }
        }

        private void Agen_P1_Tel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Agen_P2_Name.Focus();
            }
        }

        private void Agen_P1_Tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void Agen_P2_Tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void Agen_P2_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Agen_P2_Tel.Focus();
            }
        }

        private void Agen_P2_Tel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BtnSave.Focus();
            }
        }
    }
}
