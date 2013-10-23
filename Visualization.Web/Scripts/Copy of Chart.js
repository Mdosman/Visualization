    var mapOptions; 
    var map;
    var markers = [];
    var markerCluster;
    var outerLayout, innerLayout_East;

    createLayouts = function () {
        outerLayout = $('body').layout({
            name: "outer",
            spacing_open: 8, // ALL panes            
            spacing_closed: 12, // ALL panes            
            north__paneSelector: ".outer-north",
            center__paneSelector: ".outer-center",
            east__paneSelector: ".outer-east",
            east__size: 250,
            minSize: 70
        });

        innerLayout_East = $('div.outer-east').layout({
            name: "eastCenter",
            center__paneSelector: ".east-center",
            south__paneSelector: ".east-south",
            center__size: 500,
            minSize: 300,
            spacing_open: 8, // ALL panes            
            spacing_closed: 8 // ALL panes
        });

    };


    function DisplayMessages(strMessages) {
        $("#lblMsgCount").text(strMessages.split(";").length - 1);
        $("#txtMessages").val(strMessages.replace(/\;/g,"\r\n"));
    }

    
    function ClearMarkers() {
        markerCluster.clearMarkers();
        markers = [];
    }


    var LoadMap = function () {
        mapOptions = {
                center: new google.maps.LatLng(36.167597,-86.787186),
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);                
        markerCluster = new MarkerClusterer(map);
    }

//    //Old Working Maps without markerclusterer

