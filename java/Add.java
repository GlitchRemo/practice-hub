class Add {
  private int a;
  private int b;

  Add(int a, int b) {
    this.a = a;
    this.b = b;
  }

  Add() {
    this(0, 0);
  }

  public String toString() {
    return "a + b: " + (this.a + this.b);
  }
}