var diagram;
 
/*
    In diagram builder, we have provided some built-in intelligent shapes. Each intelligent shapes will have some
    specific behaviour. The below methods are used to define the resizing behaviour i.e shape appearance will be resized according to its size.
*/
function updateIntelligentNode(args) {
    diagram = $("#DiagramContent").ejDiagram("instance");
    var node = args.element;
    var points = null;
    var str = node.name.toString();
    if (str) {
        if (str.match("\\b5star")) {
            points = _updateStar(node, diagram._svg, 5, 25);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\b16star")) {
            points = _updateStar(node, diagram._svg, 16, 25);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\b6star")) {
            points = _updateStar(node, diagram._svg, 6, 25);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bLeftArrow") && !str.match("\\bCurvedLeftArrow") && !str.match("\\bJumpingLeftArrow")) {
            points = _updateLeftArrow(node, diagram._svg);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bRightArrow") && !str.match("\\bCurvedRightArrow") && !str.match("\\bJumpingRightArrow")) {
            points = _updateRightArrow(node, diagram._svg);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bParallelogram")) {
            points = _updatePallelogram(node, diagram._svg);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bbendsingleArrow")) {
            points = _updateBendSingleArrow(node, diagram._svg);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bbenddoubleArrow")) {
            points = _updateBenddoubleArrow(node, diagram._svg);
            node.pathData = points;
            return points;


        }
        else if (str.match("\\bDoubleArrow")) {
            points = _updateDoubleArrow(node, diagram._svg);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bquradicArrow")) {
            points = _updateQuradicArrow(node, diagram._svg);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bColumn") || str.match("\\bPredefinedProcess")) {
            points = _updateGridColumn(node, diagram._svg);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bRow") || str.match("\\bInternalStorage")) {
            points = _updateGridRow(node, diagram._svg);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bHexagon")) {
            points = _updateOctogan(node, diagram._svg, args);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bRRectangle")) {
            points = _updateRRectangle(node, diagram._svg);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bProceed")) {
            points = _updateProceed(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bPrepartion")) {
            points = _updatePrepartion(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bLoopLimit")) {
            points = _updateLoopLimit(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bManualInput")) {
            points = _updateManualInput(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bUTurnArrow")) {
            points = _updateUTurnArrow(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bChevronsTopArrow")) {
            points = _updateChevronsTopArrow(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bChevronsWide")) {
            points = _updateChevronsWide(node);
            node.pathData = points;
            return points;
        }

        else if (str.match("\\bChevronsLeftandTopArrow")) {
            points = _updateChevronsLeftandTopArrow(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bManualOperation")) {
            points = _updateManualOperation(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bTwoSideFlatCorner")) {

            points = _updateTwoSideFlatCorner(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bLStylishedHeadwithFanTail")) {
            points = _updateLStylishedHeadwithFanTail(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bRightNotchedArrow")) {
            points = _updateRightNotchedArrow(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bCallOutQuadArrow")) {
            points = _updateCallOutQuadArrow(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bCallOutleftArrow")) {
            points = _updateCallOutLeftArrow(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bCallOutDownArrow")) {
            points = _updateCallOutDownArrow(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bCallOutrightArrow")) {
            points = _updateCallOutRightArrow(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bCallOutupArrow")) {
            points = _updateCallOutUpArrow(node);
            node.pathData = points;
            return points;
        }

        else if (str.match("\\bUpArrow") && !(str.match("CurvedUpArrow"))) {
            points = _updateUpArrow(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bExternaldata")) {
            points = _updateExternalData(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bDisplay")) {
            points = _updateDisplay(node);
            node.pathData = points;
            return points;

        }
        else if (str.match("\\bTwoSideRoundedCorner")) {
            points = _updateTwoSideRoundedCorner(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bDelay")) {
            points = _updateDelay(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bBentleftarrow")) {
            points = _updateBentLeftArrow(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bStart")) {
            var a = 30;
            points = _updateStartPoints(node, a);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bCard")) {
            points = _updateCard(node);
            node.pathData = points;
            return points;
        }
        else if (str.match("\\bdata")) {
            points = _updateData(node);
            node.pathData = points;
            return points;
        }
    }
};


function _renderStar(node, count, radius) {
    var oRadius = 0;
    if (node.height > node.width)
        oRadius = node.width / 2;
    else
        oRadius = node.height / 2;

    var points = this._updateStarPoints(node.width / 2, node.height / 2, count, oRadius, radius);

    var mx = node.pinX - node.width / 2;
    var my = node.pinY - node.height / 2;
    for (var i = 0, len = points.length; i < len; ++i) {
        var point = this.translate(points[i], 0, 0);
        points[i] = point;
    }
    return points;
};

function _renderDoubleArrow(node) {

    var points = this._updateDoubleArrowPoints(node);
    return points;
};

function _renderLeftArrow(node) {
    var points = this._updateLeftArrowPoints(node);
    return points;
};

function _renderRightArrow(node) {
    var points = this._updateRightArrowPoints(node);
    return points;
};

function _renderQuradicArrow(node) {

    var points = this._updateQudraticArrowPoints(node);
    return points;
};

function _renderRRectangle(node) {
    var points = this._updateRoundedRectanglePoints(node, 10);
    return points;
};

function _renderParallelogram(node) {
    var points = this._updateParallelogramPoints(node);
    return points;

};

function _renderBendSingleArrow(node) {
    var points = this._updateBendSingleArrowPoints(node);
    return points;
};

function _renderBenddoubleArrow(node) {
    var points = this._updateBenddoubleArrowPoints(node);
    return points;
};

function _renderOctagon(node) {

    var points = this._updateOctagonPoints(node);
    return points;
};

function _renderDiamond(node) {
    var points = this._updateDiamondPoints(node);
    return points;

};

function _renderGridColumn(node) {
    var points = this._updateGridColumnPoints(node);
    return points;
};

function _renderGridRow(node) {
    var points = this._updateGridRowPoints(node);
    return points;
};

function _renderStoredData(node) {
    var points = this._updateStoredDataPoints(node);
    return points;
};

function _renderData(node) {
    var points = this._updateData(node);
    return points;
};

function _renderCard(node) {
    var points = this._updateCard(node);
    return points;
};

function _renderStart(node) {
    var points = this._updateStart(node);
    return points;
}

function _renderBentLeftArrow(node) {
    var points = this._updateBentLeftArrow(node);
    return points;
};

function _renderUTurnArrow(node) {
    var points = this._updateUTurnArrow(node);
    return points;
}

function _renderbendrightArrow(node) {
    var points = this._updatebendrightArrow(node);
    return points;
};

function _renderDelay(node) {
    var points = this._updateDelay(node);
    return points;
};

function _renderTwoSideRoundedCorner(node) {
    var points = this._updateTwoSideRoundedCorner(node);
    return points;

};

function _renderDisplay(node) {
    var points = this._updateDisplay(node);
    return points;
};

function _renderExternalData(node) {
    var points = this._updateExternalData(node);
    return points;
};

function _renderUpArrow(node) {
    var points = this._updateUpArrow(node);
    return points;
};

function _renderCallOutLeftArrow(node) {
    var points = this._updateCallOutLeftArrow(node);
    return points;
};

function _renderCallOutUpArrow(node) {
    var points = this._updateCallOutUpArrow(node);
    return points;
};

function _renderCallOutRightArrow(node) {
    var points = this._updateCallOutRightArrow(node);
    return points;
};

function _renderStoredData(node) {
    var points = this._updateStoredDataPoints(node);
    return points;
};

function _renderProceed(node) {
    var points = this._updateProceed(node);
    return points;
};

function _renderLoopLimit(node) {
    var points = this._updateLoopLimit(node);
    return points;
};

function _renderManualInput(node) {
    var points = this._updateManualInput(node);
    return points;
};

function _renderPrepartion(node) {
    var points = this._updatePrepartion(node);
    return points;
};

function _renderChevronsTopArrow(node) {
    var points = this._updateChevronsTopArrow(node);
    return points;
};

function _renderChevronsLeftandTopArrow(node) {
    var points = this._updateChevronsLeftandTopArrow(node);
    return points;
};

function _renderChevronsComplexArrow(node) {
    var points = this._updateChevronsComplexArrow(node);
    return points;
};

function _renderManualOperation(node) {
    var points = this._updateManualOperation(node);
    return points;
};

function _renderTwoSideFlatCorner(node) {
    // node.width = 40;
    //node.height = 32;
    var points = this._updateTwoSideFlatCorner(node);
    return points;
};

function _renderLStylishedHeadwithFanTail(node) {
    var points = this._updateLStylishedHeadwithFanTail(node);
    return points;
};

function _renderRightNotchedArrow(node) {
    var points = this._updateRightNotchedArrow(node);
    return points;
};

function _renderCallOutDownArrow(node) {
    var points = this._updateCallOutDownArrow(node);
    return points;
};

function _renderChevronsWide(node) {
    var points = this._updateChevronsWide(node);
    return points;
};

function _updateStar(node, svg, count, radius) {
    var oRadius = 0;
    if (node.height > node.width)
        oRadius = node.width / 2;
    else
        oRadius = node.height / 2;

    var points = this._updateStarPoints(node.width / 2, node.height / 2, count, oRadius, radius);
    var mx = node.pinX - node.width / 2;
    var my = node.pinY - node.height / 2;
    for (var i = 0, len = points.length; i < len; ++i) {
        var point = this.translate(points[i], 0, 0);
        points[i] = point;
    }
    return points;
};

function _updateDoubleArrow(node, svg) {
    var points = this._updateDoubleArrowPoints(node);
    var mx = node.pinX - node.width / 2;
    var my = node.pinY - node.height / 2;
    for (var i = 0, len = points.length; i < len; ++i) {
        var point = this.translate(points[i], 0, 0);
        points[i] = point;
    }
    return points;
};

function _updateLeftArrow(node, svg) {
    var points = this._updateLeftArrowPoints(node);
    var mx = node.pinX - node.width / 2;
    var my = node.pinY - node.height / 2;
    for (var i = 0, len = points.length; i < len; ++i) {
        var point = this.translate(points[i], 0, 0);
        points[i] = point;
    }
    return points;
};

function _updateRightArrow(node, svg) {
    var points = this._updateRightArrowPoints(node);
    var mx = node.pinX - node.width / 2;
    var my = node.pinY - node.height / 2;
    for (var i = 0, len = points.length; i < len; ++i) {
        var point = this.translate(points[i], 0, 0);
        points[i] = point;
    }
    return points;
};

function _updateQuradicArrow(node, svg) {
    var points = this._updateQudraticArrowPoints(node);
    var mx = node.pinX - node.width / 2;
    var my = node.pinY - node.height / 2;
    for (var i = 0, len = points.length; i < len; ++i) {
        var point = this.translate(points[i], 0, 0);
        points[i] = point;
    }
    return points;
};

function _updateRRectangle(node, svg) {
    var points = this._updateRoundedRectanglePoints(node, 10);
    return points;
}

function _updatePallelogram(node, svg) {
    var points = this._updateParallelogramPoints(node);
    return points;
};

function _updateBendSingleArrow(node, svg) {
    var points = this._updateBendSingleArrowPoints(node);
    return points;
};

function _updateBenddoubleArrow(node, svg) {
    var points = this._updateBenddoubleArrowPoints(node);
    return points;
};

function _updateOctogan(node, svg, args) {
    var points = this._updateOctagonPoints(node, args);
    return points;
};

function _updateDiamond(node) {
    var points = this._updateDiamondPoints(node);
    return points;
};

function _updateGridColumn(node) {
    var points = this._updateGridColumnPoints(node);
    return points;
};

function _updateGridRow(node) {
    var points = this._updateGridRowPoints(node);
    return points;
};

function _updateStoredData(node) {
    var points = this._updateStoredDataPoints(node);
    return points;
};

function _updateStart(node) {
    var a = 2;
    var points = this._updateStartPoints(node, a);
    return points;
};

function _updateCard(node) {
    var a = 12
    var points = this._updateCardPoints(node, a);
    return points;
};

function _updateBentLeftArrow(node) {
    var points = this._updateBentLeftArrowPoints(node);
    return points;
};

function _updatebendrightArrow(node) {
    var points = this._updatebendrightArrowPoints(node);
    return points;
};

function _updateUTurnArrow(node) {
    var a = 10;
    var points = this._updateUTurnArrowPoints(node, a);
    return points;
};

function _updateDisplay(node) {
    var a = 20;
    var points = this._updateDisplayPoints(node, a);
    return points;
};

function _updateData(node) {
    var a = 10;
    var points = this._updateDataPoints(node, a);
    return points;
};

function _updateTwoSideRoundedCorner(node) {
    var a = 20;
    var points = this._updateTwoSideRoundedCornerPoints(node, a);
    return points;
};

function _updateDelay(node) {
    var a = 2;
    var points = this._updateDelayPoints(node, a);
    return points;
};

function _updateExternalData(node) {
    var points = this._updateExternalDataPoints(node);
    return points;
};

function _updateCallOutQuadArrow(node) {
    var points = this._updateCallOutQuadArrowPoints(node);
    return points;
};

function _updateCallOutLeftArrow(node, a) {
    var a = node.width > 87 ? 70 : node.width - 17;
    var points = this._updateCallOutLeftArrowPoints(node, a);
    return points;
};

function _updateCallOutUpArrow(node) {
    var a = node.height > 87 ? 70 : node.height - 17;
    var points = this._updateCallOutUpArrowPoints(node, a);
    return points;
};

function _updateCallOutRightArrow(node) {
    var a = node.width > 87 ? 70 : node.width - 17;
    var points = this._updateCallOutRightArrowPoints(node, a);
    return points;
};

function _updateCallOutDownArrow(node) {
    var a = node.height > 87 ? 70 : node.height - 17;
    var points = this._updateCallOutDownArrowPoints(node, a);
    return points;
};

function _updateLStylishedHeadwithFanTail(node) {
    var points = this._updateLStylishedHeadwithFanTailPoints(node);
    return points;
};

function _updateRightNotchedArrow(node) {
    var points = this._updateRightNotchedArrowPoints(node);
    return points;
};

function _updateTwoSideFlatCorner(node) {
    var a = 8;
    var points = this._updateTwoSideFlatCornerPoints(node, a);
    return points;
};

function _updateManualOperation(node) {
    var points = this._updateManualOperationPoints(node);
    return points;
}

function _updatePrepartion(node) {
    var a = 7;
    var points = this._updatePrepartionPoints(node, a);
    return points;
};

function _updateLoopLimit(node) {
    var a = 10;
    var points = this._updateLoopLimitPoints(node, a);
    return points;
}

function _updateManualInput(node) {
    var points = this._updateManualInputPoints(node);
    return points;
};

function _updateChevronsComplexArrow(node) {
    var points = this._updateChevronsComplexArrowPoints(node);
    return points;
};

function _updateProceed(node) {
    var a = 20;
    var points = this._updateProceedPoints(node, a);
    return points;
};

function _updateChevronsTopArrow(node) {
    var points = this._updateChevronsTopArrowPoints(node);
    return points;
};

function _updateChevronsWide(node) {
    var a = 10
    var points = this._updateChevronsWidePoints(node, a);
    return points;
};

function _updateChevronsLeftandTopArrow(node) {
    var points = this._updateChevronsLeftandTopArrowPoints(node);
    return points;
};

function _updateUpArrow(node) {
    var points = this._updateUpArrowPoints(node);
    return points;
};

function _updateStarPoints(cx, cy, spikes, outerRadius, innerRadius) {
    var rot = Math.PI / 2 * 3;
    var x = cx;
    var y = cy;
    var step = Math.PI / spikes;
    var points = [];
    for (var i = 0; i < spikes; i++) {
        x = cx + Math.cos(rot) * outerRadius;
        y = cy + Math.sin(rot) * outerRadius;
        points.push({ x: x, y: y });
        rot += step;
        if (i <= spikes) {
            x = cx + Math.cos(rot) * innerRadius;
            y = cy + Math.sin(rot) * innerRadius;
            points.push({ x: x, y: y });
        }
        rot += step;
    }
    return points;
};

function _updateDoubleArrowPoints(node, option) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: node.height / 2 });
    points.push({ x: node.height / 2, y: node.height });
    points.push({ x: node.height / 2, y: node.height - node.height / 4 });
    points.push({ x: node.width - (node.height / 2), y: node.height - node.height / 4 });
    points.push({ x: node.width - (node.height / 2), y: node.height });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - (node.height / 2), y: y });
    points.push({ x: node.width - (node.height / 2), y: node.height / 4 });
    points.push({ x: node.height / 2, y: node.height / 4 });
    points.push({ x: node.height / 2, y: y });
    points.push({ x: x, y: node.height / 2 });

    var str = getPathData(points);

    return str;
};

function _updateLeftArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: node.height / 2 });
    points.push({ x: node.height / 2, y: node.height });
    points.push({ x: node.height / 2, y: node.height - node.height / 4 });
    points.push({ x: node.width, y: node.height - node.height / 4 });
    points.push({ x: node.width, y: node.height / 4 });
    points.push({ x: node.height / 2, y: node.height / 4 });
    points.push({ x: node.height / 2, y: y });
    points.push({ x: x, y: node.height / 2 });
    var str = getPathData(points);

    return str;
};

function _updateRightArrowPoints(node, svg, g) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: (node.height - node.height / 4) });
    points.push({ x: node.width - (node.height / 2), y: node.height - node.height / 4 });
    points.push({ x: node.width - (node.height / 2), y: node.height });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - (node.height / 2), y: y });
    points.push({ x: node.width - (node.height / 2), y: node.height / 4 });
    points.push({ x: x, y: node.height / 4 });
    var str = getPathData(points);

    return str;
};

function _updateQudraticArrowPoints(node, option) {
    var x = 0;
    var y = 0;
    var points = [];
    return points;
};

function _updateRoundedRectanglePoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: a, y: y });
    points.push({ x: 0, y: 0 });
    points.push({ x: 0, y: 0 });
    points.push({ x: 0, y: a });
    points.push({ x: 0, y: node.height - a });
    points.push({ x: 0, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: a, y: node.height });

    points.push({ x: node.width - a, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: node.height - a });
    points.push({ x: node.width, y: a });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width - a, y: 0 });
    points.push({ x: a, y: 0 });
    var str = getPathRRectangleData(points);

    return str;
};

function _updateParallelogramPoints(node) {

    var points = [];
    points.push({ x: 10, y: 0 });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width - 10, y: node.height });
    points.push({ x: 0, y: node.height });
    var str = getPathData(points);

    return str;
};

