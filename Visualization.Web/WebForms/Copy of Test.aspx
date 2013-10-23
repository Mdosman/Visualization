﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Copy of Test.aspx.cs" Inherits="WebForms_Test" %>

<!DOCTYPE html>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" /> 

	<title>Nested 3-Levels Demo</title> 

	<!--<link type="text/css" rel="stylesheet" href="../lib/css/layout-default-latest.css" />-->

	<style type="text/css">
	
	/*
	 * GENERAL COSMETICS 
	 */
	body {
		font-family: Arial, Helvetica, sans-serif;
		*font-size:	90%; /* IE only */
		padding:    15px;
		margin:		0;
	}
	#layout_container ,
	#layout_container div {
		padding:    15px;
		overflow:	auto;
	}
	div#layout_container {
		height:		500px;
		overflow:	visible;
	}
	.center {
		text-align:	center;
	}
	
	/*
	 * ADD BORDERS & COLORS TO LAYOUT ELEMENTS
	 */
	/* use 'generic classes' to add default backgrounds & borders */
	.ui-layout-pane		{ background: #FFF; border: 1px dotted #999; }
	.ui-layout-resizer	{ background: #EEE !important;; border: 1px solid #999; }
	.ui-layout-toggler	{ background: #999 !important; } /* override '.middle-center div' rule */
	/* give specific elements their own colors */
	#layout_container	{ background: #999; }
	.outer-center	 	{ background: #FDD; }
	.outer-west	 		{ background: #FDD; }
	.middle-center		{ background: #CCF; }
	.middle-south 		{ background: #CFC; }
	.middle-center div ,
	.middle-south div	{ background: #FFF; }

	</style>

<script src="../Scripts/jQuery/jquery-latest.js" type="text/javascript"></script>
<script src="../Scripts/jQuery/jquery-ui-latest.js" type="text/javascript"></script>
<script type="text/javascript" src="../Scripts/jQuery/jquery.layout-latest.js"></script>
<%--<link href="../Styles/layout-default-latest.css" rel="stylesheet" type="text/css" />--%>
	<script type="text/javascript">

	var outerLayout, middleLayout, innerLayout_Center, innerLayout_South;

	function createLayouts () {

		outerLayout = $('#layout_container').layout({
				name:					"outer"
			,	spacing_open:			8 // ALL panes
			,	spacing_closed:			12 // ALL panes
			,	north__paneSelector:	".outer-north"
			,	center__paneSelector:	".outer-center"
			,	west__paneSelector:		".outer-west"
			,	west__size:				150
			,	minSize:				50
		});

		middleLayout = $('div.outer-center').layout({
				name:					"middle"
			,	center__paneSelector:	".middle-center"
			,	south__paneSelector:	".middle-south"
			,	south__size:			300
			,	minSize:				50
			,	spacing_open:			8	// ALL panes
			,	spacing_closed:			12 // ALL panes
		});

		innerLayout_Center = $('div.middle-center').layout({
				name:					"innerCenter"
			,	center__paneSelector:	".north-center"
			,	west__paneSelector:		".north-west"
			,	west__size:				200
			,	minSize:				50
			,	spacing_open:			8	// ALL panes
			,	spacing_closed:			8	// ALL panes
			,	west__spacing_closed:	12
		});

		innerLayout_South = $('div.middle-south').layout({
				name:					"innerSouth"
			,	center__paneSelector:	".south-center"
			,	west__paneSelector:		".south-west"
			,	west__size:				200
			,	minSize:				50
			,	spacing_open:			8	// ALL panes
			,	spacing_closed:			8	// ALL panes
			,	west__spacing_closed:	12
		});

	};

	$(document).ready(function(){
		createLayouts();
		//$("#btnCreate").attr('disabled', true);
		//$("#btnCreate").removeAttr('disabled'); // avoid caching issue
	});

	</script>
</head>
<body>
<div id="layout_container"><!-- Outer Layout Container -->
    <div class="outer-north center">
		North <br /><br />(Outer Layout - North Pane)
	</div>
	<div class="outer-west center">
		West <br /><br />(Outer Layout - West Pane)
	</div>

	<div class="outer-center"><!-- Middle Layout Container -->

		<div class="middle-center"><!-- Inner-North Layout Container -->
			<div class="north-center border">North Center <br /><br />(Inner-Center Layout - Center Pane)</div>
			<div class="north-west border">North West     <br /><br />(Inner-Center Layout - West Pane)</div>
		</div>

		<div class="middle-south"><!-- Inner-South Layout Container -->
			<div class="south-center border">South Center <br /><br />(Inner-South Layout - Center Pane)</div>
			<div class="south-west border">South West     <br /><br />(Inner-South Layout - West Pane)</div>
		</div>

	</div>

</div>

<%--<p style="text-align: center;">
	<button id="btnCreate" onclick="createLayouts(); this.disabled='disabled';" style="font-size: 1.3em;">Create the Layouts NOW</button>
</p>--%>

</body>
</html>