//#region Diagram client side events

/*
    The selectionChange method will gets triggered when the selection is changed in the diagram. 
    The purpose of registering this event is explained in this method.
*/

function selectionChange(args) {
    if (args.state === "changing") {
        var type;
        if (args.selectedItems.length) {
            //gets the selected object type.
            type = args.selectedItems[0].type;
            if (!(args.selectedItems.length > 1) && type === "connector") {
                /*
                    if selected object is connector, set the connectors tab item avaiable in the property panel should be active.
                */
                $("#tabContent").ejTab("option", "selectedItemIndex", 3);
            }
            //User Handles associated with the selector will be shown based on the selection.
            ShowHandles(diagram, args);
        }
        else {
            if (!diagram._selectedObject.pinned) {
                document.getElementById("propertyEditor").style.display = "none";
            }
        }
        /*
            Toolbar visibility will be updated based on the selection and its used to perform diagram functionalities such as
            clipboard commands, align commands at run time.
        */
        ToolBarVisibility(args, type);
        if (diagram._selectedObject.pinned) {
            /*
                The selected object properties will be bind to property panel only if property panel is pinned state (i,e only in visible state).
                if property panel not in visible state, selected object properties  will be bind when click on the settings user handle.
                You can refer user handles specific information to method.js file.
            */
            ko.applyBindings(diagram._selectedObject.select(args.selectedItems));
        }
        /*
            Diagram Size will be updated based on the property panel visibility.
        */
        setDiagramSize(diagram._selectedObject.pinned);
    }
}

/*
    The below method is used to define the toolbar visibility based on selection changing in the diagram.
*/
function ToolBarVisibility(args, type) {
    document.getElementById("deleteToolbarItem").style.display = "none";
    document.getElementById("groupToolbarItem").style.display = "none";
    document.getElementById("alignmentToolbarItem").style.display = "none";
    document.getElementById("orderToolbarItem").style.display = "none";
    document.getElementById("ungroupToolbarItem").style.display = "none";
    document.getElementById("artboardToolbarItem").style.display = "none";
    // setting the toolbar visibility if none of the object gets selected.
    if (args.changeType === "remove") {
        document.getElementById("artboardToolbarItem").style.display = "block";
    }
    else {
        // setting the toolbar visibility if multiple object gets selected.
        if (args.selectedItems.length > 1) {
            document.getElementById("deleteToolbarItem").style.display = "block";
            document.getElementById("groupToolbarItem").style.display = "block";
            document.getElementById("alignmentToolbarItem").style.display = "block";
        }
            // setting the toolbar visibility if single object gets selected.
        else {
            document.getElementById("deleteToolbarItem").style.display = "block";
            document.getElementById("orderToolbarItem").style.display = "block";
            if (type == "group")
                document.getElementById("ungroupToolbarItem").style.display = "block";
        }
    }
}

/*
    The below method is used to decide the user handles visibility based on selection changing in the diagram.
*/
function ShowHandles(diagram, args) {
    var handles = diagram.model.selectedItems.userHandles;
    for (var i = 0; i < handles.length; i++) {
        handles[i].visible = false;
    }
    if (args.selectedItems.length) {
        var selectedItem = args.selectedItems[0];
        if (selectedItem && !selectedItem.isPhase) {
            for (var i = 0; i < handles.length; i++) {
                var handle = handles[i];
                if (handle.name === "Settings") {
                    /*
                        Except settings handle, remaining handles visiblity will be set to false 
                        if selected object is an connector.
                    */
                    handle.visible = true;
                }
                if (!(selectedItem && selectedItem.type === "connector")) {
                    if (handle.name === "Clone") {
                        //Clone handle will be set to true by default only if selected object is not a connector.
                        handle.visible = true;
                    }
                    else if (handle.name === "Label") {
                        if (!(args.selectedItems.length > 1)) {
                            if (selectedItem.labels[0] && selectedItem.labels[0].text.length > 0) {
                                //label handle will be set to true by only if selected object has label and its text length greater than 0.
                                handle.visible = true;
                            }
                        }
                    }
                    else if (handles[i].name == "Line") {
                        if (!(args.selectedItems.length > 1 || selectedItem.type == "group")) {
                            //line handle will be set to true by only if selected object is not an group.
                            handles[i].visible = true;
                        }
                    }
                }
            }
            if (args.selectedItems.length == 1) {
                /*
                    The client side API method "updateUserHandles" is used to customize user handle appearance at runtime.
                    We have called this method to update the user handles visibility.
                */
                diagram.updateUserHandles(args.selectedItems[0]);
            }
        }
    }
}

/*
    The drag method will gets triggered while dragging any node/group/connector in the diagram. 
    The purpose of registering this event to know whether diagram gets modified or not. 
    The diagram state will be maintained in the global variable named as "_modified".
*/

function drag(args) {
    if (args.element && diagram.getObjectType(args.element) == "node") {
        //sets the diagram state.
        diagram._modified = true;
        //bind the selected object offsetX and offsetY values to the property panel via Knockout binding object.
        diagram._selectedObject.nodeDimensionChanging(args.element);
    }
}

/*
    The drop method will gets triggered when drag and drop the object from symbol palette to diagram. 
    The purpose of registering this event, if any of the drawing tools is activated on dropping, 
    it will be reset to default tool.
*/

function drop(args) {
    var node = args.element;
    if (!(diagram.model.tool & ej.datavisualization.Diagram.Tool.SingleSelect)) {
        //Resetting to default tool.
        diagram.update({ tool: ej.datavisualization.Diagram.Tool.SingleSelect | ej.datavisualization.Diagram.Tool.MultipleSelect });
    }
    //reset the selected tool in toolbar list.
    ToolbarCommandEnabled("MoveTool_Tool");
}

/*
    The nodeSizeChanging method will gets triggered while resizing any node/group in the diagram. 
    Like drag event, purpose of registering this event to know whether diagram gets modified or not. 
*/

