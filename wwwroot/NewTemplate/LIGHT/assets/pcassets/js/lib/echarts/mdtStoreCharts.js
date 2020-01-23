/* MDT Store Charts charts */

var MDTStoreCharts=function(){
	var mdtStoreChart;
	var loadingTicket;
	var effect = 'spin';
	var theme=echartsTheme.blueTheme;
	var _this=this;
	$('[name=theme-select-chartdiv8]').on('change', function() {
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
			setTimeout(mdtStoreChart.setTheme(theme), 500);
	});
	this.drawMdtStoreChart=function(){
		mdtStoreChart = echarts.init(document.getElementById('chartdiv8'));
		var mdtStoreChartXaxisCategory=['Inventory Shrinkage','Promotional Sales','Expense to Sale','Return to Sale'];
		var mdtStoreChartTargetSeriesData=[10,2,10,1];
		var mdtStoreChartActualSeriesData=[12,3,12,0.5];
		var option=getMdtStoreChartOptionData(mdtStoreChartXaxisCategory,mdtStoreChartTargetSeriesData,
		mdtStoreChartActualSeriesData);
		mdtStoreChart.showLoading({
			text:'Store Performance MTD Chart',
			effect : effect
        });
		clearTimeout(loadingTicket);
		loadingTicket = setTimeout(function (){
			mdtStoreChart.hideLoading();
			mdtStoreChart.setOption(option);
			mdtStoreChart.setTheme(theme);
		},1000);
		
	}
	
	function getMdtStoreChartOptionData(mdtStoreChartXaxisCategory,mdtStoreChartTargetSeriesData,
		mdtStoreChartActualSeriesData){	
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
			data:['Target %','Actual %'],
			x:"right"
		},
		xAxis : [
			{
				type : 'category',
				data : mdtStoreChartXaxisCategory,
				boundaryGap: true,
				axisLabel : {
					formatter:function (params){
						if(params.length>6){
							return params.substring(0,6)+'...';
						}else{
							return params;
						}
					},
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
				name:'Target %',
				type:'bar',
				barWidth:25,
				data:mdtStoreChartTargetSeriesData
			},
			{
				name:'Actual %',
				type:'bar',
				barWidth:25,
				data:mdtStoreChartActualSeriesData
			}
		]
		};
		return option;
	}
};