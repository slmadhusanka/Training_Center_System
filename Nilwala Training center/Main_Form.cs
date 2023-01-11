using Inventory_Control_System;
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
using DevExpress.XtraReports.Design;
using Nilwala_Training_center.Report_Form;
using Nilwala_Training_center.Payments;


namespace Nilwala_Training_center
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
            timer1.Start();
        }

        User_Cotrol UserCont = new User_Cotrol();

        private void Main_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Select_User_Settings()
        {
            try
            {
                // UserCont.User_Setting

                SqlDataReader dr = UserCont.User_Setting();
                if (dr.Read())
                {
                   // MessageBox.Show("ead user setting");
                   // MessageBox.Show(dr[2].ToString());

                    #region File Menu----
                    //addnew user
                    if (dr[2].ToString() == "0")
                    {

                        Tile_New_User.Enabled = false;
                    }
                    if (dr[2].ToString() == "1")
                    {
                        Tile_New_User.Enabled = true;
                    }

                    //AddNewItem-----
                    if (dr[3].ToString() == "0")
                    {
                        Tile_New_Agency.Enabled = false;
                    }
                    if (dr[3].ToString() == "1")
                    {
                        Tile_New_Agency.Enabled = true;
                    }

                    //GRN-----
                    if (dr[4].ToString() == "0")
                    {
                        Tile_New_Course.Enabled = false;
                    }
                    if (dr[4].ToString() == "1")
                    {
                        Tile_New_Course.Enabled = true;
                    }

                    //Change Item Price
                    if (dr[5].ToString() == "0")
                    {
                        Tile_Batch_Details.Enabled = false;
                    }
                    if (dr[5].ToString() == "1")
                    {
                        Tile_Batch_Details.Enabled = true;
                    }

                    //BArcode
                    if (dr[6].ToString() == "0")
                    {
                        Tile_New_Bank.Enabled = false;
                    }
                    if (dr[6].ToString() == "1")
                    {
                        Tile_New_Bank.Enabled = true;
                    }

                    //new Invoice
                    if (dr[7].ToString() == "0")
                    {
                        Tile_Trainee_Reg.Enabled = false;
                    }
                    if (dr[7].ToString() == "1")
                    {
                        Tile_Trainee_Reg.Enabled = true;
                    }

                    //Re print invoice....
                    if (dr[8].ToString() == "0")
                    {
                        Tile_Batch_Payments.Enabled = false;
                    }
                    if (dr[8].ToString() == "1")
                    {
                        Tile_Batch_Payments.Enabled = true;
                    }

                    //repair Job
                    if (dr[9].ToString() == "0")
                    {
                        Tile_Set_Off.Enabled = false;
                    }
                    if (dr[9].ToString() == "1")
                    {
                        Tile_Set_Off.Enabled = true;
                    }

                    //repair Job  reprint 
                    if (dr[10].ToString() == "0")
                    {
                        Title_Petty_Cash.Enabled = false;
                    }
                    if (dr[10].ToString() == "1")
                    {
                        Title_Petty_Cash.Enabled = true;
                    }

                    //GRN Paymet details 
                    if (dr[11].ToString() == "0")
                    {
                        Tile_Deposit_Details.Enabled = false;
                    }
                    if (dr[11].ToString() == "1")
                    {
                        Tile_Deposit_Details.Enabled = true;
                    }

                    //Customer Credit payments
                    if (dr[12].ToString() == "0")
                    {
                        Tile_User_Control.Enabled = false;
                    }
                    if (dr[12].ToString() == "1")
                    {
                        Tile_User_Control.Enabled = true;
                    }

                    //CRepair JOB Add
                    if (dr[13].ToString() == "0")
                    {
                        Tile_DB_Backup.Enabled = false;
                    }
                    if (dr[13].ToString() == "1")
                    {
                        Tile_DB_Backup.Enabled = true;
                    }

                    //Repaird Items
                    if (dr[14].ToString() == "0")
                    {
                        Tile_Rpt_Main_Cash.Enabled = false;
                    }
                    if (dr[14].ToString() == "1")
                    {
                        Tile_Rpt_Main_Cash.Enabled = true;
                    }

                    //JOB Payments add
                    if (dr[15].ToString() == "0")
                    {
                        Tile_Rpt_Petty_Cash.Enabled = false;
                    }
                    if (dr[15].ToString() == "1")
                    {
                        Tile_Rpt_Petty_Cash.Enabled = true;
                    }

                    //Add customer
                    if (dr[16].ToString() == "0")
                    {
                        Tile_Rpt_Batch_PAyments.Enabled = false;
                    }
                    if (dr[16].ToString() == "1")
                    {
                        Tile_Rpt_Batch_PAyments.Enabled = true;
                    }

                    //Add vendor
                    if (dr[17].ToString() == "0")
                    {
                        Tile_Rpt_Cheque.Enabled = false;
                    }
                    if (dr[17].ToString() == "1")
                    {
                        Tile_Rpt_Cheque.Enabled = true;
                    }

                    //Add petty cash
                    if (dr[18].ToString() == "0")
                    {
                        Tile_Rpt_Bank_Details.Enabled = false;
                    }
                    if (dr[18].ToString() == "1")
                    {
                        Tile_Rpt_Bank_Details.Enabled = true;
                    }

                    //new

                    if (dr[19].ToString() == "1")
                    {
                        Otherexpenses.Enabled = true;
                    }
                    if (dr[19].ToString() == "0")
                    {
                        Otherexpenses.Enabled = false;
                    }//--------------------------------------------------

                    if (dr[20].ToString() == "1")
                    {
                        rptOtherExpensesBook.Enabled = true;
                    }
                    if (dr[20].ToString() == "0")
                    {
                        rptOtherExpensesBook.Enabled = false;
                    }//--------------------------------------------------
                    if (dr[21].ToString() == "1")
                    {
                        rptpettycashBook.Enabled = true;
                    }
                    if (dr[21].ToString() == "0")
                    {
                        rptpettycashBook.Enabled = false;
                    }//--------------------------------------------------
                    if (dr[22].ToString() == "1")
                    {
                        rptMainCashBook.Enabled = true;
                    }
                    if (dr[22].ToString() == "0")
                    {
                        rptMainCashBook.Enabled = false;
                    }//--------------------------------------------------


                    #endregion
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            LgDisplayName1.Text = Logged_User_Details.UserDisplayName;
            LgUser1.Text = Logged_User_Details.UserID;

            Select_User_Settings();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime DT = DateTime.Now;
            //DateTime Date=DateTime.

            this.TxtDateTime.Text = DT.ToShortTimeString();
            this.Txt_date.Text = DT.ToLongDateString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoginForm lg = new LoginForm();
            lg.Show();

            this.Hide();
        }

        private void Tile_New_User_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            UserProfile UP = new UserProfile();
            UP.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Tile_New_Agency_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Add_New.New_Agency nwA = new Add_New.New_Agency();
            nwA.Show();
        }

        private void tileItem17_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Add_New.New_Course nwc = new Add_New.New_Course();
            nwc.Show();
        }

        private void tileItem10_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Add_New.Batch_Details bt = new Add_New.Batch_Details();
            bt.Show();
        }

        private void Tile_New_Bank_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Add_New.New_Bank bnk = new Add_New.New_Bank();
            bnk.Show();
        }

        private void Tile_Set_Off_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                Payments.SET_OFF stOf = new Payments.SET_OFF();
                stOf.Show();
            }
            catch (Exception ex)
            { 
                
            }
        }

        private void Tile_Deposit_Details_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Payments.Deposit depos = new Payments.Deposit();
            depos.Show();
        }

        private void Title_Petty_Cash_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Payments.Petty_Cash ptc = new Payments.Petty_Cash();
            ptc.Show();
        }

        private void Tile_Trainee_Reg_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Payments.Course_ID reg = new Payments.Course_ID();
            reg.Show();
        }

        private void Tile_Batch_Payments_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Payments.Batch_Payments btc = new Payments.Batch_Payments();
            btc.Show();
        }

        private void Tile_User_Control_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Controling.User_Control cn = new Controling.User_Control();
            cn.Show();
        }

        private void Tile_DB_Backup_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Controling.Back_UP_Form bk= new Controling.Back_UP_Form();
            bk.Show();
        }

        private void Tile_Rpt_Petty_Cash_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Frm_Petty_cash pt = new Frm_Petty_cash();
            pt.Show();
        }

        private void Tile_Rpt_Bank_Details_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Frm_Bank_Balance bk = new Frm_Bank_Balance();
            bk.Show();
        }

        private void Tile_Rpt_Main_Cash_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Profit_AND_Lost pp = new Profit_AND_Lost();
            pp.Show();
        }

        private void Tile_Rpt_Cheque_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Report_Form.Cheque_Details ppq = new Report_Form.Cheque_Details();
            ppq.Show();
        }

        private void Tile_Rpt_Batch_PAyments_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Cusomer_Credit_Details cc = new Cusomer_Credit_Details();
            cc.Show();
        }

        private void tileItem2_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //Payments.Deposit depos = new Payments.Deposit();
            //depos.Show();

            OtherExpenses cdf = new OtherExpenses();
            cdf.Show();

            
        }

        private void tileItem3_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Report_Form.FRMPettyCashBook amr = new Report_Form.FRMPettyCashBook();
            amr.Show();
        }

        private void tileItem4_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Report_Form.FRMOtherExpenses rty = new Report_Form.FRMOtherExpenses();
            rty.Show();

        }

        private void tileItem6_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Report_Form.MainCashbook rty1 = new Report_Form.MainCashbook();
            rty1.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            LoginForm lg = new LoginForm();
            lg.Show();

            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
