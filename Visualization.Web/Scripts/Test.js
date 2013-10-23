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

    var RefreshBarGraph = function () {
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
        window.setTimeout(RefreshBarGraph, 1500);
    });