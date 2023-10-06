class Main {
  static void count(Countable countable, String text) {
    for(char c: text.toCharArray()) countable.count(c);
    System.out.println(countable);
  }

  public static void main(String[] args) {
    Add add = new Add(2, 3);
    Complex number1 = new Complex();
    Complex number2 = new Complex(4, 3);
    Complex total = number1.add(number2);
    System.out.println(add);
    System.out.println(total);

    Countable characterCounter = new CharacterCounter();
    count(characterCounter, "abc cde");

    Color.RED.printColor();

    Pikachu pikachu = new Pikachu();
    Bulbasaur bulbasaur = new Bulbasaur();

    pikachu.attack(bulbasaur);
    System.out.println(pikachu);
    System.out.println(bulbasaur);
  }
}