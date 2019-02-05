<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookmarks.aspx.cs" Inherits="EnterTheMatrix.bookmarks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link href="default.css" rel="stylesheet" />
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css"/>

<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">

      <div class="redirect">  <a  href="default.aspx">Back to Search</a></div>
        <div class="container">
              
              <div runat="server" id="div_results" class="row"></div>
        </div>
    </form>
</body>
</html>
