﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientsLiveFilterPage.aspx.cs" Inherits="TestGYApp.Presentation.ClientsLiveFilterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
        .Pager span
        {
            color: #333;
            background-color: #F7F7F7;
            font-weight: bold;
            text-align: center;
            display: inline-block;
            width: 20px;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #ccc;
        }
        .Pager a
        {
            text-align: center;
            display: inline-block;
            width: 20px;
            border: 1px solid #ccc;
            color: #fff;
            color: #333;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }
        .highlight
        {
            background-color: #FFFFAF;
        }
    </style>


</head>
<body>
    <form id="clientsLiveFilterForm" runat="server">
    <div>
    
        Список клиентов<br />
        Имя: <asp:TextBox ID="NameFilterTextBox" runat="server" OnTextChanged="NameFilterTextBox_TextChanged"></asp:TextBox>
&nbsp;<asp:Button ID="OkButton" runat="server" Text="OK" />
<%--        <script type="text/javascript">
            function NameFilterTextBox_TextChanged(sender, args) {
                __doPostBack('NameFilterTextBox', '');
             }
 </script>--%>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src="ASPSnippets_Pager.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        GetClients(1);
    });
    $("[id*=NameFilterTextBox]").live("keyup", function () {
        GetClients(parseInt(1));
    });
    $(".Pager .page").live("click", function () {
        GetClients(parseInt($(this).attr('page')));
    });
    function SearchTerm() {
        return jQuery.trim($("[id*=NameFilterTextBox]").val());
    };
    function GetClients(pageIndex) {
        $.ajax({
            type: "POST",
            url: "ClientsLiveFilterPage.aspx/GetClients",
            data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
    var row;
    function OnSuccess(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var clients = xml.find("Clients");
        if (row == null) {
            row = $("[id*=MyClientsGridView] tr:last-child").clone(true);
        }
        $("[id*=MyClientsGridView] tr").not($("[id*=MyClientsGridView] tr:first-child")).remove();
        if (clients.length > 0) {
            $.each(clients, function () {
                var customer = $(this);
                $("td", row).eq(0).html($(this).find("cl_name").text());
                $("td", row).eq(1).html($(this).find("Phone").text());
                
                $("[id*=MyClientsGridView]").append(row);
                row = $("[id*=MyClientsGridView] tr:last-child").clone(true);
            });
            var pager = xml.find("Pager");
            $(".Pager").ASPSnippets_Pager({
                ActiveCssClass: "current",
                PagerCssClass: "pager",
                PageIndex: parseInt(pager.find("PageIndex").text()),
                PageSize: parseInt(pager.find("PageSize").text()),
                RecordCount: parseInt(pager.find("RecordCount").text())
            });
 
            $(".cl_name").each(function () {
                var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
            });
        } else {
            var empty_row = row.clone(true);
            $("td:first-child", empty_row).attr("colspan", $("td", row).length);
            $("td:first-child", empty_row).attr("align", "center");
            $("td:first-child", empty_row).html("No records found for the search criteria.");
            $("td", empty_row).not($("td:first-child", empty_row)).remove();
            $("[id*=MyClientsGridView]").append(empty_row);
        }
    };
</script>






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
            DeleteCommand="DELETE FROM [Clients] WHERE [id] = @id" 
            InsertCommand="INSERT INTO [Clients] ([card_number], [cl_name], [date_of_birth], [old_count], [child], [old_man], [adress], [Phone], [email], [primechanie], [deleted], [SSMA_TimeStamp]) VALUES (@card_number, @cl_name, @date_of_birth, @old_count, @child, @old_man, @adress, @Phone, @email, @primechanie, @deleted, @SSMA_TimeStamp)" 
            SelectCommand="SELECT * FROM [Clients]" 
            UpdateCommand="UPDATE [Clients] SET [card_number] = @card_number, [cl_name] = @cl_name, [date_of_birth] = @date_of_birth, [old_count] = @old_count, [child] = @child, [old_man] = @old_man, [adress] = @adress, [Phone] = @Phone, [email] = @email, [primechanie] = @primechanie, [deleted] = @deleted, [SSMA_TimeStamp] = @SSMA_TimeStamp WHERE [id] = @id"
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

        <div class="Pager">
</div>

    </form>
</body>
</html>
