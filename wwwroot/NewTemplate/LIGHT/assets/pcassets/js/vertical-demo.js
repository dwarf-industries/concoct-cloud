 
$( document ).ready(function() {
	$(function() {
		$("#fakeLoader").fakeLoader(); 
	}); 
	$( "#pcoded" ).pcodedmenu({
		themelayout: 'vertical', 
		verticalMenuplacement: 'left',		// value should be left/right
		verticalMenulayout: 'wide',   		// value should be wide/box/widebox
		MenuTrigger: 'click', 
		SubMenuTrigger: 'click',
		activeMenuClass: 'active',
		ThemeBackgroundPattern: 'pattern6',
		HeaderBackground: 'theme2' ,
		LHeaderBackground :'theme4',
		NavbarBackground: 'theme4',
		ActiveItemBackground: 'theme0',
		SubItemBackground: 'theme2', 
		ActiveItemStyle: 'style0',
		ItemBorder: true,
		ItemBorderStyle: 'solid',
		SubItemBorder: true,
		DropDownIconStyle: 'style1', // Value should be style1,style2,style3
		FixedNavbarPosition: false,
		FixedHeaderPosition: false,
		collapseVerticalLeftHeader: true,
		VerticalSubMenuItemIconStyle: 'style6',  // value should be style1,style2,style3,style4,style5,style6
		VerticalNavigationView: 'view1',
		verticalMenueffect:{
			desktop : "shrink",
			tablet : "push",
			phone : "overlay",
		},
		defaultVerticalMenu: {
			desktop : "expanded",	// value should be offcanvas/collapsed/expanded/compact/compact-acc/fullpage/ex-popover/sub-expanded
			tablet : "collapsed",		// value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
			phone : "offcanvas",		// value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
		},
		onToggleVerticalMenu : {
			desktop : "collapsed",		// value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
			tablet : "expanded",		// value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
			phone : "expanded",			// value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
		},

	});
	 
	
	/* Left header Theme Change function Start */
	function handleleftheadertheme() {
		$('.theme-color > a.leftheader-theme').on("click", function() {
			var lheadertheme = $(this).attr("lheader-theme");
			$('.pcoded-header .pcoded-left-header').attr("lheader-theme", lheadertheme);
        });
    };
	
	handleleftheadertheme();
 /* Left header Theme Change function Close */	
 /* header Theme Change function Start */	
	function handleheadertheme() {
		$('.theme-color > a.header-theme').on("click", function() {
			var headertheme = $(this).attr("header-theme");
			$('.pcoded-header').attr("header-theme", headertheme);
        });
    };
	handleheadertheme();
 /* header Theme Change function Close */	
 /* Navbar Theme Change function Start */	
	function handlenavbartheme() {
		$('.theme-color > a.navbar-theme').on("click", function() {
			var navbartheme = $(this).attr("navbar-theme");
			$('.pcoded-navbar').attr("navbar-theme", navbartheme);
        });
    };
	
	handlenavbartheme();
 /* Navbar Theme Change function Close */
 /* Active Item Theme Change function Start */
	function handleactiveitemtheme() {
		$('.theme-color > a.active-item-theme').on("click", function() {
			var activeitemtheme = $(this).attr("active-item-theme");
			$('.pcoded-navbar').attr("active-item-theme", activeitemtheme);
        });
    };
	
	handleactiveitemtheme();
 /* Active Item Theme Change function Close */
 /* SubItem Theme Change function Start */	
	function handlesubitemtheme() {
		$('.theme-color > a.sub-item-theme').on("click", function() {
			var subitemtheme = $(this).attr("sub-item-theme");
			$('.pcoded-navbar').attr("sub-item-theme", subitemtheme);
        });
    };
	
	handlesubitemtheme();
 /* SubItem Theme Change function Close */
 /* Theme background pattren Change function Start */
	function handlethemebgpattern() {
		$('.theme-color > a.themebg-pattern').on("click", function() {
			var themebgpattern = $(this).attr("themebg-pattern");
			$('body').attr("themebg-pattern", themebgpattern);
        });
    };
	
	handlethemebgpattern();
 /* Theme background pattren Change function Close */
 /* Vertical Navigation View Change function start*/
	function handleVerticalNavigationViewChange() {
		$('#navigation-view').val('view1').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded').attr('vnavigation-view', get_value); 
		});
	};

   handleVerticalNavigationViewChange ();
 /* Theme Layout Change function Close*/
 /* Theme Layout Change function start*/
	function handlethemeverticallayout() {
		$('#theme-layout').val('wide').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded').attr('vertical-layout', get_value); 
		});
	};

   handlethemeverticallayout ();
 /* Theme Layout Change function Close*/
 /* Menu effect change function start*/
	function handleverticalMenueffect() {
		$('#vertical-menu-effect').val('shrink').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded').attr('vertical-effect', get_value); 
		});
	};

   handleverticalMenueffect ();
 /* Menu effect change function Close*/ 
 /* Vertical Menu Placement change function start*/ 
   function handleverticalMenuplacement() {
		$('#vertical-navbar-placement').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded').attr('vertical-placement', get_value);
			$('.pcoded-navbar').attr("pcoded-navbar-position", 'absolute' ); 
			$('.pcoded-header .pcoded-left-header').attr("pcoded-lheader-position", 'relative' );
		});
	};

   handleverticalMenuplacement ();
 /* Vertical Menu Placement change function Close*/  
 /* Vertical Active Item Style change function Start*/  
   function handleverticalActiveItemStyle() {
		$('#vertical-activeitem-style').val('style1').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded-navbar').attr('active-item-style', get_value); 
		});
	};

   handleverticalActiveItemStyle ();
 /* Vertical Active Item Style change function Close*/ 
 /* Vertical Item border change function Start*/   
	function handleVerticalIItemBorder() {
			$('#vertical-item-border').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-navbar .pcoded-item').attr('item-border', 'false');
				}else {
					$('.pcoded-navbar .pcoded-item').attr('item-border', 'true');
				}      
			});
		};

   handleVerticalIItemBorder ();
 /* Vertical Item border change function Close*/   
 /* Vertical SubItem border change function Start*/   
   function handleVerticalSubIItemBorder() {
			$('#vertical-subitem-border').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-navbar .pcoded-item').attr('subitem-border', 'false');
				}else {
					$('.pcoded-navbar .pcoded-item').attr('subitem-border', 'true');
				}      
			});
		};

   handleVerticalSubIItemBorder ();
 /* Vertical SubItem border change function Close*/  
 /* Vertical Item border Style change function Start*/  
   function handleverticalboderstyle() {
		$('#vertical-border-style').val('solid').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded-navbar .pcoded-item').attr('item-border-style', get_value); 
		});
	};

   handleverticalboderstyle ();
 /* Vertical Item border Style change function Close*/   
 /* Vertical Dropdown Icon change function Start*/ 
      function handleVerticalDropDownIconStyle() {
		$('#vertical-dropdown-icon').val('style1').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded-navbar .pcoded-hasmenu').attr('dropdown-icon', get_value); 
		});
	};

   handleVerticalDropDownIconStyle ();
 /* Vertical Dropdown Icon change function Close*/
 /* Vertical SubItem Icon change function Start*/

    function handleVerticalSubMenuItemIconStyle() {
		$('#vertical-subitem-icon').val('style5').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded-navbar .pcoded-hasmenu').attr('subitem-icon', get_value); 
		});
	};

   handleVerticalSubMenuItemIconStyle ();
 /* Vertical SubItem Icon change function Close*/
 /* Vertical Navbar Position change function Start*/ 
	function handlesidebarposition() {
			$('#sidebar-position').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-navbar').attr("pcoded-navbar-position", 'fixed' );
					$('.pcoded-header .pcoded-left-header').attr("pcoded-lheader-position", 'fixed' );
				}else {
					$('.pcoded-navbar').attr("pcoded-navbar-position", 'absolute' ); 
					$('.pcoded-header .pcoded-left-header').attr("pcoded-lheader-position", 'relative' );
				}      
			});
		};

   handlesidebarposition ();
 /* Vertical Navbar Position change function Close*/   
 /* Vertical Header Position change function Start*/ 
   	function handleheaderposition() {
			$('#header-position').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-header').attr("pcoded-header-position", 'fixed' );
					$('.pcoded-main-container').css('margin-top', $(".pcoded-header").outerHeight());
				}else {
					$('.pcoded-header').attr("pcoded-header-position", 'relative' );
					$('.pcoded-main-container').css('margin-top', '0px');
				}      
			});
		};

   handleheaderposition ();
 /* Vertical Header Position change function Close*/ 


