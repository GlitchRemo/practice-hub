package com.thoughtworks.step.generics;

public class Pair<A, B> {
  private final A a;
  private final B b;

  public Pair(A a, B b) {
    this.a = a;
    this.b = b;
  }

  public A getKey() {
    return this.a;
  }

  public B getValue() {
    return this.b;
  }
}
