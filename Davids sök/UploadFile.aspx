<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="Sakregister.UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="fuImportfile" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Ladda upp"
                    OnClick="DoUpload" />

        <asp:Label runat="server" id="lblStatus"></asp:Label>

        <asp:Button ID="btnInsertToSql" runat="server" Text="Importera resultatet"
                    OnClick="DoImportFileContent" /> 
        <asp:GridView runat="server" id="gvtest"></asp:GridView>
       
        <asp:GridView ID="gvFileContent" runat="server"
            DataKeyNames="Id"
            AutoGenerateColumns="False"
            AllowPaging="True"
            AllowSorting="True"
            BackColor="White"
            BorderColor="#CCCCCC"
            Font-Size="0.8em"
            BorderStyle="None"
            BorderWidth="1px"
            CellPadding="4"
            ForeColor="Black"
            GridLines="Both" PageSize="10"
            OnDataBound="gvImportFileDatabound"
            OnPageIndexChanging="GvImportGridViewOnPageIndexChanging"
            OnSorting="gvImportFileContentSorting"
            OnRowEditing="gvImportFileContent_RowEditing"
            OnRowCancelingEdit="GvImportGridViewRowCancelingEdit"
            OnRowUpdating="gvFileContent_RowUpdating">
            
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#660000" Font-Bold="True" ForeColor="White" />

            <PagerStyle CssClass="GridPager" HorizontalAlign="Right"></PagerStyle>
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />

            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />

            <PagerStyle CssClass="pgr" BackColor="White" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />

            <Columns>

                <asp:CommandField ShowDeleteButton="True" ShowEditButton="true" ShowCancelButton="true" ShowSelectButton="true">

                    <HeaderStyle Width="50px" />

                </asp:CommandField>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Ar" HeaderText="År" SortExpression="Ar">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Ord" HeaderText="Ord" SortExpression="Ord">
                    <HeaderStyle Width="340px" />
                </asp:BoundField>
                <asp:BoundField DataField="Arende" HeaderText="Ärende" SortExpression="Arende">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Betankande" HeaderText="Betänkande" SortExpression="Betankande">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Protokoll" HeaderText="Protokoll" SortExpression="Protokoll">
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Skrivelse" HeaderText="Skrivelse" SortExpression="Skrivelse">
                    <HeaderStyle Width="60px" />
                </asp:BoundField>

               
            </Columns>
            </asp:GridView>

        <asp:Button ID="btnShowTableData" runat="server" Text="Visa all tidigare tabelldata"
                    OnClick="DoShowSqlTableData" />

        <asp:GridView ID="gvSqlTableSakRegister" runat="server"
            OnRowDeleting="gvSqlTableDeleting"
            AllowPaging="True"
            AllowSorting="True"
            OnDataBound="gvSqlTableDatabound"
            OnPageIndexChanging="gvSqlTablePageIndexChanged"
            OnSorting="gvSqlTableSorting"
            OnRowDataBound="gvSqlTableRowDatabound"
            DataKeyNames="Id"
            AutoGenerateColumns="False"
            BackColor="White"
            BorderColor="#CCCCCC"
            Font-Size="0.8em"
            BorderStyle="None"
            BorderWidth="1px"
            CellPadding="4"
            ForeColor="Black"
            GridLines="Both" PageSize="20">
            

            <Columns>

                <asp:CommandField ShowDeleteButton="True">

                    <HeaderStyle Width="50px" />

                </asp:CommandField>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Ar" HeaderText="År" SortExpression="Ar">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Ord" HeaderText="Ord" SortExpression="Ord">
                    <HeaderStyle Width="340px" />
                </asp:BoundField>
                <asp:BoundField DataField="Arende" HeaderText="Ärende" SortExpression="Arende">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Betankande" HeaderText="Betänkande" SortExpression="Betankande">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Protokoll" HeaderText="Protokoll" SortExpression="Protokoll">
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Skrivelse" HeaderText="Skrivelse" SortExpression="Skrivelse">
                    <HeaderStyle Width="60px" />
                </asp:BoundField>

                <asp:BoundField DataField="Created" HeaderText="Skapad" SortExpression="Created">
                    <HeaderStyle Width="145px" />
                </asp:BoundField>
            </Columns>

            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />

            <PagerStyle CssClass="pgr" BackColor="White" ForeColor="Black" HorizontalAlign="Right"></PagerStyle>
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
    </div>
</form>
</body>
</html>
