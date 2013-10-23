$(document).ready(function() {
         
     $("#txtDate").dynDateTime({
        showsTime: true,
        ifFormat: "%Y/%m/%d %H:%M",
        daFormat: "%l;%M %p, %e %m,  %Y",
        align: "BR",
        electric: false,
        singleClick: false,
        displayArea: ".siblings('.dtcDisplayArea')",
        button: ".next()"
    });
    
    // JQuery Dialog
    $("#diagFilters").dialog( {   
        title: "FILTERS", 
        autoOpen: false, 
        modal: true, 
        bgiframe: true,
        width: 1000,
        height: 650,
        draggable: true,
        buttons: {OK: chooseOption}
     }).parent().appendTo(jQuery("form:first"));
    $("#lnkFilters").click(function() {
		$("#diagFilters").dialog("open");
	});	
	
	$("#chkVehicle").click(function() {
		UnCheckVehicleFilters();
	});
	$("#chkHuman").click(function() {
		UnCheckHumanFilters();
	});
	$("#chkObject").click(function() {
		UnCheckObjectFilters();
	});
	$("#chkGroupPattern").click(function() {
		UnCheckGroupPatternFilters();
	});
	$("#chkGroupActivity").click(function() {
		UnCheckGroupActivityFilters();
	});
	$("#chkSound").click(function() {
		UnCheckSoundFilters();
	});
	
	$('#filterTabs_tabPanelVehicle input[type=checkbox]').click(function(){
        if(this.checked){
            ChkVehicle();
        }
    });
	
	$('#filterTabs_tabPanelHuman input[type=checkbox]').click(function(){
        if(this.checked){
            ChkHuman();
        }
    });
	
	$('#filterTabs_tabPanelObject input[type=checkbox]').click(function(){
        if(this.checked){
            ChkObject();
        }
    });
	
	$('#filterTabs_tabPanelGroupPattern input[type=checkbox]').click(function(){
        if(this.checked){
            ChkGroupPattern();
        }
    });
	
	$('#filterTabs_tabPanelGroupActivity input[type=checkbox]').click(function(){
        if(this.checked){
            ChkGroupActivity();
        }
    });
	
	$('#filterTabs_tabPanelSound input[type=checkbox]').click(function(){
        if(this.checked){
            ChkSound();
        }
    });

});

function chooseOption() {
    
//	var selectedOption=$("#ctl00_ContentPlaceHolder1_ddlReviseLicenseReason option:selected").val();
//	if(selectedOption == '0' || selectedOption == '') {
//	    alert("Please choose a Revise/Re-issue Reason!");
//	}	
__doPostBack('btnSend', '');
}

function UnCheckVehicleFilters() {
	if ($("#chkVehicle").is(":checked")) {
	}
	else {        
        $('#filterTabs_tabPanelVehicle').find('input[type=checkbox]:checked').removeAttr('checked');
	}
}

function UnCheckHumanFilters() {
	if ($("#chkHuman").is(":checked")) {
	}
	else {      
        $('#filterTabs_tabPanelHuman').find('input[type=checkbox]:checked').removeAttr('checked');
	}
}
function UnCheckObjectFilters() {
	if ($("#chkObject").is(":checked")) {
	}
	else {      
        $('#filterTabs_tabPanelObject').find('input[type=checkbox]:checked').removeAttr('checked');
	}
}
function UnCheckGroupPatternFilters() {
	if ($("#chkGroupPattern").is(":checked")) {
	}
	else {      
        $('#filterTabs_tabPanelGroupPattern').find('input[type=checkbox]:checked').removeAttr('checked');
	}
}
function UnCheckGroupActivityFilters() {
	if ($("#chkGroupActivity").is(":checked")) {
	}
	else {      
        $('#filterTabs_tabPanelGroupActivity').find('input[type=checkbox]:checked').removeAttr('checked');
	}
}
function UnCheckSoundFilters() {
	if ($("#chkSound").is(":checked")) {
	}
	else {      
        $('#filterTabs_tabPanelSound').find('input[type=checkbox]:checked').removeAttr('checked');
	}
}

function ChkVehicle() {
	if ($("#chkVehicle").is(":checked")) {
	}
	else {        
        $('#chkVehicle').attr('checked','checked');
	}
}

function ChkHuman() {
	if ($("#chkHuman").is(":checked")) {
	}
	else {        
        $('#chkHuman').attr('checked','checked');
	}
}
function ChkObject() {
	if ($("#chkObject").is(":checked")) {
	}
	else {         
        $('#chkObject').attr('checked','checked');
	}
}
function ChkGroupPattern() {
	if ($("#chkGroupPattern").is(":checked")) {
	}
	else {         
        $('#chkGroupPattern').attr('checked','checked');
	}
}
function ChkGroupActivity() {
	if ($("#chkGroupActivity").is(":checked")) {
	}
	else {          
        $('#chkGroupActivity').attr('checked','checked');
	}
}
function ChkSound() {
	if ($("#chkSound").is(":checked")) {
	}
	else {        
        $('#chkSound').attr('checked','checked');
	}
}
