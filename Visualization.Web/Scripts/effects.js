// JavaScript Document
$(document).ready(function () {
    // BROWSER Detection //						   
    var browser = navigator.appName;
    var b_version = navigator.appVersion;
    var version = parseFloat(b_version);

    // Using browser detection to disable the jQuery Blend effect on the main menu in IE6 and Opera - z-index issues //

    if (b_version.indexOf("MSIE 6.0") == -1 && browser.indexOf("Opera") == -1 && b_version.indexOf("MSIE 7.0") == -1) {
        $("#menu_group_main a").blend();
    }

    // I have used IF statements to avoid missing elements or functions on pages. //
    // The effects will work only if the linked element exists in the document    //

    //Set the current class for this menu item
    setCurrentTab();

    var date = new Date();
    var currentMonth = date.getMonth();
    var currentDate = date.getDate();
    var currentYear = date.getFullYear();

    $('#datepicker').datepicker({
        beforeShow: function () { $('#ui-datepicker-div').maxZIndex(); },
        onSelect: function (date) {
            $('#ctl00_hdnDatePicker').val(date);
            document.forms[0].submit();
        },
        maxDate: new Date(currentYear, currentMonth, currentDate),
        dateFormat: 'mm/dd/yy',
        showOn: "button",
        buttonImage: "../images/calendar1.png",
        buttonImageOnly: true,
        buttonText: "Select the date to view previus census"
    });

});

function setCurrentTab() {
    //Set the current class for this menu item
    var menuItem = $('#ctl00_hdnMenuItem').val();
    $('#lnk' + menuItem).addClass("current");
}

$.maxZIndex = $.fn.maxZIndex = function (opt) {
    /// <summary>
    /// Returns the max zOrder in the document (no parameter)
    /// Sets max zOrder by passing a non-zero number
    /// which gets added to the highest zOrder.
    /// </summary>    
    /// <param name="opt" type="object">
    /// inc: increment value, 
    /// group: selector for zIndex elements to find max for
    /// </param>
    /// <returns type="jQuery" />
    var def = { inc: 10, group: "*" };
    $.extend(def, opt);
    var zmax = 0;
    $(def.group).each(function () {
        var cur = parseInt($(this).css('z-index'));
        zmax = cur > zmax ? cur : zmax;
    });
    if (!this.jquery)
        return zmax;

    return this.each(function () {
        zmax += def.inc;
        $(this).css("z-index", zmax);
    });
}