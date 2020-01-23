var initialLoading = true, gridRowTouched;
var gridloaded=false;
var oaDate = new Date(1899, 11, 30);
var rowSelect = false;
var millisecondsOfaDay = 24 * 60 * 60 * 1000;
var colChartColor = ["#E8BD46", "#369E63", "#B73939", "#2BAABF", "#CC793B", "#4586A0", "#7AA542", "#C13870", "#B348E5"];
Date.prototype.ToOADate = function () {
    var result = (Date.parse(this) - Date.parse(oaDate)) / millisecondsOfaDay;
    return result;
};
var salesOverAllData,prevDate=null; mapData = [],gridData = [], chartData = [], rangeData = [], gaugeData = [], salesData = [],contMap=[],subMap=[];
$(document).delegate($("div[data-role='page']"), "pagebeforeshow", function () {
    //document.getCurrentPage().addClass("sf-m-windows");
   
});
function clearMapRelatedData() {
    chartData = [], rangeData = [], gaugeData = [], salesData = [];
}
function clearRangeRelatedData() {

    mapData = [], gridData = [],chartData = [], gaugeData = [], salesData = [];
}
function clearOverall() {
    mapData = [], gridData = [],chartData = [], gaugeData = [], rangeData = [], salesData = [];
}
//Rendering Map
function renderMap(mapsData) {
    if (!initialLoading) {
	
        var mapObj = $("#Map").data("ejMap");
        mapObj.model.layers[0].subLayers[0].dataSource = mapsData;
        $("#Map").ejMap("refresh");
    }
    else {
        var map = $("<div  style='position:relative;height:400px;width:96%' class='mapborder' id='mapborder'><div id ='Map'> </div></div>");
        var scrollDiv = $("<div id='ScrollPanel' data-targetid='ScrollContent' data-scrolltype='Vertical' data-targetheight='175'></div>");
        $(".container .gridchart >div:last").append(map);
        $(".container").append(scrollDiv);
		
        $("#Map").ejMap({          
          
			shapeSelected: 'onDrilled',
			zoomSettings:{
                enableZoom:false
            },			
            enableAnimation: true,
            layers: [
                  {
				          layerType:'geometry',				
                          enableSelection: false,
                          shapeSettings: {
                              fill: "#515151",
                              strokeThickness: "0",
                              stroke: "white",
                              valuePath: "name",
                              selectionColor: "gray"
                          },                         
                          shapeData: world_map,
						   subLayers: [
                              {
                                   shapeDataPath: "State",
                                   shapePropertyPath: "name",
                                   showMapItems: false,  
								   showTooltip :true,
                                   tooltipTemplate:"Maptooltiptemplate",
                                   dataSource: mapsData,
                                   shapeSettings: {
                                      fill: "#707070",
                                       strokeThickness: "0",
                                       stroke: "white",
                                       selectionColor: "gray"
                                   },   
                                     bubbleSettings: {
                                      valuePath: "Sales",
                                      bubbleRatio:10,
                                      minValue: 2,
                                      maxValue: 20,
                                      color: "#379F64",
                                  },								   
                                  shapeData: mapSalesData
                              }

                          ]
                      }
                  
            ]
        });
    }
}

function renderGrid(gridData) {
    if (!initialLoading) {
        $("#Grid").ejGrid("model.dataSource", gridData);
    }
    else {
        var grid = $("<div id ='Grid' ></div>");
        var scrollDiv = $("<div id='ScrollPanel' data-targetid='ScrollContent' data-scrolltype='Vertical' data-targetheight='175'></div>");
        $(".container .gridchart >div:last > div").append(grid);
        $(".container").append(scrollDiv);
        $("#Grid").ejGrid({
            dataSource: gridData,
            allowScrolling: true,
            scrollSettings: { height: 350,width:0},
			rowSelected: "gridRowSelected",
            columns: [{ field: "State", headerText: 'Country',width: 120 }, { field: "Sales", headerText: 'Sales',width: 80 }, { field: "SalesvsTarget",width: 70, headerText: 'Marketing vs Revenue' }],
            actionComplete: "afterRender",
			
            queryCellInfo: function (args) {
                var $element = $(args.cell);
                switch (args.column.headerText) {
                    case "Sales":
                        if (!$element.hasClass("templatecell")) {
                            var value = parseInt(parseFloat($element.html()) / 10000);
                            $element.html( ej.format(value,"c0") + "M");
                        }
                        break;
                    case "Marketing vs Revenue":
                        var value = parseFloat(parseFloat($element.html()).toFixed(2));
                        $element.html("<div class ='value'>" + value + "%</div>");
                        $element.append($("<div class ='triangle'></div>"));
                        if (parseInt(parseFloat(args.data.Sales) / 10000) < 45)
                            $element.addClass('target');
                        break;
                }
            }
        });
    }
    
}