//    function SetMarkers(strLatLonJSON) {
//        marker = [];        
//        if (strLatLonJSON != undefined) {
//            if (strLatLonJSON.length > 0) {
//                var objLatLon = jQuery.parseJSON(strLatLonJSON);
//                for (var i = 0; i < objLatLon.length; i++) {
//                    var latLng = new google.maps.LatLng(objLatLon[i].Lat, objLatLon[i].Lon);
//                    marker[i] = new google.maps.Marker({
//                        position: latLng
//                    });
//                }
//            }
//        }
//        for (var i = 0; i < marker.length; i++) {
//            marker[i].setMap(map);
//        }
//    }
    
    function SetMarkers(strLatLonJSON) {
        if (strLatLonJSON != undefined) {
            if (strLatLonJSON.length > 0) {
                var objLatLon = jQuery.parseJSON(strLatLonJSON);
                for (var i = 0; i < objLatLon.length; i++) {
                    var latLng = new google.maps.LatLng(objLatLon[i].Lat, objLatLon[i].Lon);
                    var marker = new google.maps.Marker({
                        position: latLng
                    });
                    markers.push(marker);
                }
            }
        }
        markerCluster.addMarkers(markers);
    }

    function RefreshMarkers() {
        var ts = Math.round(new Date().getTime() / 1000);
        ClearMarkers();
        var event = $("#txtEventList").val();
        var eAttr = $("#txtEventAttributeTypeList").val();
        var eAttrSub = $("#txtEventAttributeSubTypeList").val();
        jQuery.ajax({
            type: "GET",
            url: "../HttpHandler/MessageHandler.ashx?operation=Map&nodeType=" + event + "&id=" + eAttr + "&attributeType=" + eAttrSub + "&ts="+ts,
            success: SetMarkers,
            error: errorFn
        });
    }

    function RefreshMessages() {
        var ts = Math.round(new Date().getTime() / 1000);
        var event = $("#txtEventList").val();
        var eAttr = $("#txtEventAttributeTypeList").val();
        var eAttrSub = $("#txtEventAttributeSubTypeList").val();
        jQuery.ajax({
            type: "GET",
            url: "../HttpHandler/MessageHandler.ashx?operation=MessageList&nodeType=" + event + "&id=" + eAttr + "&attributeType=" + eAttrSub + "&ts="+ts,
            success: DisplayMessages,
            error: errorFn
        });
    }

    function errorFn(data) {

    }

    $(function () {
        var isPostBack = $("#hdnIsPostBack").val();
        if (isPostBack == 0) {
            $("#demo")
                .jstree({
                    // the list of plugins to include
                    "plugins": ["themes", "json_data", "checkbox"],
                    // Plugin configuration

                    // I usually configure the plugin that handles the data first - in this case JSON as it is most common
                    "json_data": {
                        // I chose an ajax enabled tree - again - as this is most common, and maybe a bit more complex
                        // All the options are the same as jQuery's except for `data` which CAN (not should) be a function
                        "ajax": {
                            // the URL to fetch the data
                            "url": "../HttpHandler/MessageHandler.ashx",
                            // this function is executed in the instance's scope (this refers to the tree instance)
                            // the parameter is the node being loaded (may be -1, 0, or undefined when loading the root nodes)
                            "data": function (n) {
                                // the result is fed to the AJAX request `data` option
                                return {
                                    "operation": "jsTree",
                                    "nodeType": n.attr ? n.attr("some-other-attribute") : "Message",
                                    "id": n.attr ? n.attr("id") : 0,
                                    "attributeType": n.attr ? n.attr("attributeType") : 0,
                                    "ts": Math.round(new Date().getTime() / 1000)
                                };
                            }

                        }
                    }
                });
        }
    });

    function GetCheckedItems() {
        var events = "";
        var eventAttributes = "";
        var eventAttributeSubTypes = "";

        var eventsArray = [];
        var eventAttributesArray = [];
        var eventAttributeSubTypesArray = [];

        var i = 0;
        var j = 0;
        var k = 0;
        $('#demo .jstree-checked').each(function () {
            var node = $(this);
            var nodeType = node.attr("some-other-attribute");
            var id = node.attr("id");
            var attributeType = node.attr("attributeType");
            if (nodeType == "Event") {
                events += "'" + id + "', ";
                eventsArray[i] = id;
                i++;
            } else if (nodeType == "EventAttributeType") {
                var blnFound = false;
                for (var lp = 0; lp < eventsArray.length; lp++) {
                    if (id == eventsArray[lp]) {
                        blnFound = true;
                    }
                }
                if (!blnFound) {
                    var attributeType = node.attr("attributeType");
                    eventAttributes += id + ":" + attributeType + ", ";
                    eventAttributesArray[j] = attributeType;
                    j++;
                }
            } else if (nodeType == "EventAttributeSubType") {
                var blnFound1 = false;
                for (var lp = 0; lp < eventsArray.length; lp++) {
                    if (id == eventsArray[lp]) {
                        blnFound1 = true;
                    }
                }
                for (var lp1 = 0; lp1 < eventAttributesArray.length; lp1++) {
                    if (attributeType == eventAttributesArray[lp1]) {
                        blnFound1 = true;
                    }
                }
                if (!blnFound1) {
                    var attributeType = node.attr("attributeType");
                    var attributeSubType = node.attr("attributeSubType");
                    eventAttributeSubTypes += id + ":" + attributeType + ":" + attributeSubType + ", ";
                    eventAttributeSubTypesArray[k] = attributeSubType;
                    k++;
                }
            }
        });

        $("#txtEventList").val(events);
        $("#txtEventAttributeTypeList").val(eventAttributes);
        $("#txtEventAttributeSubTypeList").val(eventAttributeSubTypes);
    }

    var CreateBarGraph = function () {
    };

    var CreateTabs = function () {
        $("#tabs").tabs();
    };

    var onChecked = function () {
        $("#demo").bind("check_node.jstree", function (e, data) {
            GetCheckedItems();
            RefreshMarkers();
            RefreshMessages();
        });
    };


    var onUnChecked = function () {
        $("#demo").bind("uncheck_node.jstree", function (e, data) {
            GetCheckedItems();
            RefreshMarkers();
            RefreshMessages();
        });
    };

    $(document).ready(function () {
        createLayouts();
        CreateTabs();
        window.setTimeout(LoadMap, 10);
        window.setTimeout(RefreshMessages, 20);
        window.setTimeout(RefreshMarkers, 30);
        window.setTimeout(onChecked, 150);
        window.setTimeout(onUnChecked, 150);
    });
    
jQuery(function() {

    // We create the protlets and style them accordingly by script //
    $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
			.find(".portlet-header")
				.addClass("ui-widget-header ui-corner-top")
				.prepend('<span class="ui-icon ui-icon-triangle-1-n"></span>')
				.end()
			.find(".portlet-content");
    // We make arrow button on any portlet header to act as a switch for sliding up and down the portlet content //
    $(".portlet-header .ui-icon").click(function() {
        $(this).parents(".portlet:first").find(".portlet-content").slideToggle("fast");
        $(this).toggleClass("ui-icon-triangle-1-s");
        return false;
    });
});

