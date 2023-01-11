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
    public partial class Cheque_Details : Form
    {
        public Cheque_Details()
        {
            InitializeComponent();
        }

        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        private void Cheque_Details_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;


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

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String ReSelectQ = @"SELECT     InvoiceCheckDetails.InvoiceID, InvoiceCheckDetails.CkStatus, InvoiceCheckDetails.CkNumber, Bank_Category.BankName, InvoiceCheckDetails.Amount, InvoiceCheckDetails.CurrentDate,InvoiceCheckDetails.MentionDate,Agency_Payment_Doc_Details.GRN_No,Trainee_Registration_DOC_Details.Agency_Name
                                 FROM         InvoiceCheckDetails inner join Agency_Payment_Doc_Details on InvoiceCheckDetails.InvoiceID=Agency_Payment_Doc_Details.Docu_No inner join Trainee_Registration_DOC_Details on Trainee_Registration_DOC_Details.DOC_ID=Agency_Payment_Doc_Details.GRN_No inner join Bank_Category on Bank_Category.BankID=InvoiceCheckDetails.Bank
                                 WHERE     (CkStatus = 'Active') OR (CkStatus = 'Cancel') OR (CkStatus = 'Deposit')";


            SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, cnn);
            DataSet ds = new My_Data_SET();
            dscmd.Fill(ds);


            Report.RPT_Cheque_Details rpt = new Report.RPT_Cheque_Details();
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            viwerCheque_Details.ReportSource = rpt;
            viwerCheque_Details.Refresh();
            

        }
    }
}