function nodeSizeChanging(args) {
    updateIntelligentNode(args);
    if (args.element) {
        //sets the diagram state.
        diagram._modified = true;
        if (diagram.getObjectType(args.element) == "node" && args.element.type !== "pseudoGroup") {
            //bind the selected object offsetX, offsetY, width and height values to the property panel via Knockout binding object.
            diagram._selectedObject.nodeDimensionChanging(args.element);
        }
    }
}
/*
    The dragEnter method will gets triggered while dragging the shape from symbol palette and start entering into diagram. 
    The purpose of registering this event is explained in this method.
*/
function dragEnter(args) {
    var node = args.element;
    if (node) {
        if (node && node.isLane) {
            /*
               while dragging the lanes from symbolo palette, we have set the minimum size for lane.
            */
            if (node.orientation == "horizontal") {
                node.minHeight = 120;
                node.minWidth = 550;
            }
            else if (node.orientation == "vertical") {
                node.minHeight = 550;
                node.minWidth = 120;
            }
        }
        else if (!node.isSwimlane && !node.isLane) {
            /*
               while dragging any object except lane from symbolo palette, set the label mode to edit to start the editing once its dropped.
               if the object has no labels, we have added a label by default and set the mode to edit.
            */
            if (node.labels && !node.labels.length) {
                node.labels.push(ej.datavisualization.Diagram.Label({ mode: ej.datavisualization.Diagram.LabelEditMode.Edit }));
                node.labels[0].fontColor = "black";
            }
            node.labels[0].mode = ej.datavisualization.Diagram.LabelEditMode.Edit;
        }

        if (node.type !== "connector" && !node.isLane && !node.isSwimlane) {
            /*
                While dragging from symbol palette, set the node size as same as preview size.
                This is due to we have rendered the node in symbol palette with 45*45 size. 
                While dragging from symbol palette, it will be drawn with 100 * 100 size.
            */
            updateNodeSize(node);
            /*
                In diagram builder, we have provided some built-in intelligent shapes. Each intelligent shapes will have some
                specific behaviour. So when define the size to that shapes, it will be customized automatically.
            */
            editIntelligentShapes(node);
        }
    }
}

/*
    In diagram builder, we have provided some built-in intelligent shapes. Each intelligent shapes will have some
    specific behaviour. So when define the size to that shapes, it will be customized automatically.
*/
function editIntelligentShapes(node) {
    var str = node.name.toString();
    var points;
    switch (str) {
        case str.match("\\b5star"):
            points = _renderStar(node, 5, 25);
            break;
        case str.match("\\b16star"):
            points = _renderStar(node, 16, 25);
            break;
        case str.match("\\b6star"):
            points = _renderStar(node, 6, 25);
            break;
        case str.match("\\bLeftArrow") && !str.match("\\bCurvedLeftArrow") && !str.match("\\bJumpingLeftArrow"):
            points = _renderLeftArrow(node);
            break;
        case str.match("\\bRightArrow") && !str.match("\\bCurvedRightArrow") && !str.match("\\bJumpingRightArrow"):
            points = _renderRightArrow(node);
            break;
        case str.match("\\bParallelogram"):
            points = _renderParallelogram(node);
            break;
        case str.match("\\bbendsingleArrow"):
            points = _renderBendSingleArrow(node);
            break;
        case str.match("\\bbenddoubleArrow"):
            points = _renderBenddoubleArrow(node);
            break;
        case str.match("\\bDoubleArrow"):
            node.width = 120;
            node.height = 60;
            var points = _renderDoubleArrow(node);
            break;
        case str.match("\\bColumn"):
            points = _renderGridColumn(node);
            break;
        case str.match("\\bRow"):
            points = _renderGridRow(node);
            break;
        case str.match("\\bHexagon"):
            points = _renderOctagon(node);
            break;
        case str.match("\\bManualInput"):
            node.width = 100;
            node.height = 80;
            points = _updateManualInput(node);
            break;
        case str.match("\\bTwoSideFlatCorner"):
            node.width = 100;
            node.height = 80;
            var a = 8;
            points = _updateTwoSideFlatCornerPoints(node, a);
            break;
        case str.match("\\bDelay"):
            node.width = 100;
            node.height = 60;
            var a = 30;
            points = _updateDelayPoints(node, a);
            break;
        case str.match("\\bUTurnArrow"):
            var a = 20;
            node.width = 70;
            points = _updateUTurnArrowPoints(node, a);
            break;
        case str.match("\\bTwoSideRoundedCorner"):
            node.width = 100;
            node.height = 80;
            var a = 20;
            points = _updateTwoSideRoundedCornerPoints(node, a);
            break;
        case str.match("\\bRRectangle"):
            node.width = 100;
            node.height = 80;
            var a = 10;
            points = _updateRoundedRectanglePoints(node, a);
            break;
        case str.match("\\bExternaldata"):
            node.width = 120;
            node.height = 60;
            points = _updateExternalDataPoints(node);
            break;
        case str.match("\\bStart") && str.match("\\bStart").index == 0:
            node.width = 150;
            node.height = 60;
            var a = 30;
            points = _updateStartPoints(node, a);
            break;
        case str.match("\\bProceed"):
            node.width = 80;
            node.height = 80;
            var a = 40;
            points = _updateProceedPoints(node, a);
            break;
        case str.match("\\bDisplay"):
            node.width = 150;
            node.height = 80;
            var a = 40;
            points = _updateDisplayPoints(node, a);
            break;
        case str.match("\\bPrepartion"):
            node.width = 140;
            node.height = 60;
            var a = 20;
            points = _updatePrepartionPoints(node, a);
            break;
        case str.match("\\bCard"):
            node.width = 100;
            node.height = 80;
            var a = 20;
            var points = _updateCardPoints(node, a);
            break;
        case str.match("\\bLoopLimit"):
            node.width = 100;
            node.height = 60;
            var a = 10;
            var points = _updateLoopLimitPoints(node, a);
            break;
        case str.match("\\bCallOutleftArrow"):
            var a = 75;
            var points = _updateCallOutLeftArrowPoints(node, a);
            break;
        case str.match("\\bCallOutrightArrow"):
            var a = 75;
            var points = _updateCallOutRightArrowPoints(node, a);
            break;
        case str.match("\\bCallOutupArrow"):
            var a = 75;
            var points = _updateCallOutUpArrowPoints(node, a);
            break;
        case str.match("\\bCallOutDownArrow"):
            var a = 75;
            var points = _updateCallOutDownArrowPoints(node, a);
            break;
        case str.match("\\bChevronsWide"):
            var a = 20;
            node.width = 80;
            node.height = 40;
            var points = _updateChevronsWidePoints(node, a);
            break;
        case str.match("\\bLStylishedHeadwithFanTail"):
            node.width = 100;
            node.height = 40;
            points = _updateLStylishedHeadwithFanTailPoints(node);
            break;
        case str.match("\\bBentleftarrow"):
            points = _updateBentLeftArrow(node);
            break;
        case str.match("\\bChevronsLeftandTopArrow"):
            points = _updateChevronsLeftandTopArrow(node);
            break;
        case str.match("\\bChevronsTopArrow"):
            points = _updateChevronsTopArrow(node);
            break;
        case str.match("\\bbendrightArrow"):
            points = _updatebendrightArrow(node);
            break;
        case str.match("\\bCircularArrow") || str.match("\\bCurvedUpArrow") || str.match("\\bCurvedDownArrow"):
            node.width = 120;
            node.height = 60;
            break;
        case str.match("\\bCurvedRightArrow") || str.match("\\bCurvedLeftArrow"):
            node.width = 60;
            node.height = 120;
            break;
        case str.match("\\bDecision"):
            node.width = 150;
            node.height = 70;
            break;
        case str.match("\\bDirectdata"):
            node.width = 120;
            node.height = 50;
            break;
        case str.match("\\bprocess"):
            node.width = 115;
            node.height = 60;
            break;
        case "message":
        case "document":
        case "multidocument":
        case "directdata":
        case "papertap":
        case "predefinedprocess":
            node.height = 75;
            break;
    }
    if (points) {
        node.shape = "path"; node.pathData = points;
    }
    else {
        switch (node.shape) {
            case "message":
            case "document":
            case "multidocument":
            case "directdata":
            case "papertap":
            case "predefinedprocess":
                node.height = 75;
                break;
        }
    }
}

