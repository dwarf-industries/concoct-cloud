
$( document ).ready(function() {

	$( "#pcoded" ).pcodedmenu({
		themelayout: 'horizontal', 
		horizontalMenuplacement: 'top',
		horizontalBrandItem: true,
		horizontalLeftNavItem: true,
		horizontalRightItem: true,
		horizontalSearchItem: true,
		horizontalBrandItemAlign: 'left',
		horizontalLeftNavItemAlign: 'left',
		horizontalRightItemAlign: 'right',
		horizontalsearchItemAlign: 'right',
		horizontalMobileMenu: true,
		MenuTrigger: 'click',
		SubMenuTrigger: 'click',
		activeMenuClass: 'active',
		ThemeBackgroundPattern: 'pattern7',
		HeaderBackground: 'theme4' ,
		LHeaderBackground :'theme4',
		NavbarBackground: 'theme8',
		ActiveItemBackground: 'theme0',
		SubItemBackground: 'theme2',
		ActiveItemStyle: 'style1',
		ItemBorder: true,
		ItemBorderStyle: 'solid',
		SubItemBorder: true,
		DropDownIconStyle: 'style1',
		FixedNavbarPosition: false,
		FixedHeaderPosition: false,
		horizontalNavIsCentered: false,
		horizontalstickynavigation: false,
		horizontalNavigationMenuIcon: true,
	});

 /* Navbar Theme Change function Start */
	function handlenavbartheme() {
		$('.theme-color > a.navbar-theme').on("click", function() {
			var navbartheme = $(this).attr("navbar-theme");
			$('.pcoded-navbar').attr("navbar-theme", navbartheme);
        });
    };

	handlenavbartheme();
 /* Navbar Theme Change function Close */

 /* Navbar Theme Change function Start */
	function handleActiveItemTheme() {
		$('.theme-color > a.active-item-theme').on("click", function() {
			var AtciveItemTheme = $(this).attr("active-item-theme");
			$('.pcoded-navbar').attr("active-item-theme", AtciveItemTheme);
        });
    };

	handleActiveItemTheme();
 /* Navbar Theme Change function Close */ 
 

 /* Theme background pattren Change function Start */
	function handlethemebgpattern() {
		$('.theme-color > a.themebg-pattern').on("click", function() {
			var themebgpattern = $(this).attr("themebg-pattern");
			$('body').attr("themebg-pattern", themebgpattern);
        });
    };

	handlethemebgpattern();
 /* Theme background pattren Change function Close */
  
 /* Theme Layout Change function start*/
	function handlethemehorizontallayout() {
		$('#theme-layout').val('wide').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded').attr('horizontal-layout', get_value);
		});
	};

   handlethemehorizontallayout ();
 /* Theme Layout Change function Close*/
 
 /*Menu Placement change function start*/
   function handleMenuPlacement() {
		$('#navbar-placement').val('top').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded').attr('horizontal-placement', get_value); 
		});
	};

   handleMenuPlacement ();
 /*Menu Placement change function Close*/
 
 
 
 /*Item border change function Start*/
	function handleIItemBorder() {
			$('#item-border').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-navbar .pcoded-item').attr('item-border', 'false');
				}else {
					$('.pcoded-navbar .pcoded-item').attr('item-border', 'true');
				}
			});
		};

   handleIItemBorder ();
 /*Item border change function Close*/
 
 
 /*SubItem border change function Start*/
   function handleSubIItemBorder() {
			$('#subitem-border').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-navbar .pcoded-item').attr('subitem-border', 'false');
				}else {
					$('.pcoded-navbar .pcoded-item').attr('subitem-border', 'true');
				}
			});
		};

   handleSubIItemBorder ();
 /*SubItem border change function Close*/
 
 
 /*Item border Style change function Start*/
   function handlBoderStyle() {
		$('#border-style').val('solid').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded-navbar .pcoded-item').attr('item-border-style', get_value);
		});
	};

   handlBoderStyle ();
 /*Item border Style change function Close*/
 
 
 
 
 /*Dropdown Icon change function Start*/
      function handleDropDownIconStyle() {
		$('#dropdown-icon').val('style1').on('change', function (get_value) {
			get_value = $(this).val();
			$('.pcoded-navbar .pcoded-hasmenu').attr('dropdown-icon', get_value);
		});
	};

   handleDropDownIconStyle ();
 /*Dropdown Icon change function Close*/
 
 
 
 
 
 /* Horizontal Navbar Position change function Start*/
	function handleNavigationPosition() {
			$('#sidebar-position').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded-navbar').attr("pcoded-navbar-position", 'fixed' ); 
				}else {
					$('.pcoded-navbar').attr("pcoded-navbar-position", 'relative' ); 
				}
			});
		};

   handleNavigationPosition ();
   
 /* Horizontal Navbar Position change function Close*/
 /* Hide Show Menu Icon */
 	function handleNavigationMenuIcon() {
			$('#menu-icons').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded .pcoded-navbar .pcoded-item > li > a .pcoded-micon:not(".pcoded-search-item .pcoded-micon")').hide();
				}else {
					$('.pcoded .pcoded-navbar .pcoded-item > li > a .pcoded-micon:not(".pcoded-search-item .pcoded-micon")').show();
				}
			});
		};

	handleNavigationMenuIcon ();
   /* Hide Show Brand logo */
    function handlepcodedBrandVisibility() {
			$('#brand-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded .pcoded-navbar .pcoded-brand').hide();
				}else {
					$('.pcoded .pcoded-navbar .pcoded-brand').show();
				}
			});
		};

	handlepcodedBrandVisibility (); 
	function handlePcodedLeftItemVisibility() {
			$('#leftitem-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded .pcoded-navbar .pcoded-item.pcoded-left-item').hide();
				}else {
					$('.pcoded .pcoded-navbar .pcoded-item.pcoded-left-item').show();
				}
			});
		}; 
	handlePcodedLeftItemVisibility ();
	function handlePcodedRightItemVisibility() {
			$('#rightitem-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded .pcoded-navbar .pcoded-item.pcoded-right-item').hide();
				}else {
					$('.pcoded .pcoded-navbar .pcoded-item.pcoded-right-item').show();
				}
			});
		}; 
	handlePcodedRightItemVisibility ();
	function handlePcodedSearchItemVisibility() {
			$('#searchitem-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.pcoded .pcoded-navbar .pcoded-item.pcoded-search-item').hide();
				}else {
					$('.pcoded .pcoded-navbar .pcoded-item.pcoded-search-item').show();
				}
			});
		}; 
	handlePcodedSearchItemVisibility ();
	
	function handleBrandItemAlign() {
		$('#branditem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.pcoded-navbar .pcoded-brand').removeClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-brand').addClass('pcoded-left-align');
			}else{
				$('.pcoded-navbar .pcoded-brand').addClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-brand').removeClass('pcoded-left-align');
			}
		});
	};

   handleBrandItemAlign ();
   function handleLeftItemAlign() {
		$('#leftitem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.pcoded-navbar .pcoded-left-item').removeClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-left-item').addClass('pcoded-left-align');
			}else{
				$('.pcoded-navbar .pcoded-left-item').addClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-left-item').removeClass('pcoded-left-align');
			}
		});
	};

   handleLeftItemAlign ();
   function handleRightItemAlign() {
		$('#rightitem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.pcoded-navbar .pcoded-right-item').removeClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-right-item').addClass('pcoded-left-align');
			}else{
				$('.pcoded-navbar .pcoded-right-item').addClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-right-item').removeClass('pcoded-left-align');
			}
		});
	};

   handleRightItemAlign ();
   function handleSearchItemAlign() {
		$('#searchitem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.pcoded-navbar .pcoded-search-item').removeClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-search-item').addClass('pcoded-left-align');
			}else{
				$('.pcoded-navbar .pcoded-search-item').addClass('pcoded-right-align');
				$('.pcoded-navbar .pcoded-search-item').removeClass('pcoded-left-align');
			}
		});
	};

   handleSearchItemAlign ();
});
