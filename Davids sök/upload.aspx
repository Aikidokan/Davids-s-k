<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="Sakregister.upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Assets/sakregister.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function DeleteConfirmation() {
            if (confirm("Du håller på att ta bort en rad, vill du fortsätta?") == true)
                return true;
            else
                return false;
        }
    </script>
</head>
<body>
<form id="Form1" method="post" enctype="multipart/form-data" runat="server">
    <input type="file" id="ulFile" name="ulFile" runat="server" /><label for="ulFile">Ladda upp</label>
    <br>
    <input type="submit" id="btnSubmit" OnServerClick="DoSubmit" value="Ladda upp" runat="server" />
    <input type="checkbox" runat="server" ID="cbPreview" checked="False" name="preview" /><label for="<%= cbPreview.UniqueID %>">Jag godkänner uppladdning</label>
    <br /><br />
    <asp:GridView ID="gvSakRegister"
        runat="server"
                  OnRowDeleting="GvRowDeleting"
                  AllowPaging="True"
        AllowSorting="True"
        OnDataBound="gvDatabound"
        OnPageIndexChanging="gvPageIndexChanged"
        OnSorting="gvSorting"
        OnRowDataBound="gvRowDatabound"
        CssClass="Grid"
        AlternatingRowStyle-CssClass="alt"
        PagerStyle-CssClass="pgr"
        DataKeyNames="Nr1" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
        Font-Size="0.8em" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
        GridLines="Horizontal" PageSize="25">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        
        <Columns>
            
            <asp:CommandField ShowDeleteButton="True">

                <HeaderStyle Width="50px" />

            </asp:CommandField>
            <asp:TemplateField HeaderText="ID" InsertVisible="False" ShowHeader="True" SortExpression="Nr1">
              
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Nr1") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
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
</form>
</body>
</html>