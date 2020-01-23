
$(function () {
    /*
        When initial rendering, diagram builder content wont be aligned properly. 
        So display style will be set to none in default.html. It will be enabled on page loading.
    */
    var div = document.getElementsByClassName("sample-header")[0];
    div.style.display = "";
    div = document.getElementsByClassName("sample-main")[0];
    div.style.display = "";
    div = document.getElementsByClassName("diagram-dialog")[0];
    div.style.display = "";

    /* 
        Rendering symbol palette which is used add the nodes and connectors in the diagram at run time through drag and drop.
    */
    $("#symbolpalette").ejSymbolPalette({
        diagramId: "DiagramContent",
        palettes: window.diagramPaletteCollection,
        height: "100%", width: "100%",
        paletteItemWidth: 45, paletteItemHeight: 45,
        showPaletteItemText: false,
        previewWidth: 100, previewHeight: 100,
    });

    /*
        Rendering the below symbol palette which is used to show the filtered shapes.
    */
    $("#searchPalette").ejSymbolPalette({
        diagramId: "DiagramContent",
        palettes: [
            { name: "filteredShapes" }
        ],
        height: "100%", width: "255px",
        paletteItemWidth: 45, paletteItemHeight: 45,
        showPaletteItemText: false,
        previewWidth: 100, previewHeight: 100,
    });


    var nodes = [
        {
            name: "manufacturing", width: 175, height: 60, offsetX: 400, offsetY: 60, fillColor: "#05ADA4",
            labels: [{ "text": "Manufacturing Tablet PC" }], type: "basic", shape: "path",
            pathData: "M 269.711,29.3333C 269.711,44.061 257.772,56 243.044,56L 158.058,56C 143.33,56 131.391,44.061 131.391,29.3333L 131.391,29.3333C 131.391,14.6057 143.33,2.66669 158.058,2.66669L 243.044,2.66669C 257.772,2.66669 269.711,14.6057 269.711,29.3333 Z"
        },
        {
            name: "simProjection", width: 100, height: 80, offsetX: 400, offsetY: 175, fillColor: "#83A93F",
            labels: [{ "text": "Dual Sim \n Projection?" }], type: "basic", shape: "path",
            pathData: "M 253.005,115.687L 200.567,146.071L 148.097,115.687L 200.534,85.304L 253.005,115.687 Z"
        },
        {
            name: "manufacturingProcess", width: 100, height: 80, offsetX: 400, offsetY: 300, fillColor: "#33AACA",
            labels: [{ "text": "Processor \n Installation " }]
        },
        {
            name: "dualSim", width: 120, height: 60, offsetX: 600, offsetY: 175, fillColor: "#F1605A",
            labels: [{ "text": "Single Sim \n Projection \n Added" }], type: "basic", shape: "rectangle"
        },
        {
            name: "touch", width: 130, height: 120, offsetX: 400, offsetY: 445, fillColor: "#83A93F",
            labels: [{ "text": "Capacitive\n Touch?" }], type: "basic", shape: "path", pathData: "M 253.473,337.392L 200.551,390.313L 147.629,337.392L 200.551,284.47L 253.473,337.392 Z"
        },
        {
            name: "capacitiveTouch", width: 120, height: 60, offsetX: 200, offsetY: 445, fillColor: "#33AACA",
            labels: [{ "text": "Capacitive Touch \n Functionalities" }], type: "basic", shape: "rectangle"
        },
        {
            name: "resistiveTouch", width: 120, height: 60, offsetX: 600, offsetY: 445, fillColor: "#F1605A",
            labels: [{ "text": "Resistive Touch \n Functionalities" }], type: "basic", shape: "rectangle"
        },
        {
            name: "standards", width: 100, height: 80, offsetX: 400, offsetY: 575, fillColor: "#33AACA",
            labels: [{ "text": "Maintaining \n Standards" }], type: "basic", shape: "rectangle"
        },
        {
            name: "price", width: 100, height: 80, offsetX: 400, offsetY: 720, fillColor: "#33AACA",
            labels: [{ "text": "Fixing Price" }], type: "basic", shape: "rectangle"
        },
        {
            name: "selling", width: 135, height: 60, offsetX: 400, offsetY: 820, fillColor: "#05ADA4",
            labels: [{ "text": "Selling Process" }], type: "basic", shape: "path",
            pathData: "M 269.711,29.3333C 269.711,44.061 257.772,56 243.044,56L 158.058,56C 143.33,56 131.391,44.061 131.391,29.3333L 131.391,29.3333C 131.391,14.6057 143.33,2.66669 158.058,2.66669L 243.044,2.66669C 257.772,2.66669 269.711,14.6057 269.711,29.3333 Z"
        },
    ];


    var connectors = [
        { name: "connector1", sourceNode: "manufacturing", targetNode: "simProjection", sourcePort: "dport", targetPort: "bport" },
        {
            name: "connector2", sourceNode: "simProjection", targetNode: "manufacturingProcess", sourcePort: "dport", targetPort: "bport",
            labels: [{ "text": "Yes", "fontColor": "black", "fillColor": "white" }]
        },
        {
            name: "connector3", sourceNode: "simProjection", targetNode: "dualSim", sourcePort: "cport", targetPort: "aport",
            labels: [{ "text": "No", "fontColor": "black", "fillColor": "white" }]
        },
        { name: "connector4", sourceNode: "dualSim", targetNode: "manufacturingProcess", sourcePort: "dport", targetPort: "cport" },
        { name: "connector5", sourceNode: "manufacturingProcess", targetNode: "touch", sourcePort: "dport", targetPort: "bport" },
        {
            name: "connector6", sourceNode: "touch", targetNode: "capacitiveTouch", sourcePort: "aport", targetPort: "cport",
            labels: [{ "text": "Yes", "fontColor": "black", "fillColor": "white" }]
        },
        {
            name: "connector7", sourceNode: "touch", targetNode: "resistiveTouch", sourcePort: "cport", targetPort: "aport",
            labels: [{ "text": "No", "fontColor": "black", "fillColor": "white" }]
        },
        { name: "connector8", sourceNode: "capacitiveTouch", targetNode: "standards", sourcePort: "dport", targetPort: "aport" },
        { name: "connector9", sourceNode: "resistiveTouch", targetNode: "standards", sourcePort: "dport", targetPort: "cport" },
        { name: "connector10", sourceNode: "standards", targetNode: "price", sourcePort: "dport", targetPort: "bport" },
        { name: "connector11", sourceNode: "price", targetNode: "selling", sourcePort: "dport", targetPort: "bport" },
    ];

    /* 
        Rendering the diagram control to represent the simple flow diagram.
    */
    var gridline = {
        "snapInterval": [10],
        "linesInterval": [.95, 9.05, 0.2, 9.75]
    };

    $("#DiagramContent").ejDiagram({
        connectors: connectors,
        nodes: nodes,
        //default settings for nodes and connectors
        defaultSettings: {
            connector: {
                segments: [{ "type": "orthogonal" }], labels: [{ "fontColor": "black", "fillColor": "white", }],
                lineWidth: 2, targetDecorator: { shape: "arrow", width: "10", height: "10" },
                cornerRadius: 8
            },
            node: {
                borderColor: "#000000", labels: [{ "fontColor": "white" }],
                ports: [
                    { offset: { x: 0, y: 0.5 }, name: "aport" }, { offset: { x: 0.5, y: 0 }, name: "bport" },
                    { offset: { x: 1, y: 0.5 }, name: "cport" }, { offset: { x: 0.5, y: 1 }, name: "dport" }
                ]
            },
        },
        width: "100%",
        height: "100%",
        selectedItems: {
            //define the user handles to add some frequently used commands around the selector.
            userHandles: createUserHandles()
        },
        contextMenu: {
            /* 
                By default, diagram will be provide built-in context menu items.
                The context menu items which are defined below is custom context menu items 
                and its used to customize the bpmn shapes at runtime.
            */
            items: [
                {
                    name: "eventType", text: "Event Type", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("interruptingStart", "Interrupting Start"),
                        createMenuItem("nonInterruptingStart", "NonInterrupting Start"),
                        createMenuItem("interruptingIntermediate", "Interrupting Intermediate"),
                        createMenuItem("nonInterruptingIntermediate", "NonInterrupting Intermediate"),
                        createMenuItem("throwingIntermediate", "Throwing Intermediate"),
                        createMenuItem("endEvent", "End")
                    ]
                },
                {
                    name: "triggerResult", text: "Trigger Result", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("noTrigger", "None"),
                        createMenuItem("messageTrigger", "Message"),
                        createMenuItem("timerTrigger", "Timer"),
                        createMenuItem("errorTrigger", "Error"),
                        createMenuItem("escalationTrigger", "Escalation"),
                        createMenuItem("cancelTrigger", "Cancel"),
                        createMenuItem("compensationTrigger", "Compensation"),
                        createMenuItem("conditionalTrigger", "Conditional"),
                        createMenuItem("linkTrigger", "Link"),
                        createMenuItem("signalTrigger", "Signal"),
                        createMenuItem("terminateTrigger", "Terminate"),
                        createMenuItem("multipleTrigger", "Multiple"),
                        createMenuItem("parallelTrigger", "Parallel")

                    ]
                },
                {
                    name: "gateway", text: "Gateway", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("noGateway", "None"),
                        createMenuItem("exclusiveGateway", "Exclusive"),
                        createMenuItem("parallelGateway", "Parallel"),
                        createMenuItem("inclusiveGateway", "Inclusive"),
                        createMenuItem("complexGateway", "Complex"),
                        createMenuItem("eventBasedGateway", "Event Based"),
                        createMenuItem("exclusiveeventbased", "Exclusive Event Based"),
                        createMenuItem("paralleleventbased", "Parallel Event Based")
                    ]
                },
                {
                    name: "dataObject", text: "Data Object", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("noDataObject", "None"),
                        createMenuItem("input", "Input"),
                        createMenuItem("output", "Output")
                    ]
                },
                {
                    name: "collection", text: "Collection", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("noDataObject", "None"),
                        createMenuItem("collection1", "Collection")
                    ]
                },
                {
                    name: "loop", text: "Loop", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("none", "None"),
                        createMenuItem("standardLoop", "Standard"),
                        createMenuItem("parallelMultiInstanceLoop", "Parallel Multi-Instance"),
                        createMenuItem("sequenceMultiInstanceLoop", "Sequence Multi-Instance")
                    ]
                },
                {
                    name: "taskType", text: "Task Type", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("none", "None"),
                        createMenuItem("serviceTask", "Service"),
                        createMenuItem("receiveTask", "Receive"),
                        createMenuItem("sendTask", "Send"),
                        createMenuItem("instantiatingReceiveTask", "Instantiating Receive"),
                        createMenuItem("manualTask", "Manual"),
                        createMenuItem("businessRuleTask", "Business Rule"),
                        createMenuItem("userTask", "User"),
                        createMenuItem("scriptTask", "Script")
                    ]
                },
                {
                    name: "subProcessType", text: "Subprocess Type", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("none", "Default"),
                        createMenuItem("event", "Event"),
                        createMenuItem("transaction", "Transaction")
                    ]
                },
                {
                    name: "adhoc", text: "Ad-Hoc", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("none", "Nones"),
                        createMenuItem("adhoc1", "Ad-Hoc")
                    ]
                },
                {
                    name: "compensation", text: "Compensation", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("none", "None"),
                        createMenuItem("compensation1", "Compensation")
                    ]
                },
                {
                    name: "activityType", text: "Activity Type", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("task", "Task"),
                        createMenuItem("collapsedSubProcess", "Collapsed Sub-Process")
                    ]
                },
                {
                    name: "taskCall", text: "Call", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("none", "None"),
                        createMenuItem("call", "Call")
                    ]
                },
                {
                    name: "boundary", text: "Boundary", cssClass: "cm-no-image",
                    subItems: [
                        createMenuItem("defaultBoundary", "Default"),
                        createMenuItem("callBoundary", "Call"),
                        createMenuItem("eventBoundary", "Event")
                    ]
                }
            ]
        },
        pageSettings: {
            pageHeight: 1100, pageWidth: 1200,
            showPageBreak: false, multiplePage: true,
            scrollLimit: ej.datavisualization.Diagram.ScrollLimit.Infinity
        },
        snapSettings: {
            horizontalGridLines: gridline,
            verticalGridLines: gridline,
            snapConstraints: ej.datavisualization.Diagram.SnapConstraints.All & ~ej.datavisualization.Diagram.SnapConstraints.SnapToLines
        },
        //registering the client Side events to customize the diagram nodes and connectors at run time.
        contextMenuBeforeOpen: contextMenuBeforeOpen,
        contextMenuClick: contextMenuClick,
        drop: drop,
        create: create,
        dragEnter: dragEnter,
        selectionChange: selectionChange,
        nodeCollectionChange: nodeCollectionChange,
        textChange: textChange,
        drag: drag,
        sizeChange: nodeSizeChanging,
        connectorCollectionChange: connectorCollectionChange
    });

});

