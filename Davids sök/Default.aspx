<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sakregister.Default" %>

<%@ Register src="uc/menu.ascx" tagname="menu" tagprefix="uc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript">

           $(function() {
               $(document).ready(function() {

                   $(".search_button").click(function() {
                       // getting the value that user typed
                       var searchString = $("#search_box").val();
                       // forming the queryString
                       var data = searchString;

                       // if searchString is not empty
                       if (searchString) {
                           $("#results tr").remove();
                           // ajax call
                           $.ajax({
                               type: "POST",
                               url: "handlers/Handlesakregister.ashx",
                               data: { "Ord": $("#search_box").val() },
                               success: function (data) {
                                   var obj = JSON.parse(data);

                                   $("#results")
                                       .append("<thead><tr><th>År</th><th>Ord</th><th>Ärende</th><th>Betänkande</th><th>Skrivelse</th><th>Protokoll</th></tr></thead>");
                                   $.each(obj.Items, function (index, item) {

                                       $("#results").append("<tr><td>" + item.Ar + "</td> <td>" + item.Ord + "</td> <td> " + item.Arende + "</td> <td>" + item.Betankande + "</td> <td>" + item.Skrivelse + "</td> <td>"+ item.Protokoll + "</td> </tr>");
                                           
                                       });
                               },
                               error: function(err) {
                                   alert(err);
                               }
                           });
                       }
                       return false;
                   });
                   
               });
           });
    </script>
    <link href="Assets/sakregister.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<uc1:menu ID="menu1" runat="server" />
      
        <h3>Skriv det du vill söka efter</h3>

        <input type="text" name="search" id="search_box" class='search_box' />
        <input type="submit" value="Search" class="search_button" /><br />

        <div id="searchresults">Resultat :</div>

       
        <table id="results" class="mGrid">
          
           
          

            
        </table>

        

    </form>
</body>
</html>
