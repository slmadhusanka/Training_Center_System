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
using System.Configuration;


namespace Nilwala_Training_center.Controling
{
    public partial class Back_UP_Form : Form
    {
        public Back_UP_Form()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataReader reader;
        string sql = "";

        private void Btn_Backup_Brwuse_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txt_Backup_Location.Text = dlg.SelectedPath;
            }
        }

        private void btn_Backp_Click(object sender, EventArgs e)
        {
            SqlConnection con3 = new SqlConnection(IMS);
            con3.Open();

            sql = "BACKUP DATABASE " + txt_BataBase.Text + " TO DISK='" + txt_Backup_Location.Text + "\\" + txt_BataBase.Text + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
            command = new SqlCommand(sql, con3);
            command.ExecuteNonQuery();

            MessageBox.Show("DataBase Backup Successfull");

            txt_Backup_Location.Text = "";
        }

        private void Back_UP_Form_Load(object sender, EventArgs e)
        {
            LgUser.Text = Logged_User_Details.UserID;
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
        }
    }
}
