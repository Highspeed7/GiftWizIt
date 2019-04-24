"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var authConfig = require("../configs/authConfig");
exports.protectedResourceMap = [["https://localhost:44327/api/GetLists", [authConfig.config.b2cScopes[0]]],
    ["https://localhost:44327/api/Users", [authConfig.config.b2cScopes[0]]]
];
//# sourceMappingURL=protectedResource.js.map