/* Customer Conversion Charts charts */

var CCCharts=function(){
	var ccChart;
	var loadingTicket;
	var effect = 'spin';
	var theme=echartsTheme.blueTheme;
	var _this=this;
	$('[name=theme-select-chartdiv7]').on('change', function() {
			themeName = $(this).val();
			switch (themeName) {
			case "blue":
				theme = echartsTheme.blueTheme;
				break;
			case "dark":
				theme = echartsTheme.darkTheme;
				break;
			case "gray":
				theme = echartsTheme.grayTheme;
				break;
			case "mint":
				theme = echartsTheme.mintTheme;
				break;
			case "red":
				theme = echartsTheme.redTheme;
				break;
			case "green":
				theme = echartsTheme.greenTheme;
				break;
			default:
				theme = echartsTheme.blueTheme;
				break;
			}
			setTimeout(ccChart.setTheme(theme), 500);
	});
	this.drawCcChart=function(){
		ccChart = echarts.init(document.getElementById('chartdiv7'));
		var ccChartXaxisCategory=["9:00 AM","10:00 AM","11:00 AM","12:00 PM","1:00 PM","2:00 PM","3:00 PM","4:00 PM","5:00 PM","6:00 PM","7:00 PM","8:00 PM","9:00 PM"];
		var ccChartFootfallSeriesData=[73,88,88,76,70,79,76,93,74,78,86,85,96];
		var ccChartSaleTransactionSeriesData=[40,57,59,53,61,61,48,45,60,61,65,43,63];
		var option=getCcChartOptionData(ccChartXaxisCategory,ccChartFootfallSeriesData,
		ccChartSaleTransactionSeriesData);
		ccChart.showLoading({
			text:'Customer Conversion Chart',
			effect : effect
        });
		clearTimeout(loadingTicket);
		loadingTicket = setTimeout(function (){
			ccChart.hideLoading();
			ccChart.setOption(option);
			ccChart.setTheme(theme);
		},1000);
	}
	
	function getCcChartOptionData(ccChartXaxisCategory,ccChartFootfallSeriesData,
		ccChartSaleTransactionSeriesData){	
		var option = {
		tooltip : {
			trigger: 'axis',
			axisPointer : {           
            type : 'shadow' 
			},
			formatter: function (params){
            return params[0].name + '<br/>'
                   + params[0].seriesName + ' : ' + params[0].value + '<br/>'
                   + params[1].seriesName + ' : ' + params[1].value + '<br/>';
			}
		},
		 toolbox : {
                'show':true, 
                orient : 'horizontal',
                x: 'right', 
                y: 'bottom',
                'feature':{
                    'mark':{'show':false},
					'magicType':{'show':true,title : {line : 'Line',bar : 'Bar'},'type':['line','bar']},
                    'saveAsImage':{'show':true,name:'chart',title:'Save As Image'}
                }
            },
		calculable : false,
		legend: {
			data:['Footfall','Sale Transaction'],
			x:"right"
		},
		xAxis : [
			{
				type : 'category',
				data : ccChartXaxisCategory,
				boundaryGap: true,
				axisLabel : {
					rotate:'45',
					textStyle: {
						color: '#000000',
						fontFamily: 'verdana',
						fontSize: 10
					}
				},	
				axisLine : {
					show: true,
					lineStyle: {
						color: '#000000',
						type: 'solid',
						width: 1
					}
				},
				axisTick : {
					show:true,
					length: 5,
					lineStyle: {
						color: '#000000',
						type: 'solid',
						width: 1
					}
				}
			}
		],
		yAxis : [
			{
				type : 'value',
				splitNumber: 5,
				boundaryGap: [0, 0.1],
				min:0,
				axisLabel : {
                show:true,
                interval: 'auto',
                margin: 10,
                formatter: '{value}',
                textStyle: {
						color: '#000000',
						fontFamily: 'verdana',
						fontSize: 10
					}
				},
				axisLine : {
					show: true,
					lineStyle: {
						color: '#000000',
						type: 'solid',
						width: 1
					}
				},
				axisTick : {
					show:true,
					length: 5,
					lineStyle: {
						color: '#000000',
						type: 'solid',
						width: 1
					}
				}
			}
		],
		series : [
			{
				name:'Footfall',
				type:'line',
				data:ccChartFootfallSeriesData
			},
			{
				name:'Sale Transaction',
				type:'line',
				data:ccChartSaleTransactionSeriesData
			}
		]
		};
		return option;
	}
};