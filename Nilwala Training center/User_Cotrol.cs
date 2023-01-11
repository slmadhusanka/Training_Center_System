using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.IO;

namespace Nilwala_Training_center
{
    class User_Cotrol
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public SqlDataReader User_Setting()
        {
            string LoginID = Logged_User_Details.UserID;

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();

            string Select_Details = @"SELECT     Auto_ID,User_ID, New_User, New_Agency, New_Course, New_Batch, New_Bank, Trainee_Registration, Batch_Payments, Set_Off, Petty_Cash, 
                                    Cash_Deposit, User_Control, User_Backup, Rpt_Main_Cash, Rpt_Petty_Cash, Rpt_Batch_Payments, Rpt_Chk_Deposit, Rpt_Bank_Details,Other_Expenses,rpt_Other_Expenses,rpt_pettycashBook,rpt_MainCashbook
                                    FROM User_Settings WHERE User_ID='" + LoginID + "'";

            SqlCommand com = new SqlCommand(Select_Details, Conn);
            SqlDataReader dr = com.ExecuteReader();
            return dr;
        }
    }
}
