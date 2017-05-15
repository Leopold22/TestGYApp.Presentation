<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecondTestPage.aspx.cs" Inherits="TestGYApp.Presentation.SecondTestPage" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecondTestPage.aspx.cs" Inherits="TestGYApp.Presentation.SecondTestPage"  %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

 <%--    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
         a:visited 
         {
             color: blue;
         }
        table
        {
            border: 1px solid #ccc;
        }
        table th
        {
            background-color: #7B44D4;
            color: #FFF;
            font-weight: bold;
            
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }

        table tr:nth-child(odd)
        {
            background-color: #D5C6EE
        }

         table tr:nth-child(even) {
         background-color: white
         }

         

        .Pager
        {
            margin-top: 10px;

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
            background-color: #08350D;
        }
    </style>--%>


</head>
<body>

 <form id="testForm" runat="server">

 <%--   <asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server"> --%>
   
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
         <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0//jquery.min.js"></script>--%>
         <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>--%>

<script src="/Scripts/ASPSnippets_Pager.min.js" type="text/javascript"></script>
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
            url: "TestPage.aspx/GetClients",
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
        var clients = xml.find("tblClients");
        if (row == null) {
            row = $("[id*=MySecondClientsGridView] tr:last-child").clone(true);
        }
        $("[id*=MySecondClientsGridView] tr").not($("[id*=MySecondClientsGridView] tr:first-child")).remove();
        if (clients.length > 0) {
            $.each(clients, function () {
                var customer = $(this);
                //Заполняем имя как ссылку на форму просмотра
                $("td", row).eq(0).html("<a href=\"/ClientDispForm.aspx?id=" + $(this).find("ID").text() + "\">" + $(this).find("cl_name").text() + "</a>");
                //Заполняем номер телефона
                $("td", row).eq(1).html($(this).find("tel_number").text());

                //форматируем номер телефона
                $("td", row).eq(1).text(function (i, text) {
                    return text.replace(/(\d{0})(\d{3})(\d{3})(\d{2})/, '$1($2) $3-$4-');
                });

                
                $("[id*=MySecondClientsGridView]").append(row);
                row = $("[id*=MySecondClientsGridView] tr:last-child").clone(true);
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
            $("td:first-child", empty_row).html("По заданным критериям ничего не найдено.");
            $("td", empty_row).not($("td:first-child", empty_row)).remove();
            $("[id*=MySecondClientsGridView]").append(empty_row);
        }
    };
</script>






        <br />
        <br />
        <asp:GridView ID="MySecondClientsGridView" runat="server" AutoGenerateColumns="False"    >
        <%--<asp:GridView ID="MySecondClientsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="false"  DataSourceID="ClientsSqlDataSource" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="MySecondClientsGridView_PageIndexChanging">--%>
            <Columns>
                <%--<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />--%>
                <%--<asp:BoundField DataField="card_number" HeaderText="card_number" SortExpression="card_number" />--%>
                <%--<asp:BoundField DataField="cl_name" HeaderText="cl_name" SortExpression="cl_name" />--%>
                <%--<asp:BoundField DataField="date_of_birth" HeaderText="date_of_birth" SortExpression="date_of_birth" />--%>
                <%--<asp:BoundField DataField="old_count" HeaderText="old_count" SortExpression="old_count" />--%>
                <%--<asp:CheckBoxField DataField="child" HeaderText="child" SortExpression="child" />--%>
                <%--<asp:CheckBoxField DataField="old_man" HeaderText="old_man" SortExpression="old_man" />--%>
                <%--<asp:BoundField DataField="adress" HeaderText="adress" SortExpression="adress" />--%>
                <%--<asp:BoundField DataField="tel_number" HeaderText="tel_number" SortExpression="tel_number" />--%>
                <%--<asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />--%>
                <%--<asp:BoundField DataField="primechanie" HeaderText="primechanie" SortExpression="primechanie" />--%>
                <%--<asp:CheckBoxField DataField="deleted" HeaderText="deleted" SortExpression="deleted" />--%>
         

                    <asp:BoundField  HeaderText="Имя" SortExpression="cl_name" />
                <asp:BoundField  HeaderText="Телефон" SortExpression="tel_number" />

                
             <%--       <asp:BoundField DataField="cl_name" HeaderText="Имя" SortExpression="cl_name" />
                <asp:BoundField DataField="tel_number" HeaderText="Телефон" SortExpression="tel_number" />--%>
               
<%--                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="/ClientDispForm.aspx?id={0}" DataTextField="cl_name" Text="Имя" AccessibleHeaderText="Имя" HeaderText="Имя" />
               
                <asp:TemplateField HeaderText="Телефон" ConvertEmptyStringToNull="False">
                    <ItemTemplate> 
                        <asp:Literal ID="litPhone" runat="server" 
                        Text='<%# Eval("tel_number")  == null || Eval("tel_number").ToString()  == "" ?  "" : string.Format("{0:(###) ###-####}", Int64.Parse(Eval("tel_number").ToString())) %>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>



                  </Columns>

<%--            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#7B44D4" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#7B44D4" ForeColor="White" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
            <RowStyle BackColor="White" ForeColor="Black" />
            <AlternatingRowStyle BackColor="#D5C6EE" ForeColor="Black" />--%>


        </asp:GridView>
        <asp:SqlDataSource ID="ClientsSqlDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GY_ContentConnectionString %>" 
            DeleteCommand="DELETE FROM [tblClients] WHERE [id] = @id" 
            InsertCommand="INSERT INTO [tblClients] ([card_number], [cl_name], [date_of_birth], [old_count], [child], [old_man], [adress], [tel_number], [email], [primechanie], [deleted], [SSMA_TimeStamp]) VALUES (@card_number, @cl_name, @date_of_birth, @old_count, @child, @old_man, @adress, @tel_number, @email, @primechanie, @deleted, @SSMA_TimeStamp)" 
            SelectCommand="SELECT * FROM [tblClients]" 
            UpdateCommand="UPDATE [tblClients] SET [card_number] = @card_number, [cl_name] = @cl_name, [date_of_birth] = @date_of_birth, [old_count] = @old_count, [child] = @child, [old_man] = @old_man, [adress] = @adress, [tel_number] = @tel_number, [email] = @email, [primechanie] = @primechanie, [deleted] = @deleted, [SSMA_TimeStamp] = @SSMA_TimeStamp WHERE [id] = @id"
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
                <asp:Parameter Name="tel_number" Type="String" />
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
                <asp:Parameter Name="tel_number" Type="String" />
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
        <link rel="stylesheet" type="text/css" href="TestPageStyle.css" />
    

        <%--</asp:Content>--%>

</form>

</body>
</html>

