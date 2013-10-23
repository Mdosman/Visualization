
    var mapOptions;
    var map;
    var markers = [];
    var markerCluster;
    var outerLayout, innerLayout_East;

    $(document).ready(function() {
        createLayouts();
        CreateTabs();
        window.setTimeout(LoadMap, 10);
        //window.setTimeout(onChecked, 150);
        //window.setTimeout(onUnChecked, 150);

        $('#ddlSQLQuery').keypress(function() {
            OnSavedSQLQueryChange();
        });
        $('#ddlSQLQuery').change(function() {
            OnSavedSQLQueryChange();
        });

        $('#ddlCam').keypress(function() {
            OnCamChange();
        });
        $('#ddlCam').change(function() {
            OnCamChange();
        });

        $('#ddlProcess').keypress(function() {
            onProcessChange();
        });
        $('#ddlProcess').change(function() {
            onProcessChange();
        });
        $('#ddlProcessSubType').keypress(function() {
            onProcessSubTypeChange();
        });
        $('#ddlProcessSubType').change(function() {
            onProcessSubTypeChange();
        });

        $('#ddlTML').keypress(function() {
            OnTMLChange();
        });
        $('#ddlTML').change(function() {
            OnTMLChange();
        });
        
        ReloadSavedQueries();
    });

    function onProcessChange() {
        var processId = $('#ddlProcess option:selected').val();
        $("#txtProcessId").val(processId);
        ClearValues();
        ClearMessages();
        ClearMarkers();
        createJSTree();
        window.setTimeout(onChecked, 150);
        window.setTimeout(onUnChecked, 150);
    }

    function onProcessSubTypeChange() {
        var processSubTypeId = $('#ddlProcessSubType option:selected').val();
        $("#txtProcessSubTypeId").val(processSubTypeId);
        ClearValues();
        ClearMessages();
        ClearMarkers();
        createJSTree();
        window.setTimeout(onChecked, 150);
        window.setTimeout(onUnChecked, 150);
    }
             
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
            minSize: 200,
            spacing_open: 8, // ALL panes            
            spacing_closed: 8 // ALL panes
        });

    };
    
    $(function () {
        
        // We create the protlets and style them accordingly by script //
        $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
            .find(".portlet-header")
            .addClass("ui-widget-header ui-corner-top")
            .prepend('<span class="ui-icon ui-icon-triangle-1-n"></span>')
            .end()
            .find(".portlet-content");
        // We make arrow button on any portlet header to act as a switch for sliding up and down the portlet content //
        $(".portlet-header .ui-icon").click(function () {
            $(this).parents(".portlet:first").find(".portlet-content").slideToggle("fast");
            $(this).toggleClass("ui-icon-triangle-1-s");
            return false;
        });
    });
    
    

    function ReloadSavedQueries() {
       var ts = Math.round(new Date().getTime() / 1000);
       var ddlSQLQuery = $('#ddlSQLQuery');
       $.ajax({
        url: "../HttpHandler/MessageHandler.ashx?operation=GetSavedQueries&ts=" + ts,
        dataType: "json"
       }).done(function (data) {
            // Clear drop down list
            $(ddlSQLQuery).empty();
            // Fill drop down list with new data
            $(data).each(function () {
                $("<option />", {
                    val: this.ID,
                    text: this.QueryName
                }).appendTo(ddlSQLQuery);
            });
        }).fail(function (data) {
            // Clear drop down list
            alert("Error! Please try again.");
        });
    }

    function SaveQueryResult(message) {
        if (message != undefined) {
            if (message.length > 0) {
                alert(message);
            }
        }
        ReloadSavedQueries();
    }
    
    function DisplayParseResults(strParseResults) {    
       var result = strParseResults.split('~');
        DisplayMessages(result[0]);
        SetMarkers(result[1]);
    }
    
    function DisplayMessages(strMessages) {
        $("#lblMsgCount").text("Count : " + (strMessages.split(";").length - 1));
        $("#txtMessages").val(strMessages.replace(/\;/g, "\r\n"));
    }

    function ClearMarkers() {
        markerCluster.clearMarkers();
        markers = [];
    }

    function ClearMessages() {
        $("#lblMsgCount").text("Count : 0");
        $("#txtMessages").val('');
    }

    var ParseResults = function () {
        var keywords = $("#txtParseInput").val();
        var ts = Math.round(new Date().getTime() / 1000);
        
        if(keywords.trim().length > 0) {
            ClearMessages();
            ClearMarkers();
        }
        
        jQuery.ajax({
            type: "GET",
            url: "../HttpHandler/MessageHandler.ashx?operation=ParseData&Keywords=" + keywords.trim() + "&ts=" + ts,
            success: DisplayParseResults,
            error: errorFn
        });
    }

    function DeleteSelectedQuery() {
    
    }
    
    var SaveQuery = function () {
        ClearValues();
        var queryName = $("#txtQueryName").val();
        if(queryName.trim().length == 0) {
            alert('Please enter a unique query name!')
        }
        else {
            var event = $("#txtEventList").val();
            var eAttr = $("#txtEventAttributeTypeList").val();
            var eAttrSub = $("#txtEventAttributeSubTypeList").val(); 
                   
            var ts = Math.round(new Date().getTime() / 1000);
            
            if(event.trim().length == 0 && eAttr.trim().length == 0 && eAttrSub.trim().length == 0) {
                alert('Please choose a search criteria!')
            }
            else {   
                var objX = new Object();
                objX.RD_EventDateFrom = "";
                objX.RD_EventDateTo = "";
                objX.RD_Cam = "";
                objX.RD_TML = "";
                objX.RD_SQLQueryID = "";
                objX.RD_QueryName = queryName.trim();
                objX.RD_FQuery = {};
                objX.RD_FQuery.FQ_EventType = event;
                objX.RD_FQuery.FQ_EventAttr = eAttr;
                objX.RD_FQuery.FQ_EventAttrSub = eAttrSub;
                var jsonStr = JSON.stringify(objX);         
                jQuery.ajax({
                    type: "GET",
                    url: "../HttpHandler/MessageHandler.ashx?operation=SaveQuery&RequestData=" + jsonStr + "&ts=" + ts,
                    success: SaveQueryResult,
                    error: errorFn
                });
            }
        }
    }

    var LoadMap = function () {
        mapOptions = {
            center: new google.maps.LatLng(36.167597, -86.787186),
            zoom: 11,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
        markerCluster = new MarkerClusterer(map);
    }
  

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
        ClearMarkers();
        var eventDateFrom = $("#txtEventDateFrom").val();
        var eventDateTo = $("#txtEventDateTo").val();
        var sqlQuery = $('#ddlSQLQuery option:selected').val();
        var cam = $('#ddlCam option:selected').val(); 
        var event = $("#txtEventList").val();
        var eAttr = $("#txtEventAttributeTypeList").val();
        var eAttrSub = $("#txtEventAttributeSubTypeList").val();
        var processId = $("#txtProcessId").val();
        var processSubTypeId = $("#txtProcessSubTypeId").val();
        var tml = $('#ddlTML option:selected').val();
        var objX = new Object();
        objX.RD_EventDateFrom = eventDateFrom;
        objX.RD_EventDateTo = eventDateTo;
        objX.RD_Cam = cam;
        objX.RD_TML = tml;
        objX.RD_SQLQueryID = sqlQuery;
        objX.RD_FQuery = {};
        if(sqlQuery == 0) {
            objX.RD_FQuery.FQ_EventType = event;
            objX.RD_FQuery.FQ_EventAttr = eAttr;
            objX.RD_FQuery.FQ_EventAttrSub = eAttrSub;
            objX.RD_FQuery.FQ_PID = processId;
            objX.RD_FQuery.FQ_PSID = processSubTypeId;
        }
        var jsonStr = JSON.stringify(objX);
                
        var ts = Math.round(new Date().getTime() / 1000);
        
        jQuery.ajax({
            type: "GET",
            url: "../HttpHandler/MessageHandler.ashx?operation=Map&RequestData=" + jsonStr + "&ts=" + ts,
            success: SetMarkers,
            error: errorFn
        });
    }

    function RefreshMessages() {
        ClearMessages();
        var eventDateFrom = $("#txtEventDateFrom").val();
        var eventDateTo = $("#txtEventDateTo").val();
        var sqlQuery = $('#ddlSQLQuery option:selected').val();
        var cam = $('#ddlCam option:selected').val(); 
        var event = $("#txtEventList").val();
        var eAttr = $("#txtEventAttributeTypeList").val();
        var eAttrSub = $("#txtEventAttributeSubTypeList").val();
        var processId = $("#txtProcessId").val();
        var processSubTypeId = $("#txtProcessSubTypeId").val();
        var tml = $('#ddlTML option:selected').val();
        var objX = new Object();
        objX.RD_EventDateFrom = eventDateFrom;
        objX.RD_EventDateTo = eventDateTo;
        objX.RD_Cam = cam;
        objX.RD_TML = tml;
        objX.RD_SQLQueryID = sqlQuery;
        objX.RD_FQuery = {};
        if(sqlQuery == 0) {
            objX.RD_FQuery.FQ_EventType = event;
            objX.RD_FQuery.FQ_EventAttr = eAttr;
            objX.RD_FQuery.FQ_EventAttrSub = eAttrSub;
            objX.RD_FQuery.FQ_PID = processId;
            objX.RD_FQuery.FQ_PSID = processSubTypeId;
        }
        var jsonStr = JSON.stringify(objX);
                
        var ts = Math.round(new Date().getTime() / 1000);
        
        jQuery.ajax({
            type: "GET",
            url: "../HttpHandler/MessageHandler.ashx?operation=MessageList&RequestData=" + jsonStr + "&ts=" + ts,
            success: DisplayMessages,
            error: errorFn
        });
    }

    function errorFn(data) {

    }

    var createJSTree = function() {
        var isPostBack = $("#hdnIsPostBack").val();
        var processId = $("#txtProcessId").val();
        var processSubTypeId = $("#txtProcessSubTypeId").val();
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
                            "data": function(n) {
                                // the result is fed to the AJAX request `data` option
                                return {
                                    "operation": "jsTree",
                                    "nodeType": n.attr ? n.attr("some-other-attribute") : "Message",
                                    "id": n.attr ? n.attr("id") : 0,
                                    "attributeType": n.attr ? n.attr("attributeType") : 0,
                                    "ts": Math.round(new Date().getTime() / 1000),
                                    "processId": processId,
                                    "processSubTypeId": processSubTypeId
                                };
                            }

                        }
                    }
                });
        }
    };

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

    var CreateTabs = function () {
        $("#tabs").tabs();
    };

    var onChecked = function () {
        $("#demo").bind("check_node.jstree", function (e, data) {
            $("#ddlSQLQuery").val("0");
            GetCheckedItems();
            RefreshMarkers();
            RefreshMessages();
            ClearValues();
        });
    };


    var onUnChecked = function () {
        $("#demo").bind("uncheck_node.jstree", function (e, data) {
            GetCheckedItems();
            RefreshMarkers();
            RefreshMessages();
            ClearValues();
        });
    };
    
    function OnSavedSQLQueryChange() {
        $("#demo").jstree("uncheck_all");
        ClearJSTreeTextBoxes();
        ClearValues();
        RefreshMarkers();
        RefreshMessages();         
    }
    
    function OnCamChange() {
        ClearValues();
        RefreshMarkers();
        RefreshMessages();    
    }
    
    function OnTMLChange() {
        ClearValues();
        RefreshMarkers();
        RefreshMessages();  
    }
    
    function ClearValues() {
        $("#txtParseInput").val('');
    }
    
    function ClearJSTreeTextBoxes() {
        $("#txtEventList").val('');
        $("#txtEventAttributeTypeList").val('');
        $("#txtEventAttributeSubTypeList").val('');
    }
    
    
    function viewImages() {
        window.open("SlideShowPage.aspx", "View", "status=no,toolbar=no,menubar=no,resizable=0,width=450,height=380,'oChild'");
    }

        
    function viewBarChart() {
        window.open("NewBarChart.aspx", "View", "status=no,toolbar=no,menubar=no,resizable=1,width=1050,height=850,'oChild'");
    }

        
    function viewNodeGraph() {
        window.open("NodeGraph.aspx", "View", "status=no,toolbar=no,menubar=no,resizable=0,width=1050,height=850,'oChild'");
    }

        
    function ManageSavedQueries() {
        window.open("ManageSavedQuery.aspx", "View", "status=no,toolbar=no,menubar=no,resizable=1,width=730,height=320,'oChild'");
    }
       