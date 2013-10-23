<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeGraph.aspx.cs" Inherits="NodeGraph" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta charset="utf-8" />
    <script type="text/javascript" src="../Scripts/NodeGraph/raphael-min.js"></script>
    <script type="text/javascript" src="../Scripts/NodeGraph/dracula_graffle.js"></script>
    <script src="../Scripts/jQuery/jquery-latest.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>--%>
    <script type="text/javascript" src="../Scripts/NodeGraph/dracula_graph.js"></script>
    <script type="text/javascript" src="../Scripts/NodeGraph/dracula_algorithms.js"></script>
    <script src="../Scripts/NodeGraph.js" type="text/javascript"></script>
    <style type="text/css">
      body {
      overflow: hidden;
      }
    </style>        
</head>
<body>
    <div id="canvas"></div>    
</body>
</html>
