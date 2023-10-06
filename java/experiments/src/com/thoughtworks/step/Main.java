package com.thoughtworks.step;
import com.thoughtworks.step.utils.MyString;
import com.thoughtworks.step.utils.Complex;
import com.thoughtworks.step.utils.Add;
import com.thoughtworks.step.shapes.Pattern;
import com.thoughtworks.step.colors.Color;

class Main {
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
  }
}