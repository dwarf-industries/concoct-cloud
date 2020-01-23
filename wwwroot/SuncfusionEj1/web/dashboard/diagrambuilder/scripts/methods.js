/* The below variable is used to identify whether filtered shapes in visible state or not. So we can hide this when click on the document. */
var showMinimizedPalette = false;
/* create a new instance for zoom object to use it anywhere in the scripts. */
var zoom = ej.datavisualization.Diagram.Zoom();


//The below method is used to create the user handles to add some frequently used commands around the selector.
function createUserHandles() {
    var userHandles = [];
    /* Clone Tool is used to cloned the selected node in the diagram. */
    var CloneTool = (function (base) {
        ej.datavisualization.Diagram.extend(CloneTool, base);
        function CloneTool(name) {
            base.call(this, name);
            this.singleAction = true;
            this.clonedNodes = [];
            this.diffx = 0;
            this.diffy = 0;
            this.cursor = "pointer";
            this.isMouseDown = false;
        }
        CloneTool.prototype.mousedown = function (evt) {
            base.prototype.mousedown.call(this, evt);
            this.selectedObject = this.diagram.selectionList[0];
            this.isMouseDown = true;
        };
        CloneTool.prototype.mousemove = function (evt) {
            base.prototype.mousemove.call(this, evt);
            if (this.selectedObject) {
                if (!this.inAction) {
                    this.isMouseDown = false;
                    this.inAction = true;
                    this.diagram.copy();
                    this.diagram.paste();
                    this.clonedNodes = [];
                    this.clonedNodes.push(this.diagram.nameTable[this.diagram.selectionList[0].name]);
                }
                else {
                    if (ej.datavisualization.Diagram.Util.canMove(this.clonedNodes[0]))
                        this._updateClonedNode(true);
                }
            }
            this.previousPoint = this.currentPoint;
        };
        CloneTool.prototype.mouseup = function (evt) {
            if (this.isMouseDown) {
                this.diagram.copy();
                this.diagram.paste();
            } else {
                if (this.inAction) {
                    this.inAction = false;
                    this._updateClonedNode(false);

                }
            }
            this.isMouseDown = false;
            this.clonedNodes = [];
            base.prototype.mouseup.call(this, evt);
        };
        CloneTool.prototype._updateClonedNode = function (isDragging) {
            var type = diagram.getObjectType(this.clonedNodes[0]);
            if ((type == "group" || this.clonedNodes[0].type == "bpmn") && this.clonedNodes[0].type != "pseudoGroup") {
                diagram._translate(this.clonedNodes[0], this.currentPoint.x - this.previousPoint.x, this.currentPoint.y - this.previousPoint.y, this.diagram.nameTable);
                ej.datavisualization.Diagram.SvgContext.update(this.clonedNodes[0], this.diagram);

            }
            else if (this.clonedNodes[0].type == "pseudoGroup") {
                diagram._translate(this.clonedNodes[0], this.currentPoint.x - this.previousPoint.x, this.currentPoint.y - this.previousPoint.y, this.diagram.nameTable);
                for (var i = 0, len = this.clonedNodes[0].children.length; i < len; i++) {
                    var child = this.diagram.nameTable[this.clonedNodes[0].children[i]];
                    ej.datavisualization.Diagram.SvgContext.update(child, this.diagram);
                }
            }
            else {
                for (var i = 0; i < this.clonedNodes.length; i++) {
                    type = diagram.getObjectType(this.clonedNodes[i]);
                    this.clonedNodes[i].offsetX += (this.currentPoint.x - this.previousPoint.x);
                    this.clonedNodes[i].offsetY += (this.currentPoint.y - this.previousPoint.y);
                    if (type == "group") {
                        this._updateXY(this.clonedNodes[i], this.previousPoint, this.currentPoint);
                    }
                    ej.datavisualization.Diagram.SvgContext.update(this.clonedNodes[i], this.diagram);
                }
            }
            this.diagram.updateSelection(isDragging);
        };
        CloneTool.prototype._updateXY = function (shape, startPoint, endPoint) {
            var towardsLeft = endPoint.x < startPoint.x;
            var towardsTop = endPoint.y < startPoint.y;
            var difx = this.diffx + (endPoint.x - startPoint.x) / this.diagram._getCurrentZoom();
            var dify = this.diffy + (endPoint.y - startPoint.y) / this.diagram._getCurrentZoom();
            var offset = this.diagram._snapping._snapPosition(this.selectedObject, towardsLeft, towardsTop, ej.datavisualization.Diagram.Point(difx, dify), endPoint, startPoint);
            this.diffx = difx - offset.x;
            this.diffy = dify - offset.y;
            ej.datavisualization.Diagram.Util._translate(shape, (endPoint.x - startPoint.x), (endPoint.y - startPoint.y));
        };
        return CloneTool;
    })(ej.datavisualization.Diagram.ToolBase);

    var cloneHandle = ej.datavisualization.Diagram.UserHandle();
    cloneHandle.name = "Clone";
    cloneHandle.position = ej.datavisualization.Diagram.UserHandlePositions.BottomLeft;//"bottomLeft";
    cloneHandle.visible = true;
    cloneHandle.tool = new CloneTool(cloneHandle.name);
    cloneHandle.enableMultiSelection = true;
    cloneHandle.size = 30;
    cloneHandle.backgroundColor = "#4D4D4D";
    cloneHandle.pathData = "M4.6350084,4.8909971 L4.6350084,9.3649971 9.5480137,9.3649971 9.5480137,4.8909971 z M3.0000062,2.8189973 L11.184016,2.8189973 11.184016,10.999997 3.0000062,10.999997 z M0,0 L7.3649998,0 7.3649998,1.4020001 1.4029988,1.4020001 1.4029988,8.0660002 0,8.0660002 0,1.4020001 0,0.70300276 z";
    userHandles.push(cloneHandle);

    /* Anchor Tool is used to bind the selected object's label to the property panel. */
    var AnchorTool = (function (base) {
        ej.datavisualization.Diagram.extend(AnchorTool, base);
        function AnchorTool(name) {
            base.call(this, name);
            this.singleAction = true;
            this.cursor = "pointer";
        }
        AnchorTool.prototype.mouseup = function (evt) {
            $("#tabContent").ejTab("option", "selectedItemIndex", 2);
            setDiagramSize(true);
            document.getElementById("propertyEditor").style.display = "block";
            ko.applyBindings(this.diagram._selectedObject.select(this.diagram.selectionList));
            base.prototype.mouseup.call(this, evt);
        }
        return AnchorTool;
    })(ej.datavisualization.Diagram.ToolBase);

    var anchorTool = ej.datavisualization.Diagram.UserHandle();
    anchorTool.name = "Label";
    anchorTool.position = ej.datavisualization.Diagram.UserHandlePositions.TopLeft;
    anchorTool.visible = true;
    anchorTool.tool = new AnchorTool(anchorTool.name);
    anchorTool.size = 30;
    anchorTool.backgroundColor = "#4D4D4D";
    anchorTool.enableMultiSelection = true;
    anchorTool.pathData = "M64.3965,24.4844L66.4545,18.5024C66.5335,18.2714,66.6035,17.9574,66.6645,17.5614L66.7285,17.8294C66.7985,18.2604,66.8425,18.3074,66.9045,18.5024L68.9865,24.4844z M68.1495,15.3334L65.3475,15.3334L59.7315,30.3334L62.4505,30.3334L63.7485,26.5144L69.6475,26.5144L70.9955,30.3334L73.7255,30.3334z";
    userHandles.push(anchorTool);

    /* Link Tool is used to create the connection between the selected node and newly created node. */
    var LinkTool = (function (base) {
        var node = null;
        var targetNode = null;
        ej.datavisualization.Diagram.extend(LinkTool, base);
        function LinkTool(name) {
            base.call(this, name);
            this.singleAction = true;
            this.cursor = "pointer";
        }
        LinkTool.prototype.mousedown = function (evt) {
            node = this.diagram.selectionList[0];
            this._currentPossibleConnection = node;
            base.prototype.mousedown.call(this, evt);
            this._showAllPorts();
            this.diagram.addSelection(node);
            this._sourcePossibleConnection = node;
            targetNode = null;
        };
        LinkTool.prototype.mouseup = function (evt) {
            if (evt.target.id == "Line_shape") {
                this.inAction = false;
                this.diagram._clearSelection();
                if (this.helper)
                    this.diagram._remove(this.helper);
                base.prototype.mouseup.call(this, evt);
                var drawType = { type: "connector", segments: [{ type: "orthogonal" }] };
                diagram.update({ tool: ej.datavisualization.Diagram.Tool.ContinuesDraw, drawType: drawType })
            }
            else {
                if (this._sourcePossibleConnection && this._sourcePossibleConnection.ports.length > 0)
                    if (!this._targetPossibleConnection) {
                        if (!this._targetPort) {
                            var dNode = this._createDynamicNode(node);
                            dNode.parent = "";
                            this.diagram.add(dNode);
                            this._targetPossibleConnection = this.diagram.nameTable[dNode.name];
                        }
                    }
                targetNode = this._targetPossibleConnection.name;
                this.diagram._updateEdges(this);
                this._showAllPorts(true);
                base.prototype.mouseup.call(this, evt);
            }
            if (targetNode) {
                this.diagram._clearSelection();
                this.diagram.addSelection(targetNode)
            }

        };
        LinkTool.prototype._createDynamicNode = function (node) {
            var node = $.extend(true, {}, node);
            node.outEdges = [];
            node.inEdges = [];
            if (node.type == "bpmn") delete node.children;
            node.offsetX = this.currentPoint.x;
            node.offsetY = this.currentPoint.y;
            node.name = node.name + ej.datavisualization.Diagram.Util.randomId();
            return node;
        };
        return LinkTool;
    })(ej.datavisualization.Diagram.OrthogonalLineTool);

    var linkHandle = ej.datavisualization.Diagram.UserHandle();
    linkHandle.name = "Line";
    linkHandle.position = ej.datavisualization.Diagram.UserHandlePositions.MiddleRight;
    linkHandle.visible = true;
    linkHandle.tool = new LinkTool(linkHandle.name);
    linkHandle.size = 30;
    linkHandle.backgroundColor = "#4D4D4D";
    linkHandle.pathData = "M196.2104,93.6494L196.2104,90.2544L189.1674,90.2544L189.1674,87.6694L196.2104,87.6694L196.2104,84.7344L200.6674,89.1914z";
    userHandles.push(linkHandle);

    /* settings Tool is used to bind the selected object to the property panel. */
    var SettingsTool = (function (base) {
        ej.datavisualization.Diagram.extend(SettingsTool, base);
        function SettingsTool(name) {
            base.call(this, name);
            this.singleAction = true;
            this.cursor = "pointer";
        }
        SettingsTool.prototype.mouseup = function (evt) {
            var type = diagram.getObjectType(diagram.selectionList[0]);
            if (type === "connector") {
                $("#tabContent").ejTab("option", "selectedItemIndex", 3);
            }
            else {
                $("#tabContent").ejTab("option", "selectedItemIndex", 1);
            }
            setDiagramSize(true);
            document.getElementById("propertyEditor").style.display = "block";
            ko.applyBindings(this.diagram._selectedObject.select(this.diagram.selectionList));
            base.prototype.mouseup.call(this, evt);
        }
        return SettingsTool;
    })(ej.datavisualization.Diagram.ToolBase);

    var settingHandle = ej.datavisualization.Diagram.UserHandle();
    settingHandle.name = "Settings";
    settingHandle.position = ej.datavisualization.Diagram.UserHandlePositions.BottomRight;
    settingHandle.visible = false;
    settingHandle.tool = new SettingsTool(settingHandle.name);
    settingHandle.size = 30;
    settingHandle.backgroundColor = "#4D4D4D";
    settingHandle.enableMultiSelection = true;
    settingHandle.pathData = "M22.0542,27.332C20.4002,27.332,19.0562,25.987,19.0562,24.333C19.0562,22.678,20.4002,21.333,22.0542,21.333C23.7082,21.333,25.0562,22.678,25.0562,24.333C25.0562,25.987,23.7082,27.332,22.0542,27.332 M30.6282,22.889L28.3522,22.889C28.1912,22.183,27.9142,21.516,27.5272,20.905L29.1392,19.293C29.3062,19.126,29.3062,18.853,29.1392,18.687L27.7032,17.251C27.6232,17.173,27.5152,17.125,27.3982,17.125C27.2862,17.125,27.1782,17.173,27.0952,17.251L25.4872,18.863C24.8732,18.476,24.2082,18.201,23.5002,18.038L23.5002,15.762C23.5002,15.525,23.3092,15.333,23.0732,15.333L21.0422,15.333C20.8062,15.333,20.6122,15.525,20.6122,15.762L20.6122,18.038C19.9072,18.201,19.2412,18.476,18.6292,18.863L17.0192,17.252C16.9342,17.168,16.8242,17.128,16.7162,17.128C16.6052,17.128,16.4972,17.168,16.4112,17.252L14.9752,18.687C14.8952,18.768,14.8492,18.878,14.8492,18.99C14.8492,19.104,14.8952,19.216,14.9752,19.293L16.5872,20.905C16.2002,21.516,15.9242,22.183,15.7642,22.889L13.4852,22.889C13.2502,22.889,13.0572,23.08,13.0572,23.316L13.0572,25.35C13.0572,25.584,13.2502,25.777,13.4852,25.777L15.7612,25.777C15.9242,26.486,16.2002,27.15,16.5872,27.764L14.9752,29.374C14.8092,29.538,14.8092,29.813,14.9752,29.979L16.4112,31.416C16.4912,31.494,16.6022,31.541,16.7162,31.541C16.8272,31.541,16.9382,31.494,17.0192,31.416L18.6252,29.805C19.2412,30.191,19.9072,30.467,20.6122,30.63L20.6122,32.906C20.6122,33.141,20.8062,33.333,21.0422,33.333L23.0732,33.333C23.3092,33.333,23.5002,33.141,23.5002,32.906L23.5002,30.63C24.2082,30.467,24.8732,30.191,25.4872,29.805L27.0952,31.416C27.1812,31.499,27.2892,31.541,27.3982,31.541C27.5102,31.541,27.6202,31.499,27.7032,31.416L29.1392,29.979C29.2202,29.899,29.2662,29.791,29.2662,29.677C29.2662,29.563,29.2202,29.453,29.1392,29.374L27.5312,27.764C27.9142,27.149,28.1912,26.486,28.3522,25.777L30.6282,25.777C30.8652,25.777,31.0552,25.584,31.0552,25.35L31.0552,23.316C31.0552,23.08,30.8652,22.889,30.6282,22.889";
    userHandles.push(settingHandle);
    return userHandles;
}

