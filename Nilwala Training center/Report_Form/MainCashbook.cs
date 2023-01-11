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
    public partial class MainCashbook : Form
    {
        public MainCashbook()
        {
            InitializeComponent();
        }


        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        String totalTables = "";
         
        private void MainCashbook_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

       
        }
        DataRow r1;
        String Status = "";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String AgencyName = "";

                //select Set_Off_Details.Set_Off_Date,Set_Off_Details.DOC_ID,Set_Off_Details.Status,Set_Off_Details.Invoiced_Tot,Set_Off_Details.Bank_amount,Set_Off_Details.Remain_Balance from Set_Off_Details where Set_Off_Date between '2015-04-28 12:13:18.000' and'2015-05-28 12:13:18.000'
                //select the print datatables number----------------

                string date_to = Convert.ToDateTime(dateTimePicker2.Text.Trim()).AddDays(1).ToShortDateString();


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
                DataTable t1 = ds1.Tables.Add("Set_Off_DetailsCashBookNewTable");
                t1.Columns.Add("Set_Off_Date", Type.GetType("System.String"));
                t1.Columns.Add("DOC_ID", Type.GetType("System.String"));
                t1.Columns.Add("Status", Type.GetType("System.String"));
                t1.Columns.Add("Invoiced_Tot", Type.GetType("System.String"));
                t1.Columns.Add("Bank_amount", Type.GetType("System.String"));
                t1.Columns.Add("Remain_Balance", Type.GetType("System.String"));
                t1.Columns.Add("Agerncyname", Type.GetType("System.String"));

                SqlConnection cnn5 = new SqlConnection(IMS);
                cnn5.Open();
                String Cars = @"select Set_Off_Details.Set_Off_Date,Set_Off_Details.DOC_ID,Set_Off_Details.Status,Set_Off_Details.Invoiced_Tot,Set_Off_Details.Bank_amount,Set_Off_Details.Remain_Balance from Set_Off_Details where Set_Off_Date between '" + dateTimePicker1.Text + "' and '" + date_to + "' order by AutoID asc";
                SqlCommand cmm5 = new SqlCommand(Cars, cnn5);
                SqlDataReader ds = cmm5.ExecuteReader();
                while (ds.Read())
                {
                    r1 = t1.NewRow();
                    r1["Set_Off_Date"] = ds[0].ToString();
                    r1["DOC_ID"] = ds[1].ToString();

                    SqlConnection Conn3 = new SqlConnection(IMS);
                    Conn3.Open();
                    string sql3 = "select Set_Off_Details.Set_Off_Date,Set_Off_Details.DOC_ID,Set_Off_Details.Status,Set_Off_Details.Invoiced_Tot,Set_Off_Details.Bank_amount,Set_Off_Details.Remain_Balance from Set_Off_Details where Set_Off_Details.DOC_ID='" + ds[1].ToString() + "'";
                    SqlCommand cmm3 = new SqlCommand(sql3, Conn3);
                    SqlDataReader dr3 = cmm3.ExecuteReader();
                    while (dr3.Read())
                    {
                        string Sta = dr3[2].ToString();

                       

                        if (Sta == "Main_Cash_Debit")
                        {
                            //MessageBox.Show("Main_Cash_Debit");

                            Status = "Main Cash to Petty Cash";
                        }

                        else if (Sta == " Invoice Payment ")//space avilablr in the both side. othe wise it doesn't work.
                        {
                           // MessageBox.Show("Invoice Payments: " + ds[1].ToString());

                            // Status = " Invoice Payment ";
                            SqlConnection Conn33 = new SqlConnection(IMS);
                            Conn33.Open();
                            string sql33 = "select GRN_No,Trainee_Registration_DOC_Details.Agency_Name from Agency_Payment_Doc_Details inner join Trainee_Registration_DOC_Details on Trainee_Registration_DOC_Details.DOC_ID=Agency_Payment_Doc_Details.GRN_No where Agency_Payment_Doc_Details.Docu_No='" + ds[1].ToString() + "'";
                            SqlCommand cmm23 = new SqlCommand(sql33, Conn3);
                            SqlDataReader dr23 = cmm23.ExecuteReader();
                            if (dr23.Read())
                            {
                               
                                Status = " Invoice Payment - " + dr23[1].ToString();

                               // MessageBox.Show("status when : "+ Status);

                            }
                        }
                        else if (Sta == "SET_OFF")
                        {
                            Status = " Invoice Payment ";
                        }
                        else if (Sta == "Deposit")
                        {
                            Status = "Deposit";
                        }
                        else if (Sta == "Main_Cash_Debit_To_Other_Expenses")
                        {
                           // MessageBox.Show("status when Main_Cash_Debit_To_Other_Expenses");
                            Status = "Main Cash to Other Expenses";
                        }

                        //r1["Agerncyname"] = dr3[1].ToString();

                        r1["Status"] = Status;
                    }




                    r1["Invoiced_Tot"] = ds[3].ToString();
                    r1["Bank_amount"] = ds[4].ToString();
                    r1["Remain_Balance"] = ds[5].ToString();

                    SqlConnection Conn2 = new SqlConnection(IMS);
                    Conn2.Open();
                    string sql2 = "select GRN_No,Trainee_Registration_DOC_Details.Agency_Name from Agency_Payment_Doc_Details inner join Trainee_Registration_DOC_Details on Trainee_Registration_DOC_Details.DOC_ID=Agency_Payment_Doc_Details.GRN_No where Agency_Payment_Doc_Details.Docu_No='" + ds[1].ToString() + "'";
                    SqlCommand cmm2 = new SqlCommand(sql2, Conn2);
                    SqlDataReader dr2 = cmm2.ExecuteReader();
                    if (dr2.Read())
                    {
                        AgencyName = dr2[1].ToString();

                        //.Show(dr2[1].ToString());

                        r1["Agerncyname"] = dr2[1].ToString();
                    }

                    t1.Rows.Add(r1);

                    //MessageBox.Show(dr5[2].ToString()+ "card");
                }




                Report.MainCashBook rpt = new Report.MainCashBook();



                TextObject start, end;

                if (rpt.ReportDefinition.ReportObjects["Text7"] != null)
                {
                    start = (TextObject)rpt.ReportDefinition.ReportObjects["Text7"];
                    start.Text = dateTimePicker1.Text;
                }

                if (rpt.ReportDefinition.ReportObjects["Text6"] != null)
                {
                    end = (TextObject)rpt.ReportDefinition.ReportObjects["Text6"];
                    end.Text = dateTimePicker2.Text;
                }



                rpt.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
                viwerMainCashBook_Details.ReportSource = rpt;
                viwerMainCashBook_Details.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //=====================================================================================================================



        }
    }
}
