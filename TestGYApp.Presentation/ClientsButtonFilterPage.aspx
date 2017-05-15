<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientsButtonFilterPage.aspx.cs" Inherits="TestGYApp.Presentation.ClientsButtonFilterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    


</head>
<body>
    <form id="buttonFilterForm" runat="server">
    <div>
    
        Список клиентов<br />
        Имя: <asp:TextBox ID="NameFilterTextBox" runat="server" OnTextChanged="NameFilterTextBox_TextChanged"></asp:TextBox>
&nbsp;<asp:Button ID="OkButton" runat="server" Text="OK" />
<%--        <script type="text/javascript">
            function NameFilterTextBox_TextChanged(sender, args) {
                __doPostBack('NameFilterTextBox', '');
             }
 </script>--%>



        <br />
        <br />
        <asp:GridView ID="MyClientsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="true" PageSize="25" DataSourceID="ClientsSqlDataSource" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="MyClientsGridView_PageIndexChanging">
            <Columns>
                <%--<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />--%>
                <%--<asp:BoundField DataField="card_number" HeaderText="card_number" SortExpression="card_number" />--%>
                <%--<asp:BoundField DataField="cl_name" HeaderText="cl_name" SortExpression="cl_name" />--%>
                <%--<asp:BoundField DataField="date_of_birth" HeaderText="date_of_birth" SortExpression="date_of_birth" />--%>
                <%--<asp:BoundField DataField="old_count" HeaderText="old_count" SortExpression="old_count" />--%>
                <%--<asp:CheckBoxField DataField="child" HeaderText="child" SortExpression="child" />--%>
                <%--<asp:CheckBoxField DataField="old_man" HeaderText="old_man" SortExpression="old_man" />--%>
                <%--<asp:BoundField DataField="adress" HeaderText="adress" SortExpression="adress" />--%>
                <%--<asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />--%>
                <%--<asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />--%>
                <%--<asp:BoundField DataField="primechanie" HeaderText="primechanie" SortExpression="primechanie" />--%>
                <%--<asp:CheckBoxField DataField="deleted" HeaderText="deleted" SortExpression="deleted" />--%>
          <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="/ClientDispForm.aspx?id={0}" DataTextField="cl_name" Text="Имя" AccessibleHeaderText="Имя" HeaderText="Имя" />
                
                                   <asp:TemplateField HeaderText="Телефон" ConvertEmptyStringToNull="False">
                        <ItemTemplate> 
        <asp:Literal ID="litPhone" runat="server" 
            Text='<%# Eval("Phone")  == null || Eval("Phone").ToString()  == "" ?  "" : string.Format("{0:(###) ###-####}", Int64.Parse(Eval("Phone").ToString())) %>' />
    </ItemTemplate>
                </asp:TemplateField>

                  </Columns>
<%--            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#7B44D4" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
            <RowStyle BackColor="Blue" ForeColor="White" />
            <AlternatingRowStyle BackColor="#" ForeColor="White" />--%>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#7B44D4" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#7B44D4" ForeColor="White" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
            <RowStyle BackColor="White" ForeColor="Black" />
            <AlternatingRowStyle BackColor="#D5C6EE" ForeColor="Black" />
        </asp:GridView>
        <asp:SqlDataSource ID="ClientsSqlDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GY_ContentConnectionString %>" 
            DeleteCommand="DELETE FROM [] WHERE [id] = @id" 
            InsertCommand="INSERT INTO [] ([card_number], [cl_name], [date_of_birth], [old_count], [child], [old_man], [adress], [Phone], [email], [primechanie], [deleted], [SSMA_TimeStamp]) VALUES (@card_number, @cl_name, @date_of_birth, @old_count, @child, @old_man, @adress, @Phone, @email, @primechanie, @deleted, @SSMA_TimeStamp)" 
            SelectCommand="SELECT * FROM []" 
            UpdateCommand="UPDATE [] SET [card_number] = @card_number, [cl_name] = @cl_name, [date_of_birth] = @date_of_birth, [old_count] = @old_count, [child] = @child, [old_man] = @old_man, [adress] = @adress, [Phone] = @Phone, [email] = @email, [primechanie] = @primechanie, [deleted] = @deleted, [SSMA_TimeStamp] = @SSMA_TimeStamp WHERE [id] = @id"
            FilterExpression="cl_name LIKE'%{0}%'">
            <FilterParameters>
                <asp:ControlParameter Name="cl_name" ControlID="NameFilterTextBox" PropertyName="Text" />
            </FilterParameters>

            <DeleteParameters>
                <asp:Parameter Name="id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="card_number" Type="String" />
                <asp:Parameter Name="cl_name" Type="String" />
                <asp:Parameter DbType="DateTime2" Name="date_of_birth" />
                <asp:Parameter Name="old_count" Type="Int16" />
                <asp:Parameter Name="child" Type="Boolean" />
                <asp:Parameter Name="old_man" Type="Boolean" />
                <asp:Parameter Name="adress" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="primechanie" Type="String" />
                <asp:Parameter Name="deleted" Type="Boolean" />
                <asp:Parameter Name="SSMA_TimeStamp" Type="Object" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="card_number" Type="String" />
                <asp:Parameter Name="cl_name" Type="String" />
                <asp:Parameter DbType="DateTime2" Name="date_of_birth" />
                <asp:Parameter Name="old_count" Type="Int16" />
                <asp:Parameter Name="child" Type="Boolean" />
                <asp:Parameter Name="old_man" Type="Boolean" />
                <asp:Parameter Name="adress" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="primechanie" Type="String" />
                <asp:Parameter Name="deleted" Type="Boolean" />
                <asp:Parameter Name="SSMA_TimeStamp" Type="Object" />
                <asp:Parameter Name="id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
    </div>

        
    </form>
</body>
</html>