// The below method is used to set the size of symbol palette and property panel size with respect to windows size.
function updateSize() {

    var width = window.innerWidth - $("#main-left").width();

    $("#main-right").css("width", width);
    // get the header section height.
    var pixels = document.getElementById("sample-header").getBoundingClientRect().height;

    var screenHeight = document.body.clientHeight;
    // set the main section height based on the document and header section height
    document.getElementById("sample-main").style.height = (screenHeight - pixels) - 2 + "px";


    if (diagram._enableOverView)
        var height = $("#main-left").height() - $(".searchParent_Maxi").height() - $("#Overview-div").height() - 20;
    else
        height = $("#main-left").height() - $(".searchParent_Maxi").height();
    /* 
       The symbolPalette client side API method "updateScrollerViewport" is used to update the symbol palette size based on view port size.
   */
    var palette = $("#symbolpalette").ejSymbolPalette("instance");
    //set the palette height based on overview state.
    palette.setHeight(height + "px");
    // set the symbolpalette content size.
    palette.updateScrollerViewport();
}

// The below method is used to set the diagram size with respect to windows size.
function setDiagramSize(showpropeditor) {
    var width = window.innerWidth - $("#main-left").width();
    var pwidth = $("#propertyEditor").width();
    if (!showpropeditor)
        pwidth = 0;
    $("#main-right").css("width", width - pwidth);
    /* 
        We have set the diagram size in percentage. 
        The diagram client side API method "updateViewPort" is used to update the diagram based on view port size.
    */
    diagram.updateViewPort();
}

/* The below used to set the DOM attribute of that element*/
function setAttributes(element, attributes) {
    for (var atr in attributes) {
        element.setAttribute(atr.toString(), attributes[atr]);
    }
}


/* The below method will be triggered when we click to minimize or maximize the symbol palette. */
function minmaxClick() {
    isPMinized = true;
    $("#main-left").animate({ width: '40px' }, 300, function () {
        /*Trigger the below method to expose the features at minimized state.*/
        openMinimizedPalette();
        //set the diagram size based on minimized/maximized state.
        setDiagramSize();
    });
    /* hide the overview control*/
    $("#EnableOverView").find("span").css("display", "none");
    diagram._enableOverView = false;
    $("#Overview-div").css("display", "none");
    //set the palette and property panel size based on minimized/maximized state.
    updateSize();
}

/* 
    The below method is used to expose the features at minimized state. such as
    1. Visualize the symbol palette shapes separately.
    2. Provide the filter option.
*/
function openMinimizedPalette() {
    /* Hide the maximized symbol palette and its search related content */
    document.getElementById("symbolPalette_Parent_Maxi").style.display = "none";
    document.getElementById("no_match_found").style.display = "none";
    $(".chat-bubble-maxi").css("display", "none");

    /* Show the minimized element and empty its content. */
    document.getElementById("minimizedPalette").style.display = "block";
    document.getElementById("minimizedPalette").innerHTML = "";

    /* set the minimized element style */
    var mPaletteDiv = document.getElementById("minimizedPalette");
    var attr = {
        "style": "height: 100%; width: 40px; background-color: transparent; border-color: 1px solid lightgray;" +
        "position: relative;"
    };
    this.setAttributes(mPaletteDiv, attr);
    /* Trigger the method is used to create the Html element for maximize the symbol palette shapes. */
    minizedPaletteOption("maximize", mPaletteDiv);
    /* Trigger the method is used to create the Html element for filter the shapes at minimized state. */
    minizedPaletteOption("search", mPaletteDiv);
    /* Trigger the method is used to create the Html element for view the diagram in overview at minimized state. */
    minizedPaletteOption("overview", mPaletteDiv);
    var palette = $("#symbolpalette").ejSymbolPalette("instance");
    if (palette && palette.model.palettes.length) {
        for (var i = 0; i < palette.model.palettes.length; i++) {
            var name = palette.model.palettes[i].name
            /* Trigger the method is used to create the element to view the symbol palette shapes individually. */
            minizedPaletteOption(name, mPaletteDiv);
        }
    }
}