function _updateBendSingleArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: node.height });
    points.push({ x: (node.width - ((node.width / 2) / 4)), y: node.height });
    points.push({ x: (node.width - ((node.width / 2) / 4)), y: ((node.height / 2) / 4) });
    points.push({ x: node.width, y: ((node.height / 2) / 4) });
    points.push({ x: node.width - ((node.width / 2) / 2.5), y: 0 });
    points.push({ x: (node.width / 2) + ((node.width / 2) / 4), y: ((node.height / 2) / 4) });

    points.push({ x: (node.width - (node.width / 4)), y: ((node.height / 2) / 4) });
    points.push({ x: (node.width - (node.width / 4)), y: node.height - ((node.height / 2) / 4) });
    points.push({ x: 0, y: (node.height - ((node.height / 2) / 4)) });
    points.push({ x: 0, y: node.height });
    return points;
};

function _updateBenddoubleArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: 0, y: node.height - ((node.height / 2) / 2.5) });
    points.push({ x: ((node.width / 2) / 4), y: node.height });
    points.push({ x: ((node.width / 2) / 4), y: node.height - ((node.height / 2) / 4) });
    points.push({ x: (node.width - ((node.width / 2) / 4)), y: node.height - ((node.height / 2) / 4) });

    points.push({ x: (node.width - ((node.width / 2) / 4)), y: ((node.height / 2) / 4) });
    points.push({ x: node.width, y: ((node.height / 2) / 4) });
    points.push({ x: node.width - ((node.width / 2) / 2.5), y: 0 });
    points.push({ x: (node.width / 2) + ((node.width / 2) / 4), y: ((node.height / 2) / 4) });

    points.push({ x: (node.width - (node.width / 4)), y: ((node.height / 2) / 4) });
    points.push({ x: (node.width - (node.width / 4)), y: node.height - (node.height / 4) });
    points.push({ x: ((node.width / 2) / 4), y: node.height - (node.height / 4) });
    points.push({ x: ((node.width / 2) / 4), y: (node.height / 2) + ((node.height / 2) / 4) });
    return points;
};

