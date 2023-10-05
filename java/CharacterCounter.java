class CharacterCounter implements Countable {
  private int count;

  public void count(char character) {
    this.count++;
  }

  public String toString() {
    return "Character count: " + this.count;
  }
}