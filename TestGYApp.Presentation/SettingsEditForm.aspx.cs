using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestGYApp.Presentation
{
    public partial class SettingsEditForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {

                    int settingtID = int.Parse(Request.QueryString["id"]);

                    // var setting = Business.ClientsManager.GetClients();

                    //NameTextBox.Text = clients.Find(item => item.ID == clientID).FirstName;
                    //LastNameTextBox.Text = clients.Find(item => item.ID == clientID).LastName;
                    //PatronymicTextBox.Text = clients.Find(item => item.ID == clientID).Patronymic;
                    //PhoneTextBox.Text = clients.Find(item => item.ID == clientID).Phone;
                    //BirthDateTextBox.Text = clients.Find(item => item.ID == clientID).BirthDate.ToString();
                    //EmailTextBox.Text = clients.Find(item => item.ID == clientID).Email;
                    //CommentTextBox.Text = clients.Find(item => item.ID == clientID).Comment;

                    var globalSettings = Business.SettingsManager.GetGlobalSettings();

                    SystemNameTextbox.Text = globalSettings.Find(item => item.ID == settingtID).SystemName;
                    DescriptionTextbox.Text = globalSettings.Find(item => item.ID == settingtID).Description;
                    ValueTextbox.Text = globalSettings.Find(item => item.ID == settingtID).Value;

                }

            }



        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            int settingtID = int.Parse(Request.QueryString["id"]);
            string systemName = SystemNameTextbox.Text;
            string description = DescriptionTextbox.Text;
            string value = ValueTextbox.Text;

            Business.SettingsManager.UpdateGlobalSetting(settingtID, systemName, description, value);

            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);



        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);
        }
    }
}