function _updateOctagonPoints(node, args) {

    var x = 10;
    var y;
    if (node.height != 1) {
        x = (node.height / 4);
    }
    var points = [];
    points.push({ x: 0, y: node.height / 2 });
    //node.width/4
    points.push({ x: x, y: 0 });
    points.push({ x: (node.width - x), y: 0 });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: (node.width - x), y: node.height });
    points.push({ x: x, y: node.height });
    points.push({ x: 0, y: node.height / 2 });
    var str = getPathData(points);

    return str;
};

function _updateDiamondPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: 0, y: node.height / 2 });
    points.push({ x: node.width / 2, y: 0 });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width / 2, y: node.height });

    return points;
};

function _updateGridColumnPoints(node) {
    var x = 0;
    var y = 0;

    var points = [];

    points.push({ x: 10, y: 0 });
    points.push({ x: 10, y: 0 });
    points.push({ x: 10, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: 0, y: 0 });

    points.push({ x: 10, y: 0 });
    points.push({ x: node.width - 10, y: 0 });
    points.push({ x: node.width - 10, y: node.height });
    points.push({ x: 10, y: node.height });
    points.push({ x: 10, y: 0 });


    points.push({ x: node.width - 10, y: 0 });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width - 10, y: node.height });
    points.push({ x: node.width - 10, y: 0 });
    var str = getPathData(points);

    return str;
};

