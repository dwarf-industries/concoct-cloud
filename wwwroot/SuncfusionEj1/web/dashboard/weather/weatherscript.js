$(function () {
    var currentDate = new Date();
    var skyCondition = ["Snow", "Rain Storm", "Thunder", "Rainy", "Cloudy", "Partly Cloudy", "Sunny", "Partly Sunny"];
    var currentSkyCondition = Math.floor((Math.random() * 7));
    var WeatherData = [];
    WeatherData.push({
        "Date": new Date(currentDate.setHours(currentDate.getHours())),
        "SkyCondition": skyCondition[currentSkyCondition],
        "Humidity": Math.floor(Math.random() * 40 + 35),
        "Wind": Math.floor(Math.random() * 10 + 1),
        "Pressure": Math.floor(Math.random() * 30 + 70),
        "Temperature": currentSkyCondition * 5 + Math.floor(Math.random() * 5),
        "FeelsLike": currentSkyCondition * 5 + Math.floor(Math.random() * 15)
    });

    $("#CurrentDay").text(ej.format(currentDate, "dddd"));
    $("#Circle_Temperature").html(WeatherData[0].Temperature + "&deg;c");
    $("#Circle_SkyCondition").html(WeatherData[0].SkyCondition);
    $("#Circle_Time").html(ej.format(WeatherData[0].Date, "hh:mm tt"));
    $("#Circle_Day").html(ej.format(WeatherData[0].Date, "dddd"));
    $("#Circle_Humidity").html(WeatherData[0].Humidity + "%");
    $("#Circle_DewPoint").html(Math.floor((Math.random() * 10 + 10)) + "&deg;c");

    for (var i = 1; i < 24; i++) {
        var skyRandom = Math.floor((Math.random() * 7));
        WeatherData.push({
            "Date": new Date(currentDate.setHours(currentDate.getHours() + 1)),
            "SkyCondition": skyCondition[skyRandom],
            "Humidity": Math.floor(Math.random() * 40 + 35),
            "Wind": Math.floor(Math.random() * 10 + 1),
            "Pressure": Math.floor(Math.random() * 30 + 70),
            "Temperature": skyRandom * 5 + Math.floor(Math.random() * 5),
            "FeelsLike": skyRandom * 5 + Math.floor(Math.random() * 15)
        });
    }

    window.WeatherDataObject = WeatherData;

    var data = ej.DataManager(window.WeatherDataObject).executeLocal(ej.Query());

    $("#HourGrid").ejGrid({
        dataSource: data,
        allowScrolling: true,
        enableAltRow: false,
        scrollSettings: { height: 470, width: 0 },
        columns: [
                { field: "Date", headerText: "Time", textAlign: ej.TextAlign.Center, width: 125, format: "{0:hh:mm tt}", customAttributes: { "class": "e-rowcell date" } },
                { field: "SkyCondition", headerText: "Sky Condition", textAlign: ej.TextAlign.Center, width: 120, format: "<div class=\"{SkyCondition}\" width=\"48px\" heigth=\"48px\" />", customAttributes: { "class": "e-rowcell skycondition" } },
                { field: "Temperature", headerText: "Temperature", textAlign: ej.TextAlign.Center, width: 100, format: "<label>{Temperature}&deg;c</label>", customAttributes: { "class": "e-rowcell temperature" } },
                { field: "SkyCondition", headerText: "Weather Description", textAlign: ej.TextAlign.Center, width: 150, customAttributes: { "class": "e-rowcell skycondition" } }
                //{ field: "Wind", headerText: "Wind", textAlign: ej.textAlign.Center, width: 100, format: "<label>{Wind} mph</label>", customAttributes: { "class": "e-rowcell wind" } }
                //{ field: "Pressure", headerText: "Pressure", textAlign: ej.textAlign.Center, width: 100, format: "<label>{Pressure} kPa</label>", customAttributes: { "class": "rowcell pressure" } }
        ]
    });
    window.onresize = function () {
        $("#HourGrid .e-gridcontent").ejScroller("refresh");
    };
    $("#HourChart").ejChart({
        canResize: false,
        rowDefinitions:
                    [{
                        rowHeight: 24,
                        lineColor: "#A8A8A8",
                        lineWidth: 0.5,
                        unit: 'percentage'

                    },
                    {
                        rowHeight: 24,
                        lineColor: "#A8A8A8",
                        lineWidth: 0.5,
                        unit: 'percentage'
                    },
                    {
                        rowHeight: 24,
                        lineColor: "#A8A8A8",
                        lineWidth: 0.5,
                        unit: 'percentage'
                    },
                    {
                        rowHeight: 24,
                        lineColor: "#A8A8A8",
                        lineWidth: 0.5,
                        unit: 'percentage'
                    }
                    ],
        primaryXAxis: {
            range: { min: window.WeatherDataObject[0].Date, max: window.WeatherDataObject[window.WeatherDataObject.length - 1].Date, interval: 6 },
            intervalType: 'Hours',
            labelFormat: 'hh:mm tt',
            valueType: 'datetime',
            axisLine: { visible: false },
            majorGridLines: { visible: false }
        },
        primaryYAxis: {
            labelFormat: '{value}',
            majorTickLines: { visible: false },
            rangePadding: 'none',
            axisLine: { visible: false },
            majorGridLines: { visible: false },
            hidePartialLabels: true,
            font: { color: 'transparent' },
            range: { min: -10, max: 45, interval: 15 }
        },
        axes: [
            {
                majorGridLines: { visible: false },
                majorTickLines: { visible: false },
                orientation: 'Vertical',
                rowIndex: "1",
                opposedPosition: false,
                axisLine: { visible: false },
                name: 'yAxis1',
                hidePartialLabels: true,
                font: { color: 'transparent' },
                range: { min: -10, max: 50, interval: 15 },
            },
                   {
                       majorGridLines: { visible: false },
                       majorTickLines: { visible: false },
                       orientation: 'Vertical',
                       rowIndex: "2",
                       opposedPosition: false,
                       axisLine: { visible: false },
                       name: 'yAxis2',
                       hidePartialLabels: true,
                       font: { color: 'transparent' },
                       range: { min: 30, max: 80, interval: 15 },
                   },
                   {
                       majorGridLines: { visible: false },
                       majorTickLines: { visible: false },
                       orientation: 'Vertical',
                       rowIndex: "3",
                       opposedPosition: false,
                       axisLine: { visible: false },
                       name: 'yAxis3',
                       hidePartialLabels: true,
                       font: { color: 'transparent' },
                       range: { min: -5, max: 15, interval: 5 }
                   }
        ],
        commonSeriesOptions: {
            type: 'line', enableAnimation: true,
            marker: {
                shape: 'circle',
                size: { height: 6, width: 6 },
                visible: true,
                style: { borderColor: 'white', borderWidth: 3 }
            },
            style: { borderWidth: 3 }
        },

        series: [{ name: 'Temperature', style: { interior: '#A0C037' }, tooltipFormat: "#series.name#: #point.y# c" },
                 { name: 'FeelsLike', yAxisName: 'yAxis1', style: { interior: '#369E45' }, tooltipFormat: "#series.name#: #point.y# c" },
                 { name: 'Humidity', yAxisName: 'yAxis2', style: { interior: '#9F5123' }, tooltipFormat: "#series.name#: #point.y# %" },
                 { name: 'Wind', yAxisName: 'yAxis3', style: { interior: '#2DA2D8' }, tooltipFormat: "#series.name#: #point.y# mph" }],
        crosshair: {
            visible: true,
			type: 'trackball',
            line: { width: 2, color: '#F4B16D' },
            marker: { shape: 'circle', size: { height: 9, width: 9 }, visible: true }
        },

        load: 'hourChartLoad',
        background: 'white',
        size: { height: "500"  },
        chartArea: { border: { color: "transparent" } },
        margin: { left: 20, top: 20 },
        legend: { visible: true, shape: 'circle', position: 'Bottom', itemStyle: { width: 10, height: 10 } }
    });
  
    var Average = ["Average Precipitation", "Average Sunlight", "Average Minimum Temperature", "Average Maximum Temperature", "Average Wind"];
    var AverageData = [];
    var months = ["Jan", "Feb", "Mar", "Apl", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var d = {};
    d["Average"] = "Precipitation";
    for (var i = 0; i < 12; i++) {
        var date = new Date();
        var monthValue = date.getMonth() + i;
        if (monthValue < 12)
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 100 + 20));
        else {
            monthValue = monthValue - 12;
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 100 + 20));
        }
    }
    AverageData.push(d);

    var d = {};
    d["Average"] = "Sunlight";
    for (var i = 0; i < 12; i++) {
        var date = new Date();
        var monthValue = date.getMonth() + i;
        if (monthValue < 12)
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 12 + 2));
        else {
            monthValue = monthValue - 12;
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 12 + 2));
        }
    }
    AverageData.push(d);

    var d = {};
    d["Average"] = "Minimum Temperature";
    for (var i = 0; i < 12; i++) {
        var date = new Date();
        var monthValue = date.getMonth() + i;
        if (monthValue < 12)
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 15 - 5));
        else {
            monthValue = monthValue - 12;
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 15 - 5));
        }
    }
    AverageData.push(d);

    var d = {};
    d["Average"] = "Maximum Temperature";
    for (var i = 0; i < 12; i++) {
        var date = new Date();
        var monthValue = date.getMonth() + i;
        if (monthValue < 12)
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 20 + 25));
        else {
            monthValue = monthValue - 12;
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 20 + 25));
        }
    }
    AverageData.push(d);

    var d = {};
    d["Average"] = "Wind";
    for (var i = 0; i < 12; i++) {
        var date = new Date();
        var monthValue = date.getMonth() + i;
        if (monthValue < 12)
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 15));
        else {
            monthValue = monthValue - 12;
            d[months[new Date(date.setMonth(monthValue)).getMonth()]] = Math.floor((Math.random() * 15));
        }
    }
    AverageData.push(d);

    window.AverageData = AverageData;

    $("#AverageChart").ejChart({
        canResize: true,
        primaryXAxis: {
            opposedPosition: true,
            axisLine: { visible: false },
            majorGridLines: { visible: false },
            majorTickLines: { visible: false },
			valueType:"category"
        },

        primaryYAxis: {
            range: { min: 0, max: 150, interval: 25 },
            rangePadding: 'none',
            labelFormat: '{value} mm',
            axisLine: { visible: false },
            majorGridLines: { width: 2 },
			valueType:"double"
        },
        axes: [{
            majorGridLines: { visible: false },
            orientation: 'Vertical',
            rowIndex: "0",
            opposedPosition: true,
            axisLine: { visible: false },
            range: { min: 0, max: 15, interval: 2.5 },
            rangePadding: 'none',
            name: 'yAxis',
            labelFormat: '{value} hrs',
			valueType:"double"
        }],
		commonSeriesOptions:{
           tooltip:{
                 visible: true
               }
        },

        series: [{
            name: 'Average Precipitation',
            type: 'column',
            enableAnimation: true,
            fill: '#84c865',
            tooltip: {template:  'precipitationTooltip'}
        },
        {
            name: 'Average Sunlight',
            type: 'column',
            enableAnimation: true,
            yAxisName: 'yAxis',
            fill: '#E94649',
            tooltip: {template: 'sunlightTooltip'}
        }],
        load: 'averageChartLoad',
        background: 'white',
        chartArea: { border: { color: "transparent" } },
        margin: { left: 30 },
        size: { height: "500" },
        legend: { visible: true, shape: 'circle', position: 'Bottom', itemStyle: { width: 10, height: 10 } }
    });

    var averageData = ej.DataManager(window.AverageData).executeLocal(ej.Query());
    var columnNames = [];
    $.each(averageData[0], function (index, value) {
        if (index == "Average") {
            var column = { field: index, textAlign: ej.TextAlign.Center }
            columnNames.push(column);
        }
        else {
            var column = { field: index, textAlign: ej.TextAlign.Center, width: 50, customAttributes: { "class": "e-rowcell average" } }
            columnNames.push(column);
        }
    });

    $("#AverageGrid").ejGrid({
        dataSource: averageData,
        enableAltRow: false,
        allowGrouping: true,
        heigth: "500px",
        groupSettings: { groupedColumns: ["Average"] },
        columns: columnNames,
        queryCellInfo: "queryCellInfo"
    });

    $("#AverageGrid").find(".groupdroparea").hide();
    $("#HourChart").hide();
    $("#AverageGrid").hide();

    $('.buttons > a').bind('click', function () { weatherSelection(this) });
});

