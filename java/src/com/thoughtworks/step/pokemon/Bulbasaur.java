package com.thoughtworks.step.pokemon;

import com.thoughtworks.step.pokemon.Pokemon;

public class Bulbasaur extends Pokemon {
  public Bulbasaur() {
    super("Bulbasaur", 150);
  }

  public void attack(Pokemon opponent) {
    opponent.takeDamage(20);
  }
}