function _updateGridRowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];


    points.push({ x: 0, y: 0 });
    points.push({ x: 10, y: 0 });
    points.push({ x: 10, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: 0, y: 0 });

    points.push({ x: 0, y: 0 });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width, y: 10 });
    points.push({ x: 0, y: 10 });
    points.push({ x: 0, y: 0 });

    points.push({ x: 0, y: 0 });
    points.push({ x: 10, y: 0 });
    points.push({ x: 10, y: 10 });
    points.push({ x: node.width, y: 10 });
    points.push({ x: node.width, y: node.height });
    points.push({ x: 10, y: node.height });
    points.push({ x: 10, y: 10 });
    points.push({ x: 0, y: 10 });
    points.push({ x: 0, y: 0 });

    var str = getPathData(points);

    return str;
};

function _updateStoredDataPoints(node) {
    var x = 10;
    var y;
    if (node.height != 1) {
        x = (node.height / 4);
    }
    var points = [];
    points.push({ x: 0, y: node.height / 2 });
    //node.width/4
    points.push({ x: x, y: 0 });
    points.push({ x: (node.width - x), y: 0 });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: (node.width - x), y: node.height });
    points.push({ x: x, y: node.height });
    points.push({ x: 0, y: node.height / 2 });


    var d = "M" + points[0].x + "," + points[0].y + "L" + points[1].x + "," + points[1].y + "L" + points[2].x + "," + points[2].y +
        "C" + points[3].x + "," + points[3].y + "," + points[4].x + "," + points[4].y + "L" + points[5].x + "," + points[5].y + "L" + points[6].x + "," + points[6].y + "z";
    return d;
};

