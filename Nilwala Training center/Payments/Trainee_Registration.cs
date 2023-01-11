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
using System.Data.Sql;
using System.IO;
using System.Security.Cryptography;


namespace Nilwala_Training_center.Payments
{
    public partial class Course_ID : Form
    {
        public Course_ID()
        {
            InitializeComponent();
        }

        string _My_DB_CON = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;


        int i = 0;

        public void getCreate_Trainee_Code()
        {
            #region getCreate_Trainee_Code...........................................
            try
            {


                SqlConnection Conn = new SqlConnection(_My_DB_CON);
                Conn.Open();


                //=====================================================================================================================
                //  string sql = "select OrderID from CurrentStockItems";
                string sql = "SELECT DOC_ID FROM Trainee_Registration_DOC_Details";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    trainee_DOC_ID.Text = "TRN1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    // string sql1 = " SELECT TOP 1 OrderID FROM CurrentStockItems order by OrderID DESC";
                    string sql1 = " SELECT Top 1 DOC_ID FROM Trainee_Registration_DOC_Details order by DOC_ID DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    if (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string OrderNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(OrderNumOnly) + 1).ToString();

                        trainee_DOC_ID.Text = "TRN" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        public void Load_Course_Details()
        {
            #region Load_Course_Details............................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

               // List_V_Reg_Trainee_Courses.Items.Clear();

                string Course_Load = @"SELECT Course_ID, Course_Name, Course_Fee
                                       FROM  Course_Details WHERE  Course_Status='1' ORDER BY Course_ID ASC";

                 SqlCommand cmd = new SqlCommand(Course_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    
                    //List_V_Reg_Trainee_Courses.Items.Add(li);
                }

               
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the Load Course Details", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            #endregion
        }


        public void add_Course_to_Txt()
        {
            #region add course to txt........................

          //  ListViewItem lst = List_V_Reg_Trainee_Courses.SelectedItems[0];

          //  Reg_Trainee_Course_ID.Text = lst.SubItems[0].Text;
          //  Reg_Trainee_Course_Name.Text = lst.SubItems[1].Text;
          //  Reg_Trainee_Course_Fee.Text = lst.SubItems[2].Text;

          // // Reg_Trainee_Course_ID.Enabled = true;
          ////  Reg_Trainee_Course_Name.Enabled = true;
          //  Reg_Trainee_Course_Fee.Enabled = true;

            #endregion
        }

        public void Select_Available_Batch_IDs()
        {
            #region Select_Available_Batch_IDs........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                cmb_BatchID.Items.Clear();

                string Batch_Load = @"SELECT Batch_ID
                                    FROM Batch_Details WHERE Batch_Status='1' and  Course_ID='" + Course_New_ID .Text+ "' and End_Date>'" + DateTime.Now.ToShortDateString() + "' ORDER BY Batch_ID ASC";

                SqlCommand cmd = new SqlCommand(Batch_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmb_BatchID.Items.Add(dr[0].ToString());
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the Load Select_Available_Batch_IDs Details", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }

        public void Select_Available_Batch_Details()
        {
            #region Select_Available_Batch_Details........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                string Batch_Load = @"SELECT Start_Date, End_Date,Total_Trainees FROM Batch_Details WHERE Batch_ID='"+cmb_BatchID.Text+"' ";

                SqlCommand cmd = new SqlCommand(Batch_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    DateTime a = Convert.ToDateTime(dr[0].ToString());
                    DateTime b = Convert.ToDateTime(dr[1].ToString());

                    double Total_Trainees = Convert.ToDouble(dr[2].ToString());
                   // double Reserved_Seats = Convert.ToDouble(dr[3].ToString());

                    //double available_Seats = Total_Trainees - Reserved_Seats;

                    Batch_S_Date.Text= a.ToShortDateString();
                    Batch_E_Date.Text = b.ToShortDateString();
                    Batch_Tot_seats.Text = Total_Trainees.ToString();

                    //Batch_Avail_Seats.Text = available_Seats.ToString();
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the Load Select_Available_Batch_Details Details", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }

        public void Select_Agency()
        {
            #region Select_Agency........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                cmb_Reg_Trainee_Agency.Items.Clear();

                string Agency_Load = @"SELECT Agency_Name FROM Agency_Details WHERE Agency_Status='1'";

                SqlCommand cmd = new SqlCommand(Agency_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    cmb_Reg_Trainee_Agency.Items.Add(dr[0].ToString());
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the Load Select_Agency Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }

        public void Select_Agency_ID()
        {
            #region Select_Agency_ID........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

               // cmb_Reg_Trainee_Agency.Items.Clear();

                string Agency_Load = @"SELECT   Ajecy_ID FROM Agency_Details WHERE Agency_Name='" + cmb_Reg_Trainee_Agency.Text+ "'";

                SqlCommand cmd = new SqlCommand(Agency_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                   Agency_ID.Text= (dr[0].ToString());
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("This error came from the Load Select_Agency_ID Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }



        public void Clear_Course_Datails()
        {
            #region Clear_Course_Datails........................

            Cmb_Course.SelectedIndex = -1;
            Course_Fee.Text = "0.00";
            Course_New_ID.Text = "-";

            #endregion

        }

        public void Clear_Trainee_Datails()
        {
            #region Clear_Trainee_Datails........................

            Reg_Trainee_Name.Text = "";
            Reg_Trainee_Tel_Num.Text = "";
            Reg_Trainee_Address.Text = "";

            #endregion

        }

        public void Clear_BAtch_Datails()
        {
            #region Clear_BAtch_Datails........................

            cmb_BatchID.SelectedIndex = -1;
            Batch_Avail_Seats.Text = "00";
            Batch_Tot_seats.Text = "00";
            Batch_E_Date.Text = "-";
            Batch_S_Date.Text = "-";

            #endregion

        }

        public void Clear_All()
        {
            #region Clear_Other........................

            cmb_Reg_Trainee_Agency.SelectedIndex = -1;



            Clear_Course_Datails();
            Clear_Trainee_Datails();
            Clear_BAtch_Datails();

            grpB_tot_Details.Enabled = false;

            List_Final_Details.Items.Clear();

            RbNew.Checked = true;
            RbNew.Enabled = true;

            RbUp.Enabled = false;
            RbUp.Checked = false;

            lbl_Trainee_Count.Text = "0";
            Total_Amount.Text = "00.00";
            
            #endregion

        }


        public void check_Trainees_seat_count()
        {
            #region check_Trainees_seat_count..............................

            #region selcet availabe seat count in the database..........................

            double Total_Trainees = 0;
            double Reserved_Seats = 0;
            double current_Total_seats = 0;

            SqlConnection Conn = new SqlConnection(_My_DB_CON);
            Conn.Open();

            string sql = @"SELECT Total_Trainees,Reserved_Seats FROM Batch_Details WHERE Batch_ID='" + cmb_BatchID.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read())
            {
               
                Total_Trainees = Convert.ToDouble(dr[0].ToString());
                Reserved_Seats = Convert.ToDouble(dr[1].ToString());

            }

            //count total Items in the same batch in the list view..................................
            double gtotal = 0;
            foreach (ListViewItem lstItem in List_Final_Details.Items)
            {
                if (lstItem.SubItems[0].Text == cmb_BatchID.Text)
                {
                    gtotal += 1;
                }
               
            }

            current_Total_seats = (Total_Trainees - (Reserved_Seats + gtotal));

            Batch_Avail_Seats.Text = current_Total_seats.ToString();


            if (Conn.State == ConnectionState.Open)
            {
                cmd.Dispose();
                dr.Close();
                Conn.Close();
            }
            #endregion

            #endregion
        }

        public void GetTotalAmount()
        {
            #region GetTotalAmount.....................................................

            try
            {

                decimal gtotal = 0;
                foreach (ListViewItem lstItem in List_Final_Details.Items)
                {
                    gtotal += Math.Round(decimal.Parse(lstItem.SubItems[5].Text), 2);
                }
                Total_Amount.Text = Convert.ToString(gtotal);
                //MessageBox.Show(Convert.ToString(gtotal));


                if (List_Final_Details.Items.Count == 0)
                {
                    BtnSave.Enabled = false;

                }
                else
                {
                    BtnSave.Enabled = true;
                }

                lbl_Trainee_Count.Text = Convert.ToString(List_Final_Details.Items.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is an error in get total amount. please contact you System Administrator", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

        }


        public void Update_trainee_count()
        {
            #region update student count........................

            double Saved_reseved_seats = 0;

            SqlConnection Conn = new SqlConnection(_My_DB_CON);
            Conn.Open();
            //=====================================================================================================================
            //  string sql = "select OrderID from CurrentStockItems";
            string sql = @"SELECT Reserved_Seats FROM Batch_Details WHERE Batch_ID='" + List_Final_Details.Items[i].SubItems[0].Text + "'";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read())
            {
                Saved_reseved_seats = Convert.ToDouble(dr[0].ToString());
            }

            if (Conn.State == ConnectionState.Open)
            {
                cmd.Dispose();
                dr.Close();
                Conn.Close();
            }

            //update details...........................

            string New_Count = Convert.ToString(Saved_reseved_seats + 1); //add 01 to save count in the batch....

            SqlConnection con2 = new SqlConnection(_My_DB_CON);
            con2.Open();

            string BAtch_Update = @"UPDATE  Batch_Details SET Reserved_Seats ='" + New_Count + "' WHERE Batch_ID='" + List_Final_Details.Items[i].SubItems[0].Text + "'";
            SqlCommand cmd2 = new SqlCommand(BAtch_Update, con2);
            cmd2.ExecuteNonQuery();


            //=====================================================================================================================
            #endregion
        }

        public void load_Course_Name()
        {
            #region load_Course_Name...........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                Cmb_Course.Items.Clear();

                string Course_Load = @"SELECT Course_Name FROM Course_Details WHERE Course_Status='1'";

                SqlCommand cmd = new SqlCommand(Course_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cmb_Course.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the load_Course_ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        public void Agency_Credit_Balance_Update()
        {
            try
            {
                #region Agency_Credit_Balance_Update--------------------------

                double LastBalance = 0;
                double New_Bal = 0;
                string Deb_Bal = "";

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                string CusCreditBalane = "SELECT TOP (1) Balance,Debit_Balance FROM RegCusCredBalance WHERE (CusID = '" + Agency_ID.Text + "') ORDER BY AutoNum DESC";
                SqlCommand cmd1 = new SqlCommand(CusCreditBalane, con1);
                SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //dataGridView1.Rows.Clear();

                if (dr2.Read() == true)
                {
                    LastBalance = Convert.ToDouble(dr2[0].ToString());
                    Deb_Bal = dr2[1].ToString();

                }
                //check the balesce >0 or <0-----------------------------------

                #region  Customer Previos Remainder is Possitive Value----------------------------

                //balance calc-----------------------------
                New_Bal = LastBalance + Convert.ToDouble(Total_Amount.Text);

                SqlConnection con1x = new SqlConnection(_My_DB_CON);
                con1x.Open();

                string Cus_DebitPaymet = @"INSERT INTO RegCusCredBalance( CusID, DocNumber, Credit_Amount, Debit_Amount,Debit_Balance, Balance, Date) 
                                                VALUES  ('" + Agency_ID.Text + "','" + trainee_DOC_ID.Text + "','" + Total_Amount.Text + "','0','" + Deb_Bal + "','" + Convert.ToString(New_Bal) + "','" + DateTime.Now.ToString() + "')";

                // MessageBox.Show(Cus_DebitPaymet);
                SqlCommand cmd21 = new SqlCommand(Cus_DebitPaymet, con1x);
                cmd21.ExecuteNonQuery();

                if (con1x.State == ConnectionState.Open)
                {
                    con1x.Close();
                }

                #endregion


                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error1", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Trainee_Registration_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = Logged_User_Details.UserDisplayName;
            LgUser.Text = Logged_User_Details.UserID;

            getCreate_Trainee_Code();
            Load_Course_Details();
            RbNew.Checked = true;

            RbNew.Checked = true;
            RbNew.Enabled = true;

            RbUp.Enabled = false;
            RbUp.Checked = false;

            Select_Agency();//load agency drop down
            load_Course_Name();//load course names to drop down

            cmb_Reg_Trainee_Agency.Focus();

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            Pnl_DOC_Serch.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Pnl_DOC_Serch.Visible = true;

            try
            {
                List_V_DOC.Items.Clear();
                ckAll.Checked = false;

                if (ckAll.Checked == false)
                {
                   #region without deactivated DOCs...........................

                 SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                string Load_Details = @"SELECT TRDD.DOC_ID, TR.Batch_ID, TRDD.Agency_Name,TRDD.Agency_ID, TR.Trainee_Name, TR.Trainee_Address, TR.Trainee_TP, 
                                        BD.Course_ID, BD.Course_Name, TR.Course_Fee,TRDD.DOC_Status
                                        FROM Trainee_Registration TR INNER JOIN
                                        Trainee_Registration_DOC_Details TRDD ON TR.DOC_ID = TRDD.DOC_ID INNER JOIN
                                        Batch_Details BD ON TR.Batch_ID = BD.Batch_ID WHERE TRDD.DOC_Status='1'";

                SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                List_V_DOC.Items.Clear();

                while (dr.Read())
                {
                    ListViewItem li;

                    li = new ListViewItem(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());
                    li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[10].ToString());
                   

                    List_V_DOC.Items.Add(li);
                }

                #endregion
                }

           }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the Load_DOC Setails", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalAmount();
        }

        private void btn_Add_to_Course_Click(object sender, EventArgs e)
        {

        }

        private void List_V_Reg_Trainee_Courses_DoubleClick(object sender, EventArgs e)
        {
            add_Course_to_Txt();
        }

        private void cmb_BatchID_Click(object sender, EventArgs e)
        {
            
        }

        private void cmb_BatchID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Available_Batch_Details();

            check_Trainees_seat_count();
        }

        private void List_V_Reg_Trainee_Courses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Reg_Trainee_Agency_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Agency_ID();

            if (cmb_Reg_Trainee_Agency.Text != "")
            {
                grpB_tot_Details.Enabled = true;
               
            }

            else
            {
               

                Clear_Course_Datails();
                Clear_BAtch_Datails();
                Clear_Trainee_Datails();

                cmb_Reg_Trainee_Agency.Focus();
                cmb_Reg_Trainee_Agency.DroppedDown = true;

                grpB_tot_Details.Enabled = false;
                
            }

        }

        private void cmb_Reg_Trainee_Agency_Click(object sender, EventArgs e)
        {
            
        }

        private void List_View_Selected_Course_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_Course_Click(object sender, EventArgs e)
        {
            
        }

        private void Cmb_Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region load_Course_ID...........................

            try
            {

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();



                string Course_Load = @"SELECT Course_ID,Course_Fee FROM Course_Details WHERE Course_Name='" + Cmb_Course.Text + "'";

                SqlCommand cmd = new SqlCommand(Course_Load, con1);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Course_New_ID.Text = dr[0].ToString();
                    Course_Fee.Text=  dr[1].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the load_Course_ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion

            cmb_BatchID.SelectedIndex = -1;
            Batch_Avail_Seats.Text = "0";
            Batch_E_Date.Text = "-";
            Batch_S_Date.Text = "-";
            Batch_Tot_seats.Text = "0";

            if (Cmb_Course.Text != "")
            {
                grpB_Batch.Enabled = true;
            }
            else
            {
                grpB_Batch.Enabled = false;
            }

            Select_Available_Batch_IDs();
        }

        private void Btn_Add_to_Final_Click(object sender, EventArgs e)
        {
            #region check missing things.................................

                if(Reg_Trainee_Name.Text=="")
                {
                    MessageBox.Show("Please fill trainee's Name","Error name",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Reg_Trainee_Name.Focus();
                    return;
                }

                if (Reg_Trainee_Address.Text == "")
                {
                    MessageBox.Show("Please fill trainee's Address", "Error Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reg_Trainee_Address.Focus();
                    return;
                }

                if (Reg_Trainee_Tel_Num.Text == "")
                {
                    MessageBox.Show("Please fill trainee's Telephone Number", "Error Tel_number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reg_Trainee_Tel_Num.Focus();
                    return;
                }

                if (Cmb_Course.Text == "")
                {
                    MessageBox.Show("Please select a course", "Error Tel_number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cmb_Course.Focus();
                    return;
                }

                if (Course_Fee.Text == "")
                {
                    MessageBox.Show("Please add course fee", "Error fees", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course_Fee.Focus();
                    return;
                }

                if (cmb_BatchID.Text == "")
                {
                    MessageBox.Show("Please select a batch", "Error Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmb_BatchID.Focus();
                    return;
                }

            #endregion

            #region add to list view.......................................

            ListViewItem li;

            li = new ListViewItem(cmb_BatchID.Text.ToString());

            li.SubItems.Add(Reg_Trainee_Name.Text.ToString());
            li.SubItems.Add(Reg_Trainee_Address.Text.ToString());
            li.SubItems.Add(Reg_Trainee_Tel_Num.Text.ToString());
            li.SubItems.Add(Cmb_Course.Text.ToString());
            li.SubItems.Add(Course_Fee.Text.ToString());
           
            List_Final_Details.Items.Add(li);

            #endregion

            Clear_Course_Datails();
            Clear_BAtch_Datails();
           // Clear_Trainee_Datails();

            GetTotalAmount();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
               

                if (RbNew.Checked == true)
                {
                    getCreate_Trainee_Code();
                    

                    Agency_Credit_Balance_Update();

                    #region save details..............

                    #region insert in to DOC Details........................

                    SqlConnection CNN1 = new SqlConnection(_My_DB_CON);
                    CNN1.Open();

                    String Add_New_BAtch = @"INSERT INTO  Trainee_Registration_DOC_Details(  DOC_ID, Agency_Name, Agency_ID, Total_Fee, Credit_Amount, Remain_amount, Add_User, Time_Stamp,DOC_Status)
                                    VALUES('" + trainee_DOC_ID.Text + "','" + cmb_Reg_Trainee_Agency.Text + "','" + Agency_ID.Text + "','" + Total_Amount.Text + "','" + Total_Amount.Text + "','"+Total_Amount.Text+"','" + LgUser.Text + "','" + DateTime.Now.ToString() + "','1')";

                    SqlCommand cmm2 = new SqlCommand(Add_New_BAtch, CNN1);
                    cmm2.ExecuteNonQuery();

                    #endregion

                    #region add list view details to the list view............

                    for (i = 0; i <= List_Final_Details.Items.Count - 1; i++)
                    {
                        #region list view insert.......................

                        SqlConnection con3 = new SqlConnection(_My_DB_CON);
                        con3.Open();
                        string Insert_to_DB = "INSERT INTO Trainee_Registration(DOC_ID, Batch_ID, Trainee_Name, Trainee_Address, Trainee_TP, Course_Fee, Trainee_Status) VALUES(@DOC_ID, @Batch_ID, @Trainee_Name, @Trainee_Address, @Trainee_TP, @Course_Fee, @Trainee_Status)";

                        SqlCommand cmd3 = new SqlCommand(Insert_to_DB, con3);

                        //DOC_ID, Batch_ID, Trainee_Name, Trainee_Address, Trainee_TP, Course_Fee, Trainee_Status
                        cmd3.Parameters.AddWithValue("DOC_ID", trainee_DOC_ID.Text);
                        cmd3.Parameters.AddWithValue("Batch_ID", List_Final_Details.Items[i].SubItems[0].Text);
                        cmd3.Parameters.AddWithValue("Trainee_Name", List_Final_Details.Items[i].SubItems[1].Text);
                        cmd3.Parameters.AddWithValue("Trainee_Address", List_Final_Details.Items[i].SubItems[2].Text);
                        cmd3.Parameters.AddWithValue("Trainee_TP", List_Final_Details.Items[i].SubItems[3].Text);
                        cmd3.Parameters.AddWithValue("Course_Fee", List_Final_Details.Items[i].SubItems[5].Text);
                        cmd3.Parameters.AddWithValue("Trainee_Status", '1');

                        cmd3.ExecuteNonQuery();

                        if (con3.State == ConnectionState.Open)
                        {
                            cmd3.Dispose();
                            // dr.Close();
                            con3.Close();
                        }

                        Update_trainee_count();

                        #endregion

                       
                    }

                    #endregion

                    

                    MessageBox.Show("Insert Successful...", "Save Batch Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Report_Form.Register_Training_Report grnfm = new Report_Form.Register_Training_Report();
                    grnfm.RegisterId = trainee_DOC_ID.Text;
                    grnfm.RowCount = lbl_Trainee_Count.Text;
                    grnfm.Show();
                    #endregion
                }

                if (RbUp.Checked == true)
                {

                    #region update details.............................

                    #endregion
                }




                getCreate_Trainee_Code();
                Clear_All();


                cmb_Reg_Trainee_Agency.DroppedDown = false;

                


            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the save button", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void List_Final_Details_DoubleClick(object sender, EventArgs e)
        {
            List_Final_Details.SelectedItems[0].Remove();
            GetTotalAmount();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear_Course_Datails();
            Clear_BAtch_Datails();
             Clear_Trainee_Datails();

             Reg_Trainee_Name.Focus();
        }

        private void Cmb_Course_Leave(object sender, EventArgs e)
        {
            
            if (Cmb_Course.Text != "")
            {
                grpB_Batch.Enabled = true;
            }
            else
            {
                grpB_Batch.Enabled = false;
                Clear_Course_Datails();
            }
        }

        private void cmb_Reg_Trainee_Agency_Leave(object sender, EventArgs e)
        {
            if (cmb_Reg_Trainee_Agency.Text != "")
            {
                grpB_tot_Details.Enabled = true;

            }

            else
            {
               // grpB_tot_Details.Enabled = false;

                Clear_Course_Datails();
                Clear_BAtch_Datails();
                Clear_Trainee_Datails();

                grpB_tot_Details.Enabled = false;
               // cmb_Reg_Trainee_Agency.Focus();
               // cmb_Reg_Trainee_Agency.DroppedDown = true;
            }

        }

        private void Batch_Avail_Seats_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Batch_Avail_Seats.Text) == 0)
            {
                Btn_Add_to_Final.Enabled = false;
            }
            if (Convert.ToDouble(Batch_Avail_Seats.Text) > 0)
            {
                Btn_Add_to_Final.Enabled = true;
            }
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            List_V_DOC.Items.Clear();

            try
            {

                if (ckAll.Checked == false)
                {
                    #region without deactivated DOCs...........................

                    SqlConnection con1 = new SqlConnection(_My_DB_CON);
                    con1.Open();

                    string Load_Details = @"SELECT TRDD.DOC_ID, TR.Batch_ID, TRDD.Agency_Name,TRDD.Agency_ID, TR.Trainee_Name, TR.Trainee_Address, TR.Trainee_TP, 
                                        BD.Course_ID, BD.Course_Name, TR.Course_Fee,TRDD.DOC_Status
                                        FROM Trainee_Registration TR INNER JOIN
                                        Trainee_Registration_DOC_Details TRDD ON TR.DOC_ID = TRDD.DOC_ID INNER JOIN
                                        Batch_Details BD ON TR.Batch_ID = BD.Batch_ID WHERE TRDD.DOC_Status='1'";

                    SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                    List_V_DOC.Items.Clear();

                    while (dr.Read())
                    {
                        ListViewItem li;

                        li = new ListViewItem(dr[0].ToString());
                        li.SubItems.Add(dr[1].ToString());
                        li.SubItems.Add(dr[2].ToString());
                        li.SubItems.Add(dr[3].ToString());
                        li.SubItems.Add(dr[4].ToString());
                        li.SubItems.Add(dr[5].ToString());
                        li.SubItems.Add(dr[6].ToString());
                        li.SubItems.Add(dr[7].ToString());
                        li.SubItems.Add(dr[8].ToString());
                        li.SubItems.Add(dr[9].ToString());
                        li.SubItems.Add(dr[10].ToString());


                        List_V_DOC.Items.Add(li);
                    }

                    #endregion
                }

                if (ckAll.Checked == true)
                {
                    #region with deactivated............

                    SqlConnection con1 = new SqlConnection(_My_DB_CON);
                    con1.Open();

                    string Load_Details = @"SELECT TRDD.DOC_ID, TR.Batch_ID, TRDD.Agency_Name,TRDD.Agency_ID, TR.Trainee_Name, TR.Trainee_Address, TR.Trainee_TP, 
                                        BD.Course_ID, BD.Course_Name, TR.Course_Fee,TRDD.DOC_Status
                                        FROM Trainee_Registration TR INNER JOIN
                                        Trainee_Registration_DOC_Details TRDD ON TR.DOC_ID = TRDD.DOC_ID INNER JOIN
                                        Batch_Details BD ON TR.Batch_ID = BD.Batch_ID";

                    SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                    List_V_DOC.Items.Clear();

                    while (dr.Read())
                    {
                        ListViewItem li;

                        li = new ListViewItem(dr[0].ToString());
                        li.SubItems.Add(dr[1].ToString());
                        li.SubItems.Add(dr[2].ToString());
                        li.SubItems.Add(dr[3].ToString());
                        li.SubItems.Add(dr[4].ToString());
                        li.SubItems.Add(dr[5].ToString());
                        li.SubItems.Add(dr[6].ToString());
                        li.SubItems.Add(dr[7].ToString());
                        li.SubItems.Add(dr[8].ToString());
                        li.SubItems.Add(dr[9].ToString());
                        li.SubItems.Add(dr[10].ToString());


                        List_V_DOC.Items.Add(li);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the ckAll_CheckedChanged", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (ckAll.Checked == false)
                {
                    #region search without selected All...............

                    SqlConnection con1 = new SqlConnection(_My_DB_CON);
                    con1.Open();

                    string Load_Details = @"SELECT TRDD.DOC_ID, TR.Batch_ID, TRDD.Agency_Name,TRDD.Agency_ID, TR.Trainee_Name, TR.Trainee_Address, TR.Trainee_TP, 
                                        BD.Course_ID, BD.Course_Name, TR.Course_Fee,TRDD.DOC_Status
                                        FROM Trainee_Registration TR INNER JOIN
                                        Trainee_Registration_DOC_Details TRDD ON TR.DOC_ID = TRDD.DOC_ID INNER JOIN
                                        Batch_Details BD ON TR.Batch_ID = BD.Batch_ID WHERE TRDD.DOC_Status='1' AND (TRDD.DOC_ID LIKE '%" + txtSearch.Text + "%' OR TR.Batch_ID LIKE '%" + txtSearch.Text + "%' OR TRDD.Agency_Name LIKE '%" + txtSearch.Text + "%' OR TR.Trainee_Name LIKE '%" + txtSearch.Text + "%')";

                    SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                    List_V_DOC.Items.Clear();

                    while (dr.Read())
                    {
                        ListViewItem li;

                        li = new ListViewItem(dr[0].ToString());
                        li.SubItems.Add(dr[1].ToString());
                        li.SubItems.Add(dr[2].ToString());
                        li.SubItems.Add(dr[3].ToString());
                        li.SubItems.Add(dr[4].ToString());
                        li.SubItems.Add(dr[5].ToString());
                        li.SubItems.Add(dr[6].ToString());
                        li.SubItems.Add(dr[7].ToString());
                        li.SubItems.Add(dr[8].ToString());
                        li.SubItems.Add(dr[9].ToString());
                        li.SubItems.Add(dr[10].ToString());


                        List_V_DOC.Items.Add(li);
                    }

                    #endregion
                }

                if (ckAll.Checked == true)
                {
                    #region search without selected All...............

                    SqlConnection con1 = new SqlConnection(_My_DB_CON);
                    con1.Open();

                    string Load_Details = @"SELECT TRDD.DOC_ID, TR.Batch_ID, TRDD.Agency_Name,TRDD.Agency_ID, TR.Trainee_Name, TR.Trainee_Address, TR.Trainee_TP, 
                                        BD.Course_ID, BD.Course_Name, TR.Course_Fee,TRDD.DOC_Status
                                        FROM Trainee_Registration TR INNER JOIN
                                        Trainee_Registration_DOC_Details TRDD ON TR.DOC_ID = TRDD.DOC_ID INNER JOIN
                                        Batch_Details BD ON TR.Batch_ID = BD.Batch_ID WHERE (TRDD.DOC_ID LIKE '%" + txtSearch.Text + "%' OR TR.Batch_ID LIKE '%" + txtSearch.Text + "%' OR TRDD.Agency_Name LIKE '%" + txtSearch.Text + "%' OR TR.Trainee_Name LIKE '%" + txtSearch.Text + "%')";

                    SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                    SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                    List_V_DOC.Items.Clear();

                    while (dr.Read())
                    {
                        ListViewItem li;

                        li = new ListViewItem(dr[0].ToString());
                        li.SubItems.Add(dr[1].ToString());
                        li.SubItems.Add(dr[2].ToString());
                        li.SubItems.Add(dr[3].ToString());
                        li.SubItems.Add(dr[4].ToString());
                        li.SubItems.Add(dr[5].ToString());
                        li.SubItems.Add(dr[6].ToString());
                        li.SubItems.Add(dr[7].ToString());
                        li.SubItems.Add(dr[8].ToString());
                        li.SubItems.Add(dr[9].ToString());
                        li.SubItems.Add(dr[10].ToString());


                        List_V_DOC.Items.Add(li);
                    }

                    #endregion
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the search details", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void List_V_DOC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                #region add to list view...............

            List_Final_Details.Items.Clear();

                string _DOC_ID = "";

                ListViewItem itmes = List_V_DOC.SelectedItems[0];

                _DOC_ID = itmes.SubItems[0].Text;

                //MessageBox.Show(_DOC_ID);

                #region search without selected All...............

                SqlConnection con1 = new SqlConnection(_My_DB_CON);
                con1.Open();

                string Load_Details = @"SELECT TRDD.DOC_ID, TR.Batch_ID, TRDD.Agency_Name,TRDD.Agency_ID, TR.Trainee_Name, TR.Trainee_Address, TR.Trainee_TP, 
                                        BD.Course_ID, BD.Course_Name, TR.Course_Fee,TRDD.DOC_Status
                                        FROM Trainee_Registration TR INNER JOIN
                                        Trainee_Registration_DOC_Details TRDD ON TR.DOC_ID = TRDD.DOC_ID INNER JOIN
                                        Batch_Details BD ON TR.Batch_ID = BD.Batch_ID WHERE TRDD.DOC_ID='"+_DOC_ID+"' ";

                SqlCommand cmd1 = new SqlCommand(Load_Details, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

              

                while (dr.Read())
                {
                    ListViewItem li;

                  //  MessageBox.Show("Selected");

                    li = new ListViewItem(dr[1].ToString());//Batch ID
                    li.SubItems.Add(dr[4].ToString());//Tr_Name
                    li.SubItems.Add(dr[5].ToString());//tr_Address
                    li.SubItems.Add(dr[6].ToString());//tr_TP
                    li.SubItems.Add(dr[8].ToString());//course_Name
                    li.SubItems.Add(dr[9].ToString());//Fee
                    li.SubItems.Add(dr[7].ToString());//course_ID

                    trainee_DOC_ID.Text=(dr[0].ToString());//doc_ID
                    cmb_Reg_Trainee_Agency.Text=(dr[2].ToString());//Agency_Name
                    Agency_ID.Text=(dr[3].ToString());//Agen_ID
                    li.SubItems.Add(dr[10].ToString());//Status

                    List_Final_Details.Items.Add(li);
                }

                #endregion

                Pnl_DOC_Serch.Visible = false;

                RbNew.Checked = false;
                RbNew.Enabled = true;

                RbUp.Checked = false;
                RbUp.Enabled = true;

                ckAll.Checked = false;

                grpB_tot_Details.Enabled = false;
                grpB_Agency.Enabled = false;

                Clear_Trainee_Datails();
                Clear_Course_Datails();
                Clear_BAtch_Datails();

                GetTotalAmount();

                List_Final_Details.Enabled = false;

                BtnSave.Enabled = false;

            

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("This error came from the select the data add to the final list view", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            if (RbNew.Checked == true)
            {

                Clear_All();

                getCreate_Trainee_Code();
                grpB_Agency.Enabled = true;
                cmb_Reg_Trainee_Agency.DroppedDown = false;

                BtnSave.Text = "Save";
            }
        }

        private void Reg_Trainee_Tel_Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one - point
            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }

        private void Course_Fee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void Reg_Trainee_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                Reg_Trainee_Address.Focus();
            }
        }

        private void Reg_Trainee_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Reg_Trainee_Tel_Num.Focus();
            }
        }

        private void Reg_Trainee_Tel_Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Cmb_Course.Focus();
            }
        }

        private void Cmb_Course_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Course_Fee.Focus();
            }
        }

        private void Course_Fee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                cmb_BatchID.Focus();
            }
        }

        private void cmb_BatchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Btn_Add_to_Final.Focus();
            }
        }

        private void cmb_Reg_Trainee_Agency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Reg_Trainee_Name.Focus();
            }
        }
    }
}