//It is used to perform navigation between Chart and Grid
function weatherSelection(target) {
    switch (target.id) {
        case "ChartHour":
            if ($(target).hasClass("inactive")) {
                $(target).removeClass("inactive").addClass("active");
                $("#GridHour").removeClass("active").addClass("inactive");
                $("#HourGrid").hide();
                $("#HourChart").show();
				var chartobj=$("#HourChart").data("ejChart");
				chartobj.chartResize();
            }
            break;
        case "GridHour":
            if ($(target).hasClass("inactive")) {
                $(target).removeClass("inactive").addClass("active");
                $("#ChartHour").removeClass("active").addClass("inactive");
                $("#HourChart").hide();
                $("#HourGrid").show();
            }
            break;
        case "ChartAverage":
            if ($(target).hasClass("inactive")) {
                $(target).removeClass("inactive").addClass("active");
                $("#GridAverage").removeClass("active").addClass("inactive");
                $("#AverageGrid").hide();
                $("#AverageChart").show();
				var chartobj=$("#AverageChart").data("ejChart");
				chartobj.chartResize();
            }
            break;
        case "GridAverage":
            if ($(target).hasClass("inactive")) {
                $(target).removeClass("inactive").addClass("active");
                $("#ChartAverage").removeClass("active").addClass("inactive");
                $("#AverageChart").hide();
                $("#AverageGrid").show();
            }
            break;
    }
}

