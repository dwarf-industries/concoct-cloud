document.addEventListener("DOMContentLoaded", function () {
    function getData() {
        return [["", "Kia", "Nissan", "Toyota", "Honda", "Mazda", "Ford"], ["2012", 10, 11, 12, 13, 15, 16], ["2013", 10, 11, 12, 13, 15, 16], ["2014", 10, 11, 12, 13, 15, 16], ["2015", 10, 11, 12, 13, 15, 16], ["2016", 10, 11, 12, 13, 15, 16]]
    }

    function getCustomData() {
        return [["", "Kia", "Nissan", "Toyota", "Honda", "Mazda", "Ford"], ["2012", 10, 11, 12, 13, 15, 16], ["2013", 10, 11, 12, 13, 15, 16], ["2014", 10, 11, 12, 13, 15, 16], ["2015", 10, 11, 12, 13, 15, 16], ["2016", 10, 11, 12, 13, 15, 16]]
    }

    function getData() {
        return [["", "Kia", "Nissan", "Toyota", "Honda", "Mazda", "Ford"], ["2012", 10, 11, 12, 13, 15, 16], ["2013", 10, 11, 12, 13, 15, 16], ["2014", 10, 11, 12, 13, 15, 16], ["2015", 10, 11, 12, 13, 15, 16], ["2016", 10, 11, 12, 13, 15, 16]]
    }

    function getData() {
        return [["", "Kia", "Nissan", "Toyota", "Honda", "Mazda", "Ford"], ["2012", 10, 11, 12, 13, 15, 16], ["2013", 10, 11, 12, 13, 15, 16], ["2014", 10, 11, 12, 13, 15, 16], ["2015", 10, 11, 12, 13, 15, 16], ["2016", 10, 11, 12, 13, 15, 16]]
    }

    var settings1, hot1, example1 = document.getElementById("context");
    settings1 = {
        data: getData(),
        rowHeaders: !0,
        colHeaders: !0,
        contextMenu: !0
    }, hot1 = new Handsontable(example1, settings1);
    var settings3, hot3, example3 = document.getElementById("configuration");
    settings3 = {
        data: getCustomData(),
        rowHeaders: !0,
        colHeaders: !0
    }, hot3 = new Handsontable(example3, settings3), hot3.updateSettings({
        contextMenu: {
            callback: function (key, options) {
                "about" === key && setTimeout(function () {
                    alert("This is a context menu with default and custom options mixed")
                }, 100)
            }, items: {
                row_above: {
                    disabled: function () {
                        return 0 === hot3.getSelected()[0]
                    }
                }, row_below: {}, hsep1: "---------", remove_row: {
                    name: "Remove this row, ok?", disabled: function () {
                        return 0 === hot3.getSelected()[0]
                    }
                }, hsep2: "---------", about: {name: "About this menu"}
            }
        }
    });
    var hot4, copyPaste = document.getElementById("copyPaste");
    hot4 = new Handsontable(copyPaste, {
        data: getData(),
        rowHeaders: !0,
        colHeaders: !0,
        contextMenu: !0,
        contextMenuCopyPaste: {swfPath: "/bower_components/zeroclipboard/dist/ZeroClipboard.swf"}
    });
    // var hot, data = [["", "Kia", "Nissan", "Toyota", "Honda", "Mazda", "Ford"], ["2012", 10, 11, 12, 13, 15, 16], ["2013", 10, 11, 12, 13, 15, 16], ["2014", 10, 11, 12, 13, 15, 16], ["2015", 10, 11, 12, 13, 15, 16], ["2016", 10, 11, 12, 13, 15, 16]], container = document.getElementById("buttons"), selectFirst = document.getElementById("selectFirst"), rowHeaders = document.getElementById("rowHeaders"), colHeaders = document.getElementById("colHeaders");
    // hot = new Handsontable(container, {
    //     rowHeaders: !0,
    //     colHeaders: !0,
    //     outsideClickDeselects: !1,
    //     removeRowPlugin: !0
    // }), hot.loadData(data), Handsontable.Dom.addEvent(selectFirst, "click", function () {
    //     hot.selectCell(0, 0)
    // }), Handsontable.Dom.addEvent(rowHeaders, "click", function () {
    //     hot.updateSettings({rowHeaders: this.checked})
    // }), Handsontable.Dom.addEvent(colHeaders, "click", function () {
    //     hot.updateSettings({colHeaders: this.checked})
    // });
    // var hot1, container = document.getElementById("comments");
    // hot1 = new Handsontable(container, {
    //     data: getData(),
    //     rowHeaders: !0,
    //     colHeaders: !0,
    //     contextMenu: !0,
    //     comments: !0,
    //     cell: [{row: 1, col: 1, comment: "Some comment"}, {row: 2, col: 2, comment: "More comments"}]
    // })
//comment start
    function getData() {
        return [
            ['', 'Kia', 'Nissan', 'Toyota', 'Honda', 'Mazda', 'Ford'],
            ['2012', 10, 11, 12, 13, 15, 16],
            ['2013', 10, 11, 12, 13, 15, 16],
            ['2014', 10, 11, 12, 13, 15, 16],
            ['2015', 10, 11, 12, 13, 15, 16],
            ['2016', 10, 11, 12, 13, 15, 16]
        ];
    }

    var container = document.getElementById('comments-table'),
        hot1;

    hot1 = new Handsontable(container, {
        data: getData(),
        rowHeaders: true,
        colHeaders: true,
        contextMenu: true,
        comments: true,
        cell: [
            {row: 1, col: 1, comment: {value: 'Some comment'}},
            {row: 2, col: 2, comment: {value: 'More comments'}}
        ]
    });
//    Custom Buttion start
    function removeRow() {
        var eventManager = new Handsontable.EventManager(this);

        function bindMouseEvents() {
            var instance = this;

            eventManager.addEventListener(instance.rootElement, 'mouseover', function(e) {
                if (checkRowHeader(e.target)) {
                    var element = getElementFromTargetElement(e.target);

                    if (element) {
                        var btn = getButton(element);

                        if (btn) {
                            btn.style.display = 'block';
                        }
                    }
                }
            });

            eventManager.addEventListener(instance.rootElement, 'mouseout', function(e) {
                if (checkRowHeader(e.target)) {
                    var element = getElementFromTargetElement(e.target);

                    if (element) {
                        var btn = getButton(element);

                        if (btn) {
                            btn.style.display = 'none';
                        }
                    }
                }
            });
        }

        var getElementFromTargetElement = function(element) {
            if (element.tagName != 'TABLE') {
                if (element.tagName == 'TH' || element.tagName == 'TD') {
                    return element;
                } else {
                    return getElementFromTargetElement(element.parentNode);
                }
            }

            return null;
        };

        var checkRowHeader = function(element) {
            if (element.tagName != 'BODY') {
                if (element.parentNode.tagName == 'TBODY') {
                    return true;
                } else {
                    element = element.parentNode;

                    return checkRowHeader(element);
                }
            }

            return false;
        };

        function unbindMouseEvents() {
            eventManager.clear();
        }

        function getButton(td) {
            var btn = td.querySelector('.removeRowBtn');

            if (!btn) {
                var parent = td.parentNode.querySelector('th.htRemoveRow');

                if (parent) {
                    btn = parent.querySelector('.removeRowBtn');
                }
            }

            return btn;
        }

        this.init = function() {
            var instance = this;
            var pluginEnabled = !! (instance.getSettings().removeRowPlugin);

            if (pluginEnabled) {
                bindMouseEvents.call(this);
                Handsontable.dom.addClass(instance.rootElement, 'htRemoveRow');
            } else {
                unbindMouseEvents.call(this);
                Handsontable.dom.removeClass(instance.rootElement, 'htRemoveRow');
            }
        };

        this.beforeInitWalkontable = function(walkontableConfig) {
            var instance = this;

            /**
             * rowHeaders is a function, so to alter the actual value we need to alter the result returned by this function
             */
            var baseRowHeaders = walkontableConfig.rowHeaders;
            walkontableConfig.rowHeaders = function() {
                var pluginEnabled = !! (instance.getSettings().removeRowPlugin);

                var newRowHeader = function(row, elem) {
                    var child, div;

                    while (child = elem.lastChild) {
                        elem.removeChild(child);
                    }
                    elem.className = 'htNoFrame htRemoveRow';

                    if (row > -1) {
                        div = document.createElement('div');
                        div.className = 'removeRowBtn';
                        div.appendChild(document.createTextNode('x'));
                        elem.appendChild(div);

                        eventManager.addEventListener(div, 'mouseup', function() {
                            instance.alter('remove_row', row);
                        });
                    }
                };

                return pluginEnabled ? Array.prototype.concat.call([], newRowHeader, baseRowHeaders()) : baseRowHeaders();
            };
        }
    }

    var htRemoveRow = new removeRow();

    Handsontable.hooks.add('beforeInitWalkontable', function(walkontableConfig) {
        htRemoveRow.beforeInitWalkontable.call(this, walkontableConfig);
    });

    Handsontable.hooks.add('beforeInit', function() {
        htRemoveRow.init.call(this)
    });

    Handsontable.hooks.add('afterUpdateSettings', function() {
        htRemoveRow.init.call(this)
    });

    var
        data = [
            ['', 'Kia', 'Nissan', 'Toyota', 'Honda', 'Mazda', 'Ford'],
            ['2012', 10, 11, 12, 13, 15, 16],
            ['2013', 10, 11, 12, 13, 15, 16],
            ['2014', 10, 11, 12, 13, 15, 16],
            ['2015', 10, 11, 12, 13, 15, 16],
            ['2016', 10, 11, 12, 13, 15, 16]
        ],
        container = document.getElementById('buttons-handson'),
        selectFirst = document.getElementById('selectFirst'),
        rowHeaders = document.getElementById('rowHeaders'),
        colHeaders = document.getElementById('colHeaders'),
        hot;

    hot = new Handsontable(container, {
        rowHeaders: true,
        colHeaders: true,
        outsideClickDeselects: false,
        removeRowPlugin: true
    });
    hot.loadData(data);

    Handsontable.dom.addEvent(selectFirst, 'click', function () {
        hot.selectCell(0,0);
    });
    Handsontable.dom.addEvent(rowHeaders, 'click', function () {
        hot.updateSettings({
            rowHeaders: this.checked
        });
    });
    Handsontable.dom.addEvent(colHeaders, 'click', function () {
        hot.updateSettings({
            colHeaders: this.checked
        });
    });

});