function _updateUpArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: node.width / 2, y: y });
    points.push({ x: 0, y: node.width / 4 });
    points.push({ x: node.width / 4, y: node.width / 4 });
    points.push({ x: node.width / 4, y: node.height });
    points.push({ x: node.width / 2 + node.width / 4, y: node.height });
    points.push({ x: node.width / 2 + node.width / 4, y: node.width / 4 });
    points.push({ x: node.width, y: node.width / 4 });

    var str = getPathData(points);
    return str;
};

function _updatebendrightArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: node.width, y: 15 });
    points.push({ x: node.width - 15, y: 30 });
    points.push({ x: node.width - 15, y: 20 });
    points.push({ x: 10, y: 20 });
    points.push({ x: 10, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: 0, y: 10 });
    points.push({ x: node.width - 15, y: 10 });
    points.push({ x: node.width - 15, y: 0 });
    var str = getPathData(points);
    return str;
}

function _updateBentLeftArrowPoints(node) {
    var points = [];
    var x = 0;
    var y = 0;
    points.push({ x: 0, y: 15 });
    points.push({ x: 15, y: 0 });
    points.push({ x: 15, y: 10 });
    points.push({ x: node.width, y: 10 });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width - 10, y: node.height });
    points.push({ x: node.width - 10, y: 20 });
    points.push({ x: 15, y: 20 });
    points.push({ x: 15, y: 30 });

    var str = getPathData(points);

    return str;
};

function _updateTwoSideFlatCornerPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: y });
    points.push({ x: 0, y: node.height - a });
    points.push({ x: 10, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: a });
    points.push({ x: node.width - 10, y: 0 });
    var str = getPathData(points);
    return str;

};

function _updateCardPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: y });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: a });
    points.push({ x: node.width - a, y: 0 });
    var str = getPathData(points);
    return str;

};

function _updateStartPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: a, y: 0 });
    points.push({ x: node.width - a, y: 0 });
    points.push({ x: node.width + 20, y: 0 });
    points.push({ x: node.width + 20, y: node.height });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: a, y: node.height });
    points.push({ x: -20, y: node.height });
    points.push({ x: -20, y: 0 });
    points.push({ x: a, y: 0 });
    var str = getPathStartData(points);
    return str;

};

function _updateLStylishedHeadwithFanTailPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: (node.height - node.height / 4) });
    points.push({ x: node.width - (node.height / 2), y: node.height - node.height / 2.5 });
    points.push({ x: node.width - (node.height / 2 + node.height / 4), y: node.height });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - (node.height / 2 + node.height / 4), y: y });
    points.push({ x: node.width - (node.height / 2), y: node.height / 2.5 });
    points.push({ x: x, y: node.height / 4 });
    points.push({ x: 10, y: node.height / 2 });
    var str = getPathData(points);
    return str;

};

function _updateCallOutLeftArrowPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: 0, y: node.height / 2 });
    points.push({ x: 10, y: node.height / 2 + 15 });
    points.push({ x: 10, y: node.height / 2 + 5 });
    points.push({ x: node.width - a, y: node.height / 2 + 5 });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width - a, y: 0 });
    points.push({ x: node.width - a, y: node.height / 2 - 5 });
    points.push({ x: 10, y: node.height / 2 - 5 });
    points.push({ x: 10, y: node.height / 2 - 15 });

    var str = getPathData(points);
    return str;

};

function _updateCallOutRightArrowPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: 0, y: 0 });
    points.push({ x: 0, y: node.height });
    points.push({ x: a, y: node.height });
    points.push({ x: a, y: node.height / 2 + 5 });
    points.push({ x: node.width - 10, y: node.height / 2 + 5 });
    points.push({ x: node.width - 10, y: node.height / 2 + 15 });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - 10, y: node.height / 2 - 15 });
    points.push({ x: node.width - 10, y: node.height / 2 - 5 });
    points.push({ x: a, y: node.height / 2 - 5 });
    points.push({ x: a, y: 0 });

    var str = getPathData(points);
    return str;

};

function _updateCallOutUpArrowPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];

    points.push({ x: node.width / 2, y: 0 });
    points.push({ x: node.width / 2 + 15, y: 10 });
    points.push({ x: node.width / 2 + 5, y: 10 });
    points.push({ x: node.width / 2 + 5, y: node.height - a });
    points.push({ x: node.width, y: node.height - a });
    points.push({ x: node.width, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: 0, y: node.height - a });
    points.push({ x: node.width / 2 - 5, y: node.height - a });
    points.push({ x: node.width / 2 - 5, y: 10 });
    points.push({ x: node.width / 2 - 15, y: 10 });
    var str = getPathData(points);
    return str;

};

