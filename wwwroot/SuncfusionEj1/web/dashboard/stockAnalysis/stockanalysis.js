$(function () {
    $("#GOOG").click(function (e) {
	if (!$('#nok-wait').data("ejWaitingPopup").model.autoDisplay && !$('#aapl-wait').data("ejWaitingPopup").model.autoDisplay && !$('#msft-wait').data("ejWaitingPopup").model.autoDisplay) {
        $("#goog-wait").ejWaitingPopup();
        $('#goog-wait').data("ejWaitingPopup").show();
		$("#hiloOpenClose").data("ejWaitingPopup").show();
        getWebAPIData(this.id);
        $("#hiloOpenClose").ejChart("option", { title:{text: this.id }});
        var data3 = getData();
        $("#colChart").ejChart("option", { "datasource": setdatasource(data3), "primaryXAxis": setRangeAxis(5) });
       }
    });
    $("#NOK").click(function (e) {
	if (!$('#goog-wait').data("ejWaitingPopup").model.autoDisplay && !$('#aapl-wait').data("ejWaitingPopup").model.autoDisplay && !$('#msft-wait').data("ejWaitingPopup").model.autoDisplay) {
        $("#nok-wait").ejWaitingPopup();
        $('#nok-wait').data("ejWaitingPopup").show();
		$("#hiloOpenClose").data("ejWaitingPopup").show();
        getWebAPIData(this.id);
        $("#hiloOpenClose").ejChart("option", { title:{text: this.id }});
        var data3 = getData();
        $("#colChart").ejChart("option", { "datasource": setdatasource(data3), "primaryXAxis": setRangeAxis(5) });
		}
    });
    $("#AAPL").click(function (e) {
	 if (!$('#goog-wait').data("ejWaitingPopup").model.autoDisplay && !$('#nok-wait').data("ejWaitingPopup").model.autoDisplay && !$('#msft-wait').data("ejWaitingPopup").model.autoDisplay) {
        $("#aapl-wait").ejWaitingPopup();
        $('#aapl-wait').data("ejWaitingPopup").show();
		$("#hiloOpenClose").data("ejWaitingPopup").show();
        getWebAPIData(this.id);
        $("#hiloOpenClose").ejChart("option", { title:{text: this.id }});
        var data3 = getData();
        $("#colChart").ejChart("option", { "datasource": setdatasource(data3), "primaryXAxis": setRangeAxis(5) });
		}
    });
    $("#MSFT").click(function (e) {
	 if (!$('#goog-wait').data("ejWaitingPopup").model.autoDisplay && !$('#aapl-wait').data("ejWaitingPopup").model.autoDisplay && !$('#nok-wait').data("ejWaitingPopup").model.autoDisplay) {
        $("#msft-wait").ejWaitingPopup();
        $('#msft-wait').data("ejWaitingPopup").show();
		$("#hiloOpenClose").data("ejWaitingPopup").show();
        getWebAPIData(this.id);
        $("#hiloOpenClose").ejChart("option", { title:{text: this.id }});
        var data3 = getData();
        $("#colChart").ejChart("option", { "datasource": setdatasource(data3), "primaryXAxis": setRangeAxis(5) });
		}
    });
});

// load for Hilo open close Chart
function chartload(sender) {
     $("#hiloOpenClose").ejWaitingPopup();
	 $("#hiloOpenClose").data("ejWaitingPopup").show();
	 var width=$(".stockstatus").width();
	 if(width>400){
    $("#" + sender.model.title.text.toLowerCase() + "-wait").ejWaitingPopup();
	$("#" + sender.model.title.text.toLowerCase() + "-wait").data("ejWaitingPopup").show();
	}
	getWebAPIData(sender.model.title.text, sender);
	$("#" + "goog" + "-wait").ejWaitingPopup();
    getWebAPIData("GOOG", sender);
	$("#" + "nok" + "-wait").ejWaitingPopup();
    getWebAPIData("NOK", sender);
    $("#" + "aapl"+ "-wait").ejWaitingPopup();
    getWebAPIData("AAPL", sender);
    $("#" + "msft" + "-wait").ejWaitingPopup();
    getWebAPIData("MSFT", sender);
}

//Set Data entry for Hilo open close chart
function getWebAPIData(filename, sender) {
    var dataManger1 = ej.DataManager({
        url: "//mvc.syncfusion.com/Services/api/Stock/" + filename + "",
    });
	
    dataManger1.executeQuery(ej.Query()).done(function (e) {
        $("#hiloOpenClose").ejChart("option", { "datasource": sethilodatasource(e.result), "primaryXAxis": setHiloRangeXAxis(), "primaryYAxis": setHiloRangeAxis(filename) });      
		$("#" + filename.toLowerCase() + "-wait").data("ejWaitingPopup").hide();
		  $("#hiloOpenClose").data("ejWaitingPopup").hide();
    });
}

