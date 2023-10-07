package com.thoughtworks.step.utils;

public class MyString {
  public static String join(String s1, String s2) {
    return s1 + s2;
  }

  public static String repeat(String text, int times) {
    String finalStr = "";
    
    for( int j = 0; j < times; j++ ) {
        finalStr += text;
    }

    return finalStr;
  }

  public static String reverse(String text, String finalStr) {
    if(text.length() == 1) {
      return finalStr + text; 
    }

    final int lastIndex = text.length() - 1;
    return reverse(text.substring(0, lastIndex), finalStr + text.charAt(lastIndex));
  }

  public static String reverse(String text) {
    if(text.isEmpty()) {
      return text; 
    }

    return reverse(text.substring(1)) + text.charAt(0);
  }
}