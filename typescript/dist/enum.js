"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Gender;
(function (Gender) {
    Gender[Gender["MALE"] = 0] = "MALE";
    Gender[Gender["FEMALE"] = 1] = "FEMALE";
})(Gender || (Gender = {}));
const milan = {
    name: "Milan",
    age: 20,
    gender: Gender.MALE,
};
const display = (person) => {
    console.log("Name:", person.name);
    console.log("Age:", person.age);
    console.log("Gender:", person.gender);
};
display(milan);
var Level;
(function (Level) {
    Level[Level["ERROR"] = 0] = "ERROR";
    Level[Level["WARN"] = 5] = "WARN";
    Level[Level["INFO"] = 6] = "INFO";
    Level[Level["DEBUG"] = 7] = "DEBUG";
})(Level || (Level = {}));
console.log("Level: ", Level);