function sethilodatasource(data) {
    return {
        series: [{
            dataSource: data,
			xName: "XValue",
			high: "High", 
			open: "Open", 
			low: "Low", 
			close: "Close"
			}]
    };
}

// load for Column chart
function onchartload(sender) {
    var data = getData(5);
    sender.model.series[0].dataSource = data.Open;
	sender.model.series[0].xName= "XValue";
	sender.model.series[0].yName= "YValue";
    sender.model.series[1].dataSource = data.Close;
	sender.model.series[1].xName= "XValue";
	sender.model.series[1].yName= "YValue";
}

function setdatasource(data) {
    return {
        series: [{
            dataSource:data.Open,
			xName: "XValue",
			yName: "YValue"
        }, {
            dataSource: data.Close, 
			xName: "XValue",
			yName: "YValue" 
        }]
    }
}

//Set Data entry for Column chart
function getData(year) {
    var series1 = [];
    var series2 = [];
    var value = 100;
    var value1 = 150;
    
    for (var i = 1; i <= 64; i++) {
        if (Math.random() > 5) {
            value = getRandomNum(700, 800);
        } else {
            value = getRandomNum(350, 700);
        }
        var point = { XValue: new Date(2008, 01 + i, 15), YValue: value };
        series1.push(point);
        if (value > 400)
            value1 = value - 100;
        else
            value1 = value + 200;
        var point1 = { XValue: new Date(2008, 01 + i, 15), YValue: value1 };
        series2.push(point1);
    }
    var data = { Open: series1, Close: series2 };
    return data;
}

function getRandomNum(ubound, lbound) {

    return (Math.floor(Math.random() * (ubound - lbound)) + lbound);
}

function setHiloRangeXAxis() {
    return {
        range: { min: new Date(2007, 09, 1), max: new Date(2012, 10, 31), interval: 10 },
        intervalType: 'Months',
    };
}

function setHiloRangeAxis(rnge) {
    switch (rnge) {
        case "GOOG":
            return { range: { min: 0, max: 800, interval: 200 } };
            break;
        case "NOK":
            return { range: { min: 0, max: 50, interval: 10 } };
            break;
        case "MSFT":
            return { range: { min: 10, max: 40, interval: 10 } };
            break;
        case "AAPL":
            return { range: { min: 0, max: 800, interval: 200 } };
            break;
    }
}


function setRangeAxis(rnge) {
    switch (rnge) {
        case 3:
            return {

                range: { min: new Date(2010, 02, 1), max: new Date(2013, 02, 1), interval: 1 },
                intervalType: 'Years',
				labelFormat:"yyyy",
            };
            break;
        case 2:
            return {

                range: { min: new Date(2011, 02, 1), max: new Date(2013, 02, 1), interval: 1 },
                intervalType: 'Years',
				labelFormat:"yyyy",

            };
        case 1:
            return {

                range: { min: new Date(2011, 12, 1), max: new Date(2012, 12, 1), interval: 1 },
                intervalType: 'Months',
				labelFormat:"MMM",				
            };
            break;
        default:
            return {                
                range: { min: new Date(2008, 02, 1), max: new Date(2013, 02, 1), interval: 1 },
                intervalType: "Years",
				labelFormat:"yyyy",
            };
    }
}
$(".changeyear").click(function (e) {
    switch (this.id) {
        case "5y":
            $("#colChart").ejChart("option", { "primaryXAxis": setRangeAxis() });
            break;
        case "3y":
            $("#colChart").ejChart("option", { "primaryXAxis": setRangeAxis(3) });
            break;
        case "2y":
            $("#colChart").ejChart("option", {  "primaryXAxis": setRangeAxis(2) });
            break;
        case "1y":
            $("#colChart").ejChart("option", {  "primaryXAxis": setRangeAxis(1) });
            break;
    }
});
 $(window).resize(function(args)
 { 
 
 if($('#goog-wait').data("ejWaitingPopup") != undefined)
 $('#goog-wait').data("ejWaitingPopup").refresh();
 if($('#aapl-wait').data("ejWaitingPopup")!= undefined)
 $('#aapl-wait').data("ejWaitingPopup").refresh();
 if($('#msft-wait').data("ejWaitingPopup") != undefined)
 $('#msft-wait').data("ejWaitingPopup").refresh();
 if($('#nok-wait').data("ejWaitingPopup") != undefined)
 $('#nok-wait').data("ejWaitingPopup").refresh();
});