<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LayoutMap.aspx.cs" Inherits="WebForms_LayoutMap" %>

<!DOCTYPE html>
<html>
<head>
<title>Layout Example</title>

<script src="../Scripts/jQuery/jquery-latest.js" type="text/javascript"></script>
<script src="../Scripts/jQuery/jquery-ui-latest.js" type="text/javascript"></script>
<script type="text/javascript" src="../Scripts/jQuery/jquery.layout-latest.js"></script>

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
$(document).ready(function () {
	$('body').layout({ applyDemoStyles: true });
	$("#tabs" ).tabs();
});
</script>
</head>
<body>
<div class="ui-layout-center">Center
	<div id="tabs">
        <ul>
            <li><a href="#tabMap">Map</a></li>
            <li><a href="#tabGraph">Graph</a></li>
            <li><a href="#tabBar">Bar</a></li>
        </ul>
        <div id="tabMap">
            <fieldset>
                <div id="map-canvas" style="width: 500px; height: 600px"> </div>
            </fieldset>
        </div>
        <div id="tabGraph">
            GRAPH
        </div>
        <div id="tabBar">
            BAR
        </div>
    </div>
</div>
<div class="ui-layout-north">North</div>
<div class="ui-layout-south">South</div>
<div class="ui-layout-east">East</div>
<div class="ui-layout-west">West</div>
</body>
</html>