//It is used to apply formatting for the cells in Grid dynamically at run time
function queryCellInfo(args) {
    if ($(args.cell).hasClass("groupcaption")) {
        if (args.cell.innerHTML.indexOf("Precipitation") != -1)
            args.cell.innerHTML = "Average Precipitation (mm)";
        if (args.cell.innerHTML.indexOf("Sunlight") != -1)
            args.cell.innerHTML = "Average Sunlight (Hours)";
        if (args.cell.innerHTML.indexOf("Minimum Temperature") != -1)
            args.cell.innerHTML = "Average Minimum Temperature (&deg;C)";
        if (args.cell.innerHTML.indexOf("Maximum Temperature") != -1)
            args.cell.innerHTML = "Average Maximum Temperature (&deg;C)";
        if (args.cell.innerHTML.indexOf("Wind") != -1)
            args.cell.innerHTML = "Average Wind (mph)";
    }
}

//It is used to set data source for the chart to series in model
function hourChartLoad(sender) {
    var data = getHourData();
    sender.model.series[0].dataSource = data.Temperature;
	sender.model.series[0].xName ="XValue";
	 sender.model.series[0].yName = "YValue";
    sender.model.series[1].dataSource = data.FeelsLike;
	sender.model.series[1].xName = "XValue";
	sender.model.series[1].yName =  "YValue";
    sender.model.series[2].dataSource = data.Humidity;
	sender.model.series[2].xName = "XValue";
	sender.model.series[2].yName = "YValue";
    sender.model.series[3].dataSource = data.Wind;
	sender.model.series[3].xName = "XValue";
	sender.model.series[3].yName = "YValue";
}

