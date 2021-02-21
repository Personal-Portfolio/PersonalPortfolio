import { createServer } from "http";
import mockserver from "mockserver";
import { join, dirname } from "path";
import { fileURLToPath } from 'url';

const pathToMocks = join(dirname(fileURLToPath(import.meta.url)), "data");

console.info(`Starting server with data from path: ${pathToMocks}`);

const mocks = mockserver(pathToMocks);
createServer(mocks).listen(3000);

console.info("<=========>");
console.info("Started");