/* The below method is used to create the element and customize its appearance to expose the features at minimized state.*/
function minizedPaletteOption(option, parentNode) {
    var nDiv = document.createElement("div");
    var title, attr, backgroundImage;
    switch (option) {
        case "maximize":
            title = "Maximize Palette";
            backgroundImage = "rightArrow.png";
            /* Registering the click event to maximize the symbol palette. */
            nDiv.onclick = maximize;
            break;
        case "search":
            title = "Search from Palette";
            backgroundImage = "Search-Find.png";
            /* Registering the click event to filter the shapes at minimized state. */
            nDiv.onclick = Search;
            break;
        case "overview":
            title = "Overview";
            backgroundImage = "overview.png";
            /* Registering the click event toview the diagram in overview at minimized state.*/
            nDiv.onclick = Overview;
            break;
        default:
            title = option;
            backgroundImage = option.split(" ")[0].toLowerCase() + "Shape.png";
            /* Registering the click event to view the symbol palette shapes individually. */
            nDiv.onclick = $.proxy(this.minimizedPaletteClick, this);
            break;
    }
    var attr = {
        "title": title,
        "class": "collapsedPaletteStyle",
        "style": "height: 40px; width: 100%; background-image: url(images/" + backgroundImage + ");"
    };
    if (option === "overview") {
        /*positioning the overview element at the bottom. */
        attr.style = attr.style + "left:0;bottom:0;position:absolute;";
    }
    this.setAttributes(nDiv, attr);
    parentNode.appendChild(nDiv);
}

/* 
    The below method is used to maximize the symbol palette.
    Also it will hide if any elements shown at the minized state and update the size of diagram, palette and property panel.
*/
function maximize() {
    document.getElementById("minimizedPalette").style.display = "none";
    document.getElementById("symbolPalette_Parent_Mini").style.display = "none";
    document.getElementById("Overview-div").style.display = "none";
    clearText();
    if (document.getElementById("propertyEditor").style.display == "block") {
        document.getElementById("main-right").style.width = "63%";
    }
    else {
        document.getElementById("main-right").style.width = "80%";
    }
    $("#main-left").animate({ width: '270px' }, 300, function () {
        setDiagramSize();
    });
    document.getElementById("symbolPalette_Parent_Maxi").style.display = "block";
    updateSize();
}
function setPaletteCollections() {

    window.basicShapes = [];
    window.flowShapes = [];
    window.electricalShapes = [];
    window.arrowShapes = [];
    window.bpmnShapes = [];
    window.connectors = [];
    window.swimlaneShapes = [];

    var palette = $("#symbolpalette").ejSymbolPalette("instance");
    for (var i = 0; i < palette.model.palettes.length; i++) {
        var symPalette = palette.model.palettes[i];
        var j;
        switch (symPalette.name) {
            case "Basic Shapes":
                for (j = 0; j < symPalette.items.length; j++) {
                    window.basicShapes.push($.extend(true, {}, symPalette.items[j]));
                }
                break;
            case "Flowchart Shapes":
                for (j = 0; j < symPalette.items.length; j++) {
                    window.flowShapes.push($.extend(true, {}, symPalette.items[j]));
                }
                break;
            case "Arrow Shapes":
                for (j = 0; j < symPalette.items.length; j++) {
                    window.arrowShapes.push($.extend(true, {}, symPalette.items[j]));
                }
                break;
            case "Electrical Shapes":
                for (j = 0; j < symPalette.items.length; j++) {
                    window.electricalShapes.push($.extend(true, {}, symPalette.items[j]));
                }
                break;
            case "BPMN Shapes":
                for (j = 0; j < symPalette.items.length; j++) {
                    window.bpmnShapes.push($.extend(true, {}, symPalette.items[j]));
                }
                break;
            case "Connectors":
                for (j = 0; j < symPalette.items.length; j++) {
                    window.connectors.push($.extend(true, {}, symPalette.items[j]));
                }
                break;
            case "Swimlane Shapes":
                for (j = 0; j < symPalette.items.length; j++) {
                    window.swimlaneShapes.push($.extend(true, {}, symPalette.items[j]));
                }
                break;
        }
    }
}
/* 
    The below method is used to visualize the symbol palette shapes separately.
    Also it will hide if any elements shown at the minized state and update the size of diagram, palette and property panel.
*/
function minimizedPaletteClick(evt) {

    var maxiPalette = $("#searchPalette").ejSymbolPalette("instance");

    var maxipalettes = maxiPalette.model.palettes;
    for (var i = maxipalettes[0].items.length - 1; i >= 0; i--) {
        /*
            As already informered in control.js file, the above symbol palette ("searchPalette") is used to list the filtered shapes alone.
            So here we will remove the existing filtered shapes by using symbolPalette's "removePaletteItem" API method and 
            will add the selected shapes list by using "addPaletteItem" method.
        */
        maxiPalette.removePaletteItem(maxipalettes[0].name, maxipalettes[0].items[i]);
    }

    /* You can see a arrow indicator while filtering and below method is used to create the element. */
    arrowIndicator("minimize");

    var bounds = evt.currentTarget.getBoundingClientRect();

    /* set the minimized symbol palette parent element style. */
    document.getElementById("symbolPalette_Parent_Mini").style.left = bounds.right + 20 + "px";
    document.getElementById("symbolPalette_Parent_Mini").style.top = bounds.top - 50 + "px";
    document.getElementById("symbolPalette_Parent_Mini").style.position = "absolute";
    document.getElementById("symbolPalette_Parent_Mini").style.display = "block";
    /*Hide the element which will appear if shapes didnt found on searching*/
    document.getElementById("no_match_found").style.display = "none";
    /* set the minimized symbol palette element style. */
    document.getElementById("searchPalette").style.width = "255px";
    document.getElementById("searchPalette").style.height = "auto";
    document.getElementById("searchPalette").style.display = "block";
    document.getElementById("filteredShapes_content").style.display = "block";

    /* Hide the overview if its open in minimized state */
    document.getElementById("Overview-div").style.display = "none";
    $("#Overview").ejOverview({ sourceID: "" });

    /* Hide the search option if its open in minimized state */
    $(".searchParent_Mini").css("display", "none");

    var paletteItems = [];
    if (!window.basicShapes) {
        setPaletteCollections();
    }
    switch (evt.target.title) {
        case "Basic Shapes":
            paletteItems = window.basicShapes;
            break;
        case "Flowchart Shapes":
            paletteItems = window.flowShapes;
            break;
        case "Electrical Shapes":
            paletteItems = window.electricalShapes;
            break;
        case "Arrow Shapes":
            paletteItems = window.arrowShapes;
            break;
        case "BPMN Shapes":
            paletteItems = window.bpmnShapes;
            break;
        case "Swimlane Shapes":
            paletteItems = window.swimlaneShapes;
            break;
        case "Connectors":
            paletteItems = window.connectors;
            break;
    }

    for (var j = 0; j < paletteItems.length; j++) {
        /* add the selected shapes list. */
        maxiPalette.addPaletteItem(maxipalettes[0].name, paletteItems[j]);
    }
    /* create the tooltip when mouse hover on the palette item. */
    setToolTip();

    if (evt.target.title === "Electrical Shapes")
        document.getElementById("symbolPalette_Parent_Mini").style.height = $("#main-right").height() - 200 + "px";
    else
        document.getElementById("symbolPalette_Parent_Mini").style.height = "auto";

    /* update the minimized symbol palette size by using symbolpalette's clientside API method "updateScrollerViewport". */
    maxiPalette.updateScrollerViewport();
    showMinimizedPalette = true;
}

/* The below method is used to create a arrow indicator while filtering based on minimized or maximized state. */
function arrowIndicator(option) {
    var parentNode = document.getElementById("symbolPalette_Parent_Mini");
    var arrowType = option === "maximize" ? "maxi" : "mini";
    $(".chat-bubble-" + arrowType).remove();

    var arrowBorder = document.createElement("div");
    arrowBorder.setAttribute("class", "chat-bubble-" + arrowType + " arrow-border-" + arrowType);
    parentNode.appendChild(arrowBorder);

    var arrow = document.createElement("div");
    arrow.setAttribute("class", "chat-bubble-" + arrowType + " arrow-" + arrowType);
    parentNode.appendChild(arrow);

    /* set the appearance based on the state. */
    if (option === "maximize") {
        var parentStyle = {
            "style": "position:absolute; height:auto; width:250px; background-color:white; border:1px solid lightgray;" +
            "border-radius:5px; left:5px; top:137px; padding:10px;"
        };
    }
    else {
        var parentStyle = {
            "style": "position:absolute; height:auto; width:250px; background-color:white; border:1px solid lightgray;" +
            "border-radius:5px; left:100px; top:100px; padding:10px;"
        };
    }
    this.setAttributes(parentNode, parentStyle);
}

