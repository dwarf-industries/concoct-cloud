/* SKU charts */

var SkuCharts=function(){
	var skuChart;
	var loadingTicket;
	var effect = 'spin';
	var theme=echartsTheme.blueTheme;
	var chartLevel=1;
	var levelList=[{level:'1',value:''}];
	var _this=this;
	$('[name=theme-select-chartdiv6]').on('change', function() {
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
			setTimeout(skuChart.setTheme(theme), 500);
	});
	
	this.drawSkuChart=function(level){
		chartLevel=level;
		skuChart = echarts.init(document.getElementById('chartdiv6'));
		var skuChartLegendData=getSkuChartLegendData();
		var skuChartSeriesData=getSkuChartSeriesData();
		var option=getskuChartOptionData(skuChartLegendData,skuChartSeriesData);
		skuChart.showLoading({
		text:'Top Selling Item Chart',
        effect : effect
        });
		clearTimeout(loadingTicket);
		loadingTicket = setTimeout(function (){
			skuChart.hideLoading();
			skuChart.setOption(option);
			skuChart.setTheme(theme);
			if(chartLevel!=2){
				skuChart.on(echartsConfig.EVENT.CLICK, drillDownSkuChart);
			}
			popBreadcrumbLevel();
			renderBreadcrumb();
		},1000);
	}
	
	function getSkuChartLegendData(){
		switch(chartLevel) {
			case 2:
				return [ 'Apple', 'Samsung', 'Nokia', 'HTC','Motorola' ];
				break;
			default:
				return [ '10%', '20%', '30%', '40%' ];
		} 
	}
	
	function getSkuChartSeriesData(){
		switch(chartLevel) {
			case 2:
				return [ {
					name : 'Apple',
					value : 5000
				}, {
					name : 'Samsung',
					value : 3400
				}, {
					name : 'Nokia',
					value : 2975
				}, {
					name : 'HTC',
					value : 2400
				},{
					name : 'Motorola',
					value : 1640
				} ];
				break;
			default:
				return [ {
					name : '10%',
					value : 12450
				}, {
					name : '20%',
					value : 50
				}, {
					name : '30%',
					value : 10000
				}, {
					name : '40%',
					value : 3000
				} ];
		} 
	}

	function getskuChartOptionData(skuChartLegendData,skuChartSeriesData){	
		var option =  {
			legend : {
				orient : 'vertical',
				x : 'left',
				y : 'top',
				data : skuChartLegendData
			},
           tooltip : {
            		formatter: "{b}<br/>{c} ({d}%)"
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
			series : [ {
				type : 'pie',
				data : skuChartSeriesData,
				itemStyle : {
                normal : {
                    label : {
                        position : 'outer',
						formatter : function (params) {                         
                          return params.name+ ' : '+params.value
                        }
                    },
                    labelLine : {
                        show : true
                    }
                }
            }
			} ]
		};
		return option;
	}
	
	function drillDownSkuChart(param){
		if(chartLevel==1){
			chartLevel=2;
			pushBreadcrumbLevel(chartLevel);
			_this.drawSkuChart(chartLevel);
		}else{
			chartLevel=1;
			_this.drawSkuChart(1);
			
		}
	}
	
	function pushBreadcrumbLevel(){
		levelList.push({level:chartLevel,value:''});
	}
	
	function popBreadcrumbLevel(){
		var size=levelList.length;
		for(i=0;i<size-chartLevel;i++){
			levelList.pop();
		}
	}
	
	function renderBreadcrumb(){
		$("#breadcrumb-chartdiv6").empty();
		for(i=0;i<levelList.length;i++){
			if(i==levelList.length-1){
				$("#breadcrumb-chartdiv6").append('<li class="active">Level-'+levelList[i].level+'</li>');
			}else{
				$("#breadcrumb-chartdiv6").append('<li><a href="#" onclick="skuCharts.drawSkuChart('+levelList[i].level+');">Level-'+levelList[i].level+'</a></li>');
			}
		}
	}
};