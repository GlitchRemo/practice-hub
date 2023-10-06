class Pikachu extends Pokemon {
  Pikachu() {
    super("Pikachu", 150);
  }

  public void attack(Pokemon opponent) {
    opponent.takeDamage(10);
  }
}