function _updateCallOutDownArrowPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: 0, y: 0 });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width, y: a });
    points.push({ x: node.width / 2 + 5, y: a });
    points.push({ x: node.width / 2 + 5, y: node.height - 10 });
    points.push({ x: node.width / 2 + 15, y: node.height - 10 });
    points.push({ x: node.width / 2, y: node.height });
    points.push({ x: node.width / 2 - 15, y: node.height - 10 });
    points.push({ x: node.width / 2 - 5, y: node.height - 10 });
    points.push({ x: node.width / 2 - 5, y: a });
    points.push({ x: 0, y: a });
    var str = getPathData(points);
    return str;

};

function _updateRightNotchedArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: (node.height - node.height / 4) });
    points.push({ x: node.width - (node.height / 2), y: node.height - node.height / 4 });
    points.push({ x: node.width - (node.height / 2), y: node.height });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - (node.height / 2), y: y });
    points.push({ x: node.width - (node.height / 2), y: node.height / 4 });
    points.push({ x: x, y: node.height / 4 });
    points.push({ x: 10, y: node.height / 2 });


    var str = getPathData(points);
    return str;

};

function _updateChevronsComplexArrowPoints(node) {
};

function _updateChevronsWidePoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    a = node.width / 6;
    points.push({ x: x, y: y });
    points.push({ x: node.width - a, y: 0 });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: a, y: node.height / 2 });
    var str = getPathData(points);
    return str;

};

function _updateChevronsTopArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: node.height - 10 });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width - 8, y: node.height });
    points.push({ x: node.width - 8, y: 12 });
    points.push({ x: node.width, y: 12 });
    points.push({ x: node.width - 14, y: 0 });
    points.push({ x: node.width - 26, y: 12 });
    points.push({ x: node.width - 18, y: 12 });
    points.push({ x: node.width - 18, y: node.height - 10 });


    var str = getPathData(points);
    return str;

};

function _updateChevronsLeftandTopArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: node.height - 15 });
    points.push({ x: 10, y: node.height });
    points.push({ x: 10, y: node.height - 10 });
    points.push({ x: node.width - 10, y: node.height - 10 });
    points.push({ x: node.width - 10, y: 10 });
    points.push({ x: node.width, y: 10 });
    points.push({ x: node.width - 15, y: 0 });
    points.push({ x: node.width - 30, y: 10 });
    points.push({ x: node.width - 20, y: 10 });
    points.push({ x: node.width - 20, y: node.height - 20 });
    points.push({ x: 10, y: node.height - 20 });
    points.push({ x: 10, y: node.height - 30 });
    var str = getPathData(points);
    return str;
}

function _updateProceedPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: x, y: y });
    points.push({ x: a, y: node.height / 2 });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - a, y: 0 });

    var str = getPathData(points);
    return str;

};

function _updateLoopLimitPoints(node, a) {
    var x = 0;
    var y = 0;
    a = node.height / 4;
    var points = [];
    points.push({ x: a, y: 0 });
    points.push({ x: 0, y: a });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: a });
    points.push({ x: node.width - a, y: 0 });


    var str = getPathData(points);
    return str;

};

function _updateManualInputPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: 0, y: 20 });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: 0 });

    var str = getPathData(points);
    return str;

};

function _updatePrepartionPoints(node, a) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: a, y: 0 });
    points.push({ x: 0, y: node.height / 2 });
    points.push({ x: a, y: node.height });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - a, y: 0 });


    var str = getPathData(points);
    return str;

};

function _updateManualOperationPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    points.push({ x: 0, y: 0 });
    points.push({ x: node.width * (3 / 10), y: node.height });
    points.push({ x: node.width - (node.width * (3 / 10)), y: node.height });
    points.push({ x: node.width, y: 0 });

    var str = getPathData(points);
    return str;

};

function _updateCallOutQuadArrowPoints(node) {
    var x = 0;
    var y = 0;
    var points = [];
    var str = getPathData(points);
    return str;

};

function _updateDelayPoints(node, a) {
    var points = [];
    var x = 0;
    var y = 0;
    points.push({ x: x, y: y });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: node.width + 20, y: node.height });
    points.push({ x: node.width + 20, y: 0 });
    points.push({ x: node.width - a, y: 0 });
    points.push({ x: 0, y: 0 });
    var str = getPathDelayData(points);
    return str;
};

function _updateTwoSideRoundedCornerPoints(node, a) {
    var points = [];
    var x = 0;
    var y = 0;
    points.push({ x: x, y: y });
    points.push({ x: 0, y: node.height - a });
    points.push({ x: 0, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: a, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width, y: a });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width, y: 0 });
    points.push({ x: node.width - a, y: 0 });
    points.push({ x: 0, y: 0 });
    var str = getPathRCornerData(points);
    return str;
};

function _updateExternalDataPoints(node) {
    var points = [];
    var x = 0;
    var y = 0;
    points.push({ x: x, y: y });
    points.push({ x: -20, y: 0 });
    points.push({ x: -20, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width, y: node.height });
    points.push({ x: node.width - 20, y: node.height });
    points.push({ x: node.width - 20, y: 0 });
    points.push({ x: node.width, y: 0 });
    points.push({ x: 0, y: 0 });
    var str = getPathExternalData(points);

    return str;
};