/* The below method is used to create the tooltip when mouse hover on the palette items. */
function setToolTip(isLoad) {
    var paletteItems;
    if (isLoad)
        paletteItems = $(".e-paletteItem");
    else
        paletteItems = $("#filteredShapes_content")[0].childNodes;
    for (m = 0; m < paletteItems.length; m++) {
        var title = "";
        var wWord = ((paletteItems[m].id).split("_")[0]);
        if (wWord) {
            if (wWord.split(/(?=[A-Z])/)[0])
                title += " " + wWord.split(/(?=[A-Z])/)[0];
            if (wWord.split(/(?=[A-Z])/)[1])
                title += " " + wWord.split(/(?=[A-Z])/)[1];
            if (wWord.split(/(?=[A-Z])/)[2])
                title += " " + wWord.split(/(?=[A-Z])/)[2];
            if (wWord.split(/(?=[A-Z])/)[3])
                title += " " + wWord.split(/(?=[A-Z])/)[3];
            if (wWord.split(/(?=[A-Z])/)[4])
                title += " " + wWord.split(/(?=[A-Z])/)[4];
        }
        paletteItems[m].setAttribute("title", title);
    }
}

/* The below method is used to clear the text in search box and update the background image. */
function clearText() {
    document.getElementById("txtSearch_Maxi").value = "";
    document.getElementById("searchClear_Maxi").style.backgroundImage = 'url("images/Search-Find.png")';
    if (document.getElementById("searchClear_Mini")) {
        document.getElementById("txtSearch_Mini").value = "";
        document.getElementById("searchClear_Mini").style.backgroundImage = 'url("images/Search-Find.png")';
    }
}

/* The search method is used to create and display the elements for search option at minimized state.  */
function Search(evt) {

    var parentNode = document.getElementById("symbolPalette_Parent_Mini");
    $(".searchParent_Mini").remove();

    arrowIndicator("minimize");
    var pdiv = document.createElement("div");
    pdiv.setAttribute("class", "searchParent_Mini");

    var cdiv = document.createElement("div");
    cdiv.setAttribute("id", "searchClear_Mini");
    cdiv.setAttribute("class", "searchClear_Mini");
    cdiv.setAttribute("style", "float:right");
    cdiv.onclick = clearText;

    var textBox = document.createElement("input");
    pdiv.appendChild(textBox);
    pdiv.appendChild(cdiv);

    var attr = {
        "style": "height: 30px; width:80%; outline:none; margin-left:4px; border:none;",
        "type": "text",
        "id": "txtSearch_Mini"
    };
    setAttributes(textBox, attr);
    textBox.onkeyup = onSearchBoxTextChange;

    parentNode.insertBefore(pdiv, parentNode.firstChild);

    var bounds = evt.currentTarget.getBoundingClientRect();
    parentNode.style.left = bounds.right + 20 + "px";
    parentNode.style.top = bounds.top - 30 + "px";

    /* Hide the minimized symbol palette and overview option if its opened state */
    document.getElementById("searchPalette").style.display = "none";
    document.getElementById("Overview-div").style.display = "none";

    $(".chat-bubble-mini").css("display", "block");
    showMinimizedPalette = true;
}

/* 
    The below method is used to view the diagram in overview option at minimized state.
*/
function Overview(evt) {
    document.getElementById("symbolPalette_Parent_Mini").style.display = "none";
    var parentOverview = document.getElementById("Overview-div");
    if (parentOverview.style.display !== "block") {
        var bounds = evt.currentTarget.getBoundingClientRect();
        parentOverview.style.left = bounds.right + 1 + "px";
        parentOverview.style.bottom = 20 + "px";
        parentOverview.style.position = "absolute";
        parentOverview.style.display = "block";
        if (!diagram._enableOverView)
            $("#Overview").ejOverview({ sourceID: "DiagramContent" });

        $(".searchParent_Mini").css("display", "none");
        document.getElementById("searchPalette").style.display = "none";
    }
    else {
        document.getElementById("Overview-div").style.display = "none";
    }
    showMinimizedPalette = !showMinimizedPalette;
}

/*
    The below method will be triggered on text changing and its used to filter the shapes.
*/
function onSearchBoxTextChange(evt) {
    var target = evt.target ? evt.target : evt;
    var key = target.value;
    if (key && key.length > 0) {

        document.getElementById("symbolPalette_Parent_Mini").style.display = "block";
        document.getElementById("searchPalette").style.display = "block";
        document.getElementById("filteredShapes_content").style.display = "block";
        document.getElementById("searchPalette").style.width = "255px";
        document.getElementById("searchPalette").style.height = "auto";
        var maxiPalette = $("#searchPalette").ejSymbolPalette("instance");
        var maxipalettes = maxiPalette.model.palettes;

        var palette = $("#symbolpalette").ejSymbolPalette("instance");
        var palettes = palette.model.palettes;

        for (var i = maxipalettes[0].items.length - 1; i >= 0; i--) {
            maxiPalette.removePaletteItem(maxipalettes[0].name, maxipalettes[0].items[i]);
        }

        for (i = 0; i < palettes.length; i++) {
            for (var j = 0; j < palettes[i].items.length; j++) {
                item = palettes[i].items[j];
                var itemName = (item.name).split(/(?=[A-Z])/)
                key = key.toLocaleLowerCase();
                item = $.extend(true, {}, item);
                if ((itemName[0]).toLocaleLowerCase().indexOf(key) == 0) {
                    maxiPalette.addPaletteItem(maxiPalette.model.palettes[0].name, item);
                } else if (itemName[1] && ((itemName[1]).toLocaleLowerCase().indexOf(key) == 0)) {
                    maxiPalette.addPaletteItem(maxiPalette.model.palettes[0].name, item);
                } else if (itemName[2] && ((itemName[2]).toLocaleLowerCase().indexOf(key) == 0)) {
                    maxiPalette.addPaletteItem(maxiPalette.model.palettes[0].name, item);
                } else if (itemName[3] && ((itemName[3]).toLocaleLowerCase().indexOf(key) == 0)) {
                    maxiPalette.addPaletteItem(maxiPalette.model.palettes[0].name, item);
                }
            }
        }
        if (maxipalettes[0].items.length <= 0) {
            document.getElementById("no_match_found").style.display = "block";
            document.getElementById("searchPalette").style.display = "none";
        }
        else {
            document.getElementById("no_match_found").style.display = "none";
            document.getElementById("searchPalette").style.display = "block";
            if (target.id === "txtSearch_Maxi" && document.getElementsByClassName("searchParent_Mini")[0])
                document.getElementsByClassName("searchParent_Mini")[0].style.display = "none";
        }
    } else {
        document.getElementById("searchPalette").style.display = "none";
    }
    var clearDiv = target.id === "txtSearch_Maxi" ? "searchClear_Maxi" : "searchClear_Mini";

    if (!key) {
        if (clearDiv !== "searchClear_Mini")
            document.getElementById("symbolPalette_Parent_Mini").style.display = "none";
        document.getElementById(clearDiv).style.backgroundImage = 'url("images/Search-Find.png")';
    }
    else {
        document.getElementById(clearDiv).style.backgroundImage = 'url("images/Close.png")';
    }
    setToolTip();
}


