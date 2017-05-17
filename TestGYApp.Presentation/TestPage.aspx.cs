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

namespace TestGYApp.Presentation
{
    public partial class TestPage : System.Web.UI.Page
        
    {
        public static int pageSize = 25; //кол-во записей на странице

        private void BindGYDataRow()
        {
            //вставляем в пустой грид пустую таблицу с одной строкой
            //остальное будет прорисовано на клиенте
            //DataTable clients = new DataTable();
            DataTable clients = new DataTable();
            // clients. = GetClientsForExcel("", "", "", "", "", null, null, 1);
            // GetClientsForExcel("", "", "", "", "", null, null, 1);

            // clients.Rows.Add();
            clients = GetClientsForExcel("", "", "", "", "", null, null, 7);


       //     for (int i = 0; i < clients.Rows.Count; i++)
    // MyClientsGridView.Columns[1].DataP .Data [0, i].Value = clients.Rows[i]["FirstName"]; //of use column index: ...Rows[i][0]; //0 is 1st column in table

            MyClientsGridView.DataSource = GetClientsForExcel("", "", "", "", "", null, null, 7);
            MyClientsGridView.DataBind();
           // MyClientsGridView.
            
        }

        [WebMethod]
        public static string GetClients(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, string searchTermBirthDateFrom, string searchTermBirthDateTo, int pageIndex)
        {     
            return Business.ClientsManager.GetClients(searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, searchTermBirthDateFrom, searchTermBirthDateTo, pageIndex, pageSize);
        }

        public static DataSet GetData(SqlCommand cmd, int pageIndex)
        {
            return Business.ClientsManager.GetData(cmd, pageIndex, pageSize);
        }


        public static DataTable GetClientsForExcel(string searchTermName, string searchTermLastName, string searchTermPatronymic, string searchTermPhone, string searchTermMarketingInfo, int? searchTermAgeFrom, int? searchTermAgeTo, int pageIndex)
        {
            return Business.ClientsManager.GetClientsForExcel(searchTermName, searchTermLastName, searchTermPatronymic, searchTermPhone, searchTermMarketingInfo, searchTermAgeFrom, searchTermAgeTo, pageIndex, pageSize);
        }

        public static DataTable GetDataForExcel(SqlCommand cmd, int pageIndex)
        {
            return Business.ClientsManager.GetDataForExcel(cmd, pageIndex, pageSize);
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

        protected void ReportButton_Click(object sender, ImageClickEventArgs e)
        {


            /*
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            // Response.ContentEncoding = Encoding.UTF8;
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //экспорт всех страниц
                MyClientsGridView.AllowPaging = false;
                this.BindGYDataRow();

                MyClientsGridView.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in MyClientsGridView.HeaderRow.Cells)
                {
                    cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#490b41");
                    cell.ForeColor = System.Drawing.Color.White;
                    //    System.Drawing.Color["0,23, 123, 123"];

                    //["#490b41"];
                }
                foreach (GridViewRow row in MyClientsGridView.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            // cell.BackColor = MyClientsGridView.AlternatingRowStyle.BackColor;
                            cell.BackColor = System.Drawing.Color.White;

                        }
                        else
                        {
                            //cell.BackColor = MyClientsGridView.RowStyle.BackColor;
                            cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#edcee9");

                        }
                        cell.CssClass = "textmode";
                    }
                }

                MyClientsGridView.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            */



            /*
            
            DataTable dt = GetClientsForExcel("", "", "", "", "", null, null, 1); 
            
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            // Response.ContentEncoding = Encoding.UTF8;
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());




            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            // Response.Write(style);
            // Response.Output.Write(sw.ToString());

            TableHeaderCell hcell = new TableHeaderCell();
            hcell.BackColor = System.Drawing.ColorTranslator.FromHtml("#490b41");
           
          

            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
                
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }

           


            //  Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
           // Response.End();

            */

            ExporttoExcel();


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




        private void ExporttoExcel()
        {

            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //// Response.ContentEncoding = Encoding.UTF8;
            //Response.ContentEncoding = System.Text.Encoding.Unicode;
            //Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


            HttpContext.Current.Response.Clear();
           // HttpContext.Current.Response.ClearContent();
           // HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");


//            HttpContext.Current.Response.Write(@"<style rel=""stylesheet"" type=""text / css"">

//th {
//                background-color: #490b41;
//        color: #FFF;
//        font-weight: bold;
//}



//</ style > ");


            // HttpContext.Current.Response.Charset = "utf-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
       
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
         
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
         
            int columnscount = MyClientsGridView.Columns.Count;

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
            clients = GetClientsForExcel("", "", "", "", "", null, null, 7);



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



    }
}

