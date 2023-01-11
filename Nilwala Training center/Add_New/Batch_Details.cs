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
    public partial class Batch_Details : Form
    {
        public Batch_Details()
        {
            InitializeComponent();
        }

        string _My_DB_CON = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string _SQL_FOR_LOAD_BATCH_DETAIL = "";

        private void Batch_Details_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            clear_All();
            //getCreate_BAtch_Code();

            load_Quary();
            Load_Batch_Details();

            Batch_Start_Date.Focus();
        }

        //public void getCreate_BAtch_Code()
        //{
           #region getCreate_BAtch_Code...........................................
        //    try
        //    {
        //        SqlConnection Conn = new SqlConnection(_My_DB_CON);
        //        Conn.Open();


        //        //=====================================================================================================================
        //        //  string sql = "select OrderID from CurrentStockItems";
        //        string sql = "SELECT Batch_ID FROM Batch_Details";
        //        SqlCommand cmd = new SqlCommand(sql, Conn);
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        //=====================================================================================================================
        //        if (!dr.Read())
        //        {
        //            Batch_DOC_ID.Text = "BTH1001";

        //            cmd.Dispose();
        //            dr.Close();

        //        }

        //        else
        //        {

        //            cmd.Dispose();
        //            dr.Close();

        //            // string sql1 = " SELECT TOP 1 OrderID FROM CurrentStockItems order by OrderID DESC";
        //            string sql1 = " SELECT Top 1 Batch_ID FROM Batch_Details order by Batch_ID DESC";
        //            SqlCommand cmd1 = new SqlCommand(sql1, Conn);
        //            SqlDataReader dr7 = cmd1.ExecuteReader();

        //            if (dr7.Read())
        //            {
        //                string no;
        //                no = dr7[0].ToString();

        //                string OrderNumOnly = no.Substring(3);

        //                no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

        //                Batch_DOC_ID.Text = "BTH" + no;

        //            }
        //            cmd1.Dispose();
        //            dr7.Close();

        //        }
        //        Conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        //    }
           #endregion
        //}

        private void ResetFields()
        {
            #region Clear All Items...................................................

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    if (tb != null)
                    {
                        tb.Text = string.Empty;
                    }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox dd = (ComboBox)ctrl;
                    if (dd != null)
                    {
                        dd.Text = string.Empty;
                        dd.SelectedIndex = -1;
                    }
                }
                else if (ctrl is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)ctrl;
                    if (dtp != null)
                    {
                        dtp.Text = DateTime.Today.ToShortDateString();
                    }
                }
                else if (ctrl is ListView)
                {
                    ListView lst = (ListView)ctrl;
                    if (lst.Items.Count > 0)
                    {
                        lst.Refresh();
                    }
                }
            }
            #endregion
        }


        public void clear_All()
        {
            #region clear all..........................

            Batch_End_date.Text = DateTime.Now.ToShortDateString();
            Batch_Start_Date.Text = DateTime.Now.ToShortDateString();

            Batch_Tot_days.Text = "";
            Batch_Total_trainees.Text = "";

            cmb_Course.SelectedIndex = -1;
            course_ID.Text = "Course_ID";

            RbNew.Checked = true;

            RbUp.Enabled = false;
            RbUp.Checked = false;

            CkDeactivated.Enabled = false;
            CkDeactivated.Checked = false;

            chbAllView.Checked = false;

            #endregion
        }

        public void Enable_All()
        {
            #region eneble all...........................

            Batch_Start_Date.Enabled = true;
            Batch_End_date.Enabled = true;
            Batch_Total_trainees.Enabled = true;

            cmb_Course.Enabled = true;
           

            #endregion
        }

        public void Desable_All()
        {
            #region Desable All...........................

            Batch_Start_Date.Enabled = false;
            Batch_End_date.Enabled = false;
            Batch_Total_trainees.Enabled = false;

            cmb_Course.Enabled = false;

          //  CkDeactivated.Enabled = false;
            

            #endregion
        }

        private void Batch_End_date_Leave(object sender, EventArgs e)
        {
            #region select the date time defference in the batch details..................................

            DateTime s_Date = Convert.ToDateTime(Batch_Start_Date.Text);
              DateTime E_date = Convert.ToDateTime(Batch_End_date.Text);

              double abc = (E_date - s_Date).TotalDays;

              if (abc > 0)// days are +
              {
                  Batch_Tot_days.Text = abc.ToString();
              }

              if (abc <= 0)
              {
                  MessageBox.Show("your selected End date is not correct. please check it again","Date selection is wrong",MessageBoxButtons.OK,MessageBoxIcon.Error);
                  Batch_End_date.Focus();
                  Batch_Tot_days.Text = "";
                  return;
              }

            #endregion
        }

        public void load_Quary()
        {
            #region select Batch load quary...............

            if (chbAllView.Checked == false)
            {
                _SQL_FOR_LOAD_BATCH_DETAIL = @"SELECT Batch_ID,Course_Name, Course_ID, Start_Date, End_Date, Total_Days, Total_Trainees, Reserved_Seats,Batch_Status
                                               FROM Batch_Details WHERE Batch_Status='1' AND End_Date>'" + DateTime.Now.ToShortDateString() + "' ORDER BY Batch_ID ASC";
           
            }

            if (chbAllView.Checked == true)
            {
                _SQL_FOR_LOAD_BATCH_DETAIL = @"SELECT Batch_ID,Course_Name, Course_ID, Start_Date, End_Date, Total_Days, Total_Trainees, Reserved_Seats,Batch_Status
                                               FROM Batch_Details ORDER BY Batch_ID ASC";
            }


            #endregion
        }

        public void Load_Batch_Details()
        {
            #region Load_Batch_Details............................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                List_V_Batch_Details.Items.Clear();

                // string Course_Load = @";

                SqlCommand cmd = new SqlCommand(_SQL_FOR_LOAD_BATCH_DETAIL, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());

                    List_V_Batch_Details.Items.Add(li);
                }

                //color change to accoding to the status...........................
                for (int i = 0; i <= List_V_Batch_Details.Items.Count - 1; i++)
                {

                    double tot_Seat = Convert.ToDouble(List_V_Batch_Details.Items[i].SubItems[6].Text);
                    double resaved_Seats = Convert.ToDouble(List_V_Batch_Details.Items[i].SubItems[7].Text);

                    DateTime E_Date = Convert.ToDateTime(List_V_Batch_Details.Items[i].SubItems[4].Text);
                    DateTime _today = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    double abc = (E_Date - _today).TotalDays;

                  //  MessageBox.Show(abc.ToString());
                   
                    //if no trainees.....
                    if (resaved_Seats==0)
                    {
                        List_V_Batch_Details.Items[i].BackColor = Color.White;
                    }

                    //if Seat Full.....
                    if ((tot_Seat - resaved_Seats) == 0)
                    {
                        List_V_Batch_Details.Items[i].BackColor = Color.LimeGreen;
                    }

                    //if Seat avaialbe.....
                    if ((resaved_Seats > 0) && (tot_Seat > resaved_Seats))
                    {                        
                            List_V_Batch_Details.Items[i].BackColor = Color.Gold;
                    }

                    //if course completed.....
                    if (abc < 0)
                    {
                        List_V_Batch_Details.Items[i].BackColor = Color.Coral;
                    }

                    //if course Deativated.....
                    if (List_V_Batch_Details.Items[i].SubItems[8].Text=="0")
                    {
                        List_V_Batch_Details.Items[i].BackColor = Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the Load Batch Details", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            #endregion
        }

        public void load_Course_Name()
        {
            #region load_Course_Name...........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                cmb_Course.Items.Clear();

                string Course_Load = @"SELECT     Course_Name FROM Course_Details WHERE Course_Status='1'";

                SqlCommand cmd = new SqlCommand(Course_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmb_Course.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the load_Course_ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void load_Course_ID()
        {
            #region load_Course_ID...........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

             

                string Course_Load = @"SELECT Course_ID FROM Course_Details WHERE Course_Name='"+cmb_Course.Text+"'";

                SqlCommand cmd = new SqlCommand(Course_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    course_ID.Text= dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the load_Course_ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
               

                #region check items...............

                if (cmb_Course.Text == "")
                {
                    MessageBox.Show("Please enter course name to save the document", "Course name missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmb_Course.Focus();
                    return;
                }

                if (Batch_Start_Date.Text == "")
                {
                    MessageBox.Show("Please enter batch start date to save the document", "Start date missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Batch_Start_Date.Focus();
                    return;
                }

                if (Batch_Total_trainees.Text == "" || Convert.ToDouble(Batch_Total_trainees.Text) == 0)
                {
                    MessageBox.Show("Please enter Batch Total trainees to save the document", "total trainees missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Batch_Total_trainees.Focus();
                    return;
                }

                //Date time setting check.............................................................

                DateTime s_Date = Convert.ToDateTime(Batch_Start_Date.Text);
                DateTime E_date = Convert.ToDateTime(Batch_End_date.Text);

                double abc = (E_date - s_Date).TotalDays;

               
                if (abc <= 0)
                {
                    MessageBox.Show("your selected End date is not correct. please check it again", "Date selection is wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Batch_End_date.Focus();
                    Batch_Tot_days.Text = "";
                    return;
                }

                //----------------------------------------------------------------------------------

                #endregion

                if (Batch_DOC_ID.Text == "")
                {

                    MessageBox.Show("Please Enter Batch ID.......", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Batch_DOC_ID.Focus();

                }

                if (RbNew.Checked == true)
                {
                    //getCreate_BAtch_Code();

                    

                    #region save details in the Batch_Details table---------------------------------------------


                    SqlConnection CNN1 = new SqlConnection(_My_DB_CON);
                    CNN1.Open();

                    String Add_New_BAtch = @"INSERT INTO  Batch_Details( Batch_ID, Start_Date, End_Date, Total_Days, Total_Trainees, Reserved_Seats, Add_User, Time_Stamp,Batch_Status,Course_Name, Course_ID)
                                    VALUES('" + Batch_DOC_ID.Text + "','" + Batch_Start_Date.Text + "','" + Batch_End_date.Text + "','" + Batch_Tot_days.Text + "','" + Batch_Total_trainees.Text + "','0','"+LgUser.Text+"','" + DateTime.Now.ToString() + "','1','"+cmb_Course.Text+"','"+course_ID.Text+"')";

                    SqlCommand cmm2 = new SqlCommand(Add_New_BAtch, CNN1);
                    cmm2.ExecuteNonQuery();



                    MessageBox.Show("Insert Successful...", "Save Batch Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion

                }

                if (RbUp.Checked == true)
                {
                    #region update batch or deactivated.................

                    Batch_DOC_ID.Enabled = false;

                    string acti;

                    if (CkDeactivated.Checked)
                    {
                        acti = "0";
                    }
                    else
                    {
                        acti = "1";
                    }

                  

                    SqlConnection con = new SqlConnection(_My_DB_CON);
                    con.Open();

                    string BAtch_Update = @"UPDATE  Batch_Details SET  Start_Date='" + Batch_Start_Date.Text + "', End_Date='" + Batch_End_date.Text + "', Total_Days='" + Batch_Tot_days.Text + "',Total_Trainees='" + Batch_Total_trainees.Text + "', Last_Update_user='" + LgUser.Text + "', Update_stamp='" + DateTime.Now.ToString() + "',Batch_Status='" + acti + "',Course_Name='" + cmb_Course.Text + "', Course_ID='" + course_ID.Text + "' WHERE Batch_ID='" + Batch_DOC_ID.Text + "'";
                    SqlCommand cmd2 = new SqlCommand(BAtch_Update, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated the Batch Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion
                }

                clear_All();
                ResetFields();
               
                //getCreate_BAtch_Code();

                load_Quary();
                Load_Batch_Details();

                Batch_DOC_ID.Focus();
                //Batch_Start_Date.Focus();
            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the save the detail", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chbAllView_CheckedChanged(object sender, EventArgs e)
        {

            //getCreate_BAtch_Code();

            load_Quary();
            Load_Batch_Details();
        }

        private void List_V_Batch_Details_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Batch_DOC_ID.Enabled = false;

            load_Course_Name();

            #region load the details from List view to the txtbox

            ListViewItem lst = List_V_Batch_Details.SelectedItems[0];

            Batch_DOC_ID.Text = lst.SubItems[0].Text;
            course_ID.Text = lst.SubItems[1].Text;
            cmb_Course.Text=lst.SubItems[2].Text;
            Batch_Start_Date.Text = lst.SubItems[3].Text;
            Batch_End_date.Text = lst.SubItems[4].Text;
            Batch_Tot_days.Text = lst.SubItems[5].Text;
            Batch_Total_trainees.Text = lst.SubItems[6].Text;

          
            //active deactive...............
            if (lst.SubItems[8].Text == "1")
            {
                CkDeactivated.Checked = false;
            }
            if (lst.SubItems[8].Text == "0")
            {
                CkDeactivated.Checked = true;
            }

            //##################################
            RbNew.Checked = false;
            RbUp.Enabled = true;

            

            Desable_All();

          

            

            #endregion
        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {
            if (RbUp.Enabled == true)
            {
                Enable_All();
                CkDeactivated.Enabled = true;
                Batch_DOC_ID.Enabled = false;

                BtnSave.Text = "Update";
            }

        }

        private void cmb_Course_Click(object sender, EventArgs e)
        {
            if (Batch_DOC_ID.Text == "")
            {
                MessageBox.Show("Please Enter Batch ID.......", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Batch_DOC_ID.Focus();
            }

            load_Course_Name();
            
        }

        private void cmb_Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Course_ID();
        }

        private void Batch_Total_trainees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        private void Batch_Start_Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (Batch_DOC_ID.Text == "")
            {
                MessageBox.Show("Please Enter Batch ID.......", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Batch_DOC_ID.Focus();
            }

            if (e.KeyValue == 13)
            {
                Batch_End_date.Focus();
            }
        }

        private void Batch_End_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (Batch_DOC_ID.Text == "")
            {
                MessageBox.Show("Please Enter Batch ID.......", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Batch_DOC_ID.Focus();
            }

            if (e.KeyValue == 13)
            {
               cmb_Course.Focus();
               load_Course_ID();
            }
        }

        private void cmb_Course_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Batch_Total_trainees.Focus();
            }
        }

        private void Batch_Total_trainees_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BtnSave.Focus();
            }
        }

        String orderID = "";
        private void Batch_DOC_ID_Leave(object sender, EventArgs e)
        {
            #region Check Duplicate ID for Batch ID............................................................ 
           


            try
            {
                SqlConnection cnnID = new SqlConnection(_My_DB_CON);
                cnnID.Open();
                String SelectID = "SELECT Batch_ID FROM Batch_Details";
                SqlCommand cmm = new SqlCommand(SelectID, cnnID);
                SqlDataReader drid = cmm.ExecuteReader();

                while(drid.Read())
                {
                    orderID = drid[0].ToString();

                    if (orderID == Batch_DOC_ID.Text)
                    {

                        DialogResult dr = MessageBox.Show("This Batch ID Is Already Exist Please Try Another One", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dr == DialogResult.OK)
                        {
                            Batch_DOC_ID.Text = "";
                            Batch_DOC_ID.Focus();
                        }

                        return;

                    }
                }
               
               
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the save the detail", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

               
            #endregion
        }

        private void Batch_DOC_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void Batch_DOC_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                Batch_Start_Date.Focus();   
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ResetFields();
            Batch_DOC_ID.Focus();
            
        }

        private void Batch_Details_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (Batch_DOC_ID.Text == "")
            //{
            //    Batch_DOC_ID.Text = ".";
            //}
        }

        private void chbAllView_Click(object sender, EventArgs e)
        {
            //if (Batch_DOC_ID.Text == "")
            //{
            //    Batch_DOC_ID.Text = ".";
            //}
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            if (RbNew.Checked == true)
            {
                Batch_DOC_ID.Enabled = true;
                ResetFields();
                cmb_Course.Enabled = true;
                Batch_Start_Date.Enabled = true;
                Batch_End_date.Enabled = true;
                CkDeactivated.Enabled = false;
            }
        }

       

       
           
        
    }
}
