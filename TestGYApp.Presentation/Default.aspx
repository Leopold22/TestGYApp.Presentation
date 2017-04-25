
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestGYApp.Presentation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Список<br />
        <asp:TextBox ID="NameFilterTextBox" runat="server" OnTextChanged="NameFilterTextBox_TextChanged"></asp:TextBox>
&nbsp;<asp:Button ID="ApplyFiltersButton" runat="server" OnClick="ApplyFiltersButton_Click" Text="ОК" />
        <br />
        <br />
        <asp:GridView ID="ClientsGridView" runat="server"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="30" OnPageIndexChanging="ClientsGridView_PageIndexChanging" OnSelectedIndexChanging="ClientsGridView_SelectedIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="/ClientDispForm.aspx?id={0}" DataTextField="cl_name" Text="Имя" AccessibleHeaderText="Имя" HeaderText="Имя" />
                <%--<asp:BoundField DataField="tel_number" DataFormatString="{0: (000)-000-00-00}" HeaderText="Телефон" />--%>
             
                   <asp:TemplateField HeaderText="Телефон" ConvertEmptyStringToNull="False">
                        <ItemTemplate> 
        <asp:Literal ID="litPhone" runat="server" 
            Text='<%# Eval("tel_number")  == null || Eval("tel_number")  == "" ?  "" : string.Format("{0:(###) ###-####}", Int64.Parse(Eval("tel_number").ToString())) %>' />
    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#8250D2" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#8250D2" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    </div>
    </form>
</body>
</html>