// The method is used to create the custom context menu items.
function createMenuItem(name, text) {
    var menuItem = {};
    menuItem.text = text;
    menuItem.name = name;
    menuItem.imageUrl = "images/" + name + ".png";
    menuItem.cssClass = "cm-image-size";
    return menuItem;
}



function create(args) {
    /* 
        Rendering the below toolbar and menu control which is used to execute the diagram client side API methods 
        such as undo, redo, save, load, cut, copy, paste etc.
    */
    $("#toolbarEvents").ejToolbar({
        orientation: "horizontal",
        click: "toolbarClick"
    });

    $("#DiagramMenuControl").ejMenu({
        width: 612, click: "menuBarClick",
        openOnClick: false
    });

    /*
        The dropdownlist is used to change the diagram zoom level through diagram client side API method named as "ZoomTo".
    */

    var zoomPercentageList = [
       { text: "400%" }, { text: "200%" }, { text: "150%" }, { text: "100%" },
       { text: "75%" }, { text: "50%" }, { text: "25%" }
    ];

    $('#zoompercentage').ejDropDownList({
        dataSource: zoomPercentageList,
        width: "75px",
        height: "25px",
        selectedItems: [3],
        change: "zoomPercentageChange",
        popupHeight: "203px",
        /*
            defining the cssClass for customization. You can refer this in diagrambuilder.css file. 
            We have also used some other cssClass "ddl-custom" and "ddl-color-palette" in the below scope for other dropdown controls.
        */
        cssClass: "ddl-standard"
    });

    /*
        The button is used to save the diagram data into local storage.
    */
    $('#save_icon').ejButton({
        showRoundedCorner: true,
        size: ej.ButtonSize.Mini,
        text: "Save",
        click: saveDiagram,
    });

    /*
        Getting the diagram instance globally to access it anywhere across the sample.
    */
    diagram = $("#DiagramContent").ejDiagram("instance");

    /*
        The below object is used for knockout binding. When we do any changes through property panel,
        the below object will be updated and it will provide a visual representaion of the selected object properties.
        You can refer the SelectorVMClass in which we have defined what are the nodes and connector properties are visualized.
    */
    diagram._selectedObject = new SelectorVMClass();

    /*
        The below control is used to change the diagram's page width, page height and pageBackground color at run time.
    */
    $("#artBoardWidth").ejNumericTextbox({
        name: "artBoardWidth", value: diagram.model.pageSettings.pageWidth,
        width: "75px", focusOut: "setDimension"
    });
    $("#artBoardHeight").ejNumericTextbox({
        name: "artBoardHeight", value: diagram.model.pageSettings.pageHeight,
        width: "75px", focusOut: "setDimension"
    });
    /*
       The below control change the connector's corner radius at runtime.
   */
    $("#cornerradius").ejNumericTextbox({
        name: "cornerradius", value: 8,
        cssClass: "cornerRadiusDiv",
        minValue: 0,
        maxValue: 100,
        width: "75px", change: "propertyPannelChange"
    });


    var colorList = [
        { color1: "#FFFFFF", color2: "#F9D9D9", color3: "#FFE6CC", color4: "#FFFF99", color5: "#CCFFCC", color6: "#CCFFFF", color7: "#D4D4F9", color8: "#E5CCFF" },
        { color1: "#FFCCFF", color2: "#FFCCE6", color3: "#E5E5E5", color4: "#FF9999", color5: "#FCC088", color6: "#FFFF33", color7: "#99FF33", color8: "#66FFFF" },
        { color1: "#7E7EFC", color2: "#CC99FF", color3: "#FF99FF", color4: "#FF66B3", color5: "#262626", color6: "#FF0000", color7: "#FF8000", color8: "#CCCC00" },
        { color1: "#66CC00", color2: "#00CCCC", color3: "#3333FF", color4: "#7F00FF", color5: "#FF00FF", color6: "#FF0080", color7: "#0C0C0C", color8: "#CC0000" },
        { color1: "#994C00", color2: "#999900", color3: "#006600", color4: "#009999", color5: "#000066", color6: "#4C0099", color7: "#990099", color8: "#99004D" }
    ];

    $('#pageBackgroundColor').ejDropDownList({
        width: "340px",
        dataSource: colorList,
        popupHeight: "200px",
        /* define the format which is used to display the color palette apperance in popup list.*/
        template: '<div class="ddl-color-list" style="background-color: ${color1};"></div>'
            + '<div class="ddl-color-list" style="background-color: ${color2};"></div>'
        + '<div class="ddl-color-list" style="background-color: ${color3};"></div>'
        + '<div class="ddl-color-list" style="background-color: ${color4};"></div>'
        + '<div class="ddl-color-list" style="background-color: ${color5};"></div>'
        + '<div class="ddl-color-list" style="background-color: ${color6};"></div>'
        + '<div class="ddl-color-list" style="background-color: ${color7};"></div>'
        + '<div class="ddl-color-list" style="background-color: ${color8};"></div>',
        cssClass: "ddl-color-palette ddl-custom"
    });

    /*
      Rendering the overview control allows you to see a preview or an overall view of the entire content of a Diagram.
      It also allows to navigate, pan, or zoom, on a particular position of the page.
    */
    $("#Overview").ejOverview({
        height: 158,
        width: 254
    });

    /*
      The slider control is used to change the diagram zoom level through diagram client side API method named as "ZoomTo".
    */
    $("#overviewSlider").ejSlider({
        value: 100,
        change: "OnChange",
        slide: "OnChange",
        height: 8,
        width: 200,
        minValue: 100,
        maxValue: 3000,
    });

    //#region Dialog Control

    /*
        The below set of dialogs which is used to save and load the diagram from local stroage.
        Also helps to export the diagram into image and SVG format.
    */
    $("#openFileDialog").ejDialog({
        enableModal: true,
        resizable: false,
        width: 350,
        height: 400,
        close: "onDialogClose", showOnInit: false,
    });

    $("#saveDialog").ejDialog({
        width: 451,
        close: "onDialogClose",
        showOnInit: false,
        enableModal: true,
    });

    $("#exportDialog").ejDialog({
        width: 400,
        close: "onDialogClose",
        showOnInit: false,
        enableModal: true,
    });

    $("#confirmDialog").ejDialog({
        width: 451,
        close: "onDialogClose",
        showOnInit: false,
        enableModal: true,
    });

    // #endregion

    /*
       The below dropdown list control allows to choose which format diagram will be exported such as PNG, JPEG, SVG.
   */
    $('#ddlExportFormat').ejDropDownList({
        targetID: "formatDiv",
        width: "200px",
        height: "30px",
        popupHeight: "116px",
        selectedItems: [0],
        cssClass: "ddl-standard"
    });

    /*
       The below dropdown list control allows to export the diagram content with any specific region.
   */
    $('#ddlExportMode').ejDropDownList({
        targetID: "modeDiv",
        width: "200px",
        height: "30px",
        popupHeight: "59px",
        selectedItems: [0],
        cssClass: "ddl-standard"
    });

    //#region Customize Node's styles

    /*
      The node's appearance or dimension can be changed at runtime by using "updateNode" client side API method.
      We have utilized our synfusion control to expose these feature in our diagram builder sample.
    */

    /* 
        The checkBox control is used to enable/disable the aspect ratio support for node.
    */
    $('#chkAspectRatio').ejCheckBox({
        size: "small",
        change: function (args) {
            PropertyChangesFromPanel(args, "aspectRatio")
        }
    });

    /* 
        The below set of dropdownlist control is used to change the fillColor, borderColor and borderWidth of the node.
    */

    var colorList = [
        { color1: "#FFFFFF", color2: "#F9D9D9", color3: "#FFE6CC" },
        { color1: "#FFFF99", color2: "#CCFFCC", color3: "#CCFFFF" },
        { color1: "#D4D4F9", color2: "#E5CCFF", color3: "#FFCCFF" },
        { color1: "#FFCCE6", color2: "#E5E5E5", color3: "#FF9999" },
        { color1: "#FCC088", color2: "#FFFF33", color3: "#99FF33" },
        { color1: "#66FFFF", color2: "#7E7EFC", color3: "#CC99FF" },
        { color1: "#FF99FF", color2: "#FF8000", color3: "#CCCC00" },
        { color1: "#FF66B3", color2: "#262626", color3: "#FF0000" },
        { color1: "#66CC00", color2: "#00CCCC", color3: "#3333FF" },
        { color1: "#7F00FF", color2: "#FF00FF", color3: "#FF0080" }
        //{ color1: "#0C0C0C", color2: "#CC0000", color3: "#99004D" }
    ];

    /* here we have define the template format for all color palette related dropdown controls used in the property panel. */
    var template = '<div class="ddl-color-list" style="background-color: ${color1};"></div>'
            + '<div class="ddl-color-list" style="background-color: ${color2};"></div>'
        + '<div class="ddl-color-list" style="background-color: ${color3};"></div>';

    $('#fillColor').ejDropDownList({
        dataSource: colorList,
        popupHeight: "420px",
        popupShown: "popupShown",
        popupHide: "popupHide",
        template: template,
        cssClass: "ddl-color-palette ddl-custom"
    });

    $('#borderColor').ejDropDownList({
        dataSource: colorList,
        popupHeight: "420px",
        popupShown: "popupShown",
        popupHide: "popupHide",
        template: template,
        cssClass: "ddl-color-palette ddl-custom"
    });

    var borderWidthList = [
        { text: "1 px", height: "1px" },
        { text: "2 px", height: "2px" },
        { text: "3 px", height: "3px" },
        { text: "4 px", height: "4px" },
        { text: "5 px", height: "5px" },
        { text: "6 px", height: "6px" },
        { text: "7 px", height: "7px" },
        { text: "8 px", height: "8px" },
    ];

    /* here we have define the template format for border-width and line-width related dropdown controls used in the property panel. */
    var borderTemplate = '<div style="margin-top: 3px; height: 25px">' +
     '<label style="font-weight: bold">${text}</label>' +
     '<div style="background-color: black; width: 45px; height: ${height};' +
            'float: right; margin-top: 11px; margin-left: 20px; margin-right: 5px"></div>' +
     '</div>';

    $('#borderWidth').ejDropDownList({
        dataSource: borderWidthList,
        popupHeight: "320px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        template: borderTemplate,
        cssClass: "borderWidthDiv ddl-custom",
    });

    /* 
       The below numeric text box controls are used to change the dimension of the node.
   */
    $("#nodeWidth").ejNumericTextbox({
        cssClass: "widthDiv",
        value: 35,
        change: "propertyPannelChange"
    });

    $("#nodeHeight").ejNumericTextbox({
        cssClass: "heightDiv",
        value: 35,
        change: "propertyPannelChange"
    });

    $("#nodeOffsetX").ejNumericTextbox({
        cssClass: "offsetXDiv",
        value: 35,
        change: "propertyPannelChange"
    });

    $("#nodeOffsetY").ejNumericTextbox({
        cssClass: "offsetYDiv",
        value: 35,
        change: "propertyPannelChange"
    });

    //The slider control is used to change the ppacity of node.
    $("#opacity").ejSlider({
        sliderType: ej.SliderType.MinRange,
        minValue: 0,
        maxValue: 100,
        incrementStep: 1,
        slide: function (args) {
            PropertyChangesFromPanel(args, "opacity");
        },
        change: function (args) {
            if (diagram && diagram._selectedObject.opacity != args.value) {
                PropertyChangesFromPanel(args, "opacity");
            }
        }
    });

    //#endregion

    //#region Customize Connector's Styles

    /*
        The connector's appearance can be changed at runtime by using "updateConnector" client side API method.
        We have utilized our synfusion control to expose these feature in our diagram builder sample.
    */

    /* 
       The below set of dropdownlist control is used to change the lineColor, lineWidth, lineType and lineDashArray of the connector.
       Also it helps to change connector's decorator shapes.
   */

    $('#lineColor').ejDropDownList({
        dataSource: colorList,
        popupHeight: "420px",
        popupShown: "popupShown",
        popupHide: "popupHide",
        template: template,
        cssClass: "ddl-color-palette ddl-custom"

    });

    $('#lineStyle').ejDropDownList({
        targetID: "lineStyleDiv",
        width: "125px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        cssClass: "ddl-custom"
    });


    $('#lineType').ejDropDownList({
        targetID: "lineTypeDiv",
        width: "125px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        cssClass: "ddl-custom"
    });



    $('#lineWidth').ejDropDownList({
        dataSource: borderWidthList,
        cssClass: "lineWidthDiv ddl-custom",
        popupHeight: "320px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        template: borderTemplate,
    });

    var open = false;
    $('#headDecorator').ejDropDownList({
        targetID: "headDecoratorDiv",
        width: "60px",
        popupHeight: "350px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        cssClass: "ddl-custom"
    });

    $('#tailDecorator').ejDropDownList({
        targetID: "tailDecoratorDiv",
        width: "60px",
        popupHeight: "350px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        cssClass: "ddl-custom"
    });

    //#endregion

    //#region Customize Label's Style.

    /*
       The label's appearance can be changed at runtime by using "updateLabel" client side API method.
       We have utilized our synfusion control to expose these feature in our diagram builder sample.
    */

    /* 
       The below set of dropdownlist control is used to change the fontFamily, fontSize and fontColor of the label.
   */
    var fontStylepopUpOpened = false;

    var fontFamilyList = [
        { text: "Arial" }, { text: "Aharoni" }, { text: "Bell MT" }, { text: "Fantasy" },
        { text: "Times New Roman" }, { text: "Segoe UI" }, { text: "Verdana" }
    ];
    $('#fontStyle').ejDropDownList({
        dataSource: fontFamilyList,
        popupHeight: "300px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        cssClass: "fontStyleDiv ddl-standard",
    });

    var fontSizepopUpOpened = false;
    var fontSizeList = [];
    for (var i = 8; i <= 20; i++) {
        fontSizeList.push({ text: i });
    }

    $('#fontSize').ejDropDownList({
        dataSource: fontSizeList,
        popupHeight: "280px",
        select: "propertyPannelChange",
        popupShown: "popupShown",
        popupHide: "popupHide",
        cssClass: "fontSizeDiv ddl-standard",
        width: "55px",
    });

    $('#fontColor').ejDropDownList({
        dataSource: colorList,
        popupHeight: "420px",
        popupShown: "popupShown",
        popupHide: "popupHide",
        template: template,
        cssClass: "ddl-color-palette ddl-custom"
    });

    //#endregion

    /*
        We have used lot of syncfusion controls to customize the node, connector and label appearance.
        The tab control is used to split it based on the object.
    */
    $("#tabContent").ejTab({
        itemActive: "tabItemChanged",
    });

    //#endregion
    //#region - Diagram Control Updation Based on Diagram Size
    diagram._modified = false;
    updateSize();
    setToolTip(true);
    setDiagramSize();

    /*
        The below event will be triggered when mouse hover and mouse leave on the toolbar item.
        Since we have customized the parent element appearance in the "toolbarMouseEvent" function,
        We couldnt achieve this requirement via cssClass.
    */
    $(".tb-item-child").mouseover(function (evt) {
        toolbarMouseEvent(evt);
    });

    $(".tb-item-child").mouseout(function (evt) {
        toolbarMouseEvent(evt);
    });

    /*
        We have provided the enable/disable option for Overview, SnapToGrid, SmartGrid and Grid lines.
        When mouse hover on those menu item, we will highlight the image icon via cssClass. 
        you can refer this cssClass (iconhover) definition in diagrambuilder.css file.
    */
    $(".menu-item-anchor").mouseover(function (evt) {
        var target = evt.target;
        if (target.tagName.toLowerCase() === "span")
            target = evt.target.parentNode;
        $("#" + target.parentNode.id).find("span").addClass("iconhover");
    });
    $(".menu-item-anchor").mouseout(function (evt) {
        var target = evt.target;
        if (target.tagName.toLowerCase() === "span")
            target = evt.target.parentNode;
        $("#" + target.parentNode.id).find("span").removeClass("iconhover");
    });

    /*
        The below event will be triggered when mouse hover on the color palette element.
        We will highlight selected color on mouse hover and will retain to normal state once mouse leaves.
    */
    $(".ddl-color-list").mouseover(function (evt) {
        evt.target.style.width = "26px";
        evt.target.style.height = "26px";
        evt.target.style.border = "3px solid";
        evt.target.style.borderColor = "#3A3A3A";
    });

    $(".ddl-color-list").mouseout(function (evt) {
        evt.target.style.width = "30px";
        evt.target.style.height = "30px";
        evt.target.style.border = "1px solid";
        evt.target.style.borderColor = "#3A3A3A";
    });

    /*
        The below event will be triggered when mouse down on color palette item. 
        Based on the dropdown control, we will update the node, connector, label and diagram properties. 
    */
    $(".ddl-color-list").mousedown(function (evt) {
        var target = evt.target;
        while (target.parentNode && !target.parentNode.id) {
            target = target.parentNode;
        }
        var id = target.parentNode.id;
        id = id.split('_')[0];
        if (id === "pageBackgroundColor") {
            // update the diagram pageBackground Color
            setBackgroundColor(evt.target);
        }
        else {
            // update the node's fillColor/BorderColor, label's fontColor and connector's lineColor based on the dropdown id.
            PropertyChangesFromPanel(evt.target, id);
        }
    });
};

//#endregion