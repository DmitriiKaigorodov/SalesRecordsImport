"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function objectToQueryString(obj) {
    var queryString = "?";
    for (var property in obj) {
        var value = obj[property];
        if (value && typeof (value) != "function")
            queryString += property + "=" + encodeURIComponent(value) + "&";
    }
    return queryString;
}
exports.objectToQueryString = objectToQueryString;
//# sourceMappingURL=utils.js.map