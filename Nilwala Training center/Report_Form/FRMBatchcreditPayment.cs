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
    public partial class FRMBatchcreditPayment : Form
    {

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string InvoiceCreditBalance = "";

        public FRMBatchcreditPayment()
        {
            InitializeComponent();
        }

        private void viwerBatchCreditPayment_Load(object sender, EventArgs e)
        {
            



        }

        String DocNo, checAmount, chequeNo, branch, bank, mentinDate;   //cheque Details...................
        String Cash, Cheque, Credit, sumTotal, PaidAmount, chequeamount;

        TextObject cash, cheque, credit, Sum1, Paidam;

        DataRow r,r1,r2,rs;

        private void FRMBatchcreditPayment_Load(object sender, EventArgs e)
        {
            //  MessageBox.Show(InvoiceCreditBalance);
            string totalTables = "";


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




            String AgenID = "";
            String TopDbitamou = "";




            SqlConnection cnn3 = new SqlConnection(IMS);
            cnn3.Open();
            string AgencySelect = "select InvoicePaymentDetails.InvoiceID,InvoicePaymentDetails.InvoiceDate,InvoicePaymentDetails.SubTotal,InvoicePaymentDetails.GrandTotal,InvoicePaymentDetails.PayCash,InvoicePaymentDetails.PayCheck,InvoicePaymentDetails.PayCrditCard,InvoicePaymentDetails.PAyCredits,Agency_Payment_Doc_Details.GRN_No,Trainee_Registration_DOC_Details.Agency_ID,Trainee_Registration_DOC_Details.Agency_Name,Trainee_Registration_DOC_Details.Remain_amount,Trainee_Registration_DOC_Details.Credit_Amount,InvoiceCheckDetails.CkNumber,InvoiceCheckDetails.Bank,InvoiceCheckDetails.Ck_Bank_acc_Number,InvoiceCheckDetails.ChangeDate  from InvoicePaymentDetails inner join Agency_Payment_Doc_Details on InvoicePaymentDetails.InvoiceID=Agency_Payment_Doc_Details.Docu_No left outer join Trainee_Registration_DOC_Details on Trainee_Registration_DOC_Details.DOC_ID=Agency_Payment_Doc_Details.GRN_No left outer  join InvoiceCheckDetails on InvoiceCheckDetails.InvoiceID=InvoicePaymentDetails.InvoiceID where InvoicePaymentDetails.InvoiceID='" + InvoiceCreditBalance + "'";
            SqlCommand cmm3 = new SqlCommand(AgencySelect, cnn3);
            SqlDataReader dr3 = cmm3.ExecuteReader();
            if (dr3.Read())
            {
                AgenID = dr3[9].ToString();
            }
            //  MessageBox.Show(AgenID);


            SqlConnection cnn2 = new SqlConnection(IMS);
            cnn2.Open();
            String TopDebit = "SELECT TOP (1) Balance,Debit_Balance FROM RegCusCredBalance WHERE (CusID = '" + AgenID + "') ORDER BY AutoNum DESC";
            SqlCommand cmm2 = new SqlCommand(TopDebit, cnn2);
            SqlDataReader dr2 = cmm2.ExecuteReader();
            if (dr2.Read())
            {
                TopDbitamou = dr2[1].ToString();
            }

            //  MessageBox.Show(TopDbitamou);

            My_Data_SET ds1 = new My_Data_SET();
            DataTable t1 = ds1.Tables.Add("Credit");
            t1.Columns.Add("CardAmount", Type.GetType("System.String"));
            t1.Columns.Add("Card_Num", Type.GetType("System.String"));



            SqlConnection cnn5 = new SqlConnection(IMS);
            cnn5.Open();
            String Cars = "select distinct DOC_Number,Card_Amount,Card_No from Agency_Payment_Details where DOC_Number='" + InvoiceCreditBalance + "' and Card_Amount!='0'";
            SqlCommand cmm5 = new SqlCommand(Cars, cnn5);
            SqlDataReader dr5 = cmm5.ExecuteReader();
            while (dr5.Read())
            {
                r1 = t1.NewRow();
                r1["CardAmount"] = dr5[1].ToString();
                r1["Card_Num"] = dr5[2].ToString();


                t1.Rows.Add(r1);

                //MessageBox.Show(dr5[2].ToString()+ "card");
            }

            //  Cheque_details---------------------------------------------------------------------------------------







            My_Data_SET ds11 = new My_Data_SET();
            DataTable t2 = ds11.Tables.Add("BatchCreditBalance2");
            t2.Columns.Add("InvoiceID", Type.GetType("System.String"));
            t2.Columns.Add("PayCash", Type.GetType("System.String"));
            t2.Columns.Add("InvoiceDate", Type.GetType("System.String"));
            t2.Columns.Add("Agency_ID", Type.GetType("System.String"));
            t2.Columns.Add("Agency_Name", Type.GetType("System.String"));




            SqlConnection cnn44 = new SqlConnection(IMS);
            cnn44.Open();
            String Cheque2 = "select distinct InvoicePaymentDetails.InvoiceID,InvoicePaymentDetails.InvoiceDate,InvoicePaymentDetails.SubTotal,InvoicePaymentDetails.GrandTotal,InvoicePaymentDetails.PayCash,InvoiceCheckDetails.Amount,InvoiceCheckDetails.CkNumber,InvoiceCheckDetails.Bank,Agency_Payment_Details.Branch,InvoiceCheckDetails.CurrentDate,InvoiceCheckDetails.MentionDate,InvoicePaymentDetails.PayCrditCard,Agency_Payment_Details.Card_No,InvoicePaymentDetails.PAyCredits,Agency_Payment_Doc_Details.GRN_No,Agency_Payment_Doc_Details.Paid_amount,Trainee_Registration_DOC_Details.Agency_ID,Trainee_Registration_DOC_Details.Agency_Name,Trainee_Registration_DOC_Details.Remain_amount,Trainee_Registration_DOC_Details.Credit_Amount  from InvoicePaymentDetails inner join Agency_Payment_Doc_Details on InvoicePaymentDetails.InvoiceID=Agency_Payment_Doc_Details.Docu_No left outer join Trainee_Registration_DOC_Details on Trainee_Registration_DOC_Details.DOC_ID=Agency_Payment_Doc_Details.GRN_No left outer  join InvoiceCheckDetails on InvoiceCheckDetails.InvoiceID=InvoicePaymentDetails.InvoiceID inner join Agency_Payment_Details on InvoicePaymentDetails.InvoiceID=Agency_Payment_Details.DOC_Number  where InvoicePaymentDetails.InvoiceID='" + InvoiceCreditBalance + "'";
            SqlCommand cmm44 = new SqlCommand(Cheque2, cnn44);
            SqlDataReader dr44 = cmm44.ExecuteReader();
            while (dr44.Read())
            {
                r2 = t2.NewRow();
                r2["InvoiceID"] = dr44[0].ToString();
                r2["PayCash"] = dr44[4].ToString();
                r2["InvoiceDate"] = dr44[1].ToString();
                r2["Agency_ID"] = dr44[16].ToString();
                r2["Agency_Name"] = dr44[17].ToString();

                t2.Rows.Add(r2);
                // MessageBox.Show(dr44[0].ToString());
            }


            //Agency_ID,Trainee_Registration_DOC_Details.Agency_Name
            //Report.BatchCreditPayment rpt12 = new Report.BatchCreditPayment();
            ////report.Subreports["cheque.rpt"].SetDataSource
            //rpt12.SetDataSource(ds11.Tables[Convert.ToInt32(totalTables)]);
            //// rpt1.Subreports[1].SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            //viwerBatchCreditPayment.ReportSource = rpt12;
            //viwerBatchCreditPayment.Refresh();
            //cnn44.Close();




            My_Data_SET ds = new My_Data_SET();
            DataTable t = ds.Tables.Add("ChequeDetails");
            t.Columns.Add("DOC_Number", Type.GetType("System.String"));
            t.Columns.Add("Cheque_Amount", Type.GetType("System.String"));
            t.Columns.Add("Cheque_No", Type.GetType("System.String"));
            t.Columns.Add("Branch", Type.GetType("System.String"));
            t.Columns.Add("Bank", Type.GetType("System.String"));
            t.Columns.Add("MentionDate", Type.GetType("System.String"));


            SqlConnection cnn4 = new SqlConnection(IMS);
            cnn4.Open();
            String Cheque = @"select distinct InvoiceID,Amount,CkNumber,Bank,MentionDate from InvoiceCheckDetails inner join 
                                Agency_Payment_Details on InvoiceCheckDetails.InvoiceID=Agency_Payment_Details.DOC_Number where InvoiceCheckDetails.InvoiceID='" + InvoiceCreditBalance + "'";
            SqlCommand cmm4 = new SqlCommand(Cheque, cnn4);

            SqlDataReader dr4 = cmm4.ExecuteReader();

           

            while (dr4.Read())
            {
                r = t.NewRow();
                r["DOC_Number"] = dr4[0].ToString();
                r["Cheque_Amount"] = dr4[1].ToString();
                r["Cheque_No"] = dr4[2].ToString();
                r["Branch"] = dr4[3].ToString();
                r["MentionDate"] = dr4[4].ToString();

               // MessageBox.Show(dr4[0].ToString());

                string a = "";
                String Cheque_Bank = "select Bank_Category.BankName FROM InvoiceCheckDetails INNER JOIN Bank_Category ON InvoiceCheckDetails.Bank=Bank_Category.BankID WHERE InvoiceCheckDetails.Bank='" + dr4[3].ToString() + "'";
                SqlCommand cmm44z = new SqlCommand(Cheque_Bank, cnn4);

                SqlDataReader dr44z = cmm44z.ExecuteReader();
                if (dr44z.Read())
                {
                    a = dr44z[0].ToString();
                    r["Bank"] = a;
                }

                // sumTotal = (Convert.ToDecimal(Cash) + Convert.ToDecimal(Cheque) + Convert.ToDecimal(Credit)).ToString();

                t.Rows.Add(r);

            }

            //-------------------------------------------------------------------------------

            My_Data_SET dsa = new My_Data_SET();
            DataTable ta = dsa.Tables.Add("Main");
            ta.Columns.Add("Docu_No", Type.GetType("System.String"));
            ta.Columns.Add("GRN_No", Type.GetType("System.String"));
            ta.Columns.Add("Paid_amount", Type.GetType("System.String"));
            ta.Columns.Add("Agency_ID", Type.GetType("System.String"));
            ta.Columns.Add("Agency_Name", Type.GetType("System.String"));
            ta.Columns.Add("Remain_amount", Type.GetType("System.String"));
            ta.Columns.Add("Credit_Amount", Type.GetType("System.String"));

            SqlConnection cnn4s = new SqlConnection(IMS);
            cnn4s.Open();
            String Cheques = "select Docu_No,GRN_No,Paid_amount,Trainee_Registration_DOC_Details.Agency_ID,Trainee_Registration_DOC_Details.Agency_Name,Trainee_Registration_DOC_Details.Remain_amount,Trainee_Registration_DOC_Details.Credit_Amount from Agency_Payment_Doc_Details inner join Trainee_Registration_DOC_Details on Trainee_Registration_DOC_Details.DOC_ID=Agency_Payment_Doc_Details.GRN_No where Docu_No='" + InvoiceCreditBalance + "'";
            SqlCommand cmm4s = new SqlCommand(Cheques, cnn4s);
            SqlDataReader dr4s = cmm4s.ExecuteReader();
            while (dr4s.Read())
            {
                rs = ta.NewRow();
                rs["Docu_No"] = dr4s[0].ToString();
                rs["GRN_No"] = dr4s[1].ToString();
                rs["Paid_amount"] = dr4s[2].ToString();
                rs["Agency_ID"] = dr4s[3].ToString();
                rs["Agency_Name"] = dr4s[4].ToString();
                rs["Remain_amount"] = dr4s[5].ToString();
                rs["Credit_Amount"] = dr4s[6].ToString();

                ta.Rows.Add(rs);


            }



            //Docu_No,GRN_No,Paid_amount,Trainee_Registration_DOC_Details.Agency_ID,Trainee_Registration_DOC_Details.Agency_Name,Trainee_Registration_DOC_Details.Remain_amount,Trainee_Registration_DOC_Details.Credit_Amount
            #region

            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceDate", Type.GetType("System.String"));
            //t.Columns.Add("SubTotal", Type.GetType("System.String"));
            //t.Columns.Add("GrandTotal", Type.GetType("System.String"));
            //t.Columns.Add("PayCash", Type.GetType("System.String"));
            //t.Columns.Add("CkNumber", Type.GetType("System.String"));
            //t.Columns.Add("Bank", Type.GetType("System.String"));
            //t.Columns.Add("Branch", Type.GetType("System.String"));
            //t.Columns.Add("CurrentDate", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));
            //t.Columns.Add("InvoiceID", Type.GetType("System.String"));






            //<Mapping SourceColumn="InvoiceID" DataSetColumn="InvoiceID" />
            // <Mapping SourceColumn="InvoiceDate" DataSetColumn="InvoiceDate" />
            // <Mapping SourceColumn="SubTotal" DataSetColumn="SubTotal" />
            // <Mapping SourceColumn="GrandTotal" DataSetColumn="GrandTotal" />
            // <Mapping SourceColumn="PayCash" DataSetColumn="PayCash" />
            // <Mapping SourceColumn="CkNumber" DataSetColumn="CkNumber" />
            // <Mapping SourceColumn="Bank" DataSetColumn="Bank" />
            // <Mapping SourceColumn="Branch" DataSetColumn="Branch" />
            // <Mapping SourceColumn="CurrentDate" DataSetColumn="CurrentDate" />
            // <Mapping SourceColumn="MentionDate" DataSetColumn="MentionDate" />
            // <Mapping SourceColumn="PayCrditCard" DataSetColumn="PayCrditCard" />
            // <Mapping SourceColumn="Card_No" DataSetColumn="Card_No" />
            // <Mapping SourceColumn="PAyCredits" DataSetColumn="PAyCredits" />
            // <Mapping SourceColumn="GRN_No" DataSetColumn="GRN_No" />
            // <Mapping SourceColumn="Agency_ID" DataSetColumn="Agency_ID" />
            // <Mapping SourceColumn="Agency_Name" DataSetColumn="Agency_Name" />
            // <Mapping SourceColumn="Remain_amount" DataSetColumn="Remain_amount" />
            // <Mapping SourceColumn="Paid_amount" DataSetColumn="Paid_amount" />
            // <Mapping SourceColumn="Amount" DataSetColumn="Amount" />



            //SqlConnection cnn4 = new SqlConnection(IMS);
            //cnn4.Open();
            //String Cheque = "select DOC_Number,Cheque_Amount,Cheque_No,Branch,InvoiceCheckDetails.Bank,InvoiceCheckDetails.MentionDate from Agency_Payment_Details inner join InvoiceCheckDetails on InvoiceCheckDetails.InvoiceID=Agency_Payment_Details.DOC_Number where Agency_Payment_Details.DOC_Number='CCP10000002'";
            //SqlCommand cmm4 = new SqlCommand(Cheque, cnn4);
            //SqlDataReader dr4 = cmm4.ExecuteReader();
            //if(dr4.Read())
            //{
            //    DocNo = dr4[0].ToString();
            //    checAmount = dr4[1].ToString();
            //    chequeNo = dr4[2].ToString();
            //    branch = dr4[3].ToString();
            //    bank = dr4[4].ToString();
            //    mentinDate = dr4[5].ToString();

            //}

            //TextObject DocNot, checAmountt, chequeNot, brancht, bankt, mentinDatet;

            //if (rpt1.ReportDefinition.ReportObjects["Text17"] != null)
            //{
            //    DocNot = (TextObject)rpt1.ReportDefinition.ReportObjects["Text17"];
            //    DocNot.Text = DocNo;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text8"] != null)
            //{
            //    checAmountt = (TextObject)rpt1.ReportDefinition.ReportObjects["Text8"];
            //    checAmountt.Text = checAmount;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text9"] != null)
            //{
            //    chequeNot = (TextObject)rpt1.ReportDefinition.ReportObjects["Text9"];
            //    chequeNot.Text = chequeNo;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text10"] != null)
            //{
            //    brancht = (TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
            //    brancht.Text = branch;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text11"] != null)
            //{
            //    bankt = (TextObject)rpt1.ReportDefinition.ReportObjects["Text11"];
            //    bankt.Text = bank;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text12"] != null)
            //{
            //    mentinDatet = (TextObject)rpt1.ReportDefinition.ReportObjects["Text12"];
            //    mentinDatet.Text = mentinDate;
            //}

            //SqlConnection cnn4 = new SqlConnection(IMS);
            //cnn4.Open();
            //String Cheque = "select PayCash,PayCheck,PayCrditCard from InvoicePaymentDetails where InvoiceID='"+InvoiceCreditBalance+"'";
            //SqlCommand cmm4 = new SqlCommand(Cheque, cnn4);
            //SqlDataReader dr4 = cmm4.ExecuteReader();
            //if(dr4.Read())
            //{
            //    Cash = dr4[0].ToString();
            //    Cheque = dr4[1].ToString();
            //    Credit = dr4[2].ToString();

            //    sumTotal = (Convert.ToDecimal(Cash) + Convert.ToDecimal(Cheque) + Convert.ToDecimal(Credit)).ToString();



            //}

            //    PaidAmount = (Convert.ToDecimal(sumTotal) - Convert.ToDecimal(TopDbitamou)).ToString();

            // MessageBox.Show(PaidAmount);

            //Cash, Cheque, Credit;


            //Report.cheque rpt2 = new Report.cheque();
            //rpt2.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
            //viwerBatchCreditPayment.ReportSource = rpt2;
            //viwerBatchCreditPayment.Refresh();
            //cnn4.Close();

            //Report.cheque report = new Report.cheque();
            //report.Subreports["cheque.rpt"].SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
            //crystalReportViewer1.ReportSource = report;
            //crystalReportViewer1.Refresh();
            //cnn4.Close();

            //Report.cheque report = new Report.cheque();
            //report.Load(path + @"Report\cheque.rpt");
            //report.SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]); //datasource is single
            //crystalReportViewer1.ReportSource = report;


            //ReportDocument myreport = new ReportDocument();
            // myreport.Load("D:\jude sir backup\Nilwalaa\Nilwala Training center\Nilwala Training center\Nilwala Training center\Report\BatchCreditPayment.rpt");

            // ReportDocument subreport = myreport.SubReports[0];
            // DataSet subds = GenerateReportData(subreport.name)
            // myreport.SubReports[0].SetDataSource(subds);











            //SqlConnection cnn1 = new SqlConnection(IMS);
            //cnn1.Open();
            //string user1 = "select distinct InvoicePaymentDetails.InvoiceID,InvoicePaymentDetails.InvoiceDate,InvoicePaymentDetails.SubTotal,InvoicePaymentDetails.GrandTotal,InvoicePaymentDetails.PayCash,InvoiceCheckDetails.Amount,InvoiceCheckDetails.CkNumber,InvoiceCheckDetails.Bank,Agency_Payment_Details.Branch,InvoiceCheckDetails.CurrentDate,InvoiceCheckDetails.MentionDate,InvoicePaymentDetails.PayCrditCard,Agency_Payment_Details.Card_No,InvoicePaymentDetails.PAyCredits,Agency_Payment_Doc_Details.GRN_No,Agency_Payment_Doc_Details.Paid_amount,Trainee_Registration_DOC_Details.Agency_ID,Trainee_Registration_DOC_Details.Agency_Name,Trainee_Registration_DOC_Details.Remain_amount,Trainee_Registration_DOC_Details.Credit_Amount  from InvoicePaymentDetails inner join Agency_Payment_Doc_Details on InvoicePaymentDetails.InvoiceID=Agency_Payment_Doc_Details.Docu_No left outer join Trainee_Registration_DOC_Details on Trainee_Registration_DOC_Details.DOC_ID=Agency_Payment_Doc_Details.GRN_No left outer  join InvoiceCheckDetails on InvoiceCheckDetails.InvoiceID=InvoicePaymentDetails.InvoiceID inner join Agency_Payment_Details on InvoicePaymentDetails.InvoiceID=Agency_Payment_Details.DOC_Number  where InvoicePaymentDetails.InvoiceID='"+InvoiceCreditBalance+"'";
            //SqlDataAdapter de = new SqlDataAdapter(user1, cnn1);
            //My_Data_SET ds = new My_Data_SET();
            //de.Fill(ds);










            //if (rpt1.ReportDefinition.ReportObjects["Text10"] != null)
            //{
            //    cash = (TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
            //    cash.Text = Cash;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text15"] != null)
            //{
            //    cheque = (TextObject)rpt1.ReportDefinition.ReportObjects["Text15"];
            //    cheque.Text = Cheque;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text17"] != null)
            //{
            //    credit = (TextObject)rpt1.ReportDefinition.ReportObjects["Text17"];
            //    credit.Text = Credit;
            //}
            //if (rpt1.ReportDefinition.ReportObjects["Text20"] != null)
            //{
            //    Sum1 = (TextObject)rpt1.ReportDefinition.ReportObjects["Text20"];
            //    Sum1.Text = sumTotal;
            //}


            //SqlConnection cnnch = new SqlConnection(IMS);
            //cnnch.Open();
            //String aaa = "select  Amount from InvoiceCheckDetails where InvoiceID='"+InvoiceCreditBalance+"'";
            //SqlCommand cmmch = new SqlCommand(aaa, cnnch);
            //SqlDataReader drch = cmmch.ExecuteReader();
            //while (drch.Read())
            //{
            //    chequeamount = drch[0].ToString();



            //    //rpt1.Subreports[0].DataDefinition.FormulaFields["new"].Text = " '" + chequeamount + "'";

            //    MessageBox.Show(chequeamount);

            //    //if (rpt1.ReportDefinition.ReportObjects["Text8"] != null)
            //    //{
            //    //    Paidam = (TextObject)rpt1.Subreports[0].ReportDefinition.ReportObjects["Text8"];
            //    //    Paidam.Text = drch[0].ToString();

            //    //    MessageBox.Show(Paidam.Text);
            //    //}
            //}








            //TextObject cashAmo;

            //if (rpt1.ReportDefinition.ReportObjects["Text22"] != null)
            //{
            //    cashAmo = (TextObject)rpt1.ReportDefinition.ReportObjects["Text22"];
            //    cashAmo.Text = "( " + TopDbitamou + " )";
            //}

            #endregion




            Report.BatchCreditPayment rpt1 = new Report.BatchCreditPayment();





            SqlConnection cnn41 = new SqlConnection(IMS);
            cnn41.Open();
            String Cheque41 = "select PayCash,PayCheck,PayCrditCard from InvoicePaymentDetails where InvoiceID='" + InvoiceCreditBalance + "'";
            SqlCommand cmm41 = new SqlCommand(Cheque41, cnn41);
            SqlDataReader dr41 = cmm41.ExecuteReader();
            if (dr41.Read())
            {
                Cash = dr41[0].ToString();
                Cheque = dr41[1].ToString();
                Credit = dr41[2].ToString();

                sumTotal = (Convert.ToDecimal(Cash) + Convert.ToDecimal(Cheque) + Convert.ToDecimal(Credit)).ToString();



            }




            if (rpt1.ReportDefinition.ReportObjects["Text10"] != null)
            {
                cash = (TextObject)rpt1.ReportDefinition.ReportObjects["Text10"];
                cash.Text = Cash;
            }
            if (rpt1.ReportDefinition.ReportObjects["Text15"] != null)
            {
                cheque = (TextObject)rpt1.ReportDefinition.ReportObjects["Text15"];
                cheque.Text = Cheque;
            }
            if (rpt1.ReportDefinition.ReportObjects["Text17"] != null)
            {
                credit = (TextObject)rpt1.ReportDefinition.ReportObjects["Text17"];
                credit.Text = Credit;
            }
            if (rpt1.ReportDefinition.ReportObjects["Text20"] != null)
            {
                Sum1 = (TextObject)rpt1.ReportDefinition.ReportObjects["Text20"];
                Sum1.Text = sumTotal;
            }



            TextObject cashAmo;

            if (rpt1.ReportDefinition.ReportObjects["Text22"] != null)
            {
                cashAmo = (TextObject)rpt1.ReportDefinition.ReportObjects["Text22"];
                cashAmo.Text = "( " + TopDbitamou + " )";
            }











            rpt1.SetDataSource(ds11.Tables[Convert.ToInt32(totalTables)]);
            rpt1.Subreports[2].SetDataSource(dsa.Tables[Convert.ToInt32(totalTables)]);

            rpt1.Subreports[0].SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            rpt1.Subreports[1].SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
            viwerBatchCreditPayment.ReportSource = rpt1;
            viwerBatchCreditPayment.Refresh();
            cnn4.Close();
            cnn44.Close();
            cnn5.Close();
            cnn4s.Close();




            //Report.BatchCreditPayment rpt2 = new Report.BatchCreditPayment();
            ////report.Subreports["cheque.rpt"].SetDataSource
            ////rpt1.Subreports[0].SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            //rpt2.Subreports[1].SetDataSource(ds1.Tables[Convert.ToInt32(totalTables)]);
            //viwerBatchCreditPayment.ReportSource = rpt2;
            //viwerBatchCreditPayment.Refresh();
            //cnn5.Close();







        }
    }
}
