using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using System.Data.Sql;
using System.Configuration;
using System.Drawing.Printing;
using Nilwala_Training_center.Report;
using Nilwala_Training_center;

namespace Inventory_Control_System
{
    public partial class Profit_AND_Lost : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public Profit_AND_Lost()
        {
            InitializeComponent();
        }

        double GrnTot = 0.00;
        double PayCash = 0.00;
        double Cheque = 0.00;
        double Credi_Card = 0.00;
      //  double Debit_Card = 0.00;
        double Credit = 0.00;
        double Other_Expence_tot = 0.00;
        double tot_petty_cash = 0.00;


        public void total_Invoice_Payment_Details()
        {
            try
            {
                #region total_Invoice_Payment_Details-----------------------------------------------------------

                SqlConnection conx = new SqlConnection(IMS);
                conx.Open();

                string xyz = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                string ReSelecttableNumbers = @"SELECT sum(IPD.GrandTotal) AS Grand_Total, SUM(IPD.PayCash) AS Pay_By_Cash, SUM(IPD.PayCheck) AS Pay_By_Cheque, SUM(IPD.PayCrditCard) AS Credit_Card, SUM(IPD.PAyCredits) AS As_Credit
                                            FROM InvoicePaymentDetails IPD full join  Agency_Payment_DOC_Num APDD
                                            ON  APDD.Agency_DOC_ID=IPD.InvoiceID  
                                            WHERE (IPD.InvoiceDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + xyz + "')";


                //#####################......this code is the old one.. here the problem is repair paymet balance not display.......#...............................................
                //            string ReSelecttableNumbers = @"SELECT sum(InvoicePaymentDetails.GrandTotal) AS Grand_Total, SUM(InvoicePaymentDetails.PayCash) AS Pay_By_Cash, SUM(InvoicePaymentDetails.PayCheck) AS Pay_By_Cheque, SUM(InvoicePaymentDetails.PayCrditCard) AS Credit_Card, SUM(InvoicePaymentDetails.PayDebitCard) AS Debit_Card, SUM(InvoicePaymentDetails.PAyCredits) AS As_Credit
                //                                            FROM InvoicePaymentDetails Inner join SoldInvoiceDetails 
                //                                            ON SoldInvoiceDetails.InvoiceNo=InvoicePaymentDetails.InvoiceID 
                //                                            WHERE (SoldInvoiceDetails.InvoiceStatus='Sold') AND (InvoicePaymentDetails.InvoiceDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + PickerDateTo.Text + "')";


                SqlCommand cmdx = new SqlCommand(ReSelecttableNumbers, conx);
                SqlDataReader drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection);

                if (drx.Read() == true)
                {
                    #region add to read data to report-----------------------------------------

                    if (drx[0].ToString() != "")
                    {
                        GrnTot = Convert.ToDouble(drx[0].ToString());
                    }
                    else
                    {
                        GrnTot = 0.00;
                    }

                    if (drx[1].ToString() != "")
                    {
                        PayCash = Convert.ToDouble(drx[1].ToString());
                    }
                    else
                    {
                        PayCash = 0.00;
                    }

                    if (drx[2].ToString() != "")
                    {
                        Cheque = Convert.ToDouble(drx[2].ToString());
                    }
                    else
                    {
                        Cheque = 0.00;
                    }

                    if (drx[3].ToString() != "")
                    {
                        Credi_Card = Convert.ToDouble(drx[3].ToString());
                    }
                    else
                    {
                        Credi_Card = 0.00;
                    }

                    //if (drx[4].ToString() != "")
                    //{
                    //    Debit_Card = Convert.ToDouble(drx[4].ToString());
                    //}
                    //else
                    //{
                    //    Debit_Card = 0.00;
                    //}

                    if (drx[4].ToString() != "")
                    {
                        Credit = Convert.ToDouble(drx[4].ToString());
                    }
                    else
                    {
                        Credit = 0.00;
                    }


                    #endregion

                }

                if (conx.State == ConnectionState.Open)
                {
                    conx.Close();
                    drx.Close();
                }



                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_1", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        
        }

        public void total_Other_Expences_Amount()
        {
            try
            {
                #region total_Other_Expences_Amount btween dates.............................................

                SqlConnection conx = new SqlConnection(IMS);
                conx.Open();

                string xyz = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                string Return_Tot = @" SELECT SUM(OtherExpenses.Paid_amount-OtherExpenses.Received_Amount) AS Total 
                                FROM OtherExpensesReason LEFT JOIN   OtherExpenses
                                ON OtherExpenses.Reason=OtherExpensesReason.Reason WHERE ptDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + xyz + "'";

                SqlCommand cmdx = new SqlCommand(Return_Tot, conx);
                SqlDataReader drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection);

                if (drx.Read() == true)
                {
                    string valv = "";
                    if (drx[0].ToString() == "")
                    {
                        valv = "0";
                    }

                    if (drx[0].ToString() != "")
                    {
                        valv = drx[0].ToString();
                    }

                    Other_Expence_tot = Convert.ToDouble(valv);
                }

                drx.Close();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_2", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        public void calct_tptal_petty_Cash()
        {
            try
            {
                tot_petty_cash = 0.00;

                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @"SELECT      Reason_for_pettyCash.Reason,SUM(Petty_Cash.Paid_amount-Petty_Cash.Received_Amount) AS Total 
                                FROM        Reason_for_pettyCash LEFT JOIN   Petty_Cash
                                ON Petty_Cash.Reason=Reason_for_pettyCash.Reason WHERE ptDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + PickerDateTo.Text + "' GROUP BY Reason_for_pettyCash.Reason";

                SqlCommand cmdy = new SqlCommand(ReSelectQ, con1);
                SqlDataReader dry = cmdy.ExecuteReader(CommandBehavior.CloseConnection);

                //  string Cnt = "";

                while (dry.Read())
                {
                    tot_petty_cash = tot_petty_cash + Convert.ToDouble(dry[1].ToString());
                    // MessageBox.Show(tot_petty_cash.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_3", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string totalTables = "";

                //select the print datatables number-----------------------------------------------------
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


                //---------------------------------------------------------------------------------------


                string time = Convert.ToDateTime(PickerDateTo.Text.Trim()).AddDays(1).ToShortDateString();

                Rpt_Profit_and_Lost rpt = new Rpt_Profit_and_Lost();

                //calculatwe ethe total amounts according to the date range-----------------------
                total_Invoice_Payment_Details();

                //calculate the return total----------------------------
                total_Other_Expences_Amount();

                //cal net amount fron invoice..........

                //calc total peetycash amount.....
                calct_tptal_petty_Cash();

                string tot_summary_amount = Convert.ToString(GrnTot - (tot_petty_cash + Other_Expence_tot));


                TextObject Cash;
                TextObject Cheqe;
                TextObject Crd_card;
                //TextObject Dbt_Card;
                TextObject Crdt;
                TextObject Gr_Tot;
                TextObject DateFrom;
                TextObject DateTo;
                TextObject tot_Other_exp;
                TextObject selling_tot_profit;

                #region add data to lables in the report lables-----------------------------

                if (rpt.ReportDefinition.ReportObjects["Text3"] != null)
                {
                    Cash = (TextObject)rpt.ReportDefinition.ReportObjects["Text3"];
                    Cash.Text = Convert.ToString(PayCash);
                }

                if (rpt.ReportDefinition.ReportObjects["Text4"] != null)
                {
                    Cheqe = (TextObject)rpt.ReportDefinition.ReportObjects["Text4"];
                    Cheqe.Text = Convert.ToString(Cheque);
                }
                if (rpt.ReportDefinition.ReportObjects["Text5"] != null)
                {
                    Crd_card = (TextObject)rpt.ReportDefinition.ReportObjects["Text5"];
                    Crd_card.Text = Convert.ToString(Credi_Card);
                }
                //if (rpt.ReportDefinition.ReportObjects["Text6"] != null)
                //{
                //    Dbt_Card = (TextObject)rpt.ReportDefinition.ReportObjects["Text6"];
                //    Dbt_Card.Text = Convert.ToString(Debit_Card);
                //}
                if (rpt.ReportDefinition.ReportObjects["Text7"] != null)
                {
                    Crdt = (TextObject)rpt.ReportDefinition.ReportObjects["Text7"];
                    Crdt.Text = Convert.ToString(Credit);
                }
                if (rpt.ReportDefinition.ReportObjects["Text8"] != null)
                {
                    Gr_Tot = (TextObject)rpt.ReportDefinition.ReportObjects["Text8"];
                    Gr_Tot.Text = Convert.ToString(GrnTot);
                }
                //....
                if (rpt.ReportDefinition.ReportObjects["Text44"] != null)
                {
                    Gr_Tot = (TextObject)rpt.ReportDefinition.ReportObjects["Text44"];
                    Gr_Tot.Text = Convert.ToString(GrnTot);
                }

                //............
                //if (rpt.ReportDefinition.ReportObjects["Text2"] != null)
                //{
                //    Return_amount = (TextObject)rpt.ReportDefinition.ReportObjects["Text2"];
                //    Return_amount.Text = Convert.ToString(Return_tot);
                //}
                //..........................................................
                if (rpt.ReportDefinition.ReportObjects["Text6"] != null)
                {
                    tot_Other_exp = (TextObject)rpt.ReportDefinition.ReportObjects["Text6"];
                    tot_Other_exp.Text = Convert.ToString(Other_Expence_tot);
                }
                //..........................................................

                //---------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text27"] != null)
                {
                    DateFrom = (TextObject)rpt.ReportDefinition.ReportObjects["Text27"];
                    DateFrom.Text = Convert.ToString(PickerDateFrom.Text);
                }
                if (rpt.ReportDefinition.ReportObjects["Text28"] != null)
                {
                    DateTo = (TextObject)rpt.ReportDefinition.ReportObjects["Text28"];
                    DateTo.Text = Convert.ToString(PickerDateTo.Text);
                }
                //---------------------------------------------------------------------



                if (rpt.ReportDefinition.ReportObjects["Text48"] != null)
                {
                    selling_tot_profit = (TextObject)rpt.ReportDefinition.ReportObjects["Text48"];
                    selling_tot_profit.Text = Convert.ToString(tot_summary_amount);
                }

                #endregion


                SqlConnection con11 = new SqlConnection(IMS);
                con11.Open();

                string ReSelectQ1 = @" SELECT OtherExpensesReason.Reason, SUM(OtherExpenses.Paid_amount-OtherExpenses.Received_Amount) AS Total 
                                FROM OtherExpensesReason LEFT JOIN   OtherExpenses
                                ON OtherExpenses.Reason=OtherExpensesReason.Reason  WHERE ptDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + time + "' GROUP BY OtherExpensesReason.Reason";

                //   WHERE InvoicePaymentDetails.InvoiceDate>='" + PickerDateFrom.Text + "' AND InvoicePaymentDetails.InvoiceDate<='" + time + "' AND (SoldInvoiceDetails.InvoiceStatus = 'Sold') AND (InvoicePaymentDetails.InvoiceID LIKE 'INV%') ORDER BY InvoicePaymentDetails.InvoiceID";


                SqlDataAdapter dscmd1 = new SqlDataAdapter(ReSelectQ1, con11);
                My_Data_SET ds1 = new My_Data_SET();
                dscmd1.Fill(ds1);


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();

                string ReSelectQ = @"SELECT      Reason_for_pettyCash.Reason,SUM(Petty_Cash.Paid_amount-Petty_Cash.Received_Amount) AS Total 
                                FROM        Reason_for_pettyCash LEFT JOIN   Petty_Cash
                                ON Petty_Cash.Reason=Reason_for_pettyCash.Reason WHERE ptDate BETWEEN '" + PickerDateFrom.Text + "' AND '" + time + "' GROUP BY Reason_for_pettyCash.Reason";

                //   WHERE InvoicePaymentDetails.InvoiceDate>='" + PickerDateFrom.Text + "' AND InvoicePaymentDetails.InvoiceDate<='" + time + "' AND (SoldInvoiceDetails.InvoiceStatus = 'Sold') AND (InvoicePaymentDetails.InvoiceID LIKE 'INV%') ORDER BY InvoicePaymentDetails.InvoiceID";


                SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, con1);
                My_Data_SET ds = new My_Data_SET();
                dscmd.Fill(ds);



                //view the christtal report

                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                rpt.Subreports[1].SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                rpt.Subreports[0].SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
                CrystalReVie_Profit.ReportSource = rpt;
                CrystalReVie_Profit.Refresh();
                con1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error_04", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Profit_AND_Lost_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;
        }
    }
}
