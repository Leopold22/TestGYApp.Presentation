<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientDispForm.aspx.cs" Inherits="TestGYApp.Presentation.ClientDispForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-3.1.1.min.js"></script>
       <link href="Content/bootstrap.css" rel="stylesheet" />
       <link href="ClientDispFormStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="clientDispForm" runat="server">
     <script src="Scripts/bootstrap.js"></script>

        <div>
            Карточка клиента

            <br />
            <br />
            <div class="cardForm col-lg-3 col-md-3 col-sm-3 col-xs-3" >
                <div class="row">

<div class="tableHolder">
    <table class="clientInfoTable">                            
        <tr>
        <td>Фамилия:&nbsp;</td>
        <td><asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
        <td>Имя:&nbsp;</td>
        <td><asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox></td>
        </tr> 

        <tr>
        <td>Отчество:&nbsp;</td>
        <td><asp:TextBox ID="PatronymicTextBox" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
        <td>Номер телефона:&nbsp;</td>
        <td><asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
        <td>Дата рождения:&nbsp;</td>
        <td><asp:TextBox ID="BirthDateTextBox" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
        <td>E-mail:&nbsp;</td>
        <td><asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
        <td>Примечание:&nbsp;</td>
        <td><asp:TextBox ID="CommentTextBox" runat="server" Height="64px" Width="193px"></asp:TextBox></td>
        </tr>

        <tr>
        <td></td>
        <td> <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" /> 
        &nbsp; &nbsp; &nbsp;
        <asp:Button ID="CancelButton" runat="server" Text="Отмена" />
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
