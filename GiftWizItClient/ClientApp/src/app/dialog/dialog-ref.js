"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var rxjs_1 = require("rxjs");
var DialogRef = /** @class */ (function () {
    function DialogRef() {
        this._afterClosed = new rxjs_1.Subject();
        this.afterClosed = this._afterClosed.asObservable();
    }
    DialogRef.prototype.close = function (result) {
        this._afterClosed.next(result);
    };
    return DialogRef;
}());
exports.DialogRef = DialogRef;
//# sourceMappingURL=dialog-ref.js.map