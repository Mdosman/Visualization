<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SlideShowPage.aspx.cs" Inherits="WebForms_SlideShowPage" %>
<!doctype html>
<html>
<head>
  <meta charset="utf-8">
  <title>Images</title>
  <!-- SlidesJS Required (if responsive): Sets the page width to the device width. -->
 <%-- <meta name="viewport" content="width=device-width">--%>
  <!-- End SlidesJS Required -->
  
  
  <!-- CSS for slidesjs.com example -->
    <link href="../Styles/slideshow_example.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/slideshow_font-awesome.min.css" rel="stylesheet" type="text/css" />
  <!-- End CSS for slidesjs.com example -->

  
  <!-- SlidesJS Required: Link to jQuery -->
    <script src="../Scripts/jQuery/jquery-latest.js"></script>
    <script src="../Scripts/jquery.slides.min.js" type="text/javascript"></script>
  <!-- End SlidesJS Required -->
  
<style type="text/css">
    body {
      -webkit-font-smoothing: antialiased;
      font: normal 15px/1.5 "Helvetica Neue", Helvetica, Arial, sans-serif;
      color: #232525;
      padding:5px;
    }

    #slides {
      display: none
    }

    #slides .slidesjs-navigation {
      margin-top:5px;
    }

    a.slidesjs-next,
    a.slidesjs-previous,
    a.slidesjs-play,
    a.slidesjs-stop {
      background-image: url(../images/btns-next-prev.png);
      background-repeat: no-repeat;
      display:block;
      width:12px;
      height:18px;
      overflow: hidden;
      text-indent: -9999px;
      float: left;
      margin-right:5px;
    }

    a.slidesjs-next {
      margin-right:10px;
      background-position: -12px 0;
    }

    a:hover.slidesjs-next {
      background-position: -12px -18px;
    }

    a.slidesjs-previous {
      background-position: 0 0;
    }

    a:hover.slidesjs-previous {
      background-position: 0 -18px;
    }

    a.slidesjs-play {
      width:15px;
      background-position: -25px 0;
    }

    a:hover.slidesjs-play {
      background-position: -25px -18px;
    }

    a.slidesjs-stop {
      width:18px;
      background-position: -41px 0;
    }

    a:hover.slidesjs-stop {
      background-position: -41px -18px;
    }

    .slidesjs-pagination {
      margin: 7px 0 0;
      float: right;
      list-style: none;
    }

    .slidesjs-pagination li {
      float: left;
      margin: 0 1px;
    }

    .slidesjs-pagination li a {
      display: block;
      width: 13px;
      height: 0;
      padding-top: 13px;
      background-image: url(../images/pagination.png);
      background-position: 0 0;
      float: left;
      overflow: hidden;
    }

    .slidesjs-pagination li a.active,
    .slidesjs-pagination li a:hover.active {
      background-position: 0 -13px
    }

    .slidesjs-pagination li a:hover {
      background-position: 0 -26px
    }

    #slides a:link,
    #slides a:visited {
      color: #333
    }

    #slides a:hover,
    #slides a:active {
      color: #9e2020
    }

    .navbar {
      overflow: hidden
    }
  </style>
    
  <!-- SlidesJS Required: Initialize SlidesJS with a jQuery doc ready -->
  <script type="text/javascript">
    $(function() {
      $('#slides').slidesjs({
        width: 450,
        height: 350,
        play: {
          active: true,
          auto: false,
          interval: 3000,
          swap: true,
          pauseOnHover: true,
          restartDelay: 2000
        },
         pagination: {
          active: false
        }
      });
    });
  </script>
  <!-- End SlidesJS Required -->
</head>
<body>
<form runat="server">
  <!-- SlidesJS Required: Start Slides -->
  <!-- The container is used to define the width of the slideshow -->
  <div id="slidesContainer"  runat="server" style="width:441px;height:324px; text-align:center;" >
    <div runat="server"  id="slides" >
    </div>
  </div>
  <!-- End SlidesJS Required: Start Slides -->
</form>
</body>
</html>
