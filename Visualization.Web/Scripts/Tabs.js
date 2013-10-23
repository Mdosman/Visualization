var strLatLonJSON = { "count": 10785236,
 "locations": [ { "Lat" : "36.167859" , "Lon" : "-86.787601"  }, { "Lat" : "36.167732" , "Lon" : "-86.787647"  }, { "Lat" : "36.167631" , "Lon" : "-86.787545"  }, { "Lat" : "36.167598" , "Lon" : "-86.787368"  }, { "Lat" : "36.1675" , "Lon" : "-86.787277"  }, { "Lat" : "36.167595" , "Lon" : "-86.787204"  }, { "Lat" : "36.167497" , "Lon" : "-86.787137"  }, { "Lat" : "36.16757" , "Lon" : "-86.787035"  }, { "Lat" : "36.167714" , "Lon" : "-86.787084"  }, { "Lat" : "36.167797" , "Lon" : "-86.786979"  }, { "Lat" : "36.167861" , "Lon" : "-86.786858"  }, { "Lat" : "36.167561" , "Lon" : "-86.78696"  }, { "Lat" : "36.167671" , "Lon" : "-86.786845"  }]
 };
	$(document).ready(function(){
	    $("#tabs" ).tabs();
	});
	
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
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.SATELLITE
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
//            homeMarker.setMap(map);
//            homeMarker2.setMap(map);
//            homeMarker3.setMap(map);

                
            var markers = [];
            for (var i = 0; i < strLatLonJSON.locations.length; i++) {
                var objLatLon = strLatLonJSON.locations[i];
                var latLng = new google.maps.LatLng(objLatLon.Lat, objLatLon.Lon);
                var marker = new google.maps.Marker({
                    position: latLng
                });
                 markers.push(marker);
            }
//            for (var i = 0; i < 100; i++) {
//              var dataPhoto = data.photos[i];
//              var latLng = new google.maps.LatLng(dataPhoto.latitude,
//                  dataPhoto.longitude);
//              var marker = new google.maps.Marker({
//                position: latLng
//              });
//              markers.push(marker);
//            }
            var markerCluster = new MarkerClusterer(map, markers);
        }
        google.maps.event.addDomListener(window, 'load', initialize);
        