
$( document ).ready(function() {
	$( "#pcoded" ).pcodedmenu({
		themelayout: 'vertical',
		verticalMenuplacement: 'left',		// value should be left/right
		verticalMenulayout: 'wide',   		// value should be wide/box/widebox
		MenuTrigger: 'click',
		SubMenuTrigger: 'click',
		activeMenuClass: 'active',
		ThemeBackgroundPattern: 'pattern2',
		HeaderBackground: 'theme4' ,
		LHeaderBackground :'theme4',
		NavbarBackground: 'theme4',
		ActiveItemBackground: 'theme5',
		SubItemBackground: 'theme2',
		ActiveItemStyle: 'style0',
		ItemBorder: true,
		ItemBorderStyle: 'none',
		SubItemBorder: true,
		DropDownIconStyle: 'style3', // Value should be style1,style2,style3
		FixedNavbarPosition: false,
		FixedHeaderPosition: true,
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
			$('.pcoded-navigatio-lavel').attr("menu-title-theme", lheadertheme);
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
					$('.pcoded-navbar').attr("pcoded-header-position", 'fixed' );
					$('.pcoded-main-container').css('margin-top', $(".pcoded-header").outerHeight());
				}else {
					$('.pcoded-header').attr("pcoded-header-position", 'relative' );
					$('.pcoded-navbar').attr("pcoded-header-position", 'relative' );
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


});
