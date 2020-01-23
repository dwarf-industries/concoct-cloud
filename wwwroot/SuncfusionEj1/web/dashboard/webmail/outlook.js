var list,list1,data, readcountd=3,readcountc=3, readcounti=3, iread=[], dread=[],cread=[];
$(function () {
	var contact = [
"Nancy@syncfusion.com",
"Andrew@syncfusion.com",
"Janet@syncfusion.com",
"Margaret@syncfusion.com",
"Steven@syncfusion.com", "Robert@syncfusion.com", "Michael@syncfusion.com", "Laura@syncfusion.com"
];

 var listsource,dataList,listsource1,menusource = [];
 
$.validator.setDefaults({
                ignore: [],
				errorClass:'e-validation-error',
				errorPlacement: function (error, element) {
                    $(error).insertAfter(element.closest(".e-widget"));
                }
                // any other default options and/or rules
            });
   //listview,treeview and menu loaded with outlook information
  var dataManger1 = ej.DataManager({
      url: window["baseurl"] + "api/webmail/loaddata", crossDomain: true
    });
	
	 dataManger1.executeQuery(ej.Query()).done(function (e) {
		 $("#templatelist").ejListView({dataSource:e.result.OutlookItem});
			list = e.result.OutlookItem;
		 $("#templatelist1").ejListView({dataSource:e.result.OutlookItem1});
             list1 = e.result.OutlookItem1;
			
		
		 $("#treeView").ejTreeView({
		 fields: { dataSource: e.result.TreeviewDB.TreeData}
		 });
		 
		 $("#menujson").ejMenu({
			 fields: { dataSource: e.result.MenuDB.MenuData}});
	     });
	
	$("#searchAuto").ejAutocomplete({
		
		width:"100%",
		watermarkText:"Search Mail and People",
		dataSource:contact,
		popupHeight:"200px",
		filterType:"contains",
		open:"searchAutoOpen"
	});
	
	$("#treeView").ejTreeView({
		fields: { id: "ID", parentId: "PID", text: "Name", hasChild: "HasChild", expanded: "Expanded" },
		nodeClick:"nodeClick",
		template:"#treeTemplate",
	});
	
	$("#treeviewMenu").ejMenu({
		menuType:"contextmenu",
		contextMenuTarget:"#treeView",
		openOnClick:false,
	});
	
	$("#menujson").ejMenu({
		enableSeparator:false,
		width:"100%",
		fields: { id: "ID", parentId: "ParentId", text: "Text", spriteCssClass: "Sprite" },
		click:"menuClick",
	});
	
	$("#templatelist").ejListView({
		showHeader:true,
		headerTitle:"Today",
		renderTemplate:true,
		height:"20%",
		width:"100%",
		templateId: 'listTempData',
		mouseDown:"onMouseDown"
	});
	
	$("#templatelist1").ejListView({
		showHeader:true,
		headerTitle:"Yesterday",
		renderTemplate:true,
		width:"100%",
		templateId: 'listTempData',
		mouseDown:"onMouseDown"
	});
	
	 $("#listviewMenu").ejMenu({
		 menuType:"contextmenu",
		 contextMenuTarget:"#templatelist,#templatelist1",
		 openOnClick:false
	 });
	 
	$("#iconAccordion").ejAccordion({
		enableMultipleOpen:true,
		customIcon: {
                        header: "ej-icon-expander-down---01",
                        selectedHeader: "ej-icon-up-arrow---01"
                },
		events: "click",
		beforeInactivate:"onbeforeInActivate",
		inActivate:"onInActivate",		
		beforeActivate: "beforeActivate",	
        activate: "InActivate"
	});
	$("#toButton").ejButton({
		width:"55px"
	});
	
	$("#ccButton").ejButton({
		width:"55px"
	});
	
	$("#mailsubject").ejMaskEdit({
		inputMode:ej.InputMode.Text,
		watermarkText:"Enter subject here",
		width:"695px"
	});
	
	
	$("#autoTo").ejAutocomplete({
		dataSource:contact,
		filterType:"contains",
		width:"593px",
		popupWidth:"230px",
		popupHeight:"250px",
		multiSelectMode:ej.MultiSelectMode.VisualMode,
		 validationRules: {
             required: true
         },
         validationMessage: {
            required: " The message must have at least one recipient."
         }
	});
	
	$("#autoCc").ejAutocomplete({
		dataSource:contact,
		filterType:"contains",
		width:"593px",
		popupWidth:"230px",
		popupHeight:"250px",
		multiSelectMode:ej.MultiSelectMode.VisualMode		
	});
	
	$("#rteSample").ejRTE({
		width:"710px",
		height:"350px",
		toolsList:["formatStyle", "font", "style", "effects", "alignment", "lists", "indenting", "clipboard", "doAction", "clear", "casing", "print"]
	});
	
	$("#sendButton").ejButton({
		width:"65px",
		type: ej.ButtonType.Submit,
		click:"Click"
	});
	
	$("#textform").submit(function(e) {
       e.preventDefault();
    });

});