//Rendering Chart
function renderChart(chartData) {
    if (!initialLoading) {
	
        $("#chartContent").ejChart("option", {
            series: [{
                dataSource: chartData[0].series,
				xName: "Category",
				yName: "SalesPrice",
                type: 'column',
				
            }]
			
        });
    }
    else {
        var chart = $(" <div style='margin-top:10px;' class='titlediv'>Sales by Product Category</div><div id ='chartContent'></div>");
        $(".main .gridchart >div:first").empty();
        $(".main .gridchart >div:first").append(chart);
        $("#chartContent").ejChart({
            isResponsive: true,
            primaryXAxis: { majorGridLines: { visible: false },labelRotation: 35},
            primaryYAxis: { rangePadding: 'normal', labelFormat: '{value}M' },
            series: [
            {
                dataSource: chartData[0].series,
				xName: "Category",
				yName: "SalesPrice",
                type: 'column',
                enableAnimation:true
            }],
            size: { height: "420" },
			theme:'gradientdark',
            legend: { visible: false },
            preRender: "beforeChartRender",
            axesLabelRendering: "labelRendering"
        });
    }
}
//Rendering Range, LineChart and Gauge while selecting the Map
function onDrilled(args) {
    if (args.originalEvent.length>0) {
        $(".gridchart >.col-md-6 ,.mapwaiting, .gaugecontainer").addClass("addbackground");
        initialLoading = true;        
        clearMapRelatedData();
        var currentState = args.originalEvent[0].data.State;
        var dataMgr = ej.DataManager(salesOverAllData);
        var group;
        var chartgauge, range;
        //To return data to render Chart and Gauge
        group = ej.Query().from(salesOverAllData).where("State", ej.FilterOperators.equal, currentState).sortBy("CategoryName").group("CategoryName");
        chartgauge = dataMgr.executeLocal(group);
        renderChartData(chartgauge);
        renderGaugeData(chartgauge);
        dataMgr = ej.DataManager(gaugeData);
        var gaugeSort = dataMgr.executeLocal(ej.Query().from(gaugeData).sortByDesc("SalesPrice"));
        if((mapData[mapData.length-1])!=null) {
        salesData = { "MaxValue": mapData[mapData.length-1].Sales, "MinValue": mapData[mapData.length-1].Sales, "MaxSalesPrice": gaugeSort[0].SalesPrice };
        }
        //Chart
        renderChart(chartData);
        initialLoading = false;
        //Gauge
        renderGauge(gaugeData);
        $(".gridchart >.col-md-6,.mapwaiting, .gaugecontainer").removeClass("addbackground");
    }
   

}
function labelRendering(args) {
    if (args.data.axis.orientation.toLowerCase() == "vertical") {
        args.data.label.Text = args.data.label.Value / 10000000 + "M";
    }
}
function beforeChartRender(args) {
    $.each(args.model.series[0].points, function (index, value) {
        value.fill = colChartColor[index];
    });
}
//Rendering Range Chart
function renderRangeChart(rangeData) { 
    var rangeDiv = $("<div  id ='rangeChart'></div>");
    var color = { color: "#FFFFFF" };
    
    color = { stroke: 0 };
    var border;    
    border = "#878787";

    $(".main .rangecontainer >div:last").empty();
    $(".main .rangecontainer >div:last").append(rangeDiv);
	if (document.documentMode == 8){
	var rangeDateCol = [];
	for (var i=0;i<rangeData.length;i++)
	{
		var startresult = rangeData[i].Date.split("/");	
		rangeDateCol.push(new Date(startresult[2],startresult[1],startresult[0]));
	}
	
	for (var j=0;j<rangeData.length;j++)
	{
		rangeData[j].Date=rangeDateCol[j];
	}
	
	var startDate = rangeData[rangeData.length - 8].Date;
	var endDate = rangeData[rangeData.length - 6].Date;
	
	
    $("#rangeChart").ejRangeNavigator({
        enableDeferredUpdate: true,
       
        isResponsive: true,
		theme:'gradientdark',
        selectedRangeSettings: {
            start: startDate.toString(), end: endDate.toString()
        },
        tooltipSettings: { visible: true, labelFormat: "dd/MM/yyyy", color: "#2d2d2d", backgroundColor: "#e5e5e5" },
        dataSource: rangeData, xName: "Date", yName: "SalesPrice",
        navigatorStyleSettings: {
            selectedregioncolor: "lightgray",
                unselectedregioncolor:[{ color: "black", colorstop: "20%" }, { color: "white", colorstop: "100%" }],
                thumbcolor: "black",
                thumbradius:13,
                background: "transparent",
                border: { color: "transparent", stroke:0 },
                majorgridlinestyle: { color: "black", visible: true },
                minorgridlinestyle: { color: "black", visible: true },
        },
        series: [
        {
            name: 'SalesRate',
            animation: true,
            type: 'line',
			opacity: 1,
			width:3,
			interior: 'darkred',
            border: { color: 'transparent' }
        }],
        seriesSettings: {
            type: 'column', animation: true
        },
        labelSettings: {
            higherLevel: {
                visible: false,
                style: { font: { color: 'white', family: 'Segoe UI', style: 'Normal ', size: '10px', opacity: 1, weight: 'regular' } },
                border: { color: "#FFFFFF", width: 0 },
                gridLineStyle: { color: "#FFFFFF" }
            },
            lowerLevel: {
                intervalType: 'months',
                visible: true,
                style: { font: { color: 'white', family: 'Segoe UI', style: 'Normal ', size: '10px', opacity: 1, weight: 'regular' } },
                border: { color: "#FFFFFF", width: 0.1 },
                gridLineStyle: { color: "#FFFFFF", width: 0.1 }
            }
        },
        rangeChanged: 'rangeDateSelected'
    });
	}
	else{
	    $("#rangeChart").ejRangeNavigator({
        enableDeferredUpdate: true,
       
        isResponsive: true,
		theme:'gradientdark',
        selectedRangeSettings: {
            start: rangeData[rangeData.length - 8].Date.toString(), end: rangeData[rangeData.length - 6].Date.toString()
        },
        tooltipSettings: { visible: true, labelFormat: "dd/MM/yyyy", color: "#2d2d2d", backgroundColor: "#e5e5e5" },
        dataSource: rangeData, xName: "Date", yName: "SalesPrice",
        navigatorStyleSettings: {
            selectedregioncolor: "lightgray",
                unselectedregioncolor:[{ color: "black", colorstop: "20%" }, { color: "white", colorstop: "100%" }],
                thumbcolor: "black",
                thumbradius:13,
                background: "transparent",
                border: { color: "transparent", stroke:0 },
                majorgridlinestyle: { color: "black", visible: true },
                minorgridlinestyle: { color: "black", visible: true },
        },
        series: [
        {
            name: 'SalesRate',
            animation: true,
            type: 'line',
			opacity: 1,
			width:3,
			interior: 'darkred',
            border: { color: 'transparent' }
        }],
        seriesSettings: {
            type: 'column', animation: true
        },
        labelSettings: {
            higherLevel: {
                visible: false,
                style: { font: { color: 'white', family: 'Segoe UI', style: 'Normal ', size: '10px', opacity: 1, weight: 'regular' } },
                border: { color: "#FFFFFF", width: 0 },
                gridLineStyle: { color: "#FFFFFF" }
            },
            lowerLevel: {
                intervalType: 'months',
                visible: true,
                style: { font: { color: 'white', family: 'Segoe UI', style: 'Normal ', size: '10px', opacity: 1, weight: 'regular' } },
                border: { color: "#FFFFFF", width: 0.1 },
                gridLineStyle: { color: "#FFFFFF", width: 0.1 }
            }
        },
        rangeChanged: 'rangeDateSelected'
    });
	}
}
//Rendering Gauge
function renderGauge(gaugeData) {
    if (!initialLoading) {
        (parseInt($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "books") return value.SalesPrice; })) < 50) ? $('#Books .triangle').addClass('inverted') : $('#Books .triangle').removeClass('inverted');


        (parseInt($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "electronics") return value.SalesPrice; })) < 50) ? $('#Electronics .triangle').addClass('inverted') : $('#Electronics .triangle').removeClass('inverted');
        (parseInt($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "automotive") return value.SalesPrice; })) < 50) ? $('#Automotive .triangle').addClass('inverted') : $('#Automotive .triangle').removeClass('inverted');
        (parseInt($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "clothing") return value.SalesPrice; })) < 50) ? $('#Clothing .triangle').addClass('inverted') : $('#Clothing .triangle').removeClass('inverted');
        $("#Automotive").ejCircularGauge("setPointerValue", 0, 0, $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "automotive") return value.SalesPrice; }));
			
        $("#Automotive").ejCircularGauge("setCustomLabelValue", 0, 0,   "$" + parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "automotive") return value.SalesPrice; }))) + "K " + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "automotive") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%)");

        $("#Books").ejCircularGauge("setPointerValue", 0, 0, $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "books") return value.SalesPrice; }));
        $("#Books").ejCircularGauge("setCustomLabelValue", 0, 0,"$" + parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "books") return value.SalesPrice; })))+ "K "  + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "books") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%)");

        $("#Clothing").ejCircularGauge("setPointerValue", 0, 0, $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "clothing") return value.SalesPrice; }));
        $("#Clothing").ejCircularGauge("setCustomLabelValue", 0, 0, "$" +parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "clothing") return value.SalesPrice; })))+"K " + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "clothing") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%)");

        $("#Electronics").ejCircularGauge("setPointerValue", 0, 0, $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "electronics") return value.SalesPrice; }));
        $("#Electronics").ejCircularGauge("setCustomLabelValue", 0, 0, "$" + parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "electronics") return value.SalesPrice; })))+"K " + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "electronics") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%)");
    }
    else {
		var automotive = $("<div class='customlabel'>Automotive</div><div style='horiz-align:center;' id ='Automotive'></div>");
        var books = $("<div class='customlabel'>Books</div><div style='horiz-align:center;' id ='Books'></div>");
        var clothing = $("<div class='customlabel'>Clothing</div><div style='horiz-align:center;' id ='Clothing'></div>");
        var electronics = $("<div class='customlabel'>Electronics</div><div style='horiz-align:center;' id ='Electronics'></div>");
        $(".main .gaugecontainer .automotive,.main .gaugecontainer .books,.main .gaugecontainer .clothing,.main .gaugecontainer .electronics").empty();
        $(".main .gaugecontainer .automotive").append(automotive);
        $(".main .gaugecontainer .books").append(books);
        $(".main .gaugecontainer .clothing").append(clothing);
        $(".main .gaugecontainer .electronics").append(electronics);
        $("#Automotive").ejCircularGauge({
            backgroundColor: "transparent", width: 200, height: 125, animate: false,
            scales: [{
                showRanges: true,
                startAngle: 170, sweepAngle: 200, radius: 50, showScaleBar: false, size: 1,  maximum: 300, majorIntervalValue: 100, minorIntervalValue: 10,
				border:{
				    width:0.5
                },
                 pointerCap:{
				     radius: 3
				},    				
                pointers: [{
                    value: $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "automotive") return value.SalesPrice; }),
                    showBackNeedle: true,
                    backNeedleLength: 10,
                    length: 35,
                    width: 2,
                    color: "#898989",
					
                }],
                labels: [{
                    distanceFromScale: -14,
                    color: "#8c8c8c"
                }], 
                ticks: [{
                    style: "Major",
                    distanceFromScale: 2,
                    height: 16,
                    width: 1, color: "transparent"
                },
                {
                    style: "Minor",
                    height: 8,
                    width: 1,
                    distanceFromScale: 2,
                    color: "transparent"
                }],
                ranges: [{
                    distanceFromScale: -20,
                    startValue: 0,
                    endValue: 95,
                    backgroundColor: "rgba(54,158,99,0.3)",
                    border:{
					    color: "rgba(54,158,99,0.3)"
			        },
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 101,
                    endValue: 200,
                    backgroundColor: "rgba(54,158,99,0.6)",
                    border:{
					     color: "rgba(54,158,99,0.6)"
			         },
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 208,
                    endValue: 300,
                    backgroundColor: "rgba(54,158,99,1)",
                    border:{
					     color: "rgba(54,158,99,1)"
				  },
                    size: 2
                }],
				customLabels: [{
                        value:  "$" + parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "automotive") return value.SalesPrice; }))) + "K " + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "automotive") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%",
                        position: { x: 115, y: 102 },
                        font: { size: "11px", fontFamily: "Segoe UI", fontStyle: "normal" }, color: "gray"
                    }]
            }]
        });
        $("#Books").ejCircularGauge({
            backgroundColor: "transparent", width: 200, height: 125, animate: false,
            scales: [{
                showRanges: true,
                startAngle: 170, sweepAngle: 200, radius: 50, showScaleBar: false, size: 1, scaleBorderWidth: 0.5, maximum: 300, majorIntervalValue: 100, minorIntervalValue: 10,
               pointerCap:{
				     radius: 3,
					 
				},   
                color: "#898989",
                pointers: [{
                    value: $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "books") return value.SalesPrice; }),
                    showBackNeedle: true,
                    backNeedleLength: 8,
                    length: 35,
                    width: 2
                }],
                labels: [{
                    distanceFromScale: -14,
                    color: "#8c8c8c"
                }],
                ticks: [{
                    style: "Major",
                    distanceFromScale: 2,
                    height: 16,
                    width: 1, color: "transparent"
                },
                {
                    style: "Minor",
                    height: 8,
                    width: 1,
                    distanceFromScale: 2,
                    color: "transparent"
                }],

                ranges: [{
                    distanceFromScale: -20,
                    startValue: 0,
                    endValue: 95,
                    backgroundColor: "rgba(183,57,57,0.3)",
                    border:{
					    color: "rgba(183,57,57,0.3)"
				    },
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 101,
                    endValue: 200,
                    backgroundColor: "rgba(183,57,57,0.6)",
                    border:{
                       color:"rgba(183,57,57,0.6)"
		          },
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 208,
                    endValue: 300,
                    backgroundColor: "rgba(183,57,57,1)",
                    border:{
					   color: "rgba(183,57,57,1)"
					},
                    size: 2
                }],
				customLabels: [{
                        value: "$" + parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "books") return value.SalesPrice; })))+ "K "  + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "books") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%)",
                        position: { x: 115, y: 102 },
                        font: { size: "11px", fontFamily: "Segoe UI", fontStyle: "normal" }, color: "gray"
                    }]
            }]
        });
        $("#Clothing").ejCircularGauge({
            backgroundColor: "transparent", width: 200, height: 125, animate: false,
            scales: [{
                showRanges: true,
                startAngle: 170, sweepAngle: 200, radius: 50, showScaleBar: false, size: 1, scaleBorderWidth: 0.5, maximum: 330, majorIntervalValue: 100, minorIntervalValue: 10,
                pointerCap:{
				     radius: 3
				      },  
                pointers: [{
                    value: $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "clothing") return value.SalesPrice; }),
                    showBackNeedle: true,
                    backNeedleLength: 10,
                    length: 35,
                    width: 2,
                    color: "#898989",
					
                }],
                labels: [{
                    distanceFromScale: -14,
                    color: "#8c8c8c"
                }],
                ticks: [{
                    style: "Major",
                    distanceFromScale: 2,
                    height: 16,
                    width: 1, color: "transparent"
                },
                {
                    style: "Minor",
                    height: 8,
                    width: 1,
                    distanceFromScale: 2,
                    color: "transparent"
                }],

                ranges: [{
                    distanceFromScale: -20,
                    startValue: 0,
                    endValue: 95,
                    backgroundColor: "rgba(43,170,191,0.3)",
                    border:{
					     color: "rgba(43,170,191,0.3)"
				    },
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 101,
                    endValue: 200,
                    backgroundColor: "rgba(43,170,191,0.6)",
                    border:{
					      color: "rgba(43,170,191,0.6)"
				    },
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 205,
                    endValue: 330,
                    backgroundColor: "rgba(43,170,191,1)",
                    border:{
					    color: "rgba(43,170,191,1)"
				    },
                    size: 2
                }],
				customLabels: [{
                        value: "$" +parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "clothing") return value.SalesPrice; })))+"K " + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "clothing") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%)",
                        position: { x: 115, y: 102 },
                        font: { size: "11px", fontFamily: "Segoe UI", fontStyle: "normal" }, color: "gray"
                    }]
            }]
        });
        $("#Electronics").ejCircularGauge({
            backgroundColor: "transparent", width: 200, height: 125, animate: false,
            scales: [{
                showRanges: true,
                startAngle: 170, sweepAngle: 200, radius: 50, showScaleBar: false, size: 1, scaleBorderWidth: 0.5, maximum: 300, majorIntervalValue: 100, minorIntervalValue: 10,
                pointerCap:{
				       radius: 3
				},  
                pointers: [{
                    value: $.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "electronics") return value.SalesPrice; }),
                    showBackNeedle: true,
                    backNeedleLength: 10,
                    length: 35,
                    width: 2,
                    color: "#898989",
					 
                }],
                labels: [{
                    distanceFromScale: -14,
                    color: "#8c8c8c"
                }],
                ticks: [{
                    style: "Major",
                    distanceFromScale: 2,
                    height: 16,
                    width: 1, color: "transparent"
                },
                {
                    style: "Minor",
                    height: 8,
                    width: 1,
                    distanceFromScale: 2,
                    color: "transparent"
                }],
                ranges: [{
                    distanceFromScale: -20,
                    startValue: 0,
                    endValue: 95,
                    backgroundColor: "rgba(232,189,70,0.3)",
					border:{
					    color: "rgba(232,189,70,0.3)"
					},                    
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 101,
                    endValue: 200,
                    backgroundColor: "rgba(232,189,70,0.6)",
                    border:{
					       color: "rgba(232,189,70,0.6)"
				    },
                    size: 2
                },
                {
                    distanceFromScale: -20,
                    startValue: 208,
                    endValue: 300,
                    backgroundColor: "rgba(232,189,70,1)",
                    border:{
					     color: "rgba(232,189,70,1)"
				   },
                    size: 2
                }],
				customLabels: [{
                        value: "$" + parseInt(($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "electronics") return value.SalesPrice; })))+"K " + "(" + (($.map(gaugeData, function (value, index) { if (value.Category.toLowerCase() == "electronics") return value.SalesPrice; }) / 150) * 100).toFixed(2) + "%)",
                        position: { x: 115, y: 102 },
                        font: { size: "11px", fontFamily: "Segoe UI", fontStyle: "normal" }, color: "gray"
                    }]
            }]
        });
        $(".col-md-3 >div").css('margin-left', ($(".col-md-3").width() - 200) / 2);
    }
}

