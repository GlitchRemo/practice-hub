class Complex {
  private int real;
  private int imaginary;

  Complex(int real, int imaginary) {
    this.real = real;
    this.imaginary = imaginary;
  }

  Complex() {
    this(0, 0);
  }

  Complex add(Complex c) {
    Complex total = new Complex();

    total.real = this.real + c.real;
    total.imaginary = this.imaginary + c.imaginary;

    return total;
  }

  public String toString() {
    return this.real + " + i" + this.imaginary;
  }
}