<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sok.aspx.cs" Inherits="Sakregister.sok" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <title></title>
   </head>
   <body>
      <form id="form1" runat="server">
         <div style="height: 368px">
          <%--   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
            <h1><font face="Arial"><a name="Top"></a>Sakregister för kyrkomötet 1997-2015</font></h1>
            <p ALIGN="JUSTIFY">
               <font face="Arial" SIZE="4">
               Förkortningar<br>
               </font><font face="Arial" size="2"><a href="#Fork">Förkortningar före 2000</a></font>
            </p>
             <% Response.Write(Request.Url + "?ord=" + searchTerm.Text.Trim()); %>
            <p><font face="Arial" size="2"><b>KsSkr</b></font><font face="Arial" size="2">
               Kyrkostyrelsens skrivelse till Kyrkomötet, <b>Ln</b> Kyrkomötets läronämnd, <b>G</b>&nbsp;Gudstjänstutskottet,
               <b>O&nbsp;</b>Organisationsutskottet, <b>TU</b> Tillsyns- och uppdragsutskottet,
               <b>EE&nbsp;</b>Ekonomi- och egendomsutskottet, <b>Eu</b>  Ekumenikutskottet,<b>
               Kl </b>Kyrkolivsutskottet, <b>B&nbsp;</b>Budgetutskottet, <b>Kr</b>
               Kyrkorättsutskottet, <b>Fr </b>Fråga i kyrkomötet (KFråga), <b>M&nbsp;</b>Motion
               till kyrkomötet (KMot).</font>
            </p>
            <h3>Skriv det du vill söka efter</h3>
<%--            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <asp:TextBox ID="searchTerm" runat="server" Width="311px"></asp:TextBox><asp:Button ID="btn_search" runat="server" Text="Skicka" OnClick="btn_search_Click" /><asp:Button ID="btn_reset" runat="server" Text="Återställ" OnClick="btn_reset_Click" />
                    <br />
                    <br />
                 <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource2" 
                     AutoGenerateColumns="False" Width="1074px">
                     <AlternatingRowStyle BackColor="#D5EAFF" />
                  <Columns>
                      <asp:BoundField DataField="Ar" HeaderText="Ar" SortExpression="Ar"></asp:BoundField>
                      <asp:BoundField DataField="Ord" HeaderText="Ord" SortExpression="Ord"></asp:BoundField>
                      <asp:BoundField DataField="Arende" HeaderText="Arende" SortExpression="Arende"></asp:BoundField>
                      <asp:BoundField DataField="Betankande" HeaderText="Betankande" SortExpression="Betankande"></asp:BoundField>
                      <asp:BoundField DataField="Skrivelse" HeaderText="Skrivelse" SortExpression="Skrivelse"></asp:BoundField>
                      <asp:BoundField DataField="Protokoll" HeaderText="Protokoll" SortExpression="Protokoll"></asp:BoundField>
                  </Columns>
                     <HeaderStyle Font-Bold="True" />
              </asp:GridView>
           <%--  <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" Width="1074px">
                 <Columns>
                     <asp:BoundField DataField="Ar" HeaderText="Ar" SortExpression="Ar"></asp:BoundField>
                     <asp:BoundField DataField="Ord" HeaderText="Ord" SortExpression="Ord"></asp:BoundField>
                     <asp:BoundField DataField="Arende" HeaderText="Arende" SortExpression="Arende"></asp:BoundField>
                     <asp:BoundField DataField="Betankande" HeaderText="Betankande" SortExpression="Betankande"></asp:BoundField>
                     <asp:BoundField DataField="Skrivelse" HeaderText="Skrivelse" SortExpression="Skrivelse"></asp:BoundField>
                     <asp:BoundField DataField="Protokoll" HeaderText="Protokoll" SortExpression="Protokoll"></asp:BoundField>
                 </Columns>
             </asp:GridView>--%>
<%--                </ContentTemplate>
            </asp:UpdatePanel>--%>
            <p><font face="Arial"><a name="Fork"></a>Förkortningar före 2000</font></p>
            <p><font face="Arial">Kyrkomötet</font></p>
            <p><b><font SIZE="2" face="Arial">CsSkr</font></b><font SIZE="2" face="Arial">
               Centralstyrelsens skrivelse till kyrkomötet, <b>LN</b> Kyrkomötets
               läronämnd, <b>2KL</b> Andra kyrkolagsutskottet, <b>KG</b>
               Gudstjänstutskottet, <b>KF</b> Församlingsutskottet, <b>KUb</b>
               Utbildningsutskottet, <b>KEu</b> Ekumenikutskottet, <b>KK</b>&nbsp;Kulturutskottet,
               <b>KO&nbsp;</b>Organisationsutskottet, <b>KEo</b> Ekonomiutskottet, <b>Fr </b>Fråga
               i kyrkomötet (KFråga), <b>M&nbsp;</b>Motion till kyrkomötet (KMot).</font>
            </p>
            <p><font face="Arial" size="3">Ombudsmötet</font></p>
            <p><b><font SIZE="2" face="Arial">StSkr</font></b><font SIZE="2" face="Arial">
               Styrelsens för Svenska kyrkans stiftelse för rikskyrklig verksamhet skrivelse
               till ombudsmötet, <b>LN</b> Ombudsmötets läronämnd, <b>OF</b>
               Församlingsutskottet, <b>OEu</b> Ekumenikutskottet, <b>OO&nbsp;</b>Organisationsutskottet,
               <b>OEo</b> Ekonomiutskottet, <b>Fr </b>Fråga i ombudsmötet (OFråga), <b>M</b>
               Motion till ombudsmötet (OMot).</font>
            </p>
            <p><font size="2" face="Arial"><a href="#Top">Åter</a></font></p>
            <p>Preliminär version, vid kommentarer kontakta <a href="mailto:nils.warmland@svenskakyrkan.se">Nils Warmland</a>, ärkebiskopens och generalsekreterarens sekretariat</p>
             <p>&nbsp;</p>
             <p>
                 <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:CSImportConnectionString %>" 
                     SelectCommand="SELECT [Ar], [Ord], [Arende], [Betankande], [Skrivelse], [Protokoll] FROM [Sakregister] WHERE ([Ord] LIKE '%' + @Ord + '%')">
                     <SelectParameters>
                         <asp:QueryStringParameter Name="Ord" QueryStringField="ord" Type="String" />
                     </SelectParameters>
                 </asp:SqlDataSource>
             </p>
         </div>
        <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
              ConnectionString='<%$ ConnectionStrings:CSImportConnectionString %>' 
              SelectCommand="SELECT Ar, Ord, Arende, Betankande, Skrivelse, Protokoll FROM Sakregister WHERE (Ord LIKE '%' + @searchTerm + '%')">
              <SelectParameters>
                  <asp:Parameter Name="searchTerm" />
              </SelectParameters>
          </asp:SqlDataSource>--%>
          
      </form>
   </body>
</html>
