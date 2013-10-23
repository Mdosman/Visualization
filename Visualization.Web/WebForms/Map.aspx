<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Map.aspx.cs" Inherits="WebForms_Map" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <style type="text/css">
      html { height: 100% }
      body { height: 100%; margin: 0; padding: 0 }
      #map-canvas { height: 100% }
    </style>
    <script type="text/javascript"
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAuyiTAfNDyoRQybx_jy0FyAMiPVj1E92A&sensor=true">
    </script>
     <script type="text/javascript">

        var homeLatlng = new google.maps.LatLng(36.16758, -86.7874);
        var homeMarker = new google.maps.Marker({
            position: homeLatlng
        });
        var homeLatlng2 = new google.maps.LatLng(36.16759,-86.787186);
        var homeMarker2 = new google.maps.Marker({
            position: homeLatlng2
        });
        var homeLatlng3 = new google.maps.LatLng(36.16745,-86.7873);
        var homeMarker3 = new google.maps.Marker({
            position: homeLatlng3
        });

        function initialize() {
            var mapOptions = {
                center: new google.maps.LatLng(36.167597,-86.787186),
                zoom: 20,
                mapTypeId: google.maps.MapTypeId.SATELLITE
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
            homeMarker.setMap(map);
            homeMarker2.setMap(map);
            homeMarker3.setMap(map);
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>
  </head>
<body>
    <form id="form1" runat="server">
    <div id="map-canvas" style="width: 500px; height: 600px"> </div>
    </form>
</body>
</html>
