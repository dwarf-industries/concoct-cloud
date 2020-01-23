/**
 * 1. blueTheme 2. darkTheme 3. grayTheme 4. mintTheme 5. shineTheme 6. redTheme
 * 
 */
var ChartsTheme = function() {

	this.blueTheme = {
		// 默认色板
		color : [ '#1790cf', '#1bb2d8', '#99d2dd', '#88b0bb', '#1c7099',
				'#038cc4', '#75abd0', '#afd6dd' ],

		// 图表标题
		title : {
			textStyle : {
				fontWeight : 'normal',
				color : '#1790cf'
			}
		},

		// 值域
		dataRange : {
			color : [ '#1178ad', '#72bbd0' ]
		},

		// 工具箱
		toolbox : {
			color : [ '#1790cf', '#1790cf', '#1790cf', '#1790cf' ]
		},

		// 提示框
		tooltip : {
			backgroundColor : 'rgba(0,0,0,0.5)',
			axisPointer : { // 坐标轴指示器，坐标轴触发有效
				type : 'line', // 默认为直线，可选为：'line' | 'shadow'
				lineStyle : { // 直线指示器样式设置
					color : '#1790cf',
					type : 'dashed'
				},
				crossStyle : {
					color : '#1790cf'
				},
				shadowStyle : { // 阴影指示器样式设置
					color : 'rgba(200,200,200,0.3)'
				}
			}
		},

		// 区域缩放控制器
		dataZoom : {
			dataBackgroundColor : '#eee', // 数据背景颜色
			fillerColor : 'rgba(144,197,237,0.2)', // 填充颜色
			handleColor : '#1790cf' // 手柄颜色
		},

		// 网格
		grid : {
			borderWidth : 0
		},

		// 类目轴
		categoryAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#1790cf'
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		// 数值型坐标轴默认参数
		valueAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#1790cf'
				}
			},
			splitArea : {
				show : true,
				areaStyle : {
					color : [ 'rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)' ]
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		timeline : {
			lineStyle : {
				color : '#1790cf'
			},
			controlStyle : {
				normal : {
					color : '#1790cf'
				},
				emphasis : {
					color : '#1790cf'
				}
			}
		},

		// K线图默认参数
		k : {
			itemStyle : {
				normal : {
					color : '#1bb2d8', // 阳线填充颜色
					color0 : '#99d2dd', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#1c7099', // 阳线边框颜色
						color0 : '#88b0bb' // 阴线边框颜色
					}
				}
			}
		},

		map : {
			itemStyle : {
				normal : {
					areaStyle : {
						color : '#ddd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				},
				emphasis : { // 也是选中样式
					areaStyle : {
						color : '#99d2dd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					linkStyle : {
						color : '#1790cf'
					}
				}
			}
		},

		chord : {
			padding : 4,
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#1bb2d8' ], [ 0.8, '#1790cf' ],
							[ 1, '#1c7099' ] ],
					width : 8
				}
			},
			axisTick : { // 坐标轴小标记
				splitNumber : 10, // 每份split细分多少段
				length : 12, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto'
				}
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			},
			splitLine : { // 分隔线
				length : 18, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : 'auto'
				}
			},
			pointer : {
				length : '90%',
				color : 'auto'
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#333'
				}
			},
			detail : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			}
		},

		textStyle : {
			fontFamily : '微软雅黑, Arial, Verdana, sans-serif'
		}
	};

	this.darkTheme = {
		// 全图默认背景
		backgroundColor : '#1b1b1b',

		// 默认色板
		color : [ '#FE8463', '#9BCA63', '#FAD860', '#60C0DD', '#0084C6',
				'#D7504B', '#C6E579', '#26C0C0', '#F0805A', '#F4E001',
				'#B5C334' ],

		// 图表标题
		title : {
			textStyle : {
				fontWeight : 'normal',
				color : '#fff' // 主标题文字颜色
			}
		},

		// 图例
		legend : {
			textStyle : {
				color : '#ccc' // 图例文字颜色
			}
		},

		// 值域
		dataRange : {
			itemWidth : 15,
			color : [ '#FFF808', '#21BCF9' ],
			textStyle : {
				color : '#ccc' // 值域文字颜色
			}
		},

		toolbox : {
			color : [ '#fff', '#fff', '#fff', '#fff' ],
			effectiveColor : '#FE8463',
			disableColor : '#666'
		},

		// 提示框
		tooltip : {
			backgroundColor : 'rgba(250,250,250,0.8)', // 提示背景颜色，默认为透明度为0.7的黑色
			axisPointer : { // 坐标轴指示器，坐标轴触发有效
				type : 'line', // 默认为直线，可选为：'line' | 'shadow'
				lineStyle : { // 直线指示器样式设置
					color : '#aaa'
				},
				crossStyle : {
					color : '#aaa'
				},
				shadowStyle : { // 阴影指示器样式设置
					color : 'rgba(200,200,200,0.2)'
				}
			},
			textStyle : {
				color : '#333'
			}
		},

		// 区域缩放控制器
		dataZoom : {
			dataBackgroundColor : '#555', // 数据背景颜色
			fillerColor : 'rgba(200,200,200,0.2)', // 填充颜色
			handleColor : '#eee' // 手柄颜色
		},

		// 网格
		grid : {
			borderWidth : 0
		},

		// 类目轴
		categoryAxis : {
			axisLine : { // 坐标轴线
				show : false
			},
			axisTick : { // 坐标轴小标记
				show : false
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#ccc'
				}
			},
			splitLine : { // 分隔线
				show : false
			}
		},

		// 数值型坐标轴默认参数
		valueAxis : {
			axisLine : { // 坐标轴线
				show : false
			},
			axisTick : { // 坐标轴小标记
				show : false
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#ccc'
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#aaa' ],
					type : 'dashed'
				}
			},
			splitArea : { // 分隔区域
				show : false
			}
		},

		polar : {
			name : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#ccc'
				}
			},
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#ddd'
				}
			},
			splitArea : {
				show : true,
				areaStyle : {
					color : [ 'rgba(250,250,250,0.2)', 'rgba(200,200,200,0.2)' ]
				}
			},
			splitLine : {
				lineStyle : {
					color : '#ddd'
				}
			}
		},

		timeline : {
			label : {
				textStyle : {
					color : '#ccc'
				}
			},
			lineStyle : {
				color : '#aaa'
			},
			controlStyle : {
				normal : {
					color : '#fff'
				},
				emphasis : {
					color : '#FE8463'
				}
			},
			symbolSize : 3
		},

		// 折线图默认参数
		line : {
			smooth : true
		},

		// K线图默认参数
		k : {
			itemStyle : {
				normal : {
					color : '#FE8463', // 阳线填充颜色
					color0 : '#9BCA63', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#FE8463', // 阳线边框颜色
						color0 : '#9BCA63' // 阴线边框颜色
					}
				}
			}
		},

		// 雷达图默认参数
		radar : {
			symbol : 'emptyCircle', // 图形类型
			symbolSize : 3
		// symbol: null, // 拐点图形类型
		// symbolRotate : null, // 图形旋转控制
		},

		pie : {
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(255, 255, 255, 0.5)'
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(255, 255, 255, 1)'
				}
			}
		},

		map : {
			itemStyle : {
				normal : {
					borderColor : 'rgba(255, 255, 255, 0.5)',
					areaStyle : {
						color : '#ddd'
					},
					label : {
						textStyle : {
						// color: '#ccc'
						}
					}
				},
				emphasis : { // 也是选中样式
					areaStyle : {
						color : '#FE8463'
					},
					label : {
						textStyle : {
						// color: 'ccc'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					linkStyle : {
						color : '#fff'
					}
				}
			}
		},

		chord : {
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(228, 228, 228, 0.2)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(228, 228, 228, 0.2)'
						}
					}
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(228, 228, 228, 0.9)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(228, 228, 228, 0.9)'
						}
					}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#9BCA63' ], [ 0.8, '#60C0DD' ],
							[ 1, '#D7504B' ] ],
					width : 3,
					shadowColor : '#fff', // 默认透明
					shadowBlur : 10
				}
			},
			axisTick : { // 坐标轴小标记
				length : 15, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto',
					shadowColor : '#fff', // 默认透明
					shadowBlur : 10
				}
			},
			axisLabel : { // 坐标轴小标记
				textStyle : { // 属性lineStyle控制线条样式
					fontWeight : 'bolder',
					color : '#fff',
					shadowColor : '#fff', // 默认透明
					shadowBlur : 10
				}
			},
			splitLine : { // 分隔线
				length : 25, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					width : 3,
					color : '#fff',
					shadowColor : '#fff', // 默认透明
					shadowBlur : 10
				}
			},
			pointer : { // 分隔线
				shadowColor : '#fff', // 默认透明
				shadowBlur : 5
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					fontWeight : 'bolder',
					fontSize : 20,
					fontStyle : 'italic',
					color : '#fff',
					shadowColor : '#fff', // 默认透明
					shadowBlur : 10
				}
			},
			detail : {
				shadowColor : '#fff', // 默认透明
				shadowBlur : 5,
				offsetCenter : [ 0, '50%' ], // x, y，单位px
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					fontWeight : 'bolder',
					color : '#fff'
				}
			}
		},

		funnel : {
			itemStyle : {
				normal : {
					borderColor : 'rgba(255, 255, 255, 0.5)',
					borderWidth : 1
				},
				emphasis : {
					borderColor : 'rgba(255, 255, 255, 1)',
					borderWidth : 1
				}
			}
		},

		textStyle : {
			fontFamily : '微软雅黑, Arial, Verdana, sans-serif'
		}
	};

	this.grayTheme = {

		color : [ '#757575', '#c7c7c7', '#dadada', '#8b8b8b', '#b5b5b5',
				'#e9e9e9' ],

		// 图表标题
		title : {
			textStyle : {
				fontWeight : 'normal',
				color : '#757575'
			}
		},

		// 值域
		dataRange : {
			color : [ '#636363', '#dcdcdc' ]
		},

		// 工具箱
		toolbox : {
			color : [ '#757575', '#757575', '#757575', '#757575' ]
		},

		// 提示框
		tooltip : {
			backgroundColor : 'rgba(0,0,0,0.5)',
			axisPointer : { // 坐标轴指示器，坐标轴触发有效
				type : 'line', // 默认为直线，可选为：'line' | 'shadow'
				lineStyle : { // 直线指示器样式设置
					color : '#757575',
					type : 'dashed'
				},
				crossStyle : {
					color : '#757575'
				},
				shadowStyle : { // 阴影指示器样式设置
					color : 'rgba(200,200,200,0.3)'
				}
			}
		},

		// 区域缩放控制器
		dataZoom : {
			dataBackgroundColor : '#eee', // 数据背景颜色
			fillerColor : 'rgba(117,117,117,0.2)', // 填充颜色
			handleColor : '#757575' // 手柄颜色
		},

		// 网格
		grid : {
			borderWidth : 0
		},

		// 类目轴
		categoryAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#757575'
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		// 数值型坐标轴默认参数
		valueAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#757575'
				}
			},
			splitArea : {
				show : true,
				areaStyle : {
					color : [ 'rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)' ]
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		timeline : {
			lineStyle : {
				color : '#757575'
			},
			controlStyle : {
				normal : {
					color : '#757575'
				},
				emphasis : {
					color : '#757575'
				}
			}
		},

		// K线图默认参数
		k : {
			itemStyle : {
				normal : {
					color : '#8b8b8b', // 阳线填充颜色
					color0 : '#dadada', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#757575', // 阳线边框颜色
						color0 : '#c7c7c7' // 阴线边框颜色
					}
				}
			}
		},

		map : {
			itemStyle : {
				normal : {
					areaStyle : {
						color : '#ddd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				},
				emphasis : { // 也是选中样式
					areaStyle : {
						color : '#99d2dd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					linkStyle : {
						color : '#757575'
					}
				}
			}
		},

		chord : {
			padding : 4,
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#b5b5b5' ], [ 0.8, '#757575' ],
							[ 1, '#5c5c5c' ] ],
					width : 8
				}
			},
			axisTick : { // 坐标轴小标记
				splitNumber : 10, // 每份split细分多少段
				length : 12, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto'
				}
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			},
			splitLine : { // 分隔线
				length : 18, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : 'auto'
				}
			},
			pointer : {
				length : '90%',
				color : 'auto'
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#333'
				}
			},
			detail : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			}
		},

		textStyle : {
			fontFamily : '微软雅黑, Arial, Verdana, sans-serif'
		}
	};
	this.greenTheme = {
		// 默认色板
		color : [ '#408829', '#68a54a', '#a9cba2', '#86b379', '#397b29',
				'#8abb6f', '#759c6a', '#bfd3b7' ],

		// 图表标题
		title : {
			textStyle : {
				fontWeight : 'normal',
				color : '#408829'
			}
		},

		// 值域
		dataRange : {
			color : [ '#1f610a', '#97b58d' ]
		},

		// 工具箱
		toolbox : {
			color : [ '#408829', '#408829', '#408829', '#408829' ]
		},

		// 提示框
		tooltip : {
			backgroundColor : 'rgba(0,0,0,0.5)',
			axisPointer : { // 坐标轴指示器，坐标轴触发有效
				type : 'line', // 默认为直线，可选为：'line' | 'shadow'
				lineStyle : { // 直线指示器样式设置
					color : '#408829',
					type : 'dashed'
				},
				crossStyle : {
					color : '#408829'
				},
				shadowStyle : { // 阴影指示器样式设置
					color : 'rgba(200,200,200,0.3)'
				}
			}
		},

		// 区域缩放控制器
		dataZoom : {
			dataBackgroundColor : '#eee', // 数据背景颜色
			fillerColor : 'rgba(64,136,41,0.2)', // 填充颜色
			handleColor : '#408829' // 手柄颜色
		},

		// 网格
		grid : {
			borderWidth : 0
		},

		// 类目轴
		categoryAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#408829'
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		// 数值型坐标轴默认参数
		valueAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#408829'
				}
			},
			splitArea : {
				show : true,
				areaStyle : {
					color : [ 'rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)' ]
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		timeline : {
			lineStyle : {
				color : '#408829'
			},
			controlStyle : {
				normal : {
					color : '#408829'
				},
				emphasis : {
					color : '#408829'
				}
			}
		},

		// K线图默认参数
		k : {
			itemStyle : {
				normal : {
					color : '#68a54a', // 阳线填充颜色
					color0 : '#a9cba2', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#408829', // 阳线边框颜色
						color0 : '#86b379' // 阴线边框颜色
					}
				}
			}
		},

		map : {
			itemStyle : {
				normal : {
					areaStyle : {
						color : '#ddd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				},
				emphasis : { // 也是选中样式
					areaStyle : {
						color : '#99d2dd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					linkStyle : {
						color : '#408829'
					}
				}
			}
		},

		chord : {
			padding : 4,
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#86b379' ], [ 0.8, '#68a54a' ],
							[ 1, '#408829' ] ],
					width : 8
				}
			},
			axisTick : { // 坐标轴小标记
				splitNumber : 10, // 每份split细分多少段
				length : 12, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto'
				}
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			},
			splitLine : { // 分隔线
				length : 18, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : 'auto'
				}
			},
			pointer : {
				length : '90%',
				color : 'auto'
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#333'
				}
			},
			detail : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			}
		},

		textStyle : {
			fontFamily : '微软雅黑, Arial, Verdana, sans-serif'
		}
	};
	this.mintTheme = {
		// 全图默认背景
		// backgroundColor: 'rgba(0,0,0,0)',

		// 默认色板
		color : [ '#8aedd5', '#93bc9e', '#cef1db', '#7fe579', '#a6d7c2',
				'#bef0bb', '#99e2vb', '#94f8a8', '#7de5b8', '#4dfb70' ],

		// 值域
		dataRange : {
			color : [ '#93bc92', '#bef0bb' ]
		},

		// K线图默认参数
		k : {
			// barWidth : null // 默认自适应
			// barMaxWidth : null // 默认自适应
			itemStyle : {
				normal : {
					color : '#8aedd5', // 阳线填充颜色
					color0 : '#7fe579', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#8aedd5', // 阳线边框颜色
						color0 : '#7fe579' // 阴线边框颜色
					}
				},
				emphasis : {
				// color: 各异,
				// color0: 各异
				}
			}
		},

		// 饼图默认参数
		pie : {
			itemStyle : {
				normal : {
					// color: 各异,
					borderColor : '#fff',
					borderWidth : 1,
					label : {
						show : true,
						position : 'outer',
						textStyle : {
							color : '#1b1b1b'
						},
						lineStyle : {
							color : '#1b1b1b'
						}
					// textStyle: null // 默认使用全局文本样式，详见TEXTSTYLE
					},
					labelLine : {
						show : true,
						length : 20,
						lineStyle : {
							// color: 各异,
							width : 1,
							type : 'solid'
						}
					}
				}
			}
		},

		map : {
			mapType : 'china', // 各省的mapType暂时都用中文
			mapLocation : {
				x : 'center',
				y : 'center'
			// width // 自适应
			// height // 自适应
			},
			showLegendSymbol : true, // 显示图例颜色标识（系列标识的小圆点），存在legend时生效
			itemStyle : {
				normal : {
					// color: 各异,
					borderColor : '#fff',
					borderWidth : 1,
					areaStyle : {
						color : '#ccc'// rgba(135,206,250,0.8)
					},
					label : {
						show : false,
						textStyle : {
							color : 'rgba(139,69,19,1)'
						}
					}
				},
				emphasis : { // 也是选中样式
					// color: 各异,
					borderColor : 'rgba(0,0,0,0)',
					borderWidth : 1,
					areaStyle : {
						color : '#f3f39d'
					},
					label : {
						show : false,
						textStyle : {
							color : 'rgba(139,69,19,1)'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					// color: 各异,
					label : {
						show : false
					// textStyle: null // 默认使用全局文本样式，详见TEXTSTYLE
					},
					nodeStyle : {
						brushType : 'both',
						strokeColor : '#49b485'
					},
					linkStyle : {
						strokeColor : '#49b485'
					}
				},
				emphasis : {
					// color: 各异,
					label : {
						show : false
					// textStyle: null // 默认使用全局文本样式，详见TEXTSTYLE
					},
					nodeStyle : {},
					linkStyle : {}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#93bc9e' ], [ 0.8, '#8aedd5' ],
							[ 1, '#a6d7c2' ] ],
					width : 8
				}
			},
			axisTick : { // 坐标轴小标记
				splitNumber : 10, // 每份split细分多少段
				length : 12, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto'
				}
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			},
			splitLine : { // 分隔线
				length : 18, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : 'auto'
				}
			},
			pointer : {
				length : '90%',
				color : 'auto'
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#333'
				}
			},
			detail : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			}
		}
	};
	this.redTheme = {
		// 默认色板
		color : [ '#d8361b', '#f16b4c', '#f7b4a9', '#d26666', '#99311c',
				'#c42703', '#d07e75' ],

		// 图表标题
		title : {
			textStyle : {
				fontWeight : 'normal',
				color : '#d8361b'
			}
		},

		// 值域
		dataRange : {
			color : [ '#bd0707', '#ffd2d2' ]
		},

		// 工具箱
		toolbox : {
			color : [ '#d8361b', '#d8361b', '#d8361b', '#d8361b' ]
		},

		// 提示框
		tooltip : {
			backgroundColor : 'rgba(0,0,0,0.5)',
			axisPointer : { // 坐标轴指示器，坐标轴触发有效
				type : 'line', // 默认为直线，可选为：'line' | 'shadow'
				lineStyle : { // 直线指示器样式设置
					color : '#d8361b',
					type : 'dashed'
				},
				crossStyle : {
					color : '#d8361b'
				},
				shadowStyle : { // 阴影指示器样式设置
					color : 'rgba(200,200,200,0.3)'
				}
			}
		},

		// 区域缩放控制器
		dataZoom : {
			dataBackgroundColor : '#eee', // 数据背景颜色
			fillerColor : 'rgba(216,54,27,0.2)', // 填充颜色
			handleColor : '#d8361b' // 手柄颜色
		},

		// 网格
		grid : {
			borderWidth : 0
		},

		// 类目轴
		categoryAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#d8361b'
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		// 数值型坐标轴默认参数
		valueAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#d8361b'
				}
			},
			splitArea : {
				show : true,
				areaStyle : {
					color : [ 'rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)' ]
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		timeline : {
			lineStyle : {
				color : '#d8361b'
			},
			controlStyle : {
				normal : {
					color : '#d8361b'
				},
				emphasis : {
					color : '#d8361b'
				}
			}
		},

		// K线图默认参数
		k : {
			itemStyle : {
				normal : {
					color : '#f16b4c', // 阳线填充颜色
					color0 : '#f7b4a9', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#d8361b', // 阳线边框颜色
						color0 : '#d26666' // 阴线边框颜色
					}
				}
			}
		},

		map : {
			itemStyle : {
				normal : {
					areaStyle : {
						color : '#ddd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				},
				emphasis : { // 也是选中样式
					areaStyle : {
						color : '#99d2dd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					linkStyle : {
						color : '#d8361b'
					}
				}
			}
		},

		chord : {
			padding : 4,
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#f16b4c' ], [ 0.8, '#d8361b' ],
							[ 1, '#99311c' ] ],
					width : 8
				}
			},
			axisTick : { // 坐标轴小标记
				splitNumber : 10, // 每份split细分多少段
				length : 12, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto'
				}
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			},
			splitLine : { // 分隔线
				length : 18, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : 'auto'
				}
			},
			pointer : {
				length : '90%',
				color : 'auto'
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#333'
				}
			},
			detail : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			}
		},

		textStyle : {
			fontFamily : '微软雅黑, Arial, Verdana, sans-serif'
		}
	};
	this.shineTheme = {// 默认色板
		color : [ '#c12e34', '#e6b600', '#0098d9', '#2b821d', '#005eaa',
				'#339ca8', '#cda819', '#32a487' ],

		// 图表标题
		title : {
			textStyle : {
				fontWeight : 'normal'
			}
		},

		// 值域
		dataRange : {
			itemWidth : 15, // 值域图形宽度，线性渐变水平布局宽度为该值 * 10
			color : [ '#1790cf', '#a2d4e6' ]
		},

		// 工具箱
		toolbox : {
			color : [ '#06467c', '#00613c', '#872d2f', '#c47630' ]
		},

		// 提示框
		tooltip : {
			backgroundColor : 'rgba(0,0,0,0.6)'
		},

		// 区域缩放控制器
		dataZoom : {
			dataBackgroundColor : '#dedede', // 数据背景颜色
			fillerColor : 'rgba(154,217,247,0.2)', // 填充颜色
			handleColor : '#005eaa' // 手柄颜色
		},

		// 网格
		grid : {
			borderWidth : 0
		},

		// 类目轴
		categoryAxis : {
			axisLine : { // 坐标轴线
				show : false
			},
			axisTick : { // 坐标轴小标记
				show : false
			}
		},

		// 数值型坐标轴默认参数
		valueAxis : {
			axisLine : { // 坐标轴线
				show : false
			},
			axisTick : { // 坐标轴小标记
				show : false
			},
			splitArea : { // 分隔区域
				show : true, // 默认不显示，属性show控制显示与否
				areaStyle : { // 属性areaStyle（详见areaStyle）控制区域样式
					color : [ 'rgba(250,250,250,0.2)', 'rgba(200,200,200,0.2)' ]
				}
			}
		},

		timeline : {
			lineStyle : {
				color : '#005eaa'
			},
			controlStyle : {
				normal : {
					color : '#005eaa'
				},
				emphasis : {
					color : '#005eaa'
				}
			}
		},

		// K线图默认参数
		k : {
			itemStyle : {
				normal : {
					color : '#c12e34', // 阳线填充颜色
					color0 : '#2b821d', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#c12e34', // 阳线边框颜色
						color0 : '#2b821d' // 阴线边框颜色
					}
				}
			}
		},

		map : {
			itemStyle : {
				normal : {
					areaStyle : {
						color : '#ddd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				},
				emphasis : { // 也是选中样式
					areaStyle : {
						color : '#e6b600'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					linkStyle : {
						color : '#005eaa'
					}
				}
			}
		},

		chord : {
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#2b821d' ], [ 0.8, '#005eaa' ],
							[ 1, '#c12e34' ] ],
					width : 5
				}
			},
			axisTick : { // 坐标轴小标记
				splitNumber : 10, // 每份split细分多少段
				length : 8, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto'
				}
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			},
			splitLine : { // 分隔线
				length : 12, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : 'auto'
				}
			},
			pointer : {
				length : '90%',
				width : 3,
				color : 'auto'
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#333'
				}
			},
			detail : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			}
		},

		textStyle : {
			fontFamily : '微软雅黑, Arial, Verdana, sans-serif'
		}
	};
	this.redTheme = {
		// 默认色板
		color : [ '#d8361b', '#f16b4c', '#f7b4a9', '#d26666', '#99311c',
				'#c42703', '#d07e75' ],

		// 图表标题
		title : {
			textStyle : {
				fontWeight : 'normal',
				color : '#d8361b'
			}
		},

		// 值域
		dataRange : {
			color : [ '#bd0707', '#ffd2d2' ]
		},

		// 工具箱
		toolbox : {
			color : [ '#d8361b', '#d8361b', '#d8361b', '#d8361b' ]
		},

		// 提示框
		tooltip : {
			backgroundColor : 'rgba(0,0,0,0.5)',
			axisPointer : { // 坐标轴指示器，坐标轴触发有效
				type : 'line', // 默认为直线，可选为：'line' | 'shadow'
				lineStyle : { // 直线指示器样式设置
					color : '#d8361b',
					type : 'dashed'
				},
				crossStyle : {
					color : '#d8361b'
				},
				shadowStyle : { // 阴影指示器样式设置
					color : 'rgba(200,200,200,0.3)'
				}
			}
		},

		// 区域缩放控制器
		dataZoom : {
			dataBackgroundColor : '#eee', // 数据背景颜色
			fillerColor : 'rgba(216,54,27,0.2)', // 填充颜色
			handleColor : '#d8361b' // 手柄颜色
		},

		// 网格
		grid : {
			borderWidth : 0
		},

		// 类目轴
		categoryAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#d8361b'
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		// 数值型坐标轴默认参数
		valueAxis : {
			axisLine : { // 坐标轴线
				lineStyle : { // 属性lineStyle控制线条样式
					color : '#d8361b'
				}
			},
			splitArea : {
				show : true,
				areaStyle : {
					color : [ 'rgba(250,250,250,0.1)', 'rgba(200,200,200,0.1)' ]
				}
			},
			splitLine : { // 分隔线
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : [ '#eee' ]
				}
			}
		},

		timeline : {
			lineStyle : {
				color : '#d8361b'
			},
			controlStyle : {
				normal : {
					color : '#d8361b'
				},
				emphasis : {
					color : '#d8361b'
				}
			}
		},

		// K线图默认参数
		k : {
			itemStyle : {
				normal : {
					color : '#f16b4c', // 阳线填充颜色
					color0 : '#f7b4a9', // 阴线填充颜色
					lineStyle : {
						width : 1,
						color : '#d8361b', // 阳线边框颜色
						color0 : '#d26666' // 阴线边框颜色
					}
				}
			}
		},

		map : {
			itemStyle : {
				normal : {
					areaStyle : {
						color : '#ddd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				},
				emphasis : { // 也是选中样式
					areaStyle : {
						color : '#99d2dd'
					},
					label : {
						textStyle : {
							color : '#c12e34'
						}
					}
				}
			}
		},

		force : {
			itemStyle : {
				normal : {
					linkStyle : {
						color : '#d8361b'
					}
				}
			}
		},

		chord : {
			padding : 4,
			itemStyle : {
				normal : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				},
				emphasis : {
					borderWidth : 1,
					borderColor : 'rgba(128, 128, 128, 0.5)',
					chordStyle : {
						lineStyle : {
							color : 'rgba(128, 128, 128, 0.5)'
						}
					}
				}
			}
		},

		gauge : {
			axisLine : { // 坐标轴线
				show : true, // 默认显示，属性show控制显示与否
				lineStyle : { // 属性lineStyle控制线条样式
					color : [ [ 0.2, '#f16b4c' ], [ 0.8, '#d8361b' ],
							[ 1, '#99311c' ] ],
					width : 8
				}
			},
			axisTick : { // 坐标轴小标记
				splitNumber : 10, // 每份split细分多少段
				length : 12, // 属性length控制线长
				lineStyle : { // 属性lineStyle控制线条样式
					color : 'auto'
				}
			},
			axisLabel : { // 坐标轴文本标签，详见axis.axisLabel
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			},
			splitLine : { // 分隔线
				length : 18, // 属性length控制线长
				lineStyle : { // 属性lineStyle（详见lineStyle）控制线条样式
					color : 'auto'
				}
			},
			pointer : {
				length : '90%',
				color : 'auto'
			},
			title : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : '#333'
				}
			},
			detail : {
				textStyle : { // 其余属性默认使用全局文本样式，详见TEXTSTYLE
					color : 'auto'
				}
			}
		},

		textStyle : {
			fontFamily : '微软雅黑, Arial, Verdana, sans-serif'
		}
	};
};
var echartsTheme = new ChartsTheme();