//It is used to retrieve data for binding chart to display hour details for the day
function getHourData() {
    var series1 = [];
    var series2 = [];
    var series3 = [];
    var series4 = [];
    $.each(window.WeatherDataObject, function (index, value) {
        var point1 = { XValue: value.Date, YValue: value.Temperature };
        var point2 = { XValue: value.Date, YValue: value.FeelsLike };
        var point3 = { XValue: value.Date, YValue: value.Humidity };
        var point4 = { XValue: value.Date, YValue: value.Wind };
        series1.push(point1);
        series2.push(point2);
        series3.push(point3);
        series4.push(point4);
    });
    var data = { Temperature: series1, FeelsLike: series2, Humidity: series3, Wind: series4 };
    return data;
}

//It is used to set data source for the chart to series in model
function averageChartLoad(sender) {
    var data = getAverageData();
    sender.model.series[0].dataSource = data.Precipitation;
	sender.model.series[0].xName= "XValue";
	sender.model.series[0].yName= "YValue";
    sender.model.series[1].dataSource = data.Sunlight;
	sender.model.series[1].xName= "XValue";
	sender.model.series[1].yName= "YValue";
}

//It is used to retrieve data for binding chart to display average record details for the year
function getAverageData() {
    var series1 = [];
    var series2 = [];
    $.each(window.AverageData[0], function (index, value) {
        if (index != "Average") {
            var point1 = { XValue: index, YValue: value };
            series1.push(point1);
        }
    });
    $.each(window.AverageData[1], function (index, value) {
        if (index != "Average") {
            var point1 = { XValue: index, YValue: value };
            series2.push(point1);
        }
    });
    var data = { Precipitation: series1, Sunlight: series2 };
    return data;
}