/*
    In diagram builder, We have exposed to customize the node, label and connector appearance at runtime. The below class is used to visually represent the object(node,connector and its label) properties in the property panel.
    We have initialized node,connector and label properties in this object and also created some additional properties for visualization. 
    For example, while changing the node's fillColor, color will be represented via div element. 
    To achieve this we have created the three properties named as "fillColor", "fillBackgroundColor", "fillBorderColor".
    The "fillColor" value is set to be background-color for image element which we created in default.html file for visual representation.
    The "fillBackgroundColor" value will be set as background-color image parent element.
    The "fillBorderColor" value will be set as background-color for expand option available in the fillColor dropdown control.

    All properties applied to the html element via knockout binding. 
    So, when you customizing through property panel, this object will be updated since we have already created the instance for this object in control.js file (diagram._selectedObject) and further we will call the update method to update the object in the diagram.

    Similarly, we have created the remaining properties for other functionalites and its explained in that class.
*/
var SelectorVMClass = (function () {
    function SelectorVMClass() {
        //initialize the properties for node's fillColor representation.
        this.fillColor = null;
        this.fillBackgroundColor = ko.observable("#F9F9F9");
        this.fillBorderColor = ko.observable("#A3A2A2");

        //initialize the properties for node's borderColor representation.
        this.stroke = null;
        this.storkeBackgroundColor = "#F9F9F9";
        this.storkeBorderColor = "#A3A2A2";
        //we have used two font icons for borderColor representation. Hence we have added additional property here.
        this.strokePenColor = "7F7F7F";

        //initialize the properties for node's borderWidth representation.
        this.borderThickness = null;
        this.borderThicknessFontColor = "7F7F7F";
        this.borderThicknessBackColor = "#F9F9F9";
        this.borderThicknessBorderColor = "#A3A2A2";;

        //initialize the properties for node's gradient representation.
        this.gradient = false;
        this.gradientBackgroundColor = "#F9F9F9";
        this.gradientFontColor = null;

        //initialize the properties for label's fontColor representation.
        this.labelFontColor = null;
        this.labelFontBackgroundColor = "#F9F9F9";
        this.labelFontBorderColor = "#A3A2A2";

        //initialize the properties for label's bold value representation.
        this.bold = false;
        this.boldBackgroundColor = "#F9F9F9";
        this.boldFontColor = "#7F7F7F";

        //initialize the properties for label's italic value representation.
        this.italic = null;
        this.italicBackgroundColor = "#F9F9F9";
        this.italicFontColor = "#7F7F7F";

        //initialize the properties for label's alignement (left, right, center) representation.
        this.textLeftAlign = false;
        this.textLeftAlignFontColor = "#7F7F7F";
        this.textLeftAlignBackgroundColor = "#F9F9F9";

        this.textRightAlign = false;
        this.textRightAlignFontColor = "#7F7F7F";
        this.textRightAlignBackgroundColor = "#F9F9F9";

        this.textCenterAlign = false;
        this.textCenterAlignFontColor = "#7F7F7F";
        this.textCenterAlignBackgroundColor = "#F9F9F9";

        //initialize the properties for connector's lineColor representation.
        this.lineColor = null;
        this.lineBackgroundColor = "#F9F9F9";
        this.lineBorderColor = "#A3A2A2";

        //initialize the properties for connector's lineWidth representation.
        this.lineWidth = null;
        this.cornerRadius = 8;
        this.lineWidthBackgroundColor = "#F9F9F9";
        this.lineWidthFontColor = null;
        this.lineWidthBorderColor = "#A3A2A2";

        //initialize the properties for connector's decorator representation.
        this.decoratorBackgroundColor = "#F9F9F9";
        this.decoratorFontColor = null;
        this.decoratorBorderColor = "#A3A2A2";;

        //initialize the properties for connector's lineType representation.
        this.lineTypeFontColor = null;
        this.lineTypeBackgroundColor = "#F9F9F9";
        this.lineTypeBorderColor = "#A3A2A2";

        //initialize the properties for node/connector's enable/disable state representation.
        this.lock = null;
        this.lockFontColor = "#7F7F7F";
        this.lockBackgroundColor = "#F9F9F9";

        //initialize the properties for connector's lineStyle representation.
        this.lineStyleFontColor = "#7F7F7F";
        this.lineStyleBorderColor = "#A3A2A2";
        this.lineStyleBackgroundColor = "#F9F9F9";

        //initialize the properties for active tab index representation.
        this.tabPinIcon = "#999999";
        this.tabnodeicon = "#999999";
        this.tabTextIcon = "#999999";
        this.tabConnectorIcon = "#999999";

        //initialize the property to identify whether property panel in pinned state or not.
        this.pinned = false;

        //initialize the property to identify whether dropdown popup in closed state or not. (i.e color palette)
        this.dropdownOpened = false;

        //initialize the property to identify whether control rendering at inialization state.
        this.init = 0;

        /*
            initialize the property to identify the selection changes in the diagram. 
            While selecting the object property panel will be updated. So we are preventing the changes will happen in the diagram at that time.
        */
        this.selected = false;
    }

    /*
        The below method will be triggered when diagram selectionChange event gets triggered. Please refer in event.js file about selectionChange event.
        In this method we will enable/disable the node's, label's and connector's tab based on the selection.
    */
    SelectorVMClass.prototype.select = function (selectedItems) {
        if (selectedItems.length > 0 && selectedItems[0]) {
            this.selected = true;
            var item = selectedItems[0];
            var type = selectedItems.length > 1 ? "" : item.type;
            if (!(item.isSwimlane || item.isLane)) {
                if ((type == "group" || selectedItems.length > 1)) {
                    //The below variables is used to identify the nodes and connector exist in the group collection or not.
                    var connectorExist = false, nodeExist = false, LabelExist = true, childNode, childConnector;
                    var items = type == "group" ? item.children : selectedItems;
                    for (var i = 0; i < items.length; i++) {
                        //get the child item from group.
                        var child = items[i];
                        if (typeof (child) === "string") {
                            child = diagram.findNode(child);
                        }
                        //get the type of  the child.
                        var childType = child.type;
                        if (childType == "node") {
                            nodeExist = true;
                            if (!childNode)
                                childNode = child;
                        }
                        else if (childType === "connector") {
                            connectorExist = true;
                            if (!childConnector)
                                childConnector = child;
                        }
                        if (!(child.labels.length > 0 && child.labels[0].text.length > 0))
                            LabelExist = false;
                    }
                    /*
                        Enable/disable the node, connector and labels tab. When group contains multiple node or connectors, 
                        the property panel values will be updated based on first object.
                    */
                    this.EnableConnectorProperties(childConnector, connectorExist);
                    this.EnableNodeProperties(childNode, nodeExist, true);
                    this.EnableTextProperties(childNode || childConnector, LabelExist);
                }
                else if (type === "connector") {
                    /* Enable the connector tab and label tab only if connector has a label and disable the node tab. */
                    this.EnableConnectorProperties(item, true);
                    this.EnableNodeProperties(item, false);
                    this.EnableTextProperties(item, true);
                }
                else {
                    /* Enable the node tab and label tab only if node has a label and disable the connector tab. */
                    this.EnableNodeProperties(item, true);
                    this.EnableConnectorProperties(item, false);
                    this.EnableTextProperties(item, true);
                }
                this.selected = false;
            }
            else {
                /* Disable all the tabs if none of the object gets selected. */
                this.selected = true;
                this.EnableNodeProperties(null, false);
                EnableConnectorPanel(null, false);
                EnableTextPanel(null, false);
                this.selected = false;
            }
        }
        else {
            /* Disable all the tabs if none of the object gets selected. */
            this.selected = true;
            this.EnableNodeProperties(null, false);
            this.EnableConnectorProperties(null, false);
            EnableConnectorPanel(null, false);
            EnableTextPanel(null, false);
            this.selected = false;
        }
        return this;
    };

    /* The below method is used to enable/disable the controls availble in the nodes tab and its appearance. */
    SelectorVMClass.prototype.EnableNodeProperties = function (item, isEnable, isMultipleSelection) {
        EnableNodePanel(item, isEnable, isMultipleSelection);
        if (!isEnable) {
            this.borderThickness = null;
            this.lock = (item && !item.segments) ? null : this.lock;
            this.gradient = null;


            this.fillColor = this.stroke = this.borderThicknessFontColor = this.strokePenColor = "#DDDBDB";
            this.fillBackgroundColor = this.storkeBackgroundColor = this.borderThicknessBackColor = "#F2F2F2";
            this.fillBorderColor = this.storkeBorderColor = this.borderThicknessBorderColor = "#F2F2F2";

            this.gradientFontColor = this.lockFontColor = "#DDDBDB";
            this.gradientBackgroundColor = this.lockBackgroundColor = "#F2F2F2";

        }
        else {
            if (item.type != "pseudoGroup")
                this.lock = (item.constraints & (ej.datavisualization.Diagram.NodeConstraints.Delete | ej.datavisualization.Diagram.NodeConstraints.Drag | ej.datavisualization.Diagram.NodeConstraints.Resize)) ? false : true;
            if (item.type === "text" || (item.isSwimlane || item.isLane)) {
                this.fillColor = this.stroke = this.borderThicknessFontColor = this.strokePenColor = "#DDDBDB";
                this.gradient = null, this.borderThickness = null;
                this.fillBackgroundColor = this.storkeBackgroundColor = this.borderThicknessBackColor = "#F2F2F2";
                this.fillBorderColor = this.storkeBorderColor = this.borderThicknessBorderColor = "#F2F2F2";
            }
            else {
                this.fillColor = item.fillColor;
                this.stroke = item.borderColor;
                this.borderThickness = item.borderWidth;
                this.gradient = item.gradient ? true : false;
                this.borderThicknessFontColor = this.strokePenColor = "#7F7F7F";
                this.fillBackgroundColor = this.storkeBackgroundColor = this.borderThicknessBackColor = "#F9F9F9";
                this.fillBorderColor = this.storkeBorderColor = this.borderThicknessBorderColor = "#A3A2A2";
            }
            setThickness(item, "borderWidth");
            if (this.gradient)
                this.gradientBackgroundColor = "#28B1BF";
            else
                this.gradientBackgroundColor = "#F9F9F9";
            if (!this.lock) {
                this.lockFontColor = "#7F7F7F";
                this.lockBackgroundColor = "#F9F9F9";
            }
            else {
                this.lockFontColor = "#FFFFFF";
                this.lockBackgroundColor = "#28B1BF";
            }
            setOpacity(item.opacity * 100);
            setNodeDimension(item);
        }
    }

    /* The below method is used to enable/disable the controls availble in the label tab and its appearance. */
    SelectorVMClass.prototype.EnableTextProperties = function (item, isEnable) {
        if (!isEnable) {
            EnableTextPanel(isEnable);
            this.labelFontColor = "#DDDBDB";
            this.labelFontBackgroundColor = "#F2F2F2";
            this.labelFontBorderColor = "#F2F2F2";
            this.bold = this.italic = null;
            this.boldBackgroundColor = this.italicBackgroundColor = "#F2F2F2";
            this.boldFontColor = this.italicFontColor = "#DDDBDB";
            this.textLeftAlign = this.textRightAlign = this.textCenterAlign = null;
            this.textLeftAlignFontColor = this.textRightAlignFontColor = this.textCenterAlignFontColor = "#DDDBDB";
            this.textLeftAlignBackgroundColor = this.textRightAlignBackgroundColor = this.textCenterAlignBackgroundColor = "#F2F2F2";
            SetLabel(null);
        }
        else {
            var label;
            if (item) {
                if (item.type === "text")
                    label = item.textBlock;
                else {
                    if (item.isSwimlane || item.isLane) {
                        label = diagram.nameTable[item.children[0]].labels[0];
                    }
                    else {
                        if (item && item.labels && item.labels.length > 0) {
                            if (item.labels[0].text.length > 0) {
                                label = item.labels[0];
                            }
                        }
                    }
                }
            }
            if (label) {
                EnableTextPanel(isEnable);
                this.label = true;
                this.bold = label.bold;
                this.italic = label.italic;
                this.boldBackgroundColor = this.bold ? "#28B1BF" : "#F9F9F9";
                this.boldFontColor = this.bold ? "#FFFFFF" : "#7F7F7F";
                this.italicBackgroundColor = this.italic ? "#28B1BF" : "#F9F9F9";
                this.italicFontColor = this.italic ? "#FFFFFF" : "#7F7F7F";
                this.labelFontColor = label.fontColor;
                if (item.type != "bpmn") {
                    this.textAlign = label.textAlign;
                    this.textLeftAlign = this.textRightAlign = this.textCenterAlign = false;
                    this.textLeftAlignFontColor = this.textRightAlignFontColor = this.textCenterAlignFontColor = "#7F7F7F";
                    this.textLeftAlignBackgroundColor = this.textRightAlignBackgroundColor = this.textCenterAlignBackgroundColor = "#F9F9F9";
                    switch (this.textAlign) {
                        case "left":
                            this.textLeftAlign = true;
                            this.textLeftAlignFontColor = "#FFFFFF";
                            this.textLeftAlignBackgroundColor = "#28B1BF";
                            break;
                        case "right":
                            this.textRightAlign = true;
                            this.textRightAlignFontColor = "#FFFFFF";
                            this.textRightAlignBackgroundColor = "#28B1BF";
                            break;
                        case "center":
                            this.textCenterAlign = true;
                            this.textCenterAlignFontColor = "#FFFFFF";
                            this.textCenterAlignBackgroundColor = "#28B1BF";
                            break;
                    }
                    if (item.isSwimlane || item.isLane) {
                        this.textLeftAlign = this.textRightAlign = this.textCenterAlign = null;
                        this.textLeftAlignFontColor = this.textRightAlignFontColor = this.textCenterAlignFontColor = "#DDDBDB";
                        this.textLeftAlignBackgroundColor = this.textRightAlignBackgroundColor = this.textCenterAlignBackgroundColor = "#F2F2F2";
                    }
                }
                else {
                    this.textLeftAlign = this.textRightAlign = this.textCenterAlign = null;
                    this.textLeftAlignFontColor = this.textRightAlignFontColor = this.textCenterAlignFontColor = "#DDDBDB";
                    this.textLeftAlignBackgroundColor = this.textRightAlignBackgroundColor = this.textCenterAlignBackgroundColor = "#F2F2F2";
                }
                SetLabel(label);
            }
            else {
                this.EnableTextProperties(item, false);
            }
        }
    }

    /* The below method is used to enable/disable the controls availble in the connector tab and its appearance. */
    SelectorVMClass.prototype.EnableConnectorProperties = function (item, isEnable) {
        EnableConnectorPanel(item, isEnable);
        var fontColor = "#DDDBDB", backgroundColor = "#F2F2F2", borderColor = "#F2F2F2";
        if (!isEnable) {
            this.lock = (item && item.segments) ? null : this.lock;
            thisdecoratorFontColor = null;
            this.lineWidth = null;
            this.lineColor = this.lineTypeFontColor = this.lineWidthFontColor = this.decoratorFontColor = fontColor;
            this.lineStyleFontColor = fontColor;
            $("#cornerradius").ejNumericTextbox("disable");
        }
        else {
            fontColor = "#7F7F7F", backgroundColor = "#F9F9F9", borderColor = "#A3A2A2";
            this.lock = (item.constraints & (ej.datavisualization.Diagram.ConnectorConstraints.Delete | ej.datavisualization.Diagram.ConnectorConstraints.Drag)) ? false : true;
            this.lineColor = item.lineColor;
            this.lineWidth = item.lineWidth;
            setThickness(item, "lineWidth");
            this.lineTypeFontColor = this.lineWidthFontColor = this.decoratorFontColor = null;
            this.lineStyleFontColor = fontColor;
            $("#cornerradius").ejNumericTextbox(isEnable ? "enable" : "disable");
            $("#cornerradius").ejNumericTextbox({ value: item.cornerRadius });
        }

        this.lineBackgroundColor = this.lineTypeBackgroundColor = this.lineWidthBackgroundColor = this.lineStyleBackgroundColor = backgroundColor;
        this.lineBorderColor = this.lineTypeBorderColor = this.lineWidthBorderColor = this.lineStyleBorderColor = borderColor;
        this.decoratorBackgroundColor = backgroundColor;
        this.decoratorBorderColor = borderColor;

    }

    /* The below method is used to customize the active tab appearance. */
    SelectorVMClass.prototype.setTabIconColor = function (args, tab) {
        if (this.init > 0) {
            this.tabnodeicon = "#494949";
            this.tabPinIcon = "#494949";
            this.tabTextIcon = "#494949";
            this.tabConnectorIcon = "#494949";
            if (args.activeIndex == 1)
                this.tabnodeicon = "#1E989E";
            else if (args.activeIndex == 2)
                this.tabTextIcon = "#1E989E";
            else if (args.activeIndex == 3)
                this.tabConnectorIcon = "#1E989E";
            else if (args.activeIndex == 0) {
                var ele = document.getElementById("pinspan");
                this.pinned = this.pinned ? false : true;
                ele.className = this.pinned ? 'icon-PH__TabBin1' : 'icon-PH__TabBin';
                $('.icon-PH__TabBin1').css('font-size', '20px');
                $("#tabContent").ejTab({
                    selectedItemIndex: args.prevActiveIndex,
                });
            }
        }
        this.init++;
        return this;
    }

    /* 
        The below method will be triggered when mouse hover on the property panel controls. It will call the "setBackgroundOnMouseEnter" method to customize the background and border color of the control.
    */
    SelectorVMClass.prototype.MouseEnter = function (args, prop, enter) {
        switch (prop) {
            case "fillColor":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.fillColor);
                break;
            case "borderColor":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.stroke);
                break;
            case "borderThickness":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.borderThickness);
                break;
            case "gradient":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.gradient);
                break;
            case "lock":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.lock);
                break;
            case "lineWidth":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.lineWidth);
                break;
            case "lineColor":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.lineColor);
                break;
            case "line":
            case "lineStyle":
            case "headDecorator":
            case "tailDecorator":
                this.setBackgroundOnMouseEnter(args, prop, enter, "");
                break;
            case "fontColor":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.labelFontColor);
                break;
            case "bold":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.bold);
                break;
            case "italic":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.italic);
                break;
            case "left":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.textLeftAlign);
                break;
            case "right":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.textRightAlign);
                break;
            case "center":
                this.setBackgroundOnMouseEnter(args, prop, enter, this.textCenterAlign);
                break;
        }
    }

    SelectorVMClass.prototype.setBackgroundOnMouseEnter = function (args, prop, enter, object) {
        if (prop === "gradient" || prop === "lock" || prop === "bold" || prop === "italic"
            || prop == "right" || prop == "center" || prop == "left") {
            if (object != null && object != "#DDDBDB") {
                args.style.backgroundColor = "#E2E2E2";
                if (!enter)
                    args.style.backgroundColor = !object ? "#F9F9F9" : "#28B1BF";
            }
        }
        else {
            if (object != null && object != "#DDDBDB") {
                args.style.backgroundColor = "#E2E2E2";
                if (!enter && !this.dropdownOpened) {
                    args.style.backgroundColor = "#F9F9F9";
                }
            }
        }
    }

    /*
       The below method will be triggered when diagram's client side event drag and sizeChange event gets triggered. Please refer this event initialization in event.js file.
       In this method we will update the node's dimension in the property panel.
   */
    SelectorVMClass.prototype.nodeDimensionChanging = function (item) {
        this.selected = true;
        if (document.getElementById("propertyEditor").style.display == "block")
            setNodeDimension(item);
        this.selected = false;
    }

    /*
        The below is used to maintain the dropdown popup status whether its in open/closed state.
        When mouse hover on the property panel control, if dropdown is in opened state, we wont customize the background apperance.
    */
    SelectorVMClass.prototype.setPopupDivColor = function (args, model, opened) {
        model.dropdownOpened = false;
        if (opened) {
            model.dropdownOpened = true;
        }
    }
    return SelectorVMClass;
})();

