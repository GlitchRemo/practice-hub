package com.thoughtworks.step.colors;

public enum Color{
  RED("Red"),
  GREEN("Green"),
  YELLOW("Yellow");

  final String name;

  Color(String name) {
    this.name = name;
  }

  public void printColor() {
    System.out.println("Color Red");
  }
}
