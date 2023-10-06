enum Color implements PureColor{
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
