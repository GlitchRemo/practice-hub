"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Gender;
(function (Gender) {
    Gender[Gender["MALE"] = 0] = "MALE";
    Gender[Gender["FEMALE"] = 1] = "FEMALE";
})(Gender || (Gender = {}));
const o = { "0": "a", "2": "b" };
console.log(o);
const eligibleVoters = (people, selector) => people.filter(selector);
const bittu = {
    name: "Bittu",
    age: 17,
};
const sauma = {
    name: "Sauma",
    age: 23,
};
const milan = {
    name: "Milan",
    age: 20,
    gender: Gender.MALE,
};
const selector = (p) => p.age >= 18;
console.log(eligibleVoters([milan, bittu, sauma], selector));
console.log(eligibleVoters([], selector));
