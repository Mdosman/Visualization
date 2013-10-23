    $(document).ready(function () {
         window.setTimeout(CreateBarGraph, 100); 
    });
       
   
    
        function createChart(newSeriesOptions) {
//            var newSeriesOptions = '';
//            if (data != undefined) {
//                if (data.length > 0) {
//                    newSeriesOptions = jQuery.parseJSON(data);
//                }
//            }
            // Create the chart
            window.chart = new Highcharts.StockChart({
                chart: {
                    renderTo: 'divBar',
                    alignTicks: false
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Events'
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
                    selected: 2
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
                                click: function () {
                                    hs.htmlExpand(null, {
                                        pageOrigin: {
                                            x: this.pageX,
                                            y: this.pageY
                                        },
                                        headingText: this.series.name,
                                        maincontentText: Highcharts.dateFormat('%A, %b %e, %Y', this.x) + ':<br/> ' + this.y + ' ' + '<br/><img src="../Pics/' + this.x + '.bmp" alt="Highslide JS" title="Click to enlarge" />',
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
                series: newSeriesOptions

            });
        }
       
   function errorFn() {
   }
        
   var CreateBarGraph = function () {                
        var ts = Math.round(new Date().getTime() / 1000);        
        jQuery.ajax({
            type: "GET",
            url: "../HttpHandler/MessageHandler.ashx?operation=BarGraph&RequestData=&ts=" + ts,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: createChart,
            error: errorFn
        });
    };