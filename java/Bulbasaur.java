class Bulbasaur extends Pokemon {
  Bulbasaur() {
    super("Bulbasaur", 150);
  }

  public void attack(Pokemon opponent) {
    opponent.takeDamage(20);
  }
}