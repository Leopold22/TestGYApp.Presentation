
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="TestGYApp.Presentation.TestPage" MasterPageFile="~/TestMasterPage.Master" %>

    
<%--плейсхолдер:--%>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   
    
    <div class="container">
        <div class="row">

            <div class="tablegrid">

                <div>
                       


                    <%--jQuery:--%>
                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                    <%--Пейджер:--%>
                    <script src="/Scripts/ASPSnippets_Pager.min.js" type="text/javascript"></script>

                    <%--Фильтрация грида:--%>
                    <script src="Scripts/ClientsGridFiltration.js"></script>
                    <%--Блокировка запрещенных символов в фильтре:--%>
                    <script src="Scripts/ClientsBlockRestrictedCharacters.js"></script>
                    <%--Поведение фильтров:--%>
                    <script src="Scripts/ClientsFiltersBehavior.js"></script>
                 



                     <%-- Date picker --%>
                      <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css">
                     <%--<script src="//code.jquery.com/jquery-1.12.4.js"></script>--%>
                     <script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
                    <script src="Scripts/datepicker-ru.js"></script>

                    <%-- Работа с датами: --%>
                    <%--<script src="Scripts/moment.min.js"></script>--%>
                    <%--<script src="Scripts/datejs.js"></script>--%>
                    <script src="Scripts/jquery.dateFormat-1.0.js"></script>

                   
                     <%-- Скрытые контролы для работы с чекбоксами: --%>
                    <asp:TextBox ID="CheckedItemsCollector" runat="server" placeholder="My checked values..." Width="192px" Height="25px" Visible="true"></asp:TextBox>
                    <asp:TextBox ID="UncheckedItemsCollector" runat="server" placeholder="My unckecked values..." Width="192px" Height="25px" Visible="true"></asp:TextBox>
                    <asp:CheckBox ID="GeneralCheckbox" runat="server" Visible="true"></asp:CheckBox>
                  
                    
                    <%--<iframe id="iframe" style="display:none;"></iframe>--%>

                    Список клиентов<br />
                    <br />
                    <asp:TextBox ID="FirstNameFilterTextBox" runat="server" placeholder="Имя..." Width="192px" Height="25px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; 
       <asp:TextBox ID="LastNameFilterTextBox" runat="server" placeholder="Фамилия..." Width="192px" Height="25px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;             
        
        <asp:TextBox ID="PatronymicFilterTextBox" runat="server"     placeholder="Отчество..." Width="192px" Height="25px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="PhoneFilterTextBox" runat="server" placeholder="Телефон..." Width="192px" Height="25px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
        <%--<span id="FakeAgeFilterTextbox" class="fakeAgeFilter" onmouseover="this.style.visibility = 'hidden'; document.getElementById('ageFilterSpan').style.visibility = 'visible' " onmouseout="this.style.visibility = 'visible'">--%>

                    <span id="AgeOuterFilterSpan" class="AgeOuterFilterSpan">

                        <span id="AgeCoverFilterSpan" class="AgeCoverFilterSpan">
                            <asp:TextBox ID="AgeCoverFilterTextbox" runat="server" placeholder="Возраст..." Width="192px" Height="25px"></asp:TextBox>
                        </span>

                        <span class="AgeFilterSpan" id="AgeFilterSpan">
                            <asp:TextBox ID="AgeFromFilterTextBox" runat="server" Width="90px" Height="25px"></asp:TextBox>
                            <asp:Label ID="AgeFilterDashLabel" runat="server" Text="-"> </asp:Label>
                            <asp:TextBox ID="AgeToFilterTextBox" runat="server" Width="90px" Height="25px"></asp:TextBox>
                        </span>

                    </span>



                    <br />
                    <br />
                    <asp:DropDownList ID="MarketingInfoDropDownFilter" runat="server" DataSourceID="MarketingInfoSqlDataSource" DataTextField="Name" DataValueField="ID" AppendDataBoundItems="True" placeholder="Телефон..." Width="192px" Height="25px">
                        <asp:ListItem Selected="True" Enabled="True" Value="" Text=""> </asp:ListItem>
                    </asp:DropDownList>
                      &nbsp;&nbsp;&nbsp;  
                    <%--<asp:TextBox ID="BirthDateFromFilterTextBox" runat="server"/>--%>






                        <span id="BirthDateOuterFilterSpan" class="BirthDateOuterFilterSpan">

                        <span id="BirthDateCoverFilterSpan" class="BirthDateCoverFilterSpan">
                            <asp:TextBox ID="BirthDateCoverFilterTextbox" runat="server" placeholder="Дата рождения..." Width="192px" Height="25px"></asp:TextBox>
                        </span>

                        <span class="BirthDateFilterSpan" id="BirthDateFilterSpan">
                            <asp:TextBox ID="BirthDateFromFilterTextBox" runat="server" Width="70px" Height="25px"></asp:TextBox>
                            <asp:Label ID="BirthDateFilterDashLabel" runat="server" Text="-"> </asp:Label>
                            <asp:TextBox ID="BirthDateToFilterTextBox" runat="server" Width="70px" Height="25px"></asp:TextBox>
                        </span>

                    </span>






                    <asp:SqlDataSource ID="MarketingInfoSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:GY_ContentConnectionString %>" SelectCommand="SELECT * FROM [MarketingInfoChoice]"></asp:SqlDataSource>





                    <br />



                    <%-- СКРИПТЫ:--%>
                    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0//jquery.min.js"></script>--%>&nbsp;
        
       
       
        <div class="container-fluid">
            <div class="row">
                <div class="gridClients">
                    <asp:ImageButton ID="AddClientButton" runat="server" ImageUrl="~/Images/AddButton.png" CssClass="FunctionalButton"  OnClick="AddClientButton_Click" Height="40px" />
                    <asp:ImageButton ID="ClearFiltersButton" runat="server" ImageUrl="~/Images/filterTest.png" CssClass="FunctionalButton" Height="40px" OnClientClick="return false" /> 
                    <asp:ImageButton ID="ServerReportButton" runat="server" ImageUrl="~/Images/reportIconTest.png" CssClass="FunctionalButton" Height="40px" OnClick="ReportButton_Click"  /> 
                    <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/reportIconTest.png" CssClass="FunctionalButton" Height="40px" OnClick="ReportButton_Click"  />--%>                   
                    
                    <asp:ImageButton ID="ClientReportButton" runat="server" ImageUrl="~/Images/reportIconTest.png" CssClass="FunctionalButton" Height="40px" OnClientClick="return false"   />
                     <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/reportIconTest.png" CssClass="FunctionalButton" Height="40px" OnClientClick="return false"  />--%>
                    <%--<asp:ImageButton ID="SortTestButton" runat="server" ImageUrl="~/Images/reportIconTest.png" CssClass="FunctionalButton" Height="40px" OnClick="SortTestButton_Click" />--%>
                

                    <%--<asp:Image ID="testImg" runat="server" ImageUrl="~/Images/filterTest.png"  Height="40px" />--%>
                    
                

                    <asp:GridView ID="MyClientsGridView" runat="server" AutoGenerateColumns="False"  CssClass="MyClientsGridView"   >
                        <%--<asp:GridView ID="MyClientsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="false"  DataSourceID="ClientsSqlDataSource" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnPageIndexChanging="MyClientsGridView_PageIndexChanging">--%>
                        <Columns>

                          
                                <asp:CheckBoxField ItemStyle-CssClass="CheckboxHeader" HeaderStyle-CssClass="CheckboxHeader">


