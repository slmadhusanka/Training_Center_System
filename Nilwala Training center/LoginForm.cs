using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using Nilwala_Training_center;

namespace Inventory_Control_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserName = "";
        public string UPassword = "";
       // public string UserID;
        public string UserDisplayName = "";

        

        private void LoginForm_Load(object sender, EventArgs e)
        {
            LgUserName.Focus();

            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = System.Drawing.Color.Transparent;

           // LgUserName.Focus();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void logintoform()
        {

            #region Load the User Administrator=======================================================================

            if (LgUserName.Text == "Jude123" && LgPassWord.Text == "DivyaaZMHo123")
            {
                Main_Form mf = new Main_Form();

                //ID and Displayname Pass to the Form
              //  mf.UserID = "USR0000";
               // mf.UserDisplayName = "SystemAdmin";
                mf.Visible = true;
                this.Hide();

                return;
            }

            // Load the User profile Database=======================================================================
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();

            //=====================================================================================================================
            string SelectUsers = @"SELECT  UserCode, DisplayOn, Passwod, UserName FROM UserProfile WHERE AtiveDeactive='1' AND UserName='" + LgUserName.Text + "' AND Passwod='" + LgPassWord.Text + "'";
            SqlCommand cmd = new SqlCommand(SelectUsers, Conn);

            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (dr.Read())
            {
                UserName = dr[3].ToString();
                UPassword = dr[2].ToString();
                string UserID = dr[0].ToString();
                UserDisplayName = dr[1].ToString();

                //pass value to the class file.......
                Logged_User_Details.UserID = UserID;
                Logged_User_Details.UserDisplayName = UserDisplayName;

                Main_Form mf = new Main_Form();
                mf.Visible = true;

                this.Hide();

            }

            else
            {
                MessageBox.Show("Please enter correct details and try again.", "Error Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LgUserName.Focus();
            }

            #endregion =====================================================================================================

        }

        private void LogOkBtn_Click(object sender, EventArgs e)
        {
            logintoform();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LgUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void LgPassWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void LgUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                LgPassWord.Focus();
            }
        }

        private void LgPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                logintoform();
            }
        }

        private void LogOkBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                logintoform();
            }
        }

        private void LoginForm_Activated(object sender, EventArgs e)
        {
            LgUserName.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void LogOkBtn_Click_1(object sender, EventArgs e)
        {
            logintoform();
        }

        private void LogExitBtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LgUserName_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                LgPassWord.Focus();
            }
        }

        private void LgPassWord_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                logintoform();
            }
        }

        private void LogOkBtn_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                logintoform();
            }
        }
    }
}
