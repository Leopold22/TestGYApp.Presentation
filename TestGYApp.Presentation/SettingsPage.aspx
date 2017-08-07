<%@ Page   Language="C#" AutoEventWireup="true" CodeBehind="SettingsPage.aspx.cs" Inherits="TestGYApp.Presentation.SettingsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Настройки<br />
            <br />
            <asp:GridView ID="GlobalSettingsGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="GlobalSettingsSqlDataSource" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:HyperLinkField Target="_blank" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="SettingsEditForm.aspx?ID={0}"  DataTextField="SystemName" HeaderText="SystemName" SortExpression="SystemName" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField ApplyFormatInEditMode="True" DataField="Value" HeaderText="Value" SortExpression="Value" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
            <asp:SqlDataSource ID="GlobalSettingsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:GY_ContentConnectionString %>" SelectCommand="SELECT [ID], [SystemName], [Description], [Value] FROM [tbl_GlobalSettings]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [tbl_GlobalSettings] WHERE [ID] = @original_ID AND (([SystemName] = @original_SystemName) OR ([SystemName] IS NULL AND @original_SystemName IS NULL)) AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([Value] = @original_Value) OR ([Value] IS NULL AND @original_Value IS NULL))" InsertCommand="INSERT INTO [tbl_GlobalSettings] ([SystemName], [Description], [Value]) VALUES (@SystemName, @Description, @Value)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [tbl_GlobalSettings] SET [SystemName] = @SystemName, [Description] = @Description, [Value] = @Value WHERE [ID] = @original_ID AND (([SystemName] = @original_SystemName) OR ([SystemName] IS NULL AND @original_SystemName IS NULL)) AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([Value] = @original_Value) OR ([Value] IS NULL AND @original_Value IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_ID" Type="Int32" />
                    <asp:Parameter Name="original_SystemName" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Value" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="SystemName" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Value" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SystemName" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Value" Type="String" />
                    <asp:Parameter Name="original_ID" Type="Int32" />
                    <asp:Parameter Name="original_SystemName" Type="String" />
                    <asp:Parameter Name="original_Description" Type="String" />
                    <asp:Parameter Name="original_Value" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
