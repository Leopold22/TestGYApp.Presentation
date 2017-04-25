using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestGYApp.Presentation
{
    public partial class ClientDispForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int clientID = int.Parse(Request.QueryString["id"]);
            var clients = Business.ClientsManager.GetClients();

            NameTextBox.Text = clients.Find(item => item.id == clientID).cl_name;
            PhoneTextBox.Text = clients.Find(item => item.id == clientID).tel_number;
            BirthDateTextBox.Text = clients.Find(item => item.id == clientID).date_of_birth.ToString();
            EmailTextBox.Text = clients.Find(item => item.id == clientID).email;
            CommentTextBox.Text = clients.Find(item => item.id == clientID).primechanie;

        }
    }
}