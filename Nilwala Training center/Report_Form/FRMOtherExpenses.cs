using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nilwala_Training_center.Report_Form
{
    public partial class FRMOtherExpenses : Form
    {
        public FRMOtherExpenses()
        {
            InitializeComponent();
        }

         string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;



         DataRow r1;
        private void button1_Click(object sender, EventArgs e)
        {
            //string Date_From = dateTimePicker1.Text + " 00:00:00.000";
            string date_to=Convert.ToDateTime(dateTimePicker2.Text.Trim()).AddDays(1).ToShortDateString();

           

            string totalTables = "";
            String Sta = "";


            //select the print datatables number----------------
            SqlConnection conx = new SqlConnection(IMS);
            conx.Open();

            string ReSelecttableNumbers = @"SELECT RptNumbers FROM RptNumbers";
            SqlCommand cmdx = new SqlCommand(ReSelecttableNumbers, conx);
            SqlDataReader drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection);

            if (drx.Read() == true)
            {
                totalTables = drx[0].ToString();
            }

            if (conx.State == ConnectionState.Open)
            {
                conx.Close();
                drx.Close();
            }
            //....................................................................................................

            My_Data_SET ds1 = new My_Data_SET();
            DataTable t1 = ds1.Tables.Add("OtherExpensesBooK2");
            t1.Columns.Add("Current_Date", Type.GetType("System.String"));
            t1.Columns.Add("Petty_Cash_ID", Type.GetType("System.String"));
            t1.Columns.Add("Current_Bal", Type.GetType("System.String"));
            t1.Columns.Add("Update_Status", Type.GetType("System.String"));
            t1.Columns.Add("Reason", Type.GetType("System.String"));
            t1.Columns.Add("Discription", Type.GetType("System.String"));
            t1.Columns.Add("Recevied", Type.GetType("System.String"));
            t1.Columns.Add("Payment", Type.GetType("System.String"));



            SqlConnection cnn5 = new SqlConnection(IMS);
            cnn5.Open();

//            String Cars = @"select  Bal_ID,OtherExpensesBALANCE.[Current_Date],Petty_Cash_ID,Current_Bal,Update_Status,OtherExpenses.Reason,OtherExpenses.Discription,Amount from OtherExpensesBALANCE inner join OtherExpenses on OtherExpenses.PettyCashID=OtherExpensesBALANCE.Petty_Cash_ID  
//                            where [Current_Date] between '" + dateTimePicker1.Text + "'  and '" + date_to + "'  order by Bal_ID asc";

            String Cars = @"SELECT ptDate,PettyCashID,Balance,Reason,Discription,Received_Amount,Paid_amount FROM OtherExpenses
                            where [ptDate] between '" + dateTimePicker1.Text + "'  and '" + date_to + "'  order by peety_ID asc";
            SqlCommand cmm5 = new SqlCommand(Cars, cnn5);
            SqlDataReader ds = cmm5.ExecuteReader();
            while (ds.Read())
            {
                r1 = t1.NewRow();
                r1["Current_Date"] = ds[0].ToString();
                r1["Petty_Cash_ID"] = ds[1].ToString();
                r1["Current_Bal"] = ds[2].ToString();
               // r1["Update_Status"] = ds[3].ToString();
                r1["Update_Status"] = "-";
                r1["Reason"] = ds[3].ToString();
                r1["Discription"] = ds[4].ToString();
                r1["Recevied"] = ds[5].ToString();
                r1["Payment"] = ds[6].ToString();

                #region comment this one........................................

                //SqlConnection cnn51 = new SqlConnection(IMS);
                //cnn51.Open();
                //String Cars1 = "select Update_Status from OtherExpensesBALANCE where Petty_Cash_ID='" + ds[2].ToString() + "' and Bal_ID='" + ds[0].ToString() + "'";
                //SqlCommand cmm51 = new SqlCommand(Cars1, cnn51);
                //SqlDataReader ds11 = cmm51.ExecuteReader();
                //while (ds11.Read())
                //{
                //    Sta = ds11[0].ToString();

                //    if (Sta == "Main_Cash_Debit" || Sta == "Cheque_Debit" || Sta == "Updated")
                //    {
                //        r1["Recevied"] = ds[7].ToString();
                //    }

                //    if (Sta == "Credited")
                //    {
                //        r1["Payment"] = ds[7].ToString();
                //    }
                //}



                //  MessageBox.Show(ds[2].ToString());

                #endregion


                t1.Rows.Add(r1);

            }



            Report.OtherExpensesBook rpt = new Report.OtherExpensesBook();

            TextObject startdate, enddate;

            if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
            {
                startdate = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                startdate.Text = dateTimePicker1.Text;
            }

            if (rpt.ReportDefinition.ReportObjects["Text13"] != null)
            {
                enddate = (TextObject)rpt.ReportDefinition.ReportObjects["Text13"];
                enddate.Text = dateTimePicker2.Text;
            }



            rpt.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
            ViwerexpensesCash.ReportSource = rpt;
            ViwerexpensesCash.Refresh();
            //cnn3.Close();
        }

        private void FRMOtherExpenses_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;
        }
    }
}
