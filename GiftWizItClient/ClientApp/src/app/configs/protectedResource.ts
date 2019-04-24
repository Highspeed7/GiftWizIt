import * as authConfig from "../configs/authConfig";
export const protectedResourceMap: [string, string[]][] =
  [["https://localhost:44327/api/GetLists", [authConfig.config.b2cScopes[0]]],
    ["https://localhost:44327/api/Users", [authConfig.config.b2cScopes[0]]]
];
