using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace TestGYApp.Presentation
{
    public partial class ClientDispForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int clientID = int.Parse(Request.QueryString["id"]);
                var clients = Business.ClientsManager.GetClients();

                NameTextBox.Text = clients.Find(item => item.ID == clientID).FirstName;
                LastNameTextBox.Text = clients.Find(item => item.ID == clientID).LastName;
                PatronymicTextBox.Text = clients.Find(item => item.ID == clientID).Patronymic;
                PhoneTextBox.Text = clients.Find(item => item.ID == clientID).Phone;
                BirthDateTextBox.Text = clients.Find(item => item.ID == clientID).BirthDate.ToString();
                EmailTextBox.Text = clients.Find(item => item.ID == clientID).Email;
                CommentTextBox.Text = clients.Find(item => item.ID == clientID).Comment;
            }
        }


        protected void OkButton_Click(object sender, EventArgs e)
        {

            //int clientID = int.TryParse(Request.QueryString["id"], out clientID) ? ;


            if (Request.QueryString["id"] != null) //существующая карточка
            {

                int clientID = int.Parse(Request.QueryString["id"]);

                string firstName = NameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string patronymic = PatronymicTextBox.Text; 

                Business.ClientsManager.UpdateClient(clientID, firstName, lastName, patronymic);


            }

            else //новая карточка
            {

                var newClient = new DTO.Client();

                newClient.cl_name = NameTextBox.Text;
                //  newClient.BirthDate = DateTime.Parse(BirthDateTextBox.Text);
                newClient.Comment = CommentTextBox.Text;


                try
                {
                    Business.ClientsManager.AddClient(newClient);
                }
                catch (Exception)
                {
                    throw;

                }

            }

            Page.ClientScript.RegisterStartupScript(
            this.GetType(), "OpenWindow", "window.open('/TestPage.aspx','_self');", true);
        }
    }
}