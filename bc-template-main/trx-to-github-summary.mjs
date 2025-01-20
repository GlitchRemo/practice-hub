#!/usr/bin/env zx

import { XMLParser } from "fast-xml-parser";

let testResults = await glob(["test-results/*.trx"]);
const options = {
  ignoreAttributes: false,
  attributeNamePrefix: "",
};
const groupBy = (array, key, property) => {
  // Return the end result
  return array.reduce((result, currentValue) => {
    // If an array already present for key, push it to the array. Else create an array and push the object
    (result[currentValue[key]] = result[currentValue[key]] || []).push(
      currentValue[property]
    );
    // Return the current iteration `result` value, this will be taken as next iteration `result` value and accumulate
    return result;
  }, {}); // empty object is the initial value for result object
};
// WARNING: This is not a drop in replacement solution and
// it might not work for some edge cases. Test your code!
const set = (obj, path, value) => {
  // Regex explained: https://regexr.com/58j0k
  const pathArray = Array.isArray(path) ? path : path.match(/([^[.\]])+/g);

  pathArray.reduce((acc, key, i) => {
    if (acc[key] === undefined) acc[key] = {};
    if (i === pathArray.length - 1) acc[key] = value;
    return acc[key];
  }, obj);
};

const parser = new XMLParser(options);

await testResults.map(async (file) => {
  let xmlDataStr = await fs.readFile(file, "utf8");
  let jsonObj = parser.parse(xmlDataStr);
  let testResultsTree = {};
  try{
  const Results = jsonObj.TestRun.Results.UnitTestResult;
  let testResultsMd = "";
  let consolidatedResults = Results.map((entry) => {
    const { testName, duration, outcome } = entry;
    const pName = testName.split(".").slice(0,-1).join('.')
    const TestName = testName.split(".").slice(-1)[0]
    const Status = outcomeToStatus(outcome)
    const result = {Status, Duration:duration, TestName}
    return {pName, result}
  });
  let groupedResults = groupBy(consolidatedResults, "pName", "result")
  const summary = jsonObj.TestRun.ResultSummary.Counters;
  const gR = {}
  Object.entries(groupedResults).forEach(([k,v]) => {set(gR, k, v)})
  const shortName = stripCommonKeysFromObject(gR);  
  
  let output = `## ${shortName.name}\n\n`;
  const {
    total: Total,
    passed: Passed,
    failed: Failed,
    notExecuted: Skipped,
    inconclusive: Inconclusive,
    timeout: Timeout,
    error: Error,
    aborted: Aborted,
  } = summary;
  output += template({
    Total,
    Passed,
    Skipped,
    Inconclusive,
    Failed,
    Timeout,
    Error,
    Aborted,
  });
  set(shortName, ['summary'], summary)
  const tr = printTestResults(shortName.tests)
  output += `\n\n${tr}`;
  console.log(output);
  await fs.mkdir("summary-test-results")
  await fs.writeFile(`summary-test-results/${shortName.name}.md`, output)
  await fs.writeJSON(`summary-test-results/${shortName.name}.json`, shortName);
}
catch(Error){}
});

function stripCommonKeysFromObject(obj, condensed = "") {
  const keys = Object.keys(obj);
  if (keys.length !== 1) {
    
    return {name: condensed, tests: obj} ;
  } else {
    return stripCommonKeysFromObject(
      structuredClone(obj[keys[0]]),
      condensed ? `${condensed}.${keys[0]}` : keys[0],
    );
  }
}

const buildHeader = (headers) =>
  `| ${headers.join(" | ")} |\n` +
  `| ${headers.map((_) => "---").join(" | ")} |`;

const buildRow = (row) => `| ${row.join(" | ")} |`;

const buildRows = (rows) => rows.map(buildRow).join("\n");

// function template(headers, rows) {
//   return `${buildHeader(headers)}\n` + `${buildRows(rows)}`
// }

function template(object) {
  const headers = Object.keys(object);
  const values = Object.values(object);
  return `${buildHeader(headers)}\n` + `${buildRows([values])}\n`;
}

const outcomeToStatus = (outcome) => {
  const inconclusive = ["Skipped", "Inconclusive"];
  const failed = ["Failed", "Timeout", "Error", "Aborted"];
  if (outcome === "Passed") return ":white_check_mark:";
  if (inconclusive.includes(outcome)) return "❓";
  if (failed.includes(outcome)) return ":x:";
  return "⁉";
};

function printTestResults(tests) {
  const keys = Object.keys(tests);
  if (Array.isArray(tests)) {
    const rows = tests.sort((a,b)=> a.TestName - b.TestName).map(entry=>{
      return buildRow(Object.values(entry))
    }).join('\n')
    return `\n${buildHeader(Object.keys(tests[0]))}\n` + `${rows}`;
  } 
  else {
    return keys.map((k) => {
      return `<details><summary>${k}</summary>\n${printTestResults(tests[k])}\n</details>\n\n`;
    }).join('');
  }
}
