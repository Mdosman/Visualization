

//	var homeLatlng = new google.maps.LatLng(36.16758, -86.7874);
//        var homeMarker = new google.maps.Marker({
//            position: homeLatlng
//        });
//        var homeLatlng2 = new google.maps.LatLng(36.16759,-86.787186);
//        var homeMarker2 = new google.maps.Marker({
//            position: homeLatlng2
//        });
//        var homeLatlng3 = new google.maps.LatLng(36.16745,-86.7873);
//        var homeMarker3 = new google.maps.Marker({
//            position: homeLatlng3
//        });
        
        
        var CreateMarkers = function () {            
            var latLng = [];
            var marker = [];
            if(strLatLonJSON.length > 0) {
                for(var i=0; i<strLatLonJSON.length; i++) {
                    latLng[i] = new google.maps.LatLng(strLatLonJSON.Lat, strLatLonJSON.Lon);
                    marker[i] = new google.maps.Marker({
                        position: latLng[i]
                    });
                }
            var mapOptions = {
                center: new google.maps.LatLng(36.167597,-86.787186),
                zoom: 20,
                mapTypeId: google.maps.MapTypeId.SATELLITE
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
//            homeMarker.setMap(map);
//            homeMarker2.setMap(map);
//            homeMarker3.setMap(map);
        }

        var LoadMap = function () {
            var mapOptions = {
                center: new google.maps.LatLng(36.167597,-86.787186),
                zoom: 20,
                mapTypeId: google.maps.MapTypeId.SATELLITE
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
//            homeMarker.setMap(map);
//            homeMarker2.setMap(map);
//            homeMarker3.setMap(map);
        }
//        google.maps.event.addDomListener(window, 'load', initialize);
                            

//    var LoadMap = function () {
//        var mapOptions = {
//            center: new google.maps.LatLng(18.964700, 72.825800),
//            zoom: 10,
//            mapTypeId: google.maps.MapTypeId.SATELLITE 
//        };
//        var infoWindow = new google.maps.InfoWindow();
//        map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
//        var myLatlng = new google.maps.LatLng(18.964700, 72.825800);
//        var marker = new google.maps.Marker({
//            position: myLatlng,
//            map: map,
//            title: 'Mumbai'
//        });
//    }
    
	$(document).ready(function(){
	    $('#btnFilter').hide();
		createLayouts();
		CreateTabs();
		window.setTimeout(onChecked, 100);
		window.setTimeout(onUnChecked, 100);
		window.setTimeout(LoadMap, 50);
	});