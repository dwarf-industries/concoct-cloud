var sumColl = {};
$(function () {

    //grid is loaded with food information
    var dataManger1 = ej.DataManager({
        url: window["baseurl"]  + "api/healthtracker/load", crossDomain:true
    });
    dataManger1.executeQuery(ej.Query()).done(function (e) {
        $("#Grid").data("ejGrid").dataSource(e.result.FoodInfo);
        sumColl = getSummaryDetails();

        updateChartSeries();

        //render the series for calories chart
        var calChart = $("#ChartCal").data("ejChart");
        calChart.model.series[0].dataSource = e.result.ChartDB.CalData;

        //generate the series for steps moved chart

        var burntChart = $("#ChartBurnt").data("ejChart");
        burntChart.model.series[0].dataSource = e.result.ChartDB.BurntData;

        //generate the series for meals intake chart
        var mealChart = $("#MealDetails").data("ejChart");
        mealChart.model.series[0].dataSource = e.result.ChartDB.MealData.Open;
        calChart.model.series[0].xName = burntChart.model.series[0].xName = mealChart.model.series[0].xName = mealChart.model.series[1].yName = mealChart.model.series[2].yName = "XValue";
        calChart.model.series[0].yName = burntChart.model.series[0].yName = mealChart.model.series[0].yName = mealChart.model.series[1].yName = mealChart.model.series[2].yName = "YValue";
        mealChart.model.series[1].dataSource = e.result.ChartDB.MealData.Close;
        mealChart.model.series[2].dataSource = e.result.ChartDB.MealData.OpenClose;
        calChart.redraw();
        burntChart.redraw();
        mealChart.redraw();
    });

    $("#Grid").ejGrid({
        showSummary: true,
        enableAltRow: false,
        allowKeyboardNavigation: true,
        editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, editMode: ej.Grid.EditMode.DialogTemplate, dialogEditorTemplateID: "#healthAddTemplate" },
        columns: [
            { field: "Time", headerText: "TIME", width: 70, textAlign: ej.TextAlign.Center, validationRules: { required: true } },
            { field: "FoodName", headerText: "FOOD", width: 110, textAlign: ej.TextAlign.Center, validationRules: { required: true } },
            { field: "Fat", headerText: "FAT", textAlign: ej.TextAlign.Center, width: 60, format: "{0:N0}g", validationRules: { required: true } },
            { field: "Carbohydrate", headerText: "CARB", textAlign: ej.TextAlign.Center, width: 60, format: "{0:N0}g", validationRules: { required: true } },
            { field: "Protein", headerText: "PROTEIN", textAlign: ej.TextAlign.Center, width: 60, format: "{0:N0}g", validationRules: { required: true }, priority: 4 },
            { field: "Calorie", headerText: "CALORIES", width: 65, textAlign: ej.TextAlign.Center, format: "{0:N0}cal", validationRules: { required: true }, priority: 5 },
            { field: "FoodId", isPrimaryKey: true, visible: false }
        ],
        gridLines: ej.Grid.GridLines.Horizontal,        
        isResponsive: true,
        enableResponsiveRow: false,
        actionComplete: "actionComplete",
        summaryRows: [
            { title: "Sum", summaryColumns: [{ summaryType: ej.Grid.SummaryType.Sum, displayColumn: "Fat", dataMember: "Fat", format: "{0:N0}g" }, { summaryType: ej.Grid.SummaryType.Sum, displayColumn: "Carbohydrate", dataMember: "Carbohydrate", format: "{0:N0}g" }, { summaryType: ej.Grid.SummaryType.Sum, displayColumn: "Protein", dataMember: "Protein", format: "{0:N0}g" }, { summaryType: ej.Grid.SummaryType.Sum, displayColumn: "Calorie", dataMember: "Calorie", format: "{0:N0}cal" }] }
        ],
    });

    //add new food dialog box is opened when "AddMeal" button is clicked
    $(".addbutton").bind("click", function () {
        $("#Grid").ejGrid("addRecord");
    });

    $("#Chart").ejChart({
        series: [{
            marker: { dataLabel: { visible: true, font: { color: '#707070', size: '15px', fontWeight: 'lighter' } } },
            name: 'Newyork', type: 'doughnut', labelPosition: 'outside', doughnutSize: 0.9, opacity: 0.8,
            border: { width: 1 }
        }],
        isResponsive: true,
        margin: { left: 10, top: 0, right: 0, bottom: 0 },
        size: { height: "270" },
        legend: { visible: false }
    });

    // render the gauge
    $("#GaugeRDI").ejCircularGauge({
        frame: { frameType: "halfcircle" },
        width: 170,
        height: 155,
        distanceFromCorner: -5,
        gaugePosition: "bottomcenter",
        isResponsive: false,
        scales: [
            {
                startAngle: 182,
                sweepAngle: 176,
                showRanges: true,
                showLabels: false,
                radius: 120,
                minimum: 0,
                maximum: 2200,
                majorIntervalValue: 200,
                pointerCap: { radius: 15, backgroundColor: "#3AB54B", borderColor: "#3AB54B", borderWidth: 15 },
                pointers: [{ border: { color: "#3AB54B" }, needleStyle: "rectangle", width: 1, value: 450, length: 70 }],
                ticks: [
                    {
                        color: "#FFFFFF",
                        height: 16,
                        width: 3
                    }, {
                        color: "#FFFFFF",
                        height: 7,
                        width: 1
                    }
                ],
                ranges: [
                    {
                        size: 10,
                        startValue: 0,
                        endValue: 449,
                        backgroundColor: "#3AB54B",
                        border: { color: "#3AB54B" }
                    }, {
                        size: 10,
                        startValue: 449,
                        endValue: 2200,
                        backgroundColor: "#B0D2C8",
                        border: { color: "#B0D2C8" }
                    }
                ]
            }
        ]
    });

    // render the gauge
    $("#GaugeBurnt").ejCircularGauge({
        frame: { frameType: "halfcircle" },
        width: 170,
        height: 155,
        distanceFromCorner: -5,
        gaugePosition: "bottomcenter",
        isResponsive: false,
        scales: [
            {
                startAngle: 182,
                sweepAngle: 176,
                showRanges: true,
                showLabels: false,
                radius: 120,
                minimum: 0,
                maximum: 1000,
                majorIntervalValue: 200,
                pointerCap: { radius: 15, backgroundColor: "#b24848", borderColor: "#b24848", borderWidth: 15 },
                pointers: [{ border: { color: "#b24848" }, needleStyle: "rectangle", width: 1, value: 650, length: 70 }],
                ticks: [
                    {
                        color: "#FFFFFF",
                        height: 16,
                        width: 3
                    }, {
                        color: "#FFFFFF",
                        height: 7,
                        width: 1
                    }
                ],
                ranges: [
                    {
                        size: 10,
                        startValue: 0,
                        endValue: 649,
                        backgroundColor: "#b24848",
                        border: { color: "#c98c8b" }
                    }, {
                        size: 10,
                        startValue: 649,
                        endValue: 1000,
                        backgroundColor: "#C9A5A6",
                        border: { color: "#C9A5A6" }
                    }
                ]
            }
        ]
    });

    //number of steps pending chart is rendered
    $("#ChartStep").ejChart({
        series: [
            {
                points: [
                    {
                        x: 'Carbohydrate',
                        y: 10,
                        visible: true,
                        fill: "#D3C1D4"
                    }, {
                        x: 'Fat',
                        y: 90,
                        visible: true,
                        fill: "#B26CAB"
                    }
                ],
                name: 'Newyork',
                type: 'doughnut',
                doughnutSize: 0.9,
                doughnutCoefficient: 0.7,
                enableAnimation: false,
                opacity: 0.8,
                border: {
                    color: "#D3C1D4"
                }
            }
        ],
        margin: {
            top: 0,
            bottom: 0,
            left: 10,
            right: 10
        },
        size: {
            height: "170",
            width: "200"
        },
        legend: {
            visible: false,
            font: {
                color: 'Black'
            }
        },
        annotations: [
            {
                visible: true,
                content: "stepAnnotation",
                region: "series"
            }
        ]
    });

    //number of floors pending chart is rendered
    $("#ChartFloor").ejChart({
        series: [
            {
                points: [
                    {
                        x: 'Carbohydrate',
                        y: 6,
                        visible: true,
                        fill: "#7D70B3"
                    }, {
                        x: 'Fat',
                        y: 4,
                        visible: true,
                        fill: "#BFBED9"
                    }
                ],
                name: 'Newyork',
                type: 'doughnut',
                labelPosition: 'inside',
                doughnutSize: 0.9,
                doughnutCoefficient: 0.7,
                enableAnimation: false,
                opacity: 0.8,
                border: {
                    width: 1,
                    color: "#BFBED9"
                }
            }
        ],
        margin: {
            top: 0,
            bottom: 0,
            left: 10,
            right: 10
        },
        size: {
            height: "170",
            width: "200"
        },
        legend: {
            visible: false,
            font: {
                color: 'Black',
                size: '12px'
            },
            position: 'bottom'
        },
        annotations: [
            {
                visible: true,
                content: "floorAnnotation",
                region: "series"
            }
        ]
    });

    //monthly report chart for number of steps moved
    $("#ChartCal").ejChart(
    {
        chartArea:
        {
            border: { width: 1 }
        },
        primaryXAxis:
        {
            edgeLabelPlacement: "hide",
            title: { text: "Days", font: { fontStyle: 'Bold', size: '14px', fontWeight: 'Bold' } },
            font: { fontStyle: 'bold', size: '8px' },
            range: { min: 0, max: 31, interval: 3 },
            majorGridLines: { visible: false },
            columnIndex: 0,
            valueType:"double"
        },
        primaryYAxis:
        {
            rowIndex: "0",
            rangePadding: 'none',
            range: { min: 0, max: 1200, interval: 100 },
            font: { fontStyle: 'bold', size: '8px' },
            title: { text: "Calorie", font: { fontStyle: 'Bold', size: '14px', fontWeight: 'Bold' } },
            valueType:"double"
        },
        series: [
            {
                name: 'Calories Burnt',
                type: 'spline',
                enableAnimation: true,
                fill: "#24B7E5",
                tooltip: { template: 'CalTooltip' },
                xName: "XValue",
                yName: "YValue"
            }
        ],
        commonSeriesOptions: {
            type: 'line',
            tooltip: {
                visible: true
            },
            enableAnimation: true,
            marker:
            {
                size:
                {
                    height: 10,
                    width: 10
                },
                visible: true,
            },
            border: { width: 2 }
        },
        isResponsive: true,
        initSeriesRender: false,
        background: 'transparent',
        title: { text: 'CALORIES BURNT' },
        size: { height: "500" }
    });

    //monthly report chart for calories burnt
    $("#ChartBurnt").ejChart(
    {
        chartArea:
        {
            border: { width: 1 }
        },
        primaryXAxis:
        {
            edgeLabelPlacement: "hide",
            rangePadding: 'none',
            title: { text: "Days", font: { fontStyle: 'Bold', size: '14px', fontWeight: 'Bold' } },
            range: { min: 0, max: 31, interval: 3 },
            majorGridLines: { visible: false },
            font: { fontStyle: 'bold', size: '8px' },
            columnIndex: 0,
            valueType:"double"
        },
        primaryYAxis:
        {
            rowIndex: "0",
            rangePadding: 'none',
            range: { min: 0, max: 1200, interval: 100 },
            title: { text: "Steps", font: { fontStyle: 'Bold', size: '14px', fontWeight: 'Bold' } },
            font: { fontStyle: 'bold', size: '8px' },
            valueType:"double"
        },
        commonSeriesOptions: {
            tooltip: { visible: true }
        },

        series: [
            {
                name: 'Steps Moved',
                enableAnimation: true,
                fill: "#8CC640",
                tooltip: { template: 'BurntTooltip' },
                xName: "XValue",
                yName: "YValue"
            }
        ],
        isResponsive: true,
        initSeriesRender: false,
        background: 'transparent',
        title: { text: "TOTAL STEPS" },
        size: { height: "500" }
    });

    //monthly report chart for meal intake
    $("#MealDetails").ejChart(
    {
        chartArea:
        {
            Border: { width: 1 }
        },
        primaryXAxis:
        {
            edgeLabelPlacement: "hide",
            rangePadding: 'none',
            title: { text: "Days", font: { fontStyle: 'Bold', size: '14px', fontWeight: 'Bold' } },
            range: { min: 0, max: 31, interval: 3 },
            majorGridLines: { visible: false },
            columnIndex: 0,
            valueType:"double"
        },
        primaryYAxis:
        {
            rowIndex: "0",
            rangePadding: 'none',
            range: { min: 0, max: 1200, interval: 100 },
            title: { text: "Cal", font: { fontStyle: 'Bold', size: '14px', fontWeight: 'Bold' } },
            valueType:"double"
        },
        commonSeriesOptions: {
            tooltip: { visible: true }
        },
        series: [
            {
                name: 'Carb',
                type: 'column',
                enableAnimation: true,
                fill: "#8CAA55",
                tooltip: { template: 'HydrateTooltip' },
                xName: "XValue",
                yName: "YValue"

            },
            {
                name: 'Protein',
                type: 'column',
                enableAnimation: true,
                fill: "#B34949",
                tooltip: { template: 'ProteinTooltip' },
                xName: "XValue",
                yName: "YValue"
            },
            {
                name: 'Fat',
                type: 'column',
                enableAnimation: true,
                fill: "#58A7C6",
                tooltip: { template: 'FatTooltip' },
                xName: "XValue",
                yName: "YValue"
            }
        ],
        isResponsive: true,
        initSeriesRender: false,
        background: 'transparent',
        title: { text: 'MEAL INTAKE' },
        size: { height: "500" }
    });

    //load the monthly reported chart when we scroll down
    $('.loadondemand').ejSmartScroller({
        reach: "renderControl"
    });
});