/*
    The method is used to set the preview size as size of the node while dragging from symbol palette.
*/
function updateNodeSize(node) {
    var str = node.name;
    node.height = 100;
    node.width = 100;
}


/*
    The nodeCollectionChange method will gets triggered while adding any node or group in the diagram. 
    The purpose of registering this event to know whether diagram gets modified or not. 
    The diagram state will be maintained in the global variable named as "_modified".
*/

function nodeCollectionChange(args) {
    var node = args.element;
    if (args.state === "changing" && args.changeType == "insert") {
        diagram._modified = true;
        if (node && node.type == "text") {
            if (node.height < 35) {
                //While creating the text node from text tool available in the toolbar option, if its height less than 35, will set the default height.
                node.height = 35;
            }
        }
    }
}

/*
    The connectorCollectionChange method will gets triggered while adding any node or group in the diagram. 
    Like nodeCollectionChange, The purpose of registering this event to know whether diagram gets modified or not. 
*/

function connectorCollectionChange(args) {
    if (args.state === "changing" && args.changeType == "insert") {
        diagram._modified = true;
        if (args.element.segments) {
            args.element.cornerRadius = 8;
            if (args.element.lineWidth < 2) {
                //While adding the new connector, if its lineWidth less than 2, it will set to 2.
                args.element.lineWidth = 2;
            }
            if (args.element.labels && args.element.labels.length > 0)
                args.element.labels[0].fillColor = "white";
        }
    }
}

/*
    The textChange method will gets triggered once the label editing is completed. 
    The purpose of registering this event to know whether diagram gets modified or not. 
*/

function textChange(args) {
    diagram._modified = true;
}

/*
    The contextMenuBeforeOpen method will gets triggered before opening the context menu. 
    The purpose of registering this event to show list of options based on the object for its customization.
    By default, we have provided some default options to make the changes in the diagram like cut, copy, paste etc.
    In diagram builder we have provided some addtional items to customize the bpmn shape.
    The additional items will be shown only if the object is BPMN shape.
*/
function contextMenuBeforeOpen(args) {
    //Get The Context Menu Items 
    var menuItems = diagram.model.contextMenu.items;
    //Get The Context Menu Item Regin
    var context = $("#DiagramContent_contextMenu").ejMenu("instance");
    var shape;
    if (diagram.model.enableContextMenu) {
        /*
            By default, hiding all additional context menu items. It will be shown based on the different type of bpmn shapes.
            Here "DiagramContent" is an id of the diagram and "_contextMenu" is a prefix added with the each context menu item.
        */
        for (var i = 0; i < menuItems.length; i++) {
            document.getElementById("DiagramContent_contextMenu_" + menuItems[i].name).style.display = "none";
        }
    }
    if (diagram.model.selectedItems.children.length > 0) {
        if (diagram.model.selectedItems.children[0].type === "bpmn")
            shape = diagram.model.selectedItems.children[0].shape;
        switch (shape) {
            case "gateway":
                document.getElementById("DiagramContent_contextMenu_gateway").style.display = "block";
                break;
            case "dataobject":
                document.getElementById("DiagramContent_contextMenu_dataObject").style.display = "block";
                document.getElementById("DiagramContent_contextMenu_collection").style.display = "block";
                break;
            case "activity":
                document.getElementById("DiagramContent_contextMenu_loop").style.display = "block";
                document.getElementById("DiagramContent_contextMenu_compensation").style.display = "block";
                document.getElementById("DiagramContent_contextMenu_activityType").style.display = "block";
                if (diagram.model.selectedItems.children[0].activity == "task") {
                    document.getElementById("DiagramContent_contextMenu_taskType").style.display = "block";
                    document.getElementById("DiagramContent_contextMenu_taskCall").style.display = "block";
                }
                else {
                    document.getElementById("DiagramContent_contextMenu_adhoc").style.display = "block";
                    document.getElementById("DiagramContent_contextMenu_boundary").style.display = "block";
                    document.getElementById("DiagramContent_contextMenu_subProcessType").style.display = "block";
                    if (diagram.model.selectedItems.children[0].subProcess.type == "event") {
                        document.getElementById("DiagramContent_contextMenu_eventType").style.display = "block";
                        document.getElementById("DiagramContent_contextMenu_triggerResult").style.display = "block";
                    }
                }
                break;
            case "event":
                document.getElementById("DiagramContent_contextMenu_eventType").style.display = "block";
                document.getElementById("DiagramContent_contextMenu_triggerResult").style.display = "block";
                break;
        }
    }
}

