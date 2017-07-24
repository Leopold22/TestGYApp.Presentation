using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Web;
using System.Globalization;
using System.Linq;

namespace TestGYApp.Presentation
{
    public partial class TestPage : System.Web.UI.Page
        
    {
        public static int pageSize = 25; //кол-во записей на странице

        private void BindGYDataRow()
        {
            //вставляем в пустой грид пустую таблицу с одной строкой
            //остальное будет прорисовано на клиенте
            DataTable clients = new DataTable();
         
           // clients = GetClientsPageGrid("FullName asc", "", "", "", "", "", null, null, "", "",1);
            MyClientsGridView.DataSource = GetClientsPageGrid("FullName asc", "", "", "", "", "", null, null, "", "", 1);
            MyClientsGridView.DataBind();

            //string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex
        }

        //для фильтрации
        [WebMethod]
        public static string GetClients(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, bool getAllItems)
        {
            return Business.ClientsManager.GetClients(sortOrder, searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, searchTermBirthDateFrom, searchTermBirthDateTo, pageIndex, pageSize, getAllItems);
        }
                     


        public static DataSet GetData(SqlCommand cmd, int pageIndex, string sortOrder, bool getAllItems)
        {
            return Business.ClientsManager.GetData(cmd, pageIndex, pageSize, sortOrder, getAllItems);
        }


        //универсализация эксперимент

        [WebMethod]
        public static string GetClientsPageGrid(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex)
        {
            return Business.ClientsManager.GetClientsPageGrid( sortOrder,  searchTermName,  searchTermLastName,  searchTermPatronymic,  searchTermPhone,  searchTermMarketingInfo,  searchTermAgeFrom,  searchTermAgeTo,  searchTermBirthDateFrom,  searchTermBirthDateTo,  pageIndex,  pageSize);
        }








        //Получение таблицы с клиентами для Excel-отчета
        public static DataTable GetClientsForExcel(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo)
        { 
            return Business.ClientsManager.GetClientsForExcel( searchTermName,  searchTermLastName,  searchTermPatronymic,  searchTermPhone,  searchTermMarketingInfo,  searchTermAgeFrom,   searchTermAgeTo,  searchTermBirthDateFrom,  searchTermBirthDateTo);
        }

     







        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGYDataRow();
            }

        }

        protected void MyClientsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

   

        protected void CreateButton_Click(object sender, EventArgs e)
        {
           
        }

        protected void AddClientButton_Click(object sender, ImageClickEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(
           this.GetType(), "OpenWindow", "window.open('/ClientDispForm.aspx','_self');", true);
        }


        public  void Test() {
            FirstNameFilterTextBox.Text = "123";


        }



        protected void ReportButton_Click(object sender, ImageClickEventArgs e)
        {
           // CheckedItemsCollector.Text = "123";

            string CheckedItems = CheckedItemsCollector.Text;

            if (CheckedItems == "")
                {

                return;

            }


            int[] nums = Array.ConvertAll(CheckedItems.Split(','), int.Parse);


           
           ExporttoExcel(nums);

    


        }



        public static void GetExcelReport()
        {

         

        }



        protected void ClearFiltersButton_Click(object sender, ImageClickEventArgs e)
        {

            FirstNameFilterTextBox.Text = "";
            LastNameFilterTextBox.Text = "";
            PatronymicFilterTextBox.Text = "";
            PhoneFilterTextBox.Text = "";
            MarketingInfoDropDownFilter.SelectedValue = "";
            AgeFromFilterTextBox.Text = "";
            AgeToFilterTextBox.Text = "";
            BirthDateFromFilterTextBox.Text = "";
            BirthDateToFilterTextBox.Text = "";



        }

          public override void VerifyRenderingInServerForm(Control control)
    {
        /* проверка рендеринга контрола (?) */
    }





        //private void ExportDataSetToExcel(DataSet ds)
        //{
        //    
        //    Excel.Application excelApp = new Excel.Application();

        //   
        //    Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"E:\test.xlsx");

        //    foreach (DataTable table in ds.Tables)
        //    {
        //       
        //        Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
        //        excelWorkSheet.Name = table.TableName;

        //        for (int i = 1; i < table.Columns.Count + 1; i++)
        //        {
        //            excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
        //        }

        //        for (int j = 0; j < table.Rows.Count; j++)
        //        {
        //            for (int k = 0; k < table.Columns.Count; k++)
        //            {
        //                excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
        //            }
        //        }
        //    }

        //    excelWorkBook.Save();
        //    excelWorkBook.Close();
        //    excelApp.Quit();

        //}


        public class CheckedItem {
            public string itemId { get; set; }

        }



        [WebMethod]
        //public static void WebExporttoExcel(CheckedItem[] CheckedItemArray)

        public static void WebExporttoExcel(string[] test)
        {
            TestPage instance = new TestPage();
            //instance.ExporttoExcel();
        }




      //  [WebMethod]
        protected  void ExporttoExcel(int[] nums)
        {

            

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");


       
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
         
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

     

            int columnscount = MyClientsGridView.Columns.Count; 
           // int columnscount = 7;

            for (int j = 0; j < columnscount; j++)
            {   
                HttpContext.Current.Response.Write(@"<Td bgcolor='#490b41' ' >");
              
                HttpContext.Current.Response.Write("<B>");

                HttpContext.Current.Response.Write(@"<font color=""white"">");
              HttpContext.Current.Response.Write(MyClientsGridView.Columns[j].HeaderText.ToString());
                            

                HttpContext.Current.Response.Write(@"</font>");

                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");

            DataTable clients = new DataTable();    
            clients = GetClientsForExcel("", "", "", "", "", null, null, "", "");
          //  string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo



            foreach (DataRow row in clients.Rows)
            {
                //if (nums.Contains(row.Field<int>("ID")) )
                //      {
                //    HttpContext.Current.Response.Write("<TR>");
                //    for (int i = 0; i < clients.Columns.Count; i++)
                //    {
                //        HttpContext.Current.Response.Write("<Td>");
                //        HttpContext.Current.Response.Write(row[i].ToString());
                //        HttpContext.Current.Response.Write("</Td>");
                //    }

                //    HttpContext.Current.Response.Write("</TR>");

                //}


                
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < clients.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }

                    HttpContext.Current.Response.Write("</TR>");

               



            }

            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        protected void SortTestButton_Click(object sender, ImageClickEventArgs e)
        {




        }
    }
}

