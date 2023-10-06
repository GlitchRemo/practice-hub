abstract class Pokemon implements Attacker{
  String name;
  int hp;

  Pokemon(String name, int hp) {
    this.name = name;
    this.hp = hp;
  }

  void takeDamage(int damage) {
    this.hp -= damage;
  }

  // void attack(Pokemon pokemon) {};

  public String toString() {
    return "Name: " + this.name + ", HP: " + this.hp;
  }
}