/*
    The contextMenuClick method will gets triggered when a context menu item is clicked. 
    The purpose of registering this event to customize the diagram or shapes via when click on the context menu item.
    For built-in context menu items, there is no need to define anything in context menu click event.
    For custom context menu items, you can use contextMenuClick event to do what type of action needs to be done when click on context menu item.
    In diagram builder, we have exposed this by providing the option to customize the BPMN shape.
*/

function contextMenuClick(args) {
    var menu = diagram.model.contextMenu.items;
    var menuselect = args.parentText;
    var node = diagram.selectionList[0];
    var options = {}, loop;
    if (node && node.type === "bpmn") {
        options.task = {};
        options.subProcess = {};
        options.data = {};
        switch (menuselect) {
            case "Trigger Result":
                if (node.shape == "activity")
                    options.subProcess.trigger = args.text.toLowerCase();
                else
                    options.trigger = args.text.toLowerCase();
                break;
            case "Event Type":
                var obj;
                if (args.text === "Interrupting Start")
                    obj = "start";
                else if (args.text === "Interrupting Intermediate")
                    obj = "intermediate";
                else if (args.text === "End")
                    obj = "end";
                else
                    obj = args.text.replace(" ", "").toLowerCase();
                if (node.shape == "activity")
                    options.subProcess.event = obj;
                else
                    options.event = obj;
                break;
            case "Gateway":
                if (args.text === "None")
                    options.gateway = "none";
                else
                    options.gateway = args.text.replace(" ", "").toLowerCase();
                break;
            case "Data Object":
                if (args.text === "noDataObject")
                    options.data.type = "none";
                else
                    options.data.type = args.text.toLowerCase();
                break;
            case "Collection":
                options.data.collection = (args.text === "Collection") ? true : false;
                break;
            case "Loop":
                if (args.text === "None")
                    loop = "none";
                else if (args.text === "Standard")
                    loop = "standard";
                else if (args.text === "Parallel Multi-Instance")
                    loop = "parallelmultiinstance";
                else if (args.text === "Sequence Multi-Instance")
                    loop = "sequencemultiinstance";
                if (node.activity == "task")
                    options.task.loop = loop;
                else
                    options.subProcess.loop = loop;
                break;
            case "Task Type":
                options.task.type = args.text.replace(" ", "").toLowerCase();
                break;
            case "Subprocess Type":
                options.subProcess.events = [];
                if (args.text === "Default")
                    options.subProcess.type = "none";
                else if (args.text === "Event")
                    options.subProcess.type = "event";
                else if (args.text === "Transaction") {
                    options.subProcess.type = "transaction";
                    options.subProcess.events = [{ event: "intermediate", trigger: "cancel", offset: { x: 0.25, y: 1 } }, { event: "intermediate", trigger: "error", offset: { x: 0.75, y: 1 } }];
                }
                break;
            case "Compensation":
                if (node.activity == "task")
                    options.task.compensation = (args.text === "Compensation") ? true : false;
                else
                    options.subProcess.compensation = (args.text === "Compensation") ? true : false;
                break;
            case "Ad-Hoc":
                options.subProcess.adhoc = (args.text === "Ad-Hoc") ? true : false;
                break;
            case "Call":
                options.task.call = (args.text === "Call") ? true : false;
                break;
            case "Boundary":
                options.subProcess.boundary = args.text.toLowerCase();
                break;
            case "Activity Type":
                options.activity = (args.text === "Task") ? "task" : "subprocess";
                break;
            case "Direction":
                if (node.shape === "associationflow") {
                    if (args.text === "One")
                        node.direction = "unidirctional";
                    else if (args.text === "Both")
                        node.direction = "biidirctional"
                    else if (args.text === "None")
                        node.direction = "none";
                }
                break;
            default:
                return;
        }

        /*
            using the clienSide API method "updateNode" to update the BPMN Shapes appearance at runtime.
        */
        diagram.updateNode(node.name, options);
    }
}

//#endregion

/*
    The menuBarClick method will gets triggered when a menu item is clicked. 
    The purpose of registering this event is to execute the diagram client side API methods 
    such as undo, redo, save, load, cut, copy, paste etc at run time.
*/

