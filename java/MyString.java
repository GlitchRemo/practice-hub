class MyString {
  static String join(String s1, String s2) {
    return s1 + s2;
  }

  static String repeat(String text, int times) {
    String finalStr = "";
    
    for( int j = 0; j < times; j++ ) {
        finalStr += text;
    }

    return finalStr;
  }

  static String reverse(String text, String finalStr) {
    if(text.length() == 1) {
      return finalStr + text; 
    }

    final int lastIndex = text.length() - 1;
    return reverse(text.substring(0, lastIndex), finalStr + text.charAt(lastIndex));
  }

  static String reverse(String text) {
    if(text.isEmpty()) {
      return text; 
    }

    return reverse(text.substring(1)) + text.charAt(0);
  }

  public static void main(String args[]) {
    System.out.println(join("Sourav", "Halder"));
    System.out.println(repeat("Hello", 5));
    System.out.println(reverse("hello"));
    System.out.println(reverse("hello", ""));
  }
}