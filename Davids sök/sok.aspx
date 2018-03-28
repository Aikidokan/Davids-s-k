<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sok.aspx.cs" Inherits="Sakregister.sok" %>
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
                           // ajax call
                           $.ajax({
                               type: "POST",
                               url: "handlers/Handlesakregister.ashx",
                               data: { "ord":$("#search_box").val()},
                               success: function(res) {

                                   console.log(res);
                                   $.each(res,
                                       function(index, item) {
                                           $("<tr>").append(
                                               $("<td>").text(item.Ar),
                                               $("<td>").text(item.Ord)
                                           ).appendTo("#mytable tbody");

                                       });
                               },
                               error: function(err) {
                                   alert(err);
                               }
                           });
                       }
                   });
               });
           });
       </script>
   </head>
   <body>
      <form id="form1" runat="server">
     

            <h3>Skriv det du vill söka efter</h3>
            
             <input type="text" name="search" id="search_box" class='search_box' />
             <input type="submit" value="Search" class="search_button" /><br />
             
          <div id="searchresults">Resultat :</div>
            
           <ul id="results" class="update"></ul>
          <table id="mytable">
              <tbody>
                  <tr>
                      <td></td>
                    
              </tbody>
          </table>
          
      </form>
   </body>
</html>