function menuBarClick(sender) {
    var option = sender.events.text.trim();
    if (!(option == "Align" || option == "Grouping" || option == "Order" || option == "Resize" || option == "File" || option == "Edit" || option == "View" || option == "Actions" || option == "Export" || option == "Print" || option == "Save" || option == "Open" || option == "New")) {
        diagram._modified = true;
    }
    switch (option) {
        case "Export":
            $("#exportDialog").ejDialog("open");
            $("#exportDialog").focus();
            break;
        case "Print":
            diagram.print();
            break;
        case "Undo":
            diagram.undo();
            break;
        case "Redo":
            diagram.redo();
            break;
        case "Cut":
            diagram.cut();
            break;
        case "Copy":
            diagram.copy();
            break;
        case "Paste":
            diagram.paste();
            break;
        case "Delete":
            diagram.remove();
            break;
        case "Select All":
            diagram.selectAll();
            break;
        case "Left":
        case "Right":
        case "Center":
        case "Top":
        case "Bottom":
        case "Middle":
            diagram.align(option.toLowerCase());
            break;
        case "Group":
            diagram.group();
            break;
        case "Ungroup":
            diagram.ungroup();
            break;
        case "Bring To Front":
            diagram.bringToFront();
            break;
        case "Bring Forward":
            diagram.moveForward();
            break;
        case "Send To Back":
            diagram.sendToBack();
            break;
        case "Send Backward":
            diagram.sendBackward();
            break;
        case "Same Size":
            diagram.sameSize();
            break;
        case "Same Height":
            diagram.sameHeight();
            break;
        case "Same Width":
            diagram.sameWidth();
            break;
        case "Space Down":
            diagram.spaceDown();
            break;
        case "Space Across":
            diagram.spaceAcross();
            break;
        case "Grid Lines":
            var snapConstraints = diagram.model.snapSettings.snapConstraints ^ ej.datavisualization.Diagram.SnapConstraints.ShowLines;
            var style = snapConstraints & ej.datavisualization.Diagram.SnapConstraints.ShowLines ? "block" : "none";
            //Define The CSS Display For Grid Lines
            $("#GridLines").find("span").css("display", style);
            //Update The Snap Constraints
            $("#DiagramContent").ejDiagram({ snapSettings: { snapConstraints: snapConstraints } });
            break;
        case "Snap To Grid":
            var snapConstraints = diagram.model.snapSettings.snapConstraints ^ ej.datavisualization.Diagram.SnapConstraints.SnapToLines;
            var style = snapConstraints & ej.datavisualization.Diagram.SnapConstraints.SnapToLines ? "block" : "none";
            //Define The CSS Display Style For Snap To Grid
            $("#SnapToGrid").find("span").css("display", style);
            //Update The Snap Constraints
            $("#DiagramContent").ejDiagram({ snapSettings: { snapConstraints: snapConstraints } });
            break;
        case "Overview":
            var displayStyle = "none";
            var sourceId = "";
            if (!diagram._enableOverView) {
                var displayStyle = "block";
                var sourceId = "DiagramContent";
            }
            $("#EnableOverView").find("span").css("display", displayStyle);
            diagram._enableOverView = !diagram._enableOverView;
            $("#Overview-div").css("display", displayStyle);
            $("#Overview").ejOverview({ sourceID: sourceId });
            updateSize(diagram._enableOverView);
            break;
        case "Show Rulers":
            var enableRuler = !diagram.model.rulerSettings.showRulers;
            if (enableRuler) {
                diagram.model.snapSettings.horizontalGridLines.linesInterval = [1.25, 18.75, 0.25, 19.75, 0.25, 19.75, 0.25, 19.75, 0.25, 19.75];
                diagram.model.snapSettings.verticalGridLines.linesInterval = [1.25, 18.75, 0.25, 19.75, 0.25, 19.75, 0.25, 19.75, 0.25, 19.75];
                $("#ShowRulers").find("span").css("display", "block");
            }
            else {
                diagram.model.snapSettings.horizontalGridLines.linesInterval = [.95, 9.05, 0.2, 9.75];
                diagram.model.snapSettings.verticalGridLines.linesInterval = [.95, 9.05, 0.2, 9.75];
                $("#ShowRulers").find("span").css("display", "none");
            }
            $("#DiagramContent").ejDiagram({
                rulerSettings: {
                    showRulers: enableRuler,
                    horizontalRuler: { interval: 10, arrangeTick: "arrangeTick", markerColor: "#505050" },
                    verticalRuler: { interval: 10, arrangeTick: "arrangeTick", markerColor: "#505050" },
                },

            });
            break;
        case "Smart Guide":
            diagram.model.snapSettings.enableSnapToObject = !diagram.model.snapSettings.enableSnapToObject;
            if (diagram.model.snapSettings.enableSnapToObject) {
                $("#SmartGuid").find("span").css("display", "block");
            }
            else {
                $("#SmartGuid").find("span").css("display", "none");
            }
            break;
        case "Save":
            saveDiagram();
            break;
        case "New":
            newDiagram();
            break;
        case "Save As":
            saveDiagram("Save As");
            break;
        case "Open":
            openDialog();
            break;
    }
};

/*
    The toolbarClick method will gets triggered when a toolbar item is clicked. 
    The purpose of registering this event is to execute the diagram client side API methods 
    such as undo, redo, save, load, cut, copy, paste etc at run time.
*/

function toolbarClick(sender) {
    var object;
    var nodes, i;
    var option = sender.currentTarget.id;
    if (!(option == "Pan_Tool"))
        diagram._modified = true;
    switch (option) {
        case "Redo_Tool":
            diagram.redo();
            break;
        case "Undo_Tool":
            diagram.undo();
            break;
        case "Delete_Tool":
            diagram.remove();
            break;
        case "ZoomIn_Tool":
        case "ZoomOut_Tool":
            var zoom1 = ej.datavisualization.Diagram.Zoom();
            //set The Zoom Factor Value
            zoom1.zoomFactor = 0.2;
            //set The Zoom Command (Zoom In Or Zoom Out) value
            zoom1.zoomCommand = option == "ZoomOut_Tool" ? ej.datavisualization.Diagram.ZoomCommand.ZoomOut : ej.datavisualization.Diagram.ZoomCommand.ZoomIn;
            diagram.zoomTo(zoom1);
            break;
        case "FitToPage_Tool":
            diagram.fitToPage();
            break;
        case "Pan_Tool":
        case "MoveTool_Tool":
        case "Text_Tool":
        case "ConnectorDraw_Tool":
        case "ConnectorDraw_Tool_Straight":
            var updateTool;
            //Enable The Tool Bar Visibility
            ToolbarCommandEnabled(option);
            if (option == "Pan_Tool") {
                updateTool = ej.datavisualization.Diagram.Tool.ZoomPan;
            }
            else if (option == "MoveTool_Tool") {
                updateTool = ej.datavisualization.Diagram.Tool.SingleSelect | ej.datavisualization.Diagram.Tool.MultipleSelect;
            }
            else if (option == "Text_Tool") {
                diagram.model.drawType = { type: "text" };
                updateTool = ej.datavisualization.Diagram.Tool.ContinuesDraw;
            }
            else if (option == "ConnectorDraw_Tool") {
                diagram.model.drawType = { type: "connector", segments: [{ type: "orthogonal" }] };
                updateTool = ej.datavisualization.Diagram.Tool.ContinuesDraw;
            }
            else {
                diagram.model.drawType = { type: "connector", segments: [{ type: "straight" }] };
                updateTool = ej.datavisualization.Diagram.Tool.ContinuesDraw;
            }
            diagram.update({ tool: updateTool, drawType: diagram.model.drawType });
            break;
        case "AlignLeft_Tool":
        case "AlignCenter_Tool":
        case "AlignRight_Tool":
        case "AlignBottom_Tool":
        case "AlignMiddle_Tool":
        case "AlignTop_Tool":
            var direction = option.split("_");
            direction[0] = direction[0].substring(5, direction[0].length);
            diagram.align(direction[0].toLowerCase());
            break;
        case "Group_Tool":
            diagram.group();
            break;
        case "UnGroup_Tool":
            diagram.ungroup();
            break;
        case "SendToBack_Tool":
            diagram.sendToBack();
            break;
        case "MoveForward_Tool":
            diagram.moveForward();
            break;
        case "SendBackward_Tool":
            diagram.sendBackward();
            break;
        case "BringToFront_Tool":
            diagram.bringToFront();
            break;
        case "Lock_Tool":
            update(null, "lock");
            break;
    }
    if (option == "ZoomIn_Tool" || option == "ZoomOut_Tool" || option == "FitToPage_Tool") {
        $('#zoompercentage').ejDropDownList("option", "value", String(Math.round(diagram._getCurrentZoom() * 100)) + "%");
    }
}