<ItemStyle CssClass="CheckboxField"></ItemStyle>
                                </asp:CheckBoxField>


                            <asp:BoundField HeaderText="Имя" SortExpression="FullName" HeaderStyle-CssClass="FullNameHeader">
<HeaderStyle CssClass="FullNameHeader"></HeaderStyle>

                                <ItemStyle Width="200px" CssClass="FullNameCol" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Телефон" SortExpression="Phone"  HeaderStyle-CssClass="PhoneHeader">
<HeaderStyle CssClass="PhoneHeader"></HeaderStyle>

                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Дата рождения" SortExpression="BirthDate"  HeaderStyle-CssClass="BirthDateHeader">
<HeaderStyle CssClass="BirthDateHeader"></HeaderStyle>

                                <ItemStyle Width="105px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Возраст" SortExpression="Age"  HeaderStyle-CssClass="AgeHeader">
<HeaderStyle CssClass="AgeHeader"></HeaderStyle>

                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="E-mail" SortExpression="Email"  HeaderStyle-CssClass="EmailHeader">
<HeaderStyle CssClass="EmailHeader"></HeaderStyle>

                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Примечание" SortExpression="Comment"  HeaderStyle-CssClass="CommentHeader">
<HeaderStyle CssClass="CommentHeader"></HeaderStyle>

                                <ItemStyle Width="275px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Откуда узнал о клубе" SortExpression="MarketingInfo"  HeaderStyle-CssClass="MarketingInfoHeader">
<HeaderStyle CssClass="MarketingInfoHeader"></HeaderStyle>

                                <ItemStyle Width="200px" />
                            </asp:BoundField>



                        



                               



                        



                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>


                </div>

            </div>

        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="Pager">
            </div>
            <link rel="stylesheet" type="text/css" href="TestPageStyle.css" />
        </div>
    </div>


        </asp:Content>


