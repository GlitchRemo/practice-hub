class Pattern {
  static String printSquare(int length, String symbol) {
    char newLine = '\n';

    String row = MyString.repeat(symbol, length);
    return MyString.repeat(row + newLine, length);
  }

  static String printRightTriangle(int height, String symbol) {
    char newLine = '\n';
    String pattern = "";

    for(int i = 0; i < height; i++) {
      String row = MyString.repeat(symbol, i + 1) + MyString.repeat("  ", height - (i + 1)) ;
      pattern += row + newLine;
    }

    return pattern;
  }

  static String printMirroredRightTriangle(int height, String symbol) {
    char newLine = '\n';
    String pattern = "";

    for(int i = 0; i < height; i++) {
      String row = MyString.repeat("  ", height - (i + 1)) + MyString.repeat(symbol, i + 1);
      pattern += row + newLine;
    }

    return pattern;
  }

  public static void main(String[] args) {
    System.out.println(Pattern.printSquare(5, "* "));
    System.out.println(Pattern.printMirroredRightTriangle(5, "* "));
    System.out.println(MyString.reverse(Pattern.printRightTriangle(5, " *")));
    System.out.println(Pattern.printRightTriangle(5, "* "));
    System.out.println(MyString.reverse(Pattern.printMirroredRightTriangle(5, " *")));
  }
}