/*
    The zoomPercentageChange method will gets triggered when zoom percentage changed from drop down list. 
    The purpose of registering this event is to zoom the diagram with different level.
*/

function zoomPercentageChange(args) {
    var currentZoom = diagram._getCurrentZoom();
    if (args.text) {
        switch (args.text) {
            case "400%":
                zoom.zoomFactor = (4 / currentZoom) - 1;
                break;
            case "200%":
                zoom.zoomFactor = (2 / currentZoom) - 1;
                break;
            case "150%":
                zoom.zoomFactor = (1.5 / currentZoom) - 1;
                break;
            case "100%":
                zoom.zoomFactor = (1 / currentZoom) - 1;
                break;
            case "75%":
                zoom.zoomFactor = (0.75 / currentZoom) - 1;
                break;
            case "50%":
                zoom.zoomFactor = (0.5 / currentZoom) - 1;
                break;
            case "25%":
                zoom.zoomFactor = (0.25 / currentZoom) - 1;
                break;
        }
        diagram.zoomTo(zoom);
    }
}


/*
  The toolbarMouseEvent method will be raised when hover on the toolbar items.
  By default, when hover on the toolbar item, default appearance will be shown and it will be defined in ej.web.all.min.css file.
  We have override this appearance in diagram builder sample. 
  Due to some customization done in the below method, we have achieved this in jQuery instead of css.
*/
function toolbarMouseEvent(evt) {
    var target = evt.target;
    if ($(target).hasClass("tb-icon") || $(target).hasClass("ddl-standard"))
        target = evt.target.parentNode;
    var targetClass = target.classList ? target.classList[0] : target.className.split(" ")[0];
    if (targetClass === "tb-item-child") {
        var x = target.style.backgroundColor;
        if (evt.type == "mouseover") {
            if ((x != "rgb(211, 209, 209)")) {
                if (x != "#D3D1D1") {
                    target.style.backgroundColor = "#EAEAEA";
                    target.style.border = "1px solid #808080";
                }
            }
        }
        else if (evt.type == "mouseout") {
            if ((x != "#D3D1D1"))
                target.style.backgroundColor = x != "rgb(211, 209, 209)" ? "#F7F5F5" : "#D3D1D1";
            else
                target.style.backgroundColor = "#D3D1D1";
            target.style.border = "1px solid transparent";
        }
    }
}

/*
    The below method will be triggered while changing the artboard Width and Height from the toolbar.
*/
function setDimension(args) {
    var id = args.model.name;
    var obj = id == "artBoardWidth" ? { pageWidth: args.value } : { pageHeight: args.value }
    //update the diagram.model.pageSettings width and height.
    diagram.updatePageSettings(obj);

    var artBoardWidth = $("#artBoardWidth").data("ejNumericTextbox");
    var artBoardHeight = $("#artBoardHeight").data("ejNumericTextbox");
    artBoardWidth.option("value", diagram.model.pageSettings.pageWidth);
    artBoardHeight.option("value", diagram.model.pageSettings.pageHeight);
}

function setBackgroundColor(args) {
    document.getElementById("backgroundIcon").style.color = args.style.backgroundColor;
    //set the background color of diagram
    diagram.updatePageSettings({ "pageBackgroundColor": args.style.backgroundColor });
}

/*
    The below method is used to create the new Diagram. It will be triggered while click on the "new" menu item.
    Also it will ask for confimation if the diagram gets modified before creating the new diagram.
*/

function newDiagram() {
    if (diagram._modified == true) {
        $("#confirmDialog").ejDialog("open");
        $("#confirmDialog").focus();
    }
    else {
        clearDiagram();
    }
    window.loadFileName = null;
    window.saveFileName = null;
}

/*
    The below method is used to list the all saved Diagram. It will be triggered while click on the "open" menu item.
*/
function openDialog() {
    var hasProperties = false;
    $.each(localStorage, function (key, value) {
        hasProperties = true;
    });
    if (hasProperties) {
        if (document.getElementById("savedDiagramList")) {
            var root = document.getElementById("savedDiagramList");
            root.innerHTML = "";
            /*
                Here we getting the all the diagram stored in the local storage, and creating the saved diagram list like FileTree 
                for visual representation with the help of "listSavedDiagrams" method.
            */
            $.each(localStorage, function (key, value) {
                listSavedDiagrams(key);
            });
        }
        $("#openFileDialog").ejDialog("open");
    } else {
        alert("There is no saved diagram");
    }
}