//Rendering LineChart, Gauge and RangeChart while selecting Grid items
function gridRowSelected(args) {
      
        $(".gridchart >.col-md-6, .mapwaiting, .gaugecontainer").addClass("addbackground");
        clearMapRelatedData();
        initialLoading = true;
        var currentState = args.data.State;
        var dataMgr = ej.DataManager(salesOverAllData);
        var group;
        var chartgauge, range;
        //To return data to render Chart and Gauge
        group = ej.Query().from(salesOverAllData).where("State", ej.FilterOperators.equal, currentState).sortBy("CategoryName").group("CategoryName");
        chartgauge = dataMgr.executeLocal(group);
        renderChartData(chartgauge);
        renderGaugeData(chartgauge);
        dataMgr = ej.DataManager(gaugeData);
        var gaugeSort = dataMgr.executeLocal(ej.Query().from(gaugeData).sortByDesc("SalesPrice"));
        salesData = { "MaxValue": mapData[mapData.length-1].Sales, "MinValue": mapData[mapData.length-1].Sales, "MaxSalesPrice": gaugeSort[0].SalesPrice };
        //Chart
        renderChart(chartData);
        initialLoading = false;
        //Gauge
        renderGauge(gaugeData);
        gridRowTouched = false;
        $(".gridchart >.col-md-6,.mapwaiting,.gaugecontainer").removeClass("addbackground");
    
    gridRowTouched = true;
    rowSelect = false;
}
ej.WaitingPopup.prototype.show = function () {
    this.hide();
}
//To set id for grid inner element for scroll panel usage
function afterRender(args) {
    
    //$(this.getGridContentTable().parents()[1]).attr('id', 'ScrollContent');
   // $('#ScrollPanel').attr('data-targetwidth', $('.col-md-6').width() - 4);
    var t = setTimeout(function () { 
       // gridRowTouched = false;
        //var grid = $("#Grid").ejGrid("instance");
        //rowSelect = true;
        //grid.selectRows(0);
        $(".gridchart >.col-md-6, .mapwaiting,.gaugecontainer").removeClass("addbackground");
       // $('#ScrollPanel').mScrollPanel();
        initialLoading = false;
        $('#MainContent').css('visibility', 'visible');
    }, 0);
}


