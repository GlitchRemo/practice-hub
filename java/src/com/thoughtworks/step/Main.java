package com.thoughtworks.step;

import com.thoughtworks.step.utils.MyString;
import com.thoughtworks.step.utils.Complex;
import com.thoughtworks.step.utils.Add;

import com.thoughtworks.step.shapes.Pattern;
import com.thoughtworks.step.colors.Color;

import com.thoughtworks.step.pokemon.Pikachu;
import com.thoughtworks.step.pokemon.Bulbasaur;

import com.thoughtworks.step.counters.CharacterCounter;
import com.thoughtworks.step.counters.Countable;

class Main {
  static void count(Countable countable, String text) {
    for(char c: text.toCharArray()) countable.count(c);
    System.out.println(countable);
  }

  public static void main(String args[]) {
    System.out.println(MyString.join("Sourav", "Halder"));
    System.out.println(MyString.repeat("Hello", 5));
    System.out.println(MyString.reverse("hello"));
    System.out.println(MyString.reverse("hello", ""));

    System.out.println(Pattern.printSquare(5, "* "));
    System.out.println(Pattern.printMirroredRightTriangle(5, "* "));
    System.out.println(MyString.reverse(Pattern.printRightTriangle(5, " *")));
    System.out.println(Pattern.printRightTriangle(5, "* "));
    System.out.println(MyString.reverse(Pattern.printMirroredRightTriangle(5, " *")));

    Add add = new Add(2, 3);
    Complex number1 = new Complex();
    Complex number2 = new Complex(4, 3);
    Complex total = number1.add(number2);

    Color.RED.printColor();

    System.out.println(add);
    System.out.println(total);

    Pikachu pikachu = new Pikachu();
    Bulbasaur bulbasaur = new Bulbasaur();

    pikachu.attack(bulbasaur);
    System.out.println(pikachu);
    System.out.println(bulbasaur);

    Countable characterCounter = new CharacterCounter();
    count(characterCounter, "abc cde");
  }
}