/*
     The below method is used to open the saveFileDialog to save the diagram . It will be triggered while click on the "save" menu item.
     If its already saved diagram, then it will be restored with existing diagram.
     We have maintained the already saved diagram in Window.SaveFileName variable.
     If you want to save the already saved diagram with another name, you can use the saveAs option.
*/
function saveDiagram(type, isNew) {
    if (isNew)
        this._isNewDiagram = true;
    if (type === "Save As") {
        if (window.loadFileName) {
            // bind the already opened diagram name.
            document.getElementById("txtFileName").value = window.loadFileName;
        }
        else if (window.saveFileName) {
            // bind the last saved diagram name.
            document.getElementById("txtFileName").value = window.saveFileName;
        }
        $("#saveDialog").ejDialog("open");
        $("#SaveFileName").focus();
    }
    else {
        if (!(window.loadFileName) && !(window.saveFileName)) {
            $("#saveDialog").ejDialog("open");
            $("#SaveFileName").focus();
        }
        else if ((window.loadFileName) || (window.saveFileName)) {
            document.getElementById("txtFileName").value = window.loadFileName ? window.loadFileName : window.saveFileName;
            //store the diagram into localStorage.
            saveFileAsString();
        }
    }
}

/*
    The below method is used to store the diagram into localStorage. if your browser doesnt support local storage, then diagram wont be saved.
*/
function saveFileAsString() {
    var fileName = "";
    if (document.getElementById("txtFileName").value) {
        if (localStorage == undefined)
            alert("Your Browser Doesn't support this action.");
        else {
            fileName = document.getElementById("txtFileName").value;
            //By using our client side API method "save", we can get the diagram data.
            saveData = diagram.save(fileName);
            if (this._isNewDiagram) {
                //The diagram will be cleared if we saving the diagram at the time of creating the new diagram.
                clearDiagram();
            }
            $("#saveDialog").ejDialog("close");
            //saving the diagram data in local storage with file name.
            localStorage.setItem(fileName, JSON.stringify(saveData));
        }
    }
    document.getElementById("txtFileName").value = "";
    //store the filename for future references.
    window.saveFileName = fileName;
    window.loadFileName = "";
    //reset the diagram state.
    diagram._modified = false;
    delete this._isNewDiagram;
}


/*
    The below method is used to clear the diagram.
*/
function clearDiagram() {
    diagram.clear();
    $("#confirmDialog").ejDialog("close");
    diagram._modified = false;
}

/*
    The below method is used to create the saved diagram list like FileTree for visual representation.
*/
function listSavedDiagrams(key) {
    var root = document.getElementById("savedDiagramList");
    var li = document.createElement("li");
    li.setAttribute("class", "e-item");

    var div = document.createElement("div");
    div.setAttribute("class", "fileItemHover");
    div.setAttribute("id", key + "_hover");
    /*
        The purpose of binding the below events is indicate the which item gets selected in the file tree and
        highlight when the mouse hover on the item.
    */
    div.onclick = onFileItemSelect;
    div.onmousemove = fileTreeMouseEvt;
    div.onmouseout = fileTreeMouseEvt;

    /*
        The saved diagram name will be rendered with anchor tag.
    */
    var a = document.createElement("a");
    a.setAttribute("style", "pointer-events:none;margin-left:19px;");
    a.setAttribute("class", "e-text CanSelect");
    a.setAttribute("href", "#" + key + "_fileItem");
    a.innerHTML = key;

    div.appendChild(a);

    /*
        The below element is used to remove the saved diagram from the list.
    */
    var checkbox = document.createElement("div");
    checkbox.setAttribute("class", "deleteIconfileItem");
    checkbox.setAttribute("style", "background-image: url('images/Close.png');background-repeat: no-repeat;float: right;height: 18px;margin-right: 12px;width: 20px;display:none;");
    checkbox.onclick = onFileDeleteClick;
    div.appendChild(checkbox);
    li.appendChild(div);
    root.appendChild(li);
}

/*
    The below method is used to highlight the item when mouse hovered on it.
*/
function fileTreeMouseEvt(evt) {
    var target;
    target = $(evt.target).hasClass("deleteIconfileItem") ? evt.target : evt.target.lastChild;
    target.style.display = "block";
    if (evt.type === "mouseout")
        target.style.display = "none";
}

/*
    The below element is used to remove the saved diagram from the list.
*/
function onFileDeleteClick(evt) {
    var delParent = evt.target.parentNode;
    if (delParent) {
        //removes the element.
        delParent.parentNode.removeChild(delParent);
        //removed the item from local storage.
        localStorage.removeItem(evt.target.previousSibling.innerHTML);
    }
}

/*
    The below method is used to indicate to select the saved items from the file tree list.
*/
function onFileItemSelect(evt) {
    var target = evt.target;
    if (window.selectedFileItemName != target.id) {
        $("#" + window.selectedFileItemName).removeClass("selected");
    }
    if (target) {
        $(target).addClass("selected");
        window.selectedFileItemName = target.id;
    }
}

/*
    The below method is used to getting the selectedItem from the file tree and using this getting the saved diagram data from local storage.
    Once we get the data, we can use our client side API "load" method to load the diagram.
*/
function loadDiagram(args) {
    var fileName = (window.selectedFileItemName).split("_")[0];
    //close the dialog box
    $("#openFileDialog").ejDialog("close");
    if (fileName != null && fileName != "") {
        var data = localStorage.getItem(fileName);
        if (data) {
            var jsonData = JSON.parse(data);
            //load the JSON data to diagram
            diagram.load(jsonData);
            window.loadFileName = fileName;
        }
    }
}