function updateElements()
{         
        var dataMgr = ej.DataManager(salesOverAllData);
        var group;
        var grid, chartgauge, range;
        var predicate = ej.Predicate("ShipDate", ej.FilterOperators.greaterThan, window.startDate, false);
        predicate = predicate.and("ShipDate",  ej.FilterOperators.lessThanOrEqual, window.endDate, false);
        //To return data to render Grid
        group = ej.Query().from(salesOverAllData).where(predicate).group("State");
        grid = dataMgr.executeLocal(group);
        if (grid.length > 0) {
            rendermapData(grid);
            //To return data to render Chart and Gauge
            group = ej.Query().from(salesOverAllData).where(predicate).sortBy("CategoryName").group("CategoryName");
            chartgauge = dataMgr.executeLocal(group);
            renderChartData(chartgauge);
            renderGaugeData(chartgauge, true);
            dataMgr = ej.DataManager(mapData);
            var gridSort = dataMgr.executeLocal(ej.Query().from(mapData).sortByDesc("Sales"));
            dataMgr = ej.DataManager(gaugeData);
            var gaugeSort = dataMgr.executeLocal(ej.Query().from(gaugeData).sortByDesc("SalesPrice"));
            salesData = { "MaxValue": gridSort[0].Sales, "MinValue": gridSort[gridSort.length-1].Sales, "MaxSalesPrice": gaugeSort[0].SalesPrice };
            //Grid
			
                if($("#Map").css('display')!='none'){
                    renderMap(mapData);			  
				}
			    renderChart(chartData);
    //Gauge
			    renderGauge(gaugeData);
				renderGrid(mapData);
				
			    $(".gridchart >.col-md-6,.mapwaiting, .gaugecontainer").removeClass("addbackground");
        }
        
}
//Rendering Grid, LineChart and Gauge while selecting the Range Date
function rangeDateSelected(args) {
    clearRangeRelatedData();	
	  if (!initialLoading) {
        $(".gridchart >.col-md-6, .gaugecontainer, .mapwaiting").addClass("addbackground");
        window.startDate = args.selectedRangeSettings.start;
        window.endDate = args.selectedRangeSettings.end;
		
		if(window.endDate=="Invalid Date")
		{
		if(prevDate==null)
		{
			if (document.documentMode == 8){
				var result = rangeData[rangeData.length - 6].Date;
	
				result = result.split("/");
		
		    	prevDate= new Date(result[2],result[1],result[0]).toString();
			}
			else{
				prevDate=rangeData[rangeData.length - 6].Date.toString();
			}
		}
		   window.endDate= prevDate;
		}
		else
		{
		   prevDate=window.endDate;
		}
        if (!window.isUpdated) {
            window.isUpdated = true;
           window.timeoutCallback = setTimeout(updateElements, 500);
        }
        else
        {
            clearTimeout(window.timeoutCallback);
            window.timeoutCallback = setTimeout(updateElements, 500);
            window.isUpdated = true;
        }
        $(".gridchart >.col-md-6,.mapwaiting, .gaugecontainer").removeClass("addbackground");
    }
}