//update chart with summary values of grid
function updateChartSeries() {
    $("#Chart").ejChart("option", {
        "model": {
            series: [{
                "points": [{ x: 'Carb', y: sumColl[1], fill: "#B44A4A", text: sumColl[1] + "g" + " Carb" },
                    { x: 'Protein', y: sumColl[2], fill: "#53B2C1", text: sumColl[2] + "g" + " Protein" },
                    { x: 'Fat', y: sumColl[0], fill: "#F9AF3C", text: sumColl[0] + "g" + " Fat" }
                ]
            }]
        }
    });
}

//update calories burnt gauge
function updateGauge(caloriesValue) {
    var gaugeObj = $("#GaugeRDI").data("ejCircularGauge");
    gaugeObj.setPointerValue(0, 0, caloriesValue);
    gaugeObj.setRangeStartValue(0, 0, 0);
    gaugeObj.setRangeEndValue(0, 0, caloriesValue);
    gaugeObj.setRangeStartValue(0, 1, caloriesValue);
    gaugeObj.setRangeEndValue(0, 1, 2200);
    $(".rdilabel").text("Calories Intake - " + caloriesValue + "/2200");
    $(".rdipenlabel").text(2200 - caloriesValue + " calories pending");
}

//get the summary details of the grid
function getSummaryDetails() {
    var griddata = $("#Grid").data("ejGrid");
    var sumValue = griddata.model.summaryRows[0].summaryColumns;
    var sumColl = [];
    $.each(sumValue, function (index, item) {
        sumColl.push(Math.round(griddata.getSummaryValues(item)));
    });
    return sumColl;
}

