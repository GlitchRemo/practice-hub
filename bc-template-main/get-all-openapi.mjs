#!/usr/bin/env zx

$.verbose = true;

$.shell = await $`echo $SHELL`

const workingDir = argv.workingDir || "src/Api"
const dll = argv.dll

await fs.ensureDir("./openapi")

$.cwd = workingDir;

if (!dll) echo`Please specify which DLL to inspect`;

const genenerateOpenAPI = async (openapi_name, dll) =>
  await $`dotnet swagger tofile --yaml --output ../../openapi/${openapi_name}.yaml bin/Debug/net6.0/${dll} ${openapi_name}`.nothrow();

const badCall = await genenerateOpenAPI("all", dll);

const specNames = [...badCall.stderr.matchAll(/"([\w-]+)"/g)]
  .map((entry) => {
    return entry[1];
  })
  .filter((name) => name !== "all");

if(specNames.length === 0) echo`No OpenAPI specs found, check the working directory or turn on verbose mode.`

console.info(specNames);

specNames.map(async (specName) => {
  await genenerateOpenAPI(specName, dll);
});
