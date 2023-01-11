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
    public partial class New_Course : Form
    {
        public New_Course()
        {
            InitializeComponent();
        }

        string _My_DB_CON = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string _SQL_FOR_LOAD_COURSE = "";

        public void Clear_all()
        {
            #region clear all.................................

            Course_Name.Text = "";
            Course_Fee.Text = "";

            RbNew.Checked = true;
            RbUp.Enabled = false;
            RbUp.Checked = false;

            CkDeactivated.Enabled = false;
            CkDeactivated.Checked = false;

            chbAllView.Checked = false;

            #endregion
        }

        public void Desable_All()
        {
            #region Desable all........................

            Course_Name.Enabled = false;
            Course_Fee.Enabled = false;

            #endregion
        }

        public void Enable_All()
        {
            #region Enable_All........................

            Course_Name.Enabled = true;
            Course_Fee.Enabled = true;

            #endregion
        }

        public void load_Quary()
        {
            #region select course load quary...............

            if (chbAllView.Checked == false)
            {
                _SQL_FOR_LOAD_COURSE = @"SELECT Course_ID, Course_Name, Course_Fee, Course_Status 
                                        FROM Course_Details WHERE Course_Status='1'";
            }

            if (chbAllView.Checked == true)
            {
                _SQL_FOR_LOAD_COURSE = @"SELECT Course_ID, Course_Name, Course_Fee, Course_Status 
                                        FROM Course_Details";
            }
 
 
            #endregion
        }

        public void load_Corses()
        {
            #region load all active courses............................

            SqlConnection con1 = new SqlConnection(_My_DB_CON);
            con1.Open();

            list_V_Course.Items.Clear();

           // string Course_Load = @";

            SqlCommand cmd = new SqlCommand(_SQL_FOR_LOAD_COURSE, con1);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListViewItem li;

                li = new ListViewItem(dr[0].ToString());
                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(dr[2].ToString());
                li.SubItems.Add(dr[3].ToString());

                list_V_Course.Items.Add(li);
            }

            for (int i = 0; i <= list_V_Course.Items.Count - 1; i++)
            {

                if (list_V_Course.Items[i].SubItems[3].Text == "0")
                {
                    list_V_Course.Items[i].BackColor = Color.Coral;
                }
            }
            

            #endregion
        }

        private void New_Course_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;


            load_Quary();//select the agency status select quart
            load_Corses();// load the agency to the list view

            getCreate_Agency_Code();// new doc code

            RbNew.Checked = true;
        }

        public void getCreate_Agency_Code()
        {
            #region getCreate_Agency_Code...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(_My_DB_CON);
                Conn.Open();


                //=====================================================================================================================
                //  string sql = "select OrderID from CurrentStockItems";
                string sql = "SELECT Course_ID FROM Course_Details";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    Course_ID.Text = "CRS1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    // string sql1 = " SELECT TOP 1 OrderID FROM CurrentStockItems order by OrderID DESC";
                    string sql1 = " SELECT TOP 1 Course_ID FROM Course_Details order by Course_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        Course_ID.Text = "CRS" + no;

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
              

                if (RbNew.Checked == true)
                {
                    getCreate_Agency_Code();

                    #region Save new Course...........................

                    SqlConnection con = new SqlConnection(_My_DB_CON);
                    con.Open();

                    string Course_Insert = @"INSERT INTO Course_Details (Course_ID, Course_Name, Course_Fee, Course_Status, Add_User, Add_Stamp) VALUES('" + Course_ID.Text + "','" + Course_Name.Text + "','" + Course_Fee.Text + "','1','" + LgUser.Text + "','" + DateTime.Now.ToString() + "')";

                    SqlCommand cmd2 = new SqlCommand(Course_Insert, con);
                    cmd2.ExecuteNonQuery();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    MessageBox.Show("Insert Successfully", "Recoded", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                    

                    SqlConnection con = new SqlConnection(_My_DB_CON);
                    con.Open();

                    string Agen_Update = @"UPDATE  Course_Details SET Course_Name='" + Course_Name.Text + "', Course_Fee='" + Course_Fee.Text + "',Course_Status='" + acti + "', Last_Update_By='" + LgUser.Text + "', Update_Stamp='" + DateTime.Now.ToString() + "' WHERE Course_ID='" + Course_ID.Text + "'";

                    SqlCommand cmd2 = new SqlCommand(Agen_Update, con);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Successfully Updated the Course.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion
                }

                Enable_All();
                Clear_all();
                getCreate_Agency_Code();

                RbUp.Enabled = false;
                RbUp.Checked = false;
                chbAllView.Checked = false;

                load_Quary();//select the agency status select quart
                load_Corses();// load the agency to the list view

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the load course details to the system", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chbAllView_CheckedChanged(object sender, EventArgs e)
        {
            load_Quary();//select the agency status select quart
            load_Corses();// load the agency to the list view
        }

        private void list_V_Course_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem itmes = list_V_Course.SelectedItems[0];

            Course_ID.Text = itmes.SubItems[0].Text;
            Course_Name.Text = itmes.SubItems[1].Text;
            Course_Fee.Text = itmes.SubItems[2].Text;

            if (itmes.SubItems[3].Text == "1")
            {
                CkDeactivated.Checked = false;
            }

            if (itmes.SubItems[3].Text == "0")
            {
                CkDeactivated.Checked = true;
            }

            RbUp.Enabled = true;
            RbNew.Checked = false;
            Desable_All();
           


        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {
            if (RbUp.Checked == true)
            {
                Enable_All();
                CkDeactivated.Enabled = true;

                BtnSave.Text = "Update";
            }

            //if (RbUp.Checked == false)
            //{
            //    Desable_All();
            //    CkDeactivated.Enabled = false;
            //}
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            if (RbNew.Checked == true)
            {
                Enable_All();

                getCreate_Agency_Code();

                RbUp.Enabled = false;
                RbUp.Checked = false;
                chbAllView.Checked = false;

                Clear_all();

                BtnSave.Text = "Save";
            }

           
        }

        private void list_V_Course_ForeColorChanged(object sender, EventArgs e)
        {
            
        }

        private void Course_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Course_Fee.Focus();
            }
        }

        private void Course_Fee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BtnSave.Focus();
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (RbNew.Checked == true)
            {
                Enable_All();

                getCreate_Agency_Code();

                RbUp.Enabled = false;
                RbUp.Checked = false;
                chbAllView.Checked = false;

                Clear_all();

                BtnSave.Text = "Save";
            }
        }
    }
}