/* Enable/disable the controls used in the node tab. */
function EnableNodePanel(item, isEnable, isMultipleSelection) {
    var enabled = !isEnable ? "disable" : "enable";
    if (item && (item.type === "text" || (item.isSwimlane || item.isLane))) {
        enabled = "disable";
    }
    $("#fillColor").ejDropDownList(enabled);
    $("#stroke").ejDropDownList(enabled);
    $("#borderWidth").ejDropDownList(enabled);
    $("#opacity").ejSlider(enabled);
    if (isEnable)
        enabled = "enable";
    if (isMultipleSelection)
        enabled = "disable";
    $("#nodeOffsetX").ejNumericTextbox(enabled);
    $("#nodeOffsetY").ejNumericTextbox(enabled);
    $("#nodeHeight").ejNumericTextbox(enabled);
    $("#nodeWidth").ejNumericTextbox(enabled);
    if ((item && item.type == "group") && isEnable)
        enabled = "enable";
    $("#chkAspectRatio").ejCheckBox(enabled);
}

/* Enable/disable the controls used in the connector tab. */
function EnableConnectorPanel(item, isEnable) {
    var enabled = !isEnable ? "disable" : "enable";
    $("#lineColor").ejDropDownList(enabled);
    $("#lineType").ejDropDownList(enabled);
    $("#lineWidth").ejDropDownList(enabled);
    $("#headDecorator").ejDropDownList(enabled);
    $("#tailDecorator").ejDropDownList(enabled);
    $("#lineStyle").ejDropDownList(enabled);
}