function searchAutoOpen(args)
{
	var obj=$("#searchAuto").data("ejAutocomplete");
	obj.suggestionList.attr("style","top:85px;width:216px");
}

 //list item click
   function onMouseDown(args) {
	   var selectnode,initialNode,initialNode1,data;
        selectnode = $("#treeView").data("ejTreeView").model.selectedNode;
        countnode = $(".e-treeview").find(".e-active").find("#count");
        initialNode = $($(".e-treeview").find("ul:not(ul:first)")[0]).find("li:first").find("#count");
        initialNode1 = $($(".e-treeview").find("ul:not(ul:first)")[1]).find("li:first").find("#count");
        data = $($(".e-treeview").find(".e-active").closest("ul").parent()).next().length !== 0 ? $($(".e-treeview").find(".e-active").closest("ul").parent()).next() : $($(".e-treeview").find(".e-active").closest("ul").parent()).prev();
        if (args.item.closest("#templatelist") && args.item.find(".subjectstyle").attr("style") != "undefined") {
            if (selectnode == -1 && (args.item.find(".subjectstyle").css("font-weight") == "bold"||args.item.find(".subjectstyle").css("font-weight") == "700")) {
                initialNode.text(initialNode.text() - 1);
                initialNode1.text(initialNode1.text() - 1);
                if (initialNode.text() == "0")
                    initialNode.text("");
                if (initialNode1.text() == "0")
                    initialNode1.text("");

            }
            if (countnode.length !== 0 && (args.item.find(".subjectstyle").css("font-weight") == "bold"||args.item.find(".subjectstyle").css("font-weight") == "700")) {
                for (var j = 0; j < data.find("li").length; j++) {

                    if ($(".e-treeview").find(".e-active").find(".treeitem").text() == $(data.find("li")[j]).find(".treeitem").text()) {
                        $(data.find("li")[j]).find("#count").text($(data.find("li")[j]).find("#count").text() - 1);
                        countnode.text(countnode.text() - 1);
                        if ($(data.find("li")[j]).find("#count").text() == "0")
                            $(data.find("li")[j]).find("#count").text("");
                        if (countnode.text() == "0")
                            countnode.text("");
                    }
                }
            }
        }
		if($("#ItemTitle").text()=="Drafts"){
		 readcountd=countnode.text();
		 dread.push(args.index);
		}
		if($("#ItemTitle").text()=="Inbox") {
			readcounti=countnode.text() || initialNode.text() ;
			iread.push(args.index);
		}
		if($("#ItemTitle").text()=="Clutter") {
			readcountc=countnode.text();
			cread.push(args.index);
		}
        $(".e-acrdn").css("display", "block");
        $(".paneltxt").css("display", "none");
		if($(args.item).find(".descriptionstyle").text() == " No preview is available ")
			$("#accContent").attr("style","display:none");
		else
		{
			$("#accCont").text($(args.item).find(".descriptionstyle").text());
			$("#accFrom").text($(args.item).find(".templatetext").text().trim().concat("."));
			$("#accTo").text($(args.item).find(".receiver").text().trim().concat(","));
		}
        $("#sub").text($(args.item).find(".subjectstyle").text());
        $("#date").text($(args.item).find(".designationstyle").text());
        $("#to").text($(args.item).find(".receiver").text());
        $(args.item).find(".designationstyle").css({ "font-weight": "normal", "font-family": "Segoe UI", color: "#333" });
        $(args.item).find(".subjectstyle").css({ "font-weight": "normal", "font-family": "Segoe UI", color: "#333" });
        $("#mailarea").addClass("hidden");
        $("#menujson li:nth-child(n+2)").css("display", "inline-block");
    }

 //compose menu item click
   function menuClick (args) {
        if (args.text == "New") {
			$("#autoTo").data("ejAutocomplete").clearText();
			$("#autoTo").data("ejAutocomplete").clearText();
			$("#mailsubject").ejMaskEdit({"value": "" });
			$("#rteSample").ejRTE({ "value": "" });
            $("#mailarea").removeClass("hidden");
            $(".paneltxt").attr("style", "display:none");
            $("#iconAccordion").attr("style", "display:none");
			$("#menujson li:nth-child(n+2)").css("display", "none");
        }
    };