$(document).ready(function () {
    
	salesOverAllData = JSON.parse(JSON.stringify(salesOverAllData2011), json_deserialize_helper);
    renderDatasource();

 });
function renderDatasource() {
    var dataMgr = ej.DataManager(salesOverAllData);
    var group;
	
    var grid, chartgauge, range;
    //To return data to render Grid
    group = ej.Query().from(salesOverAllData).group("State");   
    grid = dataMgr.executeLocal(group);
    
    //To return data to render Chart and Gauge
    var singleDataMgr = ej.DataManager(grid[0].items);

    group = ej.Query().from(grid[0].items).sortBy("CategoryName").group("CategoryName");
    chartgauge = singleDataMgr.executeLocal(group);   
    
    //To return data to render Range Chart
    group = ej.Query().from(grid[0].items).group("ShipDate");
    range = singleDataMgr.executeLocal(group);
    renderRangeData(range);
	renderRangeChart(rangeData);
	renderGaugeData(chartgauge);
	renderChartData(chartgauge);
	rendermapData(grid);
    dataMgr = ej.DataManager(gaugeData);
    var gaugeSort = dataMgr.executeLocal(ej.Query().from(gaugeData).sortByDesc("SalesPrice"));
    salesData = { "MaxValue": mapData[mapData.length-1].Sales, "MinValue": mapData[mapData.length-1].Sales, "MaxSalesPrice": gaugeSort[0].SalesPrice };
    //Map    
	renderGauge(gaugeData);
   
	renderGrid(mapData);
    //Chart
    renderChart(chartData);
    //RangeChart
    
	
	 renderMap(mapData);
    //Gauge
    $("#Grid").css({'display':'none'});
    afterRender(null);
    
}
function rendermapData(data) {
    $.each(data, function (index, value) {
        var state = value.key;
		
        var sales = ej.dataUtil.sum(value.items, "SalesAmount");
        grid = { "State": state, "Sales": sales, "SalesvsTarget": window.targetData[value.key] / 100 };
        mapData.push(grid);
    });
    var dataMgr = ej.DataManager(mapData); 
}
function renderChartData(data) {
    var cData = [];
    var dataMgr;
    $.each(data, function (index, value) {
        var category = value.key;
        dataMgr = ej.DataManager(value.items);
        var sales = ej.dataUtil.sum(value.items, "SalesAmount");
        dataVal = { "SalesPrice": sales, "Category": category };
        cData.push(dataVal);

    });
    chartData.push({ "series": cData });
}
function renderGaugeData(data, rangeChart) {
    $.each(data, function (index, value) {
        var gauge = { "Category": value.key, "SalesPrice": ej.dataUtil.sum(value.items, "SalesAmount") / (rangeChart?100000:10000) };
        gaugeData.push(gauge);
    });
}
function renderRangeData(data) {
    var rangeAmount = 0;
    $.each(data, function (index, value) {
        $.each(value.items, function (index, value) {
            rangeAmount = rangeAmount + value.SalesAmount;
        });
        range = { "Date": value.key, "SalesPrice": rangeAmount };
        rangeAmount = 0;
        rangeData.push(range);
    });
}
function json_deserialize_helper(key, value) {
    if (typeof value === 'string') {
        var regexp;
        regexp = /^\d\d\d\d-\d\d-\d\dT/.exec(value);
        if (regexp) {
            return new Date(value);
        }
    }
    return value;
}