function _updateDataPoints(node, a) {
    var points = [];
    var x = 0;
    var y = 0;
    points.push({ x: a, y: y });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: node.width, y: 0 });
    var str = getPathData(points, node);

    return str;
};

function _updateDisplayPoints(node, a) {
    var points = [];
    var x = 0;
    var y = 0;
    points.push({ x: x, y: y });
    points.push({ x: -70, y: 0 });
    points.push({ x: -70, y: node.height });
    points.push({ x: 0, y: node.height });
    points.push({ x: node.width - a, y: node.height });
    points.push({ x: node.width, y: node.height / 2 });
    points.push({ x: node.width - a, y: 0 });
    var str = getPathDisplayData(points, node);

    return str;
};

function _updateUTurnArrowPoints(node, a) {
    var points = [];
    var x = 0;
    var y = 0;
    points.push({ x: 0, y: 0 });
    points.push({ x: 0, y: node.height });
    points.push({ x: a, y: node.height });
    points.push({ x: a, y: 0 });
    points.push({ x: a, y: -20 });
    points.push({ x: node.width - a - 7.5, y: -20 });
    points.push({ x: node.width - a - 7.5, y: 0 });
    points.push({ x: node.width - a - 7.5, y: 15 });
    points.push({ x: node.width - a - 15, y: 15 });
    points.push({ x: node.width - 7.5 - (a / 2), y: 35 });
    points.push({ x: node.width, y: 15 });
    points.push({ x: node.width - 7.5, y: 15 });
    points.push({ x: node.width - 7.5, y: 0 });
    points.push({ x: node.width - 7.5, y: -50 });
    points.push({ x: 0, y: -50 });
    points.push({ x: 0, y: 0 })
    var str = getPathUTurnData(points);
    return str;
};

function _convertToSVGPoints(points) {
    var pts = "";
    for (var i = 0, len = points.length; i < len; i++) {
        pts += points[i].toString() + " ";
    }
    return pts.trim();
};

function translate(pt1, dx, dy) {
    return ej.datavisualization.Diagram.Point(pt1.x + dx, pt1.y + dy);
};

function getPathData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }
        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};

function getPathExternalData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }
        else if (i == 1 || i == 2 || i == 3 || i == 5 || i == 6 || i == 7) {
            if (i == 1 || i == 5) {
                str += "C" + points[i].x + "," + points[i].y;
            }
            else
                str += " " + points[i].x + "," + points[i].y;
        }
        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};

function getPathRCornerData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }
        else if (i == 2 || i == 3 || i == 4 || i == 7 || i == 8 || i == 9) {
            if (i == 2 || i == 7) {
                str += "C" + points[i].x + "," + points[i].y;
            }
            else
                str += " " + points[i].x + "," + points[i].y;
        }
        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};

function getPathDelayData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }
        else if (i == 3 || i == 4 || i == 5) {
            if (i == 3) {
                str += "C" + points[i].x + "," + points[i].y;
            }
            else
                str += " " + points[i].x + "," + points[i].y;
        }
        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};

function getPathUTurnData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }
        else if (i == 4 || i == 5 || i == 6 || i == 13 || i == 14 || i == 15) {
            if (i == 4 || i == 13) {
                str += "C" + points[i].x + "," + points[i].y;
            }
            else
                str += " " + points[i].x + "," + points[i].y;
        }
        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};

function getPathDisplayData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }
        else if (i == 1 || i == 2 || i == 3) {
            if (i == 1) {
                str += "C" + points[i].x + "," + points[i].y;
            }
            else
                str += " " + points[i].x + "," + points[i].y;
        }
        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};

function getPathRRectangleData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }

        else if (i == 1 || i == 2 || i == 3 || i == 5 || i == 6 || i == 7 || i == 9 || i == 10 || i == 11 || i == 13 || i == 14 || i == 15) {
            if (i == 1 || i == 5 || i == 9 || i == 13) {
                str += "C" + points[i].x + "," + points[i].y;
            }
            else
                str += " " + points[i].x + "," + points[i].y;
        }

        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};

function getPathStartData(points) {
    var str;
    for (var i = 0; i < points.length; i++) {
        if (i == 0) {
            str = "M" + points[i].x + "," + points[i].y;
        }

        else if (i == 2 || i == 3 || i == 4 || i == 6 || i == 7 || i == 8) {
            if (i == 2 || i == 6) {
                str += "C" + points[i].x + "," + points[i].y;
            }
            else
                str += " " + points[i].x + "," + points[i].y;
        }

        else {
            str += "L" + points[i].x + "," + points[i].y;
        }
        if (i == (points.length - 1)) {
            str += "z";
        }
    }
    return str;
};