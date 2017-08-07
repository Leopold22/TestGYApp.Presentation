<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="SettingsEditForm.aspx.cs" Inherits="TestGYApp.Presentation.SettingsEditForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="Scripts/jquery-3.1.1.min.js"></script>
       <link href="Content/bootstrap.css" rel="stylesheet" />
       <link href="SettingsEditFormStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="settingEditForm" runat="server">
     <script src="Scripts/bootstrap.js"></script>

        <div>
            Константы системы

            <br />
            <br />
            <div class="cardForm col-lg-3 col-md-3 col-sm-3 col-xs-3" >
                <div class="row">

<div class="tableHolder">
    <table class="settingItemTable">                            
        <tr>
        <td>Системное название:&nbsp;</td>
        <td><asp:TextBox ID="SystemNameTextbox" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>

        <tr>
        <td>Описание:&nbsp;</td>
        <td><asp:TextBox ID="DescriptionTextbox" runat="server" Height="147px" Width="300px" TextMode="MultiLine"></asp:TextBox></td>
        </tr> 

        <tr>
        <td>Значение:&nbsp;</td>
        <td><asp:TextBox ID="ValueTextbox" runat="server" Height="368px" Width="300px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>

        <tr>
        <td></td>
        <td> <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" /> 
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="CancelButton" runat="server" Text="Отмена" OnClick="CancelButton_Click" />
        </td>
        </tr>


    </table>
</div>


                    </div>

                <div class="row">
                                          

            
                    </div>



            </div>



  



        </div>
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
&nbsp;</p>




</body>
</html>