function actionComplete(args) {
    if (args.requestType == "beginedit" || args.requestType == "add") {
        $("#Fat,#Carbohydrate,#Protein,#Calorie").ejNumericTextbox({ width: "120px", minValue: 1, maximum: 1000 });
        $("#EditDialog_Grid_Save").val("Done");
        $("#Fat, #Carbohydrate, #Protein, #Calorie,#Time, #FoodName").css("text-align", "left");
        $("#EditDialog_Grid_Cancel").remove();
        $('#Time').ejTimePicker();
        if (args.requestType == "beginedit")
            $("#MealSummary span.e-title").text("Edit");
        else {
            $("#Time").data("ejTimePicker").setCurrentTime();
            $("#MealSummary span.e-title").text("Add Food");
        }
    }

    if (args.requestType != "refresh" && args.requestType == "save") {
        args.data.FoodId = getRandomNum(6, 50);
        sumColl = getSummaryDetails();
        //refresh the chart if the new food item is added.
        updateChartSeries();
        //calories sum to refresh Gauge
        updateGauge(sumColl[3]);
    }
}
//generate random numbers
function getRandomNum(ubound, lbound) {
    return (Math.floor(Math.random() * (ubound - lbound)) + lbound);
}

//load the monthly reported charts when we scroll down
function renderControl(args) {
    if (args.element.hasClass("titlecss"))
        renderBurntCal();
    else
        renderMealIntake();
}

