#!/usr/bin/env zx

// Supress all output from zx
$.verbose = false;
const toggle = argv.toggle || 'Toggles'
const seperator = argv.seperator || '__'
/**
 * Content to append to the introduction file
 */
let append = "";
// await $`sh <(curl tea.xyz) +crates.io/ripgrep`;
/**
 * Rust based regex pattern to match using ripgrep via tea.xyz
 * @param {string} search
 * @param {string[]} flags
 * @returns ProcessOutput
 */
const useRipgrep = async (search, flags) =>
  await $`sh <(curl -Ssf tea.xyz) rg ${search} ${flags} ./`.quiet();


/**
 * Create a markdown section with a specified heading level
 * @param {number} level heading level
 * @param {string} title
 * @param {string} content
 * @returns
 */
const printSection = (level, title, content) => {
  return `\n\n${"#".repeat(level)} ${title}\n\n${content}`;
};

const togglesOutput = await useRipgrep(`${toggle}${seperator}`, [
  "--sort",
  "path",
  "--json",
]);

function JSONParsable(entry) {
  try {
    JSON.parse(entry);
    return true;
  } catch (SyntaxError) {}
  return false;
}

const matches = togglesOutput.stdout
  .split("\n")
  .filter(JSONParsable)
  .map((entry) => {
    return JSON.parse(entry);
  })
  .filter(({ type }) => type === "match")
  .filter(({ data }) => data.path.text !== "./get-feature-toggles.mjs")
  .map((entry) => {
    const { data } = entry;
    const path = data.path.text;
    const toggle = data.lines.text;
    const env = [...path.matchAll(/overlays\/(?<env>[\w]+)\/configmap.yaml/g)].map((entry) => {
        return entry[1];
      }).toString()
    const [key, value] = toggle.split(':')
    return { path, env, toggle, key, value };
  });

const envs = [... new Set(matches.map(({env}) => {return env}))]

envs.forEach(env=>{
    append += printSection(2, env ? env.toUpperCase() : "Base", '')
    matches.filter((match) => match.env === env ).map(({key, value})=>{
        append += `${key.split(seperator)[1]}: ${value}`
    })
})


console.log(append)