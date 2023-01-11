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
    public partial class Register_Training_Report : Form
    {
        string _My_DB_CON = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string RegisterId = "";
        public string RowCount = "";


        public Register_Training_Report()
        {
            InitializeComponent();
        }

        private void Register_Training_Report_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            string totalTables = "";

          //  MessageBox.Show(RegisterId);

              #region report number..................................................
            try
            {
                

                //select the print datatables number----------------
                SqlConnection conx = new SqlConnection(_My_DB_CON);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error1", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
              #endregion


            SqlConnection cnn1 = new SqlConnection(_My_DB_CON);
            cnn1.Open();
            String Register = @"SELECT Trainee_Registration.DOC_ID, Trainee_Registration.Batch_ID, Trainee_Registration.Trainee_Name, Trainee_Registration.Trainee_Address, 
                         Trainee_Registration.Trainee_TP, Trainee_Registration.Course_Fee, Trainee_Registration.Trainee_Status, Trainee_Registration_DOC_Details.Agency_Name, 
                         Trainee_Registration_DOC_Details.Agency_ID,
                         Trainee_Registration_DOC_Details.Remain_amount
                         FROM Trainee_Registration INNER JOIN
                         Trainee_Registration_DOC_Details ON Trainee_Registration.DOC_ID = Trainee_Registration_DOC_Details.DOC_ID where Trainee_Registration.DOC_ID='" + RegisterId + "'";

            SqlDataAdapter drc = new SqlDataAdapter(Register, cnn1);
            My_Data_SET sd = new My_Data_SET();
            drc.Fill(sd);

            Report.RegisterTrainningReport rpt1 = new Report.RegisterTrainningReport();
            rpt1.SetDataSource(sd.Tables[Convert.ToInt32(totalTables)]);
            ViwerRegisterTraineereport.ReportSource = rpt1;
            ViwerRegisterTraineereport.Refresh();
            cnn1.Close();

            
        }
    }
}
