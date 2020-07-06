"use strict";
function __export(m) {
    for (var p in m) if (!exports.hasOwnProperty(p)) exports[p] = m[p];
}
exports.__esModule = true;
var index = require("@syncfusion/ej2-calendars");
index.Calendar.Inject(index.Islamic);
index.DatePicker.Inject(index.Islamic);
index.DateTimePicker.Inject(index.Islamic);
__export(require("@syncfusion/ej2-calendars"));
