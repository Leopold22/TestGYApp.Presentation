<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientDispForm.aspx.cs" Inherits="TestGYApp.Presentation.ClientDispForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="clientDispForm" runat="server">
    <div>
    
        Карточка клиента<br />
        <br />
        Имя:&nbsp;
        <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Номер телефона:&nbsp;
        <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Дата рождения:&nbsp;
        <asp:TextBox ID="BirthDateTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        E-mail:&nbsp;
        <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Примечание:&nbsp;
        <asp:TextBox ID="CommentTextBox" runat="server" Height="64px" Width="193px"></asp:TextBox>
        <br />
    
    </div>
    </form>
</body>
</html>
