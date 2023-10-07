package com.thoughtworks.step.pokemon;

import com.thoughtworks.step.pokemon.Pokemon;

public class Pikachu extends Pokemon {
  public Pikachu() {
    super("Pikachu", 150);
  }

  public void attack(Pokemon opponent) {
    opponent.takeDamage(10);
  }
}