"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Utilities = /** @class */ (function () {
    function Utilities() {
    }
    Utilities.prototype.isEmpty = function (obj) {
        return Object.keys(obj).length === 0 && obj.constructor === Object;
    };
    Utilities.prototype.arrayRemove = function (arr, value) {
        return arr.filter(function (elem) {
            return elem != value;
        });
    };
    Utilities.prototype.getCurrTimeInSeconds = function () {
        return Math.round(Date.now() / 1000);
    };
    return Utilities;
}());
exports.Utilities = Utilities;
//# sourceMappingURL=Utilities.js.map