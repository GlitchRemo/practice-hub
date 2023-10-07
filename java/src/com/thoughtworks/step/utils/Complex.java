package com.thoughtworks.step.utils;

public class Complex {
  private int real;
  private int imaginary;

  public Complex(int real, int imaginary) {
    this.real = real;
    this.imaginary = imaginary;
  }

  public Complex() {
    this(0, 0);
  }

  public Complex add(Complex c) {
    Complex total = new Complex();

    total.real = this.real + c.real;
    total.imaginary = this.imaginary + c.imaginary;

    return total;
  }

  public String toString() {
    return this.real + " + i" + this.imaginary;
  }
}