$(function() {

    Highcharts.setOptions({
        global: {
            useUTC: false
        }
    });
    var seriesOptions = [],
		    seriesCounter = 0,
        	names = ['Group Pattern', 'Human', 'Object', 'Vehicle'],
//        	names = ['GP-EVENTID','GP-EA1', 'GP-EA2', 'GP-EA3','GP-EA4', 'GP-EA5', 'GP-EA6'
//        	         'H-EVENTID','H-EA1', 'H-EA2', 'H-EA3','H-EA4', 'H-EA5', 'H-EA6',
//        	         'O-EVENTID','O-EA1', 'O-EA2', 'O-EA3','O-EA4', 'O-EA5', 'O-EA6',
//        	         'V-EVENTID','V-EA1', 'V-EA2', 'V-EA3','V-EA4', 'V-EA5', 'V-EA6'
//        	        ],
        	colors = Highcharts.getOptions().colors;

    $.each(names, function(i, name) {

        //        var data = [], time = (new Date()).getTime(), j;
        //        for (j = -9; j <= 0; j++) {
        //            data.push([
        //                    time + j * 1000,
        //                    Math.round(Math.random() * 10)
        //                    ]);
        //            //var d = new Date();
        //            //d.setTime(time + j * 1000);
        //        }

        var data = [];
        //EA1
        if (i == 0) {
            data = [
         [1351643708843, 0],
         [1351643768843, 0],
         [1351643828843, 1],
         [1351643888843, 1],
         [1351643948843, 1],
         [1351644008843, 1],
         [1351644068843, 1],
         [1351644128843, 1],
         [1351644188843, 1],
         [1351644248843, 1],
         [1351644308843, 1],
         [1351644368843, 1],
         [1351644428843, 1],
         [1351644488843, 1],
         [1351644548843, 8]];
        }
        //EA2
        if (i == 1) {
            data = [
         [1351643708843, 3],
         [1351643768843, 3],
         [1351643828843, 3],
         [1351643888843, 0],
         [1351643948843, 0],
         [1351644008843, 0],
         [1351644068843, 3],
         [1351644128843, 0],
         [1351644188843, 0],
         [1351644248843, 0],
         [1351644308843, 5],
         [1351644368843, 5],
         [1351644428843, 0],
         [1351644488843, 0],
         [1351644548843, 3]];
        }
        //EA3
        if (i == 2) {
            data = [
         [1351643708843, 2],
         [1351643768843, 2],
         [1351643828843, 2],
         [1351643888843, 0],
         [1351643948843, 0],
         [1351644008843, 0],
         [1351644068843, 2],
         [1351644128843, 0],
         [1351644188843, 0],
         [1351644248843, 0],
         [1351644308843, 0],
         [1351644368843, 0],
         [1351644428843, 0],
         [1351644488843, 0],
         [1351644548843, 0]];
        }

        seriesOptions[i] = {
            name: name,
            data: data,
            type: 'column'
        };

        // As we're loading the data asynchronously, we don't know what order it will arrive. So
        // we keep a counter and create the chart when all the data is loaded.
        seriesCounter++;

        if (seriesCounter == names.length) {
            window.setTimeout(createChart, 1500);
            //createChart();
        }

    });

    function createChart() {

        // Create the chart
        window.chart = new Highcharts.StockChart({
            chart: {
                renderTo: 'divBar',
                alignTicks: false
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Event SubTypes'
                },
                stackLabels: {
                    enabled: true,
                    style: {
                        fontWeight: 'bold',
                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                    }
                }
            },
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                }
            },
            legend: {
                align: 'right',
                x: -70,
                verticalAlign: 'top',
                y: 20,
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColorSolid) || 'white',
                borderColor: '#CCC',
                borderWidth: 1,
                shadow: false
            },
            rangeSelector: {
                buttons: [{
                    count: 1,
                    type: 'minute',
                    text: '1M'
                }, {
                    count: 5,
                    type: 'minute',
                    text: '5M'
                }, {
                    type: 'all',
                    text: 'All'
}],
                    inputEnabled: false,
                    selected: 0
                },

                title: {
                    text: 'Live random data'
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    },
                    series: {
                        cursor: 'pointer',
                        point: {
                            events: {
                                click: function() {
                                    hs.htmlExpand(null, {
                                        pageOrigin: {
                                            x: this.pageX,
                                            y: this.pageY
                                        },
                                        headingText: this.series.name,
                                        maincontentText: Highcharts.dateFormat('%A, %b %e, %Y', this.x) + ':<br/> ' +
                                        this.y + ' ' + '<br/><img src="../Pics/' + this.x + '.bmp" alt="Highslide JS" title="Click to enlarge" />',
                                        width: 350,
                                        height: 350


                                    });
                                }
                            }
                        },
                        marker: {
                            lineWidth: 1
                        }
                    }
                },
                series: seriesOptions

            });
        }
    });