//render the series for the steps moved and calories burnt chart
function renderBurntCal() {
        var chartCalObj = $("#ChartCal").data("ejChart");
        var chartBurntObj = $("#ChartBurnt").data("ejChart");
        chartCalObj.seriesRender();
        chartBurntObj.seriesRender();
}

//render the series for the meal intake chart
function renderMealIntake() {
    var chartMealObj = $("#MealDetails").data("ejChart");
    chartMealObj.seriesRender();
}

(function ($, ej, undefined) {
    // Example plugin creation code
    // ejScr is the plugin name 
    // "ej.Scr" is "namespace.className" will hold functions and properties
    ej.widget("ejSmartScroller", "ej.SmartScroller", {
        // widget element will be automatically set in this
        element: null,
        // user defined model will be automatically set in this
        model: null,
        //Root Css Class
        _rootCSS: "",
        // default model
        defaults: {
            /// <summary>This Contains default property of button </summary>
            triggerOnce: true,
            reach: null
        },
        //Data Types
        dataTypes: {
            triggerOnce: "boolean"
        },

        // constructor function
        _init: function () {
            this._initialize();
            this._wireEvents();
        },

        // all events bound using this._on will be unbind automatically
        _destroy: function () {
            this._off($(window), "scroll", this._scrollHandler);
        },
        _setModel: function (options) {

        },

        _initialize: function () {
            this._oldScroll = this._newScroll = 0;
            this._triggered = false;
            if (this._oldScroll >= this._getOffset())
                this._triggerFn();
        },
        _scrollHandler: function (e) {
            if (!this._triggered || !this.model.triggerOnce) {
                this._triggerOnReach();
                this._oldScroll = this._newScroll;
            }
        },
        _getOffset: function () {
            return $(this.element).offset().top - (document.documentElement.clientHeight - $(this.element).outerHeight());
        },
        _triggerOnReach: function () {
            this._newScroll = $(document).scrollTop();
            var offset = this._getOffset();
            if (this._oldScroll < offset && offset <= this._newScroll) {
                this._triggerFn();
            } else if (this._newScroll < offset && offset <= this._oldScroll) {
                this._triggerFn();
            }
        },
        _triggerFn: function () {
            this._triggered = true;
            this._trigger("reach", { element: this.element, offsetTop: this._newScroll });
        },
        /*-----------------------Event Handlers -----------------------------------------*/
        _wireEvents: function () {
            this._on($(window), "scroll", this._scrollHandler);
        },
    });

})(jQuery, Syncfusion);