//treenode click
    function nodeClick (args) {
        switch (args.currentElement.textContent) {
            case "Inbox":
                $("#templatelist").ejListView({ "dataSource": list });
                $("#templatelist1").ejListView({ "dataSource": list1 });
                $("#ItemTitle").text("Inbox");
				updateReadUnread(iread, readcounti, args);
                break;
            case "Clutter":
                $("#templatelist").ejListView({ "dataSource": list });
                $("#templatelist1").attr("style", "display:none");
                $("#ItemTitle").text("Clutter");
				updateReadUnread(cread, readcountc, args);
                break;
            case "Sent Items":
			    $("#templatelist1").ejListView({ "dataSource": list1 });
				$("#templatelist").attr("style", "display:none");
                $("#ItemTitle").text("Sent Items");
                break;
            case "Drafts":
                $("#templatelist").ejListView({ "dataSource": list });
                $("#templatelist1").attr("style", "display:none");
                $("#ItemTitle").text("Drafts");
				updateReadUnread(dread, readcountd, args);
                break;
        }
    }
	
	function updateReadUnread(read, readcount, args ){
		var listv=$("#templatelist").ejListView("instance");
		for(var i=0;i<read.length;i++)
			$(listv._liEl[read[i]]).addClass("markread");
        if(!ej.isNullOrUndefined(args.currentElement.nextSibling)) args.currentElement.nextSibling.textContent = readcount;
        setCount(args);
		 $("#iconAccordion").css("display", "none");
         $("#mailarea").addClass("hidden");
		 $("#menujson li:nth-child(n+2)").css("display", "none");
         $(".paneltxt").attr("style", "display:block");
    }
	
	function setCount(args) {
        var value = $(args.currentElement).closest("li").parent().parent().next().length !== 0 ? $(args.currentElement).closest("li").parent().parent().next().find("ul") : $(args.currentElement).closest("li").parent().parent().prev().find("ul");
        for (var i = 0; i < value.children().length; i++) {
            if (args.currentElement.textContent == $(value.children()[i]).find(".treeitem").text()) {
                $(value.children()[i]).find("#count").text((args.currentElement.textContent=="Drafts")?readcountd:(args.currentElement.textContent=="Inbox")?readcounti:readcountc);
            }
        }
    }

	//send button click
    function Click(args) {
		var val=$("#autoTo").ejAutocomplete("option", "value");
		var name =val.substr(0,val.indexOf('@'));
		var title = $("#mailsubject").ejMaskEdit("option", "value") == null ? "(No Subject)" : $("#mailsubject").ejMaskEdit("option", "value");
		var msg = $("#rteSample").ejRTE("option", "value") == "" ? "No preview is available" : $("#rteSample").ejRTE("option", "value");
		if(val == "")
			return false;
		else {
			var obj = $("#templatelist1").ejListView('instance');
        obj.addItem({ "ContactName": name, "ContactTitle": title, "Time":"9.00AM", "Message": msg ,"To" : "Krish Kael" },0);
	    list1=obj.model.dataSource;		
		$("#mailarea").addClass("hidden");
		$(".paneltxt").attr("style", "display:block");
		}
    }
	
	// function onInActivate(args) {
		// $("#accContent").attr("style","display:none");
	// }
	
	// function beforeActivate(args) {
		// $("#accContent").attr("style","display:none");
	// }
	// function onbeforeInActivate(args) {
		// $("#accContent").attr("style","display:none");
	// }
	// function InActivate(args) {
		// $("#accContent").attr("style","display:none");
	// }
	// function AccClick(args) {
		// $("#accContent").attr("style","display:none");
	// }
	
	
