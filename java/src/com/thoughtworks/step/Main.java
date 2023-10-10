package com.thoughtworks.step;

import com.thoughtworks.step.generics.Pair;
import com.thoughtworks.step.utils.MyString;
import com.thoughtworks.step.utils.Complex;
import com.thoughtworks.step.utils.Add;

import com.thoughtworks.step.shapes.Pattern;
import com.thoughtworks.step.colors.Color;

import com.thoughtworks.step.pokemon.Pikachu;
import com.thoughtworks.step.pokemon.Bulbasaur;

import com.thoughtworks.step.counters.CharacterCounter;
import com.thoughtworks.step.counters.Countable;
import com.thoughtworks.step.wildcard.Family;
import com.thoughtworks.step.wildcard.Me;
import com.thoughtworks.step.wildcard.Mother;

class Main {
  static void count(Countable countable, String text) {
    for(char c: text.toCharArray()) countable.count(c);
    System.out.println(countable);
  }

  private static void countCharacters() {
    Countable characterCounter = new CharacterCounter();

    count(characterCounter, "abc cde");
    count(characterCounter, "abc");
  }

  private static void addNumbers() {
    Add add = new Add(2, 3);
    System.out.println(add);

    Complex number1 = new Complex();
    Complex number2 = new Complex(4, 3);
    Complex total = number1.add(number2);

    System.out.println(total);
  }

  private static void attackPokemon() {
    Pikachu pikachu = new Pikachu();
    Bulbasaur bulbasaur = new Bulbasaur();
    pikachu.attack(bulbasaur);

    System.out.println(pikachu);
    System.out.println(bulbasaur);
  }

  private static void printPatterns() {
    System.out.println(Pattern.printSquare(5, "* "));
    System.out.println(Pattern.printMirroredRightTriangle(5, "* "));
    System.out.println(MyString.reverse(Pattern.printRightTriangle(5, " *")));
    System.out.println(Pattern.printRightTriangle(5, "* "));
    System.out.println(MyString.reverse(Pattern.printMirroredRightTriangle(5, " *")));
  }

  private static void useStringMethods() {
    System.out.println(MyString.join("Sourav", "Halder"));
    System.out.println(MyString.repeat("Hello", 5));
    System.out.println(MyString.reverse("hello"));
    System.out.println(MyString.reverse("hello", ""));
  }

  private static <T> int count(T[] arr) {
    return arr.length;
  }

  private static void useGenericTypes() {
    Integer[] numbers = { 1, 2, 3 };
    System.out.println(count(numbers));

    Pair<Integer, String> pair = new Pair<>(1, "abc");
    System.out.println(pair.getKey());
    System.out.println(pair.getValue());
  }

  private static void printColor() {
    Color.RED.printColor();
  }

  public static void main(String[] args) {
    useStringMethods();
    printPatterns();
    addNumbers();
    attackPokemon();
    countCharacters();
    printColor();
    useGenericTypes();
    useWildcards();
  }

  private static void printName(Family<? extends Mother> family) {
  }

  private static void useWildcards() {
//    Family<Grandmother> family = new Family<>();
    Family<Me> family = new Family<>();

    printName(family);
  }

}