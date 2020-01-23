/* Sales charts */

var SalesCharts=function(){
	var salesChart;
	var loadingTicket;
	var effect = 'spin';
	var theme=echartsTheme.blueTheme;
	var chartLevel=1;
	var levelList=[{level:'1',value:''}];
	var _this=this;
	var salesData=getSalesData();
	$('[name=theme-select-chartdiv5]').on('change', function() {
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
			setTimeout(salesChart.setTheme(theme), 500);
	});
	this.drawSalesChart=function(level){
		chartLevel=level;
		document.getElementById('chartdiv5').innerHTML='';
		salesChart=echarts.init(document.getElementById('chartdiv5'));
		var salesChartXaxisCategory=getSalesChartXaxisCategory();
		var salesChartPlannedSeriesData=getSalesChartPlannedSeriesData();
		var salesChartActualsSeriesData=getSalesChartActualsSeriesData();
		var salesChartAchievedSeriesData=getSalesChartAchievedSeriesData();
		var option=getSalesChartOptionData(salesChartXaxisCategory,salesChartPlannedSeriesData,
		salesChartActualsSeriesData,salesChartAchievedSeriesData);
		salesChart.showLoading({
		text:'Sales - Planned vs Actual Chart',
        effect : effect
        });
		clearTimeout(loadingTicket);
		loadingTicket = setTimeout(function (){
			salesChart.hideLoading();
			salesChart.setOption(option);
			salesChart.setTheme(theme);
			if(chartLevel!=3){
				salesChart.on(echartsConfig.EVENT.CLICK, drillDownSalesChart);
			}
			popBreadcrumbLevel();
			renderBreadcrumb();
		},1000);
	}
	
	function getSalesChartXaxisCategory(){
		switch(chartLevel) {
			case 2:
				//Category
				var args=levelList[chartLevel-1].value;
				return salesData[args]["legendData"];
				break;
			case 3:
				//Sub - Category
				var args1=levelList[chartLevel-2].value;
				var args2=levelList[chartLevel-1].value;
				return salesData[args1][args2]["legendData"];
				break;
			default:
				return salesData["legendData"];
		} 
	}
	
	function getSalesChartPlannedSeriesData(){
		switch(chartLevel) {
			case 2:
				var args=levelList[chartLevel-1].value;
				return salesData[args]["plannedSeriesData"];
				break;
			case 3:
				var args1=levelList[chartLevel-2].value;
				var args2=levelList[chartLevel-1].value;
				return salesData[args1][args2]["plannedSeriesData"];
				break;
			default:
				return salesData["plannedSeriesData"];
		} 
	}
	
	function getSalesChartActualsSeriesData(){
		switch(chartLevel) {
			case 2:
				var args=levelList[chartLevel-1].value;
				return salesData[args]["actualsSeriesData"];
				break;
			case 3:
				var args1=levelList[chartLevel-2].value;
				var args2=levelList[chartLevel-1].value;
				return salesData[args1][args2]["actualsSeriesData"];
				break;
			default:
				return salesData["actualsSeriesData"];
		} 
	}
	
	function getSalesChartAchievedSeriesData(){
		switch(chartLevel) {
			case 2:
				var args=levelList[chartLevel-1].value;
				return salesData[args]["achievedSeriesData"];
				break;
			case 3:
				var args1=levelList[chartLevel-2].value;
				var args2=levelList[chartLevel-1].value;
				return salesData[args1][args2]["achievedSeriesData"];
				break;
			default:
				return salesData["achievedSeriesData"];
		} 
	}
	
	function getSalesChartOptionData(salesChartXaxisCategory,salesPlannedData,salesActualsData,salesAchievedData){	
		var option = {
		tooltip : {
			trigger: 'axis',
			axisPointer : {           
            type : 'shadow' 
			},
			formatter: function (params){
            return params[0].name + '<br/>'
                   + params[0].seriesName + ' : ' + params[0].value + '<br/>'
                   + params[1].seriesName + ' : ' + params[1].value + '<br/>'
				   + params[2].seriesName + ' : ' + params[2].value + '%';
			}
		},
		 toolbox : {
                'show':true, 
                orient : 'horizontal',
                x: 'right', 
                y: 'bottom',
                'feature':{
                    'mark':{'show':false},
                    'saveAsImage':{'show':true,name:'chart',title:'Save As Image'}
                }
            },
		calculable : false,
		legend: {
			data:['Planned','Actuals','Achieved'],
			x:"right"
		},
		xAxis : [
			{
				type : 'category',
				data : salesChartXaxisCategory,
				boundaryGap: true,
				axisLabel : {
					/*formatter:function (params){
						if(params.length>6){
							return params.substring(0,6)+'...';
						}else{
							return params;
						}
					},*/
					rotate:'10',
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
			},
			{
				type : 'category',
				axisLine: {show:false},
				axisTick: {show:false},
				axisLabel: {show:false},
				splitArea: {show:false},
				splitLine: {show:false},
				data : salesChartXaxisCategory
			}
		],
		yAxis : [
			{
				type : 'value',
				name:"Planned & Actuals",
				nameTextStyle:{
					color: '#000000',
                    fontFamily: 'verdana',
                    fontSize: 10
				},
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
			},
			 {
				type : 'value',
				name:"Achieved in %",
				nameTextStyle:{
					color: '#000000',
                    fontFamily: 'verdana',
                    fontSize: 10
				},
				splitNumber: 5,
				boundaryGap: [0, 0.1],
				min:0,
				axisLabel : {
						formatter: '{value} %', 
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
				name:'Planned',
				type:'bar',
				barWidth:25,
				data:salesPlannedData
			},
			{
				name:'Actuals',
				type:'bar',
				barWidth:25,
				data:salesActualsData
			},
		   {
				name:'Achieved',
				type:'line',
				yAxisIndex:1,
				smooth:true,
				data:salesAchievedData
			}
		]
		};
		return option;
	}
	
	function drillDownSalesChart(param){
		var args=param.name;
		if(chartLevel==1){
			chartLevel=2;
			pushBreadcrumbLevel(args);
			_this.drawSalesChart(chartLevel);
		}else if(chartLevel==2){
			chartLevel=3;
			pushBreadcrumbLevel(args);
			_this.drawSalesChart(chartLevel);
		}else{
			chartLevel=1;
			_this.drawSalesChart(chartLevel);
		}
	}
	
	function pushBreadcrumbLevel(args){
		levelList.push({level:chartLevel,value:args});
	}
	
	function popBreadcrumbLevel(){
		var size=levelList.length;
		for(i=0;i<size-chartLevel;i++){
			levelList.pop();
		}
	}
	
	function renderBreadcrumb(){
		$("#breadcrumb-chartdiv5").empty();
		for(i=0;i<levelList.length;i++){
			if(i==levelList.length-1){
				$("#breadcrumb-chartdiv5").append('<li class="active">Level-'+levelList[i].level+'</li>');
			}else{
				$("#breadcrumb-chartdiv5").append('<li><a href="#" onclick="salesCharts.drawSalesChart('+levelList[i].level+');">Level-'+levelList[i].level+'</a></li>');
			}
		}
	}
	
	function getSalesData(){
		var salesData={
			"legendData":["Q1","Q2","Q3","Jan","Feb"],
			"plannedSeriesData":[12,11,15,4,3],
			"actualsSeriesData":[14,10,21,2,5],
			"achievedSeriesData":[116.67,90.91,140,50, 166.67],
			"Q1":{
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"Grocery":{
					"legendData":['Staples','Dairy & Egg','Personal Care','Fruits & Vegetables','Meat'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Apparel":{
					"legendData":['Jackets‎','Sports clothing','Saris','Suit (clothing)','Dresses'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Footwear":{
					"legendData":['Shoes','Slipeer','Sandals‎','Socks‎','Sports footwear‎'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Electronics":{
					"legendData":['Digital Cameras','iPads & Tablets','Printers, Scanners & Supplies','Laptops & Notebooks','Cables & Connectors'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Home Décor":{
					"legendData":['Window Treatments','Curtain Rods & Hardware','Decorative Accessories','Lighting','Candles & Fragrance'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				}
			},
			"Q2":{
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"Grocery":{
					"legendData":['Staples','Dairy & Egg','Personal Care','Fruits & Vegetables','Meat'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Apparel":{
					"legendData":['Jackets‎','Sports clothing','Saris','Suit (clothing)','Dresses'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Footwear":{
					"legendData":['Shoes','Slipeer','Sandals‎','Socks‎','Sports footwear‎'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Electronics":{
					"legendData":['Digital Cameras','iPads & Tablets','Printers, Scanners & Supplies','Laptops & Notebooks','Cables & Connectors'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Home Décor":{
					"legendData":['Window Treatments','Curtain Rods & Hardware','Decorative Accessories','Lighting','Candles & Fragrance'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				}
			},
			"Q3":{
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"Grocery":{
					"legendData":['Staples','Dairy & Egg','Personal Care','Fruits & Vegetables','Meat'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Apparel":{
					"legendData":['Jackets‎','Sports clothing','Saris','Suit (clothing)','Dresses'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Footwear":{
					"legendData":['Shoes','Slipeer','Sandals‎','Socks‎','Sports footwear‎'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Electronics":{
					"legendData":['Digital Cameras','iPads & Tablets','Printers, Scanners & Supplies','Laptops & Notebooks','Cables & Connectors'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Home Décor":{
					"legendData":['Window Treatments','Curtain Rods & Hardware','Decorative Accessories','Lighting','Candles & Fragrance'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				}
			},
			"Jan":{
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"Grocery":{
					"legendData":['Staples','Dairy & Egg','Personal Care','Fruits & Vegetables','Meat'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Apparel":{
					"legendData":['Jackets‎','Sports clothing','Saris','Suit (clothing)','Dresses'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Footwear":{
					"legendData":['Shoes','Slipeer','Sandals‎','Socks‎','Sports footwear‎'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Electronics":{
					"legendData":['Digital Cameras','iPads & Tablets','Printers, Scanners & Supplies','Laptops & Notebooks','Cables & Connectors'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Home Décor":{
					"legendData":['Window Treatments','Curtain Rods & Hardware','Decorative Accessories','Lighting','Candles & Fragrance'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				}
			},
			"Feb":{
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"legendData":['Grocery','Apparel','Footwear','Electronics','Home Décor'],
				"plannedSeriesData":[12,11,15,4,3],
				"actualsSeriesData":[14,10,21,2,5],
				"achievedSeriesData":[116.67,90.91,140,50, 166.67],
				"Grocery":{
					"legendData":['Staples','Dairy & Egg','Personal Care','Fruits & Vegetables','Meat'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Apparel":{
					"legendData":['Jackets‎','Sports clothing','Saris','Suit (clothing)','Dresses'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Footwear":{
					"legendData":['Shoes','Slipeer','Sandals‎','Socks‎','Sports footwear‎'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Electronics":{
					"legendData":['Digital Cameras','iPads & Tablets','Printers, Scanners & Supplies','Laptops & Notebooks','Cables & Connectors'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				},
				"Home Décor":{
					"legendData":['Window Treatments','Curtain Rods & Hardware','Decorative Accessories','Lighting','Candles & Fragrance'],
					"plannedSeriesData":[12,11,15,4,3],
					"actualsSeriesData":[14,10,21,2,5],
					"achievedSeriesData":[116.67,90.91,140,50, 166.67]
				}
			}
		};
		return salesData;
	}
};