/*  collapseable Left Header Change Function Start here*/
   	function handlecollapseLeftHeader() {
			$('#collapse-left-header').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-header, .pcoded ').removeClass('iscollapsed');
					$('.pcoded-header, .pcoded').addClass('nocollapsed');
				}else { 
					$('.pcoded-header, .pcoded').addClass('iscollapsed');
					$('.pcoded-header, .pcoded').removeClass('nocollapsed');					
				}      
			});
		};

   handlecollapseLeftHeader ();


/*  collapseable Left Header Change Function Close here*/
 
 
  $(function() { 
		var values = [3,4,5,8,6,10,3,6,9,12,5,6,10,8,9,15,14,10,9,20,16,14,10,12,9,5,6,8,6,10,6];
		$('.rsa').sparkline(values, {
			type: "bar",
			tooltipSuffix: " widgets",
			height: '50px',
			barSpacing: 1,
			barWidth: 4,
			barColor: '#70ca63',
			tooltipFormat: "{{value:val}}",
			tooltipValueLookups: {"val": {"-1": "N/A"}}
		});
	 });
	 $(function() { 
		var values = [3,4,5,8,6,10,3,6,9,12,5,6,10,8,9,15,14,10,9,20,16,14,10,12,9,5,6,8,6,10,6];
		$('.tsa').sparkline(values, {
			type: "bar",
			tooltipSuffix: " widgets",
			height: '50px',
			barSpacing: 1,
			barWidth: 4,
			barColor: '#f9ab49',
		});
	 });
	 $(function() { 
		var values = [3,4,5,8,6,10,3,6,9,12,5,6,10,8,9,15,14,10,9,20,16,14,10,12,9,5,6,8,6,10,6];
		$('.isa').sparkline(values, {
			type: "bar",
			tooltipSuffix: " widgets",
			height: '50px',
			barSpacing: 1,
			barWidth: 4,
			barColor: '#24b4b7',
		});
	 });
	 $(function() { 
		var values = [3,4,5,8,6,10,3,6,9,12,5,6,10,8,9,15,14,10,9,20,16,14,10,12,9,5,6,8,6,10,6];
		$('.ssa').sparkline(values, {
			type: "bar",
			tooltipSuffix: " widgets",
			height: '50px',
			barSpacing: 1,
			barWidth: 4,
			barColor: '#25726e',
			
		});
	 });
 
	function salesChart() {  
    var myChart = echarts.init(document.getElementById('sales-chart'));
    var option = {
    title : {
		show : false,
        text: 'line Chart',
        subtext: 'sub title'
    },
    tooltip : {
        trigger: 'axis',
		backgroundColor: '#354052',
		axisPointer: {
			type: 'cross',
		},
    },


    legend: {
		backgroundColor: '#FFF',
		borderColor: '#ddd',
		borderWidth : 0.5,
		padding: 5,
		orient: 'horizontal',
		x: 'right',
		y: 'top',
		data:['Total Sale','Item Sale','Referral Sale','Support Sale'],
		 
    },
	grid: {
		zlevel: 0,
		z: 1,
		x: 50,
		y: 50,
		x2: 15,
		y2: 45,
//		width: 400,
//		height: 150,
		backgroundColor: '#FFF',
		borderWidth: 1,
		borderColor: '#f1f1f1',
	},
    toolbox: {
        show : false,
        feature : {
            mark : {show: true},
            dataView : {show: true, readOnly: false},
            magicType : {show: true, type: ['line', 'bar', 'stack', 'tiled']},
            restore : {show: true},
            saveAsImage : {show: true}
        }
    },
	
	
    calculable : true,
    xAxis : [
        {
            type : 'category',
            boundaryGap : false,
			axisLabel: {
				show: true, 
				rotate: 45, 
			},
            data : ['oct 1','oct 2','oct 3','oct 4','oct 5','oct 6','oct 7','oct 8','oct 9','oct 10','oct 11','oct 12','oct 13','oct 14','oct 15','oct 16','oct 17','oct 18','oct 19','oct 20','oct 21','oct 22','oct 23','oct 24','oct 25','oct 26','oct 27','oct 28','oct 29','oct 30','oct 31']
        }
    ],
    yAxis : [
        {
            type : 'value',
			show: true,
			name: '',
			nameLocation: 'end',
			scale: true,
			min: 0,
			max: 150,
			splitNumber: 10,
			axisLine: {
				show: true,
				onZero: true,
				lineStyle: {
					type: 'solid',
					color: '#f1f1f1',
					width: 1,
				},
			},
			axisLabel: {
				show: true,
				formatter: '${value}'
			},
        }
    ],
    series : [
		{
            name:'Support Sale',
            type:'line',
            smooth:true,
			symbol: 'none',
			symbolSize: 10,
			large: false, 
			legendHoverLink: false,
            itemStyle: {
				normal: {
					color: '#25726e',
					areaStyle: {
						type: 'solid',
						color: '#25726e',
					},
					lineStyle: {
							color: '#25726e',
					},
				},
			},
			data:[5, 12, 3, 16, 13, 12,13, 6, 15, 19, 16,13,10,2,20,25,18,15,13,22,16,22,11,12,9,15,8,5,3,16,18,10]
		},
		{
            name:'Referral Sale',
            type:'line',
            smooth:true,
			symbol: 'none',
			symbolSize: 10,
			large: false, 
			legendHoverLink: false,
            itemStyle: {
				normal: {
					color: '#70ca63',
					areaStyle: {
						type: 'solid',
						color: '#70ca63',
					},
					lineStyle: {
							color: '#70ca63',
					},
				},
			},
			data:[10, 15, 8, 11, 14, 22,17, 33, 18, 28, 18,16,14,22,23,38,30,40,43,30,15,0,15,25,5,10,15,0,5,18,22,12]
		}, 
		{
            name:'Item Sale',
            type:'line',
            smooth:true,
			symbol: 'none',
			symbolSize: 10,
			large: false, 
            itemStyle: {
				normal: {
					color: '#24b4b7',
					areaStyle: {
						type: 'solid',
						color: '#24b4b7',
					},
					lineStyle: {
							color: '#24b4b7',
					},
				},
			},
			data:[35, 25, 30, 15, 25, 30,40, 50, 60, 45, 55,50,75,35,15,20,35,40,45,70,80,60,75,45,25,30,35,40,60,70,85]
		},
		{
            name:'Total Sale',
            type:'line',
            smooth:true,
			symbol: 'none',
			symbolSize: 10,
			large: false, 
            itemStyle: {
				normal: {
					color: '#f9ab49',
					areaStyle: {
						type: 'solid',
						color: '#f9ab49',
					},
					lineStyle: {
							color: '#f9ab49',
					},
				},
			},
			data:[50,52,41,42,52,64,70,89,93,92,89,79,99,59,58,83,83,95,105,122,111,82,101,82,39,55,58,45,68,104,125]
		},
    ]
};                 
        myChart.setOption(option);
 
	 };
		salesChart();
		$('.sidebar_toggle').click(function(){
			setTimeout(function() {
				salesChart();
			}, 300)
		})
		function pageviewChart() { 	
			var myChart = echarts.init(document.getElementById('pageview-chart'));
				var option = {
					title : {
					show : false,
					text: 'title',
					subtext: 'sub title'
				},
				tooltip : {
					trigger: 'axis',
					backgroundColor: '#354052',
					axisPointer: {
						type: 'cross',
					},
				},
				legend: {
					backgroundColor: '#FFF',
					borderColor: '#ddd',
					borderWidth : 0.5,
					padding: 5,
					orient: 'horizontal',
					x: 'left',
					y: 'top',
					data:['Daily Page View'],
				},
				grid: {
					zlevel: 0,
					z: 1,
					x: 40,
					y: 40,
					x2: 30,
					y2: 50,
					//width: 400,
					//height: 150,
					backgroundColor: '#FFF',
					borderWidth: 1,
					borderColor: '#f1f1f1',
				},
				toolbox: {
					show : false,
					feature : {
						mark : {show: true},
						dataView : {show: true, readOnly: false},
						magicType : {show: true, type: ['line', 'bar']},
						restore : {show: true},
						saveAsImage : {show: true}
					}
				},
				calculable : true,
					xAxis : [
					{
					type : 'category',
					axisLine: {
						show: true,
						onZero: true,
						lineStyle: {
							color: "#ddd",
							type: 'solid',
							width: 1,
						},
					},
					axisLabel: {
						show: true, 
						rotate: 45, 
					},
					data : ['oct 1','oct 2','oct 3','oct 4','oct 5','oct 6','oct 7','oct 8','oct 9','oct 10','oct 11','oct 12','oct 13','oct 14','oct 15','oct 16','oct 17','oct 18','oct 19','oct 20','oct 21','oct 22','oct 23','oct 24','oct 25','oct 26','oct 27','oct 28','oct 29','oct 30' ]
				}
			],
			yAxis : [
				{
					type : 'value',
					show: true,
					name: '',
					nameLocation: 'end',
					scale: true,
					min: 0,
					max: 1000,
					splitNumber: 10,
					axisLine: {
						show: true,
						onZero: true,
						lineStyle: {
							type: 'solid',
							color: '#f1f1f1',
							width: 1,
						},
					},
					axisLabel: {
						show: true,
						formatter: '{value}'
					},
				}
			],	
			series : [
				{
					name:'Daily Page View',
					type:'bar',
					itemStyle: {
						normal: {
							color: '#00bcd4', 
						},
					},
					data:[90, 215, 345, 210, 522, 355, 645, 855, 750, 65, 210, 500, 800, 750, 722, 980, 750, 655, 820, 420, 330, 555, 651, 220, 359, 100, 600, 450, 550, 550, 550],
					markPoint : {
						data : [
							{type : 'max', name: 'max page view'},
							{type : 'min', name: 'min page view'}
						]
					},
					markLine : {
						
						data : [
							{type : 'average', name: 'average page view'}
						]
					}
				},
			]
		};
										
			myChart.setOption(option);
		};
		pageviewChart();
		$('.sidebar_toggle').click(function(){
			setTimeout(function() {
				pageviewChart();
			}, 300)
		})
});