package com.thoughtworks.step.shapes;
import com.thoughtworks.step.utils.MyString;

public class Pattern {
  public static String printSquare(int length, String symbol) {
    char newLine = '\n';

    String row = MyString.repeat(symbol, length);
    return MyString.repeat(row + newLine, length);
  }

  public static String printRightTriangle(int height, String symbol) {
    char newLine = '\n';
    String pattern = "";

    for(int i = 0; i < height; i++) {
      String row = MyString.repeat(symbol, i + 1) + MyString.repeat("  ", height - (i + 1)) ;
      pattern += row + newLine;
    }

    return pattern;
  }

  public static String printMirroredRightTriangle(int height, String symbol) {
    char newLine = '\n';
    String pattern = "";

    for(int i = 0; i < height; i++) {
      String row = MyString.repeat("  ", height - (i + 1)) + MyString.repeat(symbol, i + 1);
      pattern += row + newLine;
    }

    return pattern;
  }
}