function gridiconChanged(grid){
$(grid).css({opacity:1});
var map = document.getElementById("mapbutton");
$(map).css({opacity:0.2});
$("#Grid").css({'display':'block'});
$("#Map").css({'display':'none'});
$("#mapborder").css({'display':'none'});
}
function mapiconChanged(map){
$(map).css({opacity:1});
var grid = document.getElementById("gridbutton");
$(grid).css({opacity:0.2});
$("#Grid").css({'display':'none'});
$("#mapborder").css({'display':'block'});
$("#Map").css({'display':'block'});
$("#Map").ejMap("refresh");
}
 
function changeRecord(year) {
    //clearOverall();
    if (year.value.toLowerCase() == "year 2011 report") { 
        salesOverAllData = JSON.parse(JSON.stringify(salesOverAllData2011), json_deserialize_helper);
    }
   else if (year.value.toLowerCase() == "year 2010 report") { 
        salesOverAllData = JSON.parse(JSON.stringify(salesOverAllData2010), json_deserialize_helper);
    }
   else if (year.value.toLowerCase() == "year 2009 report") { 
        salesOverAllData = JSON.parse(JSON.stringify(salesOverAllData2009), json_deserialize_helper); 
   }
   else if (year.value.toLowerCase() == "year 2008 report") {
       salesOverAllData = JSON.parse(JSON.stringify(salesOverAllData2008), json_deserialize_helper); 
   }
   else if (year.value.toLowerCase() == "year 2012 report") {
       salesOverAllData = JSON.parse(JSON.stringify(salesOverAllData2012), json_deserialize_helper);
   }
    renderDatasource();
}

$.views.helpers({
			parseDouble: function (amount) {
			var delimiter = ","; 
    var i = parseInt(amount);    
    if(isNaN(i)) { return ''; }    
    var origi = i;  // store original to check sign
    i = Math.abs(i);    
    var minus = '';
    if (origi < 0) { minus = '-'; } // sign based on original
    var n = new String(i);
    var a = [];
    while(n.length > 3)
    {
        var nn = n.substr(n.length-3);
        a.unshift(nn);
        n = n.substr(0,n.length-3);
    }
    if (n.length > 0) { a.unshift(n); }
    n = a.join(delimiter);
    amount = minus + n;
    return amount;         
           
        }
    });