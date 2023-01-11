using Inventory_Control_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nilwala_Training_center;

using Nilwala_Training_center.Payments;namespace Nilwala_Training_center
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Add_New.Batch_Details());
            Application.Run(new LoginForm());
          // Application.Run(new Report_Form.MainCashbook());
           //ApplicD:\jude sir backup\Nilwalaa\2015-04-30 After\Nilwala Training center\Nilwala Training center\Program.csation.Run(new Report_Form.FRMBatchcreditPayment());

            //
        }
    }
}
