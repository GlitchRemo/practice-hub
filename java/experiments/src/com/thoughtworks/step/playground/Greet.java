class Greet {
  public static int addTwo(int a, int b) {
    return a + b;
  }

  static void oneToTen() {
    for(int i = 1; i <= 10; i++) {
      System.out.print(i);
    }
     System.out.println();
  }

  public static void main(String[] args) {
    String name = "Riya";
    System.out.println("Hello World " + name);
    System.out.println(Greet.addTwo(2, 3));
    oneToTen();
  }
}

