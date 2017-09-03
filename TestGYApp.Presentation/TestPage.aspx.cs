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
using ClosedXML.Excel;


namespace TestGYApp.Presentation
{

    //public class FilterObject
    //{

    //    public string sortOrder { get; set; }
    //    public string searchTermName { get; set; }
    //    public string searchTermLastName { get; set; }
    //    public string searchTermPatronymic { get; set; }
    //    public string searchTermPhone { get; set; }
    //    public string searchTermMarketingInfo { get; set; }
    //    public int? searchTermAgeFrom { get; set; }
    //    public int? searchTermAgeTo { get; set; }
    //    public string searchTermBirthDateFrom { get; set; }
    //    public string searchTermBirthDateTo { get; set; }
    //    public int pageIndex { get; set; }


    //}


    public partial class TestPage : System.Web.UI.Page

    {
        public static int pageSize = 25; //кол-во записей на странице

        private void BindGYDataRow()
        {
            //вставляем в пустой грид пустую таблицу с одной строкой
            //остальное будет прорисовано на клиенте
            DataTable clients = new DataTable();
            DTO.ClientFilterObject defaultFilters = new DTO.ClientFilterObject();
            MyClientsGridView.DataSource = GetClientsPageGrid(defaultFilters);
            MyClientsGridView.DataBind();
        }


        //для фильтрации
        [WebMethod]
        //public static string GetClients(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, bool getAllItems)
        public static string GetClients(string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, bool getAllItems)

        {

            //string sortOrder, string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex, bool getAllItems
            return Business.ClientsManager.GetClients(sortOrder, searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, searchTermBirthDateFrom, searchTermBirthDateTo, pageIndex, pageSize, getAllItems);
        }


        public static DataSet GetData(SqlCommand cmd, int pageIndex, string sortOrder, bool getAllItems)
        {
            return Business.ClientsManager.GetData(cmd, pageIndex, pageSize, sortOrder, getAllItems);
        }


        //универсализация эксперимент
        [WebMethod]
        public static string GetClientsPageGrid(DTO.ClientFilterObject filters)
        {
            return Business.ClientsManager.GetClientsPageGrid(filters, filters.pageIndex, pageSize);
        }


        //Получение таблицы с клиентами для Excel-отчета
        //public static DataTable GetClientsForExcel(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo)
        public static DataTable GetClientsForExcel(DTO.ClientFilterObject filters)
        {
            return Business.ClientsManager.GetClientsForExcel(filters);
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


        public void Test() {
            FirstNameFilterTextBox.Text = "123";

        }


        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// ////////////////////////////////////////////////////////////////////
        /// 

        protected void ReportButton_Click(object sender, ImageClickEventArgs e)
        {

            //собираем информацию о выбранных элементах в объект
            DTO.CheckedItemsInfo checkedItemsInfo = new DTO.CheckedItemsInfo();
            checkedItemsInfo.checkedItems = CheckedItemsCollector.Text;
            checkedItemsInfo.uncheckedItems = UncheckedItemsCollector.Text;
            checkedItemsInfo.GeneralCheckboxChecked = GeneralCheckbox.Checked;

            //собираем значения фильтров в объект
            DTO.ClientFilterObject filters = new DTO.ClientFilterObject();
            filters.searchTermName = FirstNameFilterTextBox.Text;
            filters.searchTermLastName = LastNameFilterTextBox.Text;
            filters.searchTermPatronymic = PatronymicFilterTextBox.Text;
            filters.searchTermPhone = PhoneFilterTextBox.Text;
            if (!(AgeFromFilterTextBox.Text == "")) { filters.searchTermAgeFrom = int.Parse(AgeFromFilterTextBox.Text);  };
            if (!(AgeToFilterTextBox.Text == "")) { filters.searchTermAgeTo = int.Parse(AgeToFilterTextBox.Text); };

            filters.searchTermMarketingInfo = MarketingInfoDropDownFilter.Text;  ///???

            if (!(BirthDateFromFilterTextBox.Text == "")) { filters.searchTermBirthDateFrom = BirthDateFromFilterTextBox.Text; };
            if (!(BirthDateToFilterTextBox.Text == "")) { filters.searchTermBirthDateTo = BirthDateToFilterTextBox.Text; };

            //filters.searchTermBirthDateFrom = BirthDateFromFilterTextBox.Text;
            //filters.searchTermBirthDateTo = BirthDateToFilterTextBox.Text;


            //получаем выборку с учетом фильтров и выбранных элементов
            DataTable clients = new DataTable();
            clients = Business.ClientsManager.BuildExcelReport(checkedItemsInfo, filters);


            CompileExcelContentTest(clients);
            

        }


        public static void GetExcelReport()
        {
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
        protected void ExporttoExcel(int[] nums)
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

            DataTable clients = new DataTable();
            clients = GetClientsForExcel(new DTO.ClientFilterObject());

            int columnscount = clients.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {
                HttpContext.Current.Response.Write(@"<Td bgcolor='#490b41' ' >");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(@"<font color=""white"">");
                HttpContext.Current.Response.Write(clients.Columns[j].ColumnName.ToString());
                HttpContext.Current.Response.Write(@"</font>");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");


            foreach (DataRow row in clients.Rows)
            {
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





        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////\////////////////////////////////////////////////////////////////////
        //////////////////////////////////////\//\//\//////////////////////////////////////////////////////////////////
        ///////////////////////////////////////\////\////////////////////////////////////////////////////////////////
        ////////////////////////////////////////\///\/////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [WebMethod]
        public static void BuildExcelReport(DTO.CheckedItemsInfo checkedItems)
        {
            DTO.ClientFilterObject filters = new DTO.ClientFilterObject();
            DataTable clients = new DataTable();
            clients = Business.ClientsManager.BuildExcelReport(checkedItems, filters);
            TestPage test = new TestPage();
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            test.CompileExcelContentTest(clients);

          //  checkedItems.checkedItems = Request.Form[];
           // return "Hello!";

        }


        protected void CompileExcelContent(DataTable clients)
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

            int columnscount = clients.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {


                HttpContext.Current.Response.Write(@"<Td bgcolor='#490b41' ' >");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(@"<font color=""white"">");
                HttpContext.Current.Response.Write(clients.Columns[j].ColumnName.ToString());
                HttpContext.Current.Response.Write(@"</font>");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");

            foreach (DataRow row in clients.Rows)
            {
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

        protected void CompileExcelContentTest(DataTable clients)
        {

            //DataTable clients = new DataTable();
            //clients = GetClientsForExcel(new DTO.ClientFilterObject());

            using (XLWorkbook wb = new XLWorkbook())
            {
               // wb.Worksheets.Add(clients, "Клиенты");
                var ws = wb.Worksheets.Add(clients, "Клиенты");
                ws.Row(1).Cells(true).Style.Fill.BackgroundColor = XLColor.FromHtml("#490B41");
                //ws.Cells()

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Отчет.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }


        }





    }
}