/*
    The below method is used to indicate the current activated tool from the list of available drawing tools.
*/
function ToolbarCommandEnabled(id) {
    $('#Pan_Tool').css('background-color', '#F7F5F5');
    $('#MoveTool_Tool').css('background-color', '#F7F5F5');
    $('#Text_Tool').css('background-color', '#F7F5F5');
    $('#ConnectorDraw_Tool').css('background-color', '#F7F5F5');
    $('#ConnectorDraw_Tool_Straight').css('background-color', '#F7F5F5');
    $('#' + id).css('background-color', '#D3D1D1');
}

/*
    We have rendered a slider control behind the overview control. 
    The event will be trigger when the slider control value is changed and its used to increase/decrease the diagram zoom level.
*/
function OnChange(args) {
    if (diagram) {
        var sliderValue = document.getElementsByClassName("sliderValue");
        sliderValue[0].textContent = args.value + "%";
        //gets the current diagram zoom level.
        var currentZoom = diagram.model.scrollSettings.currentZoom;
        $('#zoompercentage').ejDropDownList("option", "value", args.value + "%");
        if (args.value / 100 !== Number(currentZoom.toFixed(2))) {
            var zoom = ej.datavisualization.Diagram.Zoom();
            zoom.zoomFactor = (args.value / 100) / currentZoom;
            if (zoom.zoomFactor > currentZoom)
                zoom.zoomCommand = ej.datavisualization.Diagram.ZoomCommand.ZoomIn;
            else
                zoom.zoomCommand = ej.datavisualization.Diagram.ZoomCommand.ZoomOut;
            //increase/decrease the zoom level by using client side API method.
            diagram.zoomTo(zoom);
        }
    }
}

/*
   The below method is used to export the diagram with different format such as PNG, JPEG, SVG and also with the specific region.
*/
function exportDiagram() {
    var fileName = document.getElementById("txtExportFileName").value;
    //get the region to be exported.
    var region = $("#ddlExportMode").ejDropDownList("instance").selectedTextValue;
    switch (region) {
        case "Content":
            region = "content";
            break;
        case "PageSettings":
            region = "pageSettings";
            break;
    }
    //get the format in which diagram to be exported.
    var format = $("#ddlExportFormat").ejDropDownList("instance").selectedTextValue;
    switch (format) {
        case "JPG":
            format = "jpg";
            break;
        case "PNG":
            format = "png";
            break;
        case "SVG":
            format = "svg";
            break;
        case "BMP":
            format = "bmp";
            break;
    }
    var content = diagram.exportDiagram({
        fileName: fileName, region: region, format: format, mode: "download",
    });
    if (content) alert("received");
    document.getElementById("txtExportFileName").value = "Diagram";
    $("#exportDialog").ejDialog("close");
}


/*
    Triggered after a tab item gets activated. It is used to customize the background color of the tab header when its activated.
*/
function tabItemChanged(args, tab) {
    if (diagram && diagram._selectedObject != undefined) {
        ko.applyBindings(diagram._selectedObject.setTabIconColor(args, tab));
    }
}

/* 
    The purpose of below method, when mouse hover on the controls available in the property panel such as fillColor, borderColor etc.., its background
    color will be highlighted. The below method is used to prevent this highlighter if drop down gets opened.
*/
function popupShown(args) {
    dropdownOpened(args);
}

function popupHide(args) {
    dropdownClosed(args);
}

/*
    The below method will be triggered if we do any changes from the property panel.
*/
function propertyPannelChange(args) {
    var id = args.model.targetID ? args.model.targetID : args.model.cssClass.split(' ')[0];
    PropertyChangesFromPanel(args, id.substring(0, id.length - 3));
}

/*
    The below event will be trigger when we click on the document.
    The purpose of this event, to hide the filtered shapes  when click on the document.
*/
$(document).click(function (e) {
    if ($(e.target).is('#symbolPalette_Parent_Mini,#symbolPalette_Parent_Mini *')) {
        if (showMinimizedPalette) {
            if (document.getElementById("symbolPalette_Parent_Mini"))
                document.getElementById("symbolPalette_Parent_Mini").style.display = "block";

        }
    }
    else {
        if (!showMinimizedPalette) {
            if (e.target.localName !== "body" && e.target.localName !== "form" && document.getElementById("symbolPalette_Parent_Mini")) {
                document.getElementById("symbolPalette_Parent_Mini").style.display = "none";
                clearText()
            }
        }
    }
    showMinimizedPalette = false;
});

$(window).keydown(function (e) {
    // SaveFileDialog will be opened when pressing the Ctrl+S.
    if (e.ctrlKey && e.keyCode == 83) {
        saveDiagram();
        e.originalEvent.preventDefault();
    }
    // Label editing will be enabled when pressing the enter key.
    if (e.keyCode == 13 && $(document.activeElement)[0] && $(document.activeElement)[0].id === "DiagramContent") {
        if (this.diagram.selectionList.length > 0) {
            var node = this.diagram.selectionList[0];
            if (node.labels[0] && !node.labels[0].readOnly) {
                diagram.startLabelEdit(node, node.labels[0]);
                e.originalEvent.preventDefault();
            }
        }
    }
    // OpenFileDialog will be opened when pressing the Ctrl+O.
    if (e.ctrlKey && e.keyCode == 79) {
        openDialog();
        e.originalEvent.preventDefault();
    }
    //Group the selected nodes in the diagram when pressing the Ctrl+G
    if (e.ctrlKey && e.keyCode == 71) {
        e.originalEvent.preventDefault();
        diagram.group();
    }
    //Ungroup the group node when pressing the Ctrl+K
    if (e.ctrlKey && e.keyCode == 85) {
        e.originalEvent.preventDefault();
        diagram.ungroup();
    }
    //Visually move the selected object over all other intersected objects when pressing Ctrl+F
    if (e.ctrlKey && e.keyCode == 70) {
        e.originalEvent.preventDefault();
        diagram.bringToFront();
    }
    //Visually move the selected object behind all other intersected objects when pressing Ctrl+B
    if (e.ctrlKey && e.keyCode == 66) {
        e.originalEvent.preventDefault();
        diagram.sendToBack();
    }
});

//Resize the diagram and symbol palette size when window gets resized.
$(window).resize(function () {
    this.updateSize();
    setDiagramSize();
});