/* Enable/disable the controls used in the Text tab. */
function EnableTextPanel(isEnable) {
    var enabled = !isEnable ? "disable" : "enable";
    $("#fontColor").ejDropDownList(enabled);
    $("#fontSize").ejDropDownList(enabled);
    $("#fontStyle").ejDropDownList(enabled);
}

function SetLabel(args) {
    if (args != null) {
        /* update the font size control in the property panel based on the selected object. */
        if (args.fontSize >= 8) {
            $("#fontSize").ejDropDownList("option", { selectedItems: [args.fontSize - 8] });
        }

        /* update the font style control in the property panel based on the selected object. */
        if (args.fontFamily == "Arial") {
            $("#fontStyle").ejDropDownList("option", { selectedItems: [0] });
        }
        else if (args.fontFamily == "Aharoni") {
            $("#fontStyle").ejDropDownList("option", { selectedItems: [1] });
        }
        else if (args.fontFamily == "Bell MT") {
            $("#fontStyle").ejDropDownList("option", { selectedItems: [2] });
        }
        else if (args.fontFamily == "Fantasy") {
            $("#fontStyle").ejDropDownList("option", { selectedItems: [3] });
        }
        else if (args.fontFamily == "Times New Roman") {
            $("#fontStyle").ejDropDownList("option", { selectedItems: [4] });
        }
        else if (args.fontFamily == "Segoe UI") {
            $("#fontStyle").ejDropDownList("option", { selectedItems: [5] });
        }
        else if (args.fontFamily == "Verdana") {
            $("#fontStyle").ejDropDownList("option", { selectedItems: [6] });
        }

    }
}

// update the node dimension controls such as  height, width, offsetX, offsetY in the property panel.
function setNodeDimension(item) {
    $('#chkAspectRatio').ejCheckBox({ checked: (item.constraints & ej.datavisualization.Diagram.NodeConstraints.AspectRatio) ? true : false });
    $("#nodeWidth").ejNumericTextbox({ value: item.width });
    $("#nodeHeight").ejNumericTextbox({ value: item.height });
    $("#nodeOffsetX").ejNumericTextbox({ value: item.offsetX });
    $("#nodeOffsetY").ejNumericTextbox({ value: item.offsetY });
}

// update the opacity in the property panel.
function setOpacity(value) {
    $("#opacity").ejSlider({ value: value });
}

// update the node's borderWidth or connector's lineWidth in the property panel based on the type.
function setThickness(item, type) {
    var value = !item.segments ? item.borderWidth : item.lineWidth;
    $("#" + type).ejDropDownList("option", { selectedItems: [Math.round(value - 1)] });
}

/* The below method will be triggered if we do any changes in the property panel. */
function PropertyChangesFromPanel(args, prop) {
    if (!diagram._selectedObject.selected && diagram.selectionList[0]) {
        /* Here we will call update method to update the object in the diagram as well as visualize the appearance in property panel. */
        update(args, prop);
    }
}

/*
    The below method is used to customize the node/connetor appearance at runtime by using the diagram client side API method.
    Also it will update the "_selectedObject" via knockout binding to visualize the node,connector and label appearance in the property panel.
*/
function update(args, prop) {
    if (!diagram._selectedObject.selected) {
        diagram._modified = true;
        if (diagram.model.selectedItems.children.length > 0) {
            for (var i = 0; i < diagram.model.selectedItems.children.length; i++) {
                var item = diagram.model.selectedItems.children[i];
                this.updateItem(args, prop, item);
            }
        }
    }
}

/* The below method is used to identify the type of the object. */
function updateItem(args, prop, item) {
    var type = diagram.getObjectType(item);
    if (type === "node") {
        this.updateNode(args, prop, item);
    }
    else if (type === "connector") {
        this.updateConnector(args, prop, item);
    }
    else if (type === "group") {
        this.updateGroup(args, prop, item);
    }
}

/* The below method is used to identify the type of the object. */
function updateGroup(args, prop, item) {
    if (!item.isSwimlane || !item.isLane) {
        for (var i = 0; i < item.children; i++) {
            var child = item.children[i];
            if (typeof child === "string")
                child = diagram.findNode(child);
            this.updateItem(args, prop, child);
        }
    }
}

/*
    The below method is used to customize the node appearance at runtime by using the diagram client side API method.
    Also it will update the "_selectedObject" via knockout binding to visualize the node appearance in the property panel.
*/
function updateNode(args, prop, item) {
    var option = {};
    var selectedObject = diagram._selectedObject;
    switch (prop) {
        case "width":
            if (!item.isLane)
                option.width = args.value;
            break;
        case "height":
            if (!item.isLane)
                option.height = args.value;
            break;
        case "aspectRatio":
            option.constraints = item.constraints ^ ej.datavisualization.Diagram.NodeConstraints.AspectRatio;
            break;
        case "offsetX":
            option.offsetX = args.value;
            break;
        case "offsetY":
            option.offsetY = args.value;
            break;
        case "borderWidth":
            selectedObject.borderThickness = option.borderWidth = Number(String(args.text.trim()).substring(0, (args.text.trim()).length - 3));
            break;
        case "gradient":
            if (item.type != "text") {
                option.gradient = null;
                selectedObject.gradientBackgroundColor = "#F9F9F9";
                if (!item.gradient) {
                    option.gradient = item.type === "group" ? this.getNewGradient(selectedObject.fillColor) : this.getNewGradient(item.fillColor);
                    selectedObject.gradientBackgroundColor = "#28B1BF";
                }
            }
            break;
        case "lock":
            {
                var nodeConstraints = ej.datavisualization.Diagram.NodeConstraints;
                var constraints;
                selectedObject.lockFontColor = !selectedObject.lock ? "#FFFFFF" : "#7F7F7F";
                selectedObject.lockBackgroundColor = !selectedObject.lock ? "#28B1BF" : "#F9F9F9";
                constraints = !selectedObject.lock ? nodeConstraints.Select | nodeConstraints.PointerEvents : nodeConstraints.Default;
                if (item.constraints & nodeConstraints.AspectRatio)
                    constraints = constraints | nodeConstraints.AspectRatio;
                if (item.constraints & nodeConstraints.Shadow)
                    constraints = constraints | nodeConstraints.Shadow;
                option.constraints = constraints;
                selectedObject.lock = !selectedObject.lock;
                break;
            }
        case "fillColor":
            selectedObject.fillColor = option.fillColor = args.style.backgroundColor;
            if (item.gradient != null) {
                option.gradient = this.getNewGradient(args.style.backgroundColor);
            }
            break;
        case "opacity":
            option.opacity = args.value / 100;
            break;
        case "borderColor":
            selectedObject.stroke = option.borderColor = args.style.backgroundColor;
            break;
    }
    if (Object.keys(option).length > 0) {
        /* update the node properties at runtime using diagram client side API method "updateNode". */
        diagram.updateNode(item.name, option);
        /* binding the selected object to visualize the node appearance in the property panel.  */
        ko.applyBindings(diagram._selectedObject);
    }
    else
        this.updateText(args, prop, item);
}

