"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.logger = void 0;
const logger = (...data) => process.stdout.write(data.join(" ") + "\n");
exports.logger = logger;