/*
    The below method is used to customize the connector appearance at runtime by using the diagram client side API method.
    Also it will update the "_selectedObject" via knockout binding to visualize the connector appearance in the property panel.
*/
function updateConnector(args, prop, item) {
    var option = {};
    var selectedObject = diagram._selectedObject;
    switch (prop) {
        case "lineColor":
            option.sourceDecorator = {};
            option.targetDecorator = {};
            selectedObject.lineColor = option.lineColor = args.style.backgroundColor;
            option.sourceDecorator.borderColor = option.targetDecorator.borderColor = args.style.backgroundColor;
            option.sourceDecorator.fillColor = option.targetDecorator.fillColor = args.style.backgroundColor;
            break;
        case "lineWidth":
            option.sourceDecorator = {};
            option.targetDecorator = {};
            selectedObject.lineWidth = option.lineWidth = Number(String(args.text.trim()).substring(0, (args.text.trim()).length - 3));
            option.sourceDecorator.width = option.lineWidth * item.sourceDecorator.width / item.lineWidth;;
            option.sourceDecorator.height = option.lineWidth * item.sourceDecorator.height / item.lineWidth;
            option.targetDecorator.width = option.lineWidth * item.targetDecorator.width / item.lineWidth;
            option.targetDecorator.height = option.lineWidth * item.targetDecorator.height / item.lineWidth;
            break;
        case "headDecorator":
        case "tailDecorator":
            var decorator = new Object();
            switch (String(args.itemId)) {
                case "0":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "none", fillColor: item.lineColor });
                    break;
                case "1":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "arrow", fillColor: item.lineColor });
                    break;
                case "2":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "diamond", fillColor: item.lineColor });
                    break;
                case "3":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "circle", fillColor: item.lineColor });
                    break;
                case "4":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "path", pathData: "M 376.892,225.284L 371.279,211.95L 376.892,198.617L 350.225,211.95L 376.892,225.284 Z", fillColor: item.lineColor });
                    break;
                case "5":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "arrow", fillColor: "white" });
                    break;
                case "6":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "diamond", fillColor: "white" });
                    break;
                case "7":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "circle", fillColor: "white" });
                    break;
                case "8":
                    decorator = ej.datavisualization.Diagram.Decorator({ shape: "path", pathData: "M 376.892,225.284L 371.279,211.95L 376.892,198.617L 350.225,211.95L 376.892,225.284 Z", fillColor: "white" });
                    break;
            }
            if (prop === "headDecorator") {
                decorator.width = item.sourceDecorator.width;
                decorator.height = item.sourceDecorator.height;
                decorator.borderColor = item.lineColor;
                option.sourceDecorator = decorator;
            }
            else {
                decorator.width = item.targetDecorator.width;
                decorator.height = item.targetDecorator.height;
                decorator.borderColor = item.lineColor;
                option.targetDecorator = decorator;
            }
            break;
        case "lineType":
            switch (args.text.trim()) {
                case "Straight":
                case "Bezier":
                case "Orthogonal":
                    var value = args.text.trim().toLowerCase();
                    option.segments = [{ type: value }];
                    break;
            }
            break;
        case "lineStyle":
            switch (String(args.itemId)) {
                case "0":
                    option.lineDashArray = "0,0";
                    break;
                case "1":
                    option.lineDashArray = "10,10";
                    break;
                case "2":
                    option.lineDashArray = "2,2";
                    break;
                case "3":
                    option.lineDashArray = "20,5,5,5";
                    break;
            }
            break;
        case "Lock":
            var constraints;
            var connectorconstraints = ej.datavisualization.Diagram.ConnectorConstraints;
            constraints = !selectedObject.lock ? connectorconstraints.Select | connectorconstraints.PointerEvents : connectorconstraints.Default;
            option.constraints = constraints;
            selectedObject.lock = !selectedObject.lock;
            break;
        case "cornerRadius":
            option.cornerRadius = args.value;
            break;
    }
    if (Object.keys(option).length > 0) {
        /* update the node properties at runtime using diagram client side API method "updateConnector". */
        diagram.updateConnector(item.name, option);
        if (prop === "ConnectorType") {
            /* While changing the connector type, connector segments will be rendered and selection will be updated. Here we retain the property panel state and diagram size. */
            setDiagramSize(true);
            document.getElementById("propertyEditor").style.display = "block";
        }
        /* binding the selected object to visualize the connector appearance in the property panel.  */
        ko.applyBindings(diagram._selectedObject);
    }
    else
        this.updateText(args, prop, item);

}

/*
    The below method is used to customize the label appearance at runtime by using the diagram client side API method.
    Also it will update the "_selectedObject" via knockout binding to visualize the label appearance in the property panel.
*/
function updateText(args, prop, item) {
    var option = {};
    var label = item.type === "text" ? item.textBlock : item.labels[0];
    var selectedObject = diagram._selectedObject;
    switch (prop) {
        case "fontColor":
            selectedObject.labelFontColor = option.fontColor = args.style.backgroundColor;
            break;
        case "bold":
            option.bold = selectedObject.bold = label ? !label.bold : !selectedObject.bold;
            selectedObject.boldBackgroundColor = option.bold ? "#28B1BF" : "#F9F9F9";
            selectedObject.boldFontColor = option.bold ? "#FFFFFF" : "#7F7F7F";
            break;
        case "italic":
            option.italic = selectedObject.italic = label ? !label.italic : !selectedObject.italic;
            selectedObject.boldBackgroundColor = option.italic ? ko.observable("#28B1BF") : ko.observable("#F9F9F9");
            selectedObject.boldFontColor = option.italic ? ko.observable("#FFFFFF") : ko.observable("#7F7F7F");
            break;
        case "fontSize":
            option.fontSize = Number(args.text.trim());
            break;
        case "fontStyle":
            option.fontFamily = args.text;
            break;
        case "left":
        case "right":
        case "center":
            {

                selectedObject.textLeftAlign = selectedObject.textRightAlign = selectedObject.textCenterAlign = false;
                selectedObject.textLeftAlignFontColor = selectedObject.textRightAlignFontColor = selectedObject.textCenterAlignFontColor = "#7F7F7F";
                selectedObject.textLeftAlignBackgroundColor = selectedObject.textRightAlignBackgroundColor = selectedObject.textCenterAlignBackgroundColor = "#F9F9F9";
                if (!(item.isSwimlane || item.isLane)) {
                    option.horizontalAlignment = prop;
                    option.textAlign = prop;
                    if (prop == "left") {
                        selectedObject.textLeftAlign = true;
                        selectedObject.textLeftAlignFontColor = "#FFFFFF";
                        selectedObject.textLeftAlignBackgroundColor = "#28B1BF";
                        option.offset = ej.datavisualization.Diagram.Point(0, 0.5);
                    }
                    else if (prop == "right") {
                        selectedObject.textRightAlign = true;
                        selectedObject.textRightAlignFontColor = "#FFFFFF";
                        selectedObject.textRightAlignBackgroundColor = "#28B1BF";
                        option.offset = ej.datavisualization.Diagram.Point(1, 0.5);
                    }
                    else {
                        selectedObject.textCenterAlign = true;
                        selectedObject.textCenterAlignFontColor = "#FFFFFF";
                        selectedObject.textCenterAlignBackgroundColor = "#28B1BF";
                        option.offset = ej.datavisualization.Diagram.Point(0.5, 0.5);
                    }
                }
            }
            break;

    }
    if (Object.keys(option).length > 0) {
        /* update the label properties at runtime using diagram client side API method "updateLabel". */
        if (item.type === "text") {
            /* if node type is "text", then will update the textblock or else will update the node's labels property. */
            diagram.updateLabel(item.name, item.textBlock, option);
        }
        else {
            for (var i = 0; i < item.labels.length; i++) {
                diagram.updateLabel(item.name, item.labels[i], option);
            }
        }
        /* binding the selected object to visualize the connector appearance in the property panel.  */
        ko.applyBindings(diagram._selectedObject);
    }
}

/* The below method is used to get the gradient style for node based on its fillColor */
function getNewGradient(value) {
    var gradient = {
        type: "linear", x1: 0, x2: 21, y1: -50, y2: 115, stops: [
        { color: "white", offset: -200 }, { color: value, offset: 230 }]
    };
    return gradient;
}
/* 
    The below methods will be triggered when mouse hover/leave on the property panel controls. 
    It will call the "MouseEnter" method to customize the background and border color of the control.
*/
function MouseEnterOnDiv(args, prop, fontclass) {
    diagram._selectedObject.MouseEnter(args, prop, true);
}

function MouseLeaveFromDiv(args, prop, fontclass) {
    diagram._selectedObject.MouseEnter(args, prop, false);
}

/*
    The below is used to maintain the dropdown popup status whether its in open/closed state.
    When mouse hover on the property panel control, if dropdown is in opened state, we wont customize the background apperance.
*/
function dropdownOpened(args) {
    diagram._selectedObject.setPopupDivColor(args, diagram._selectedObject, true);
}

function dropdownClosed(args) {
    diagram._selectedObject.setPopupDivColor(args, diagram._selectedObject, false);
}

function arrangeTick(args) {
    if (args.tickInterval % 100 == 0) {
    }
    else if (args.tickInterval % 50 == 0) {
        args.tickLength = 12.5
    }
}