class Playground {
  static int addAll(int... nums) {
    System.out.println(nums);
    int total = 0;
    for(int n: nums) total += n;
    return total;
  }

  public static void main(String[] args) {
    int n1 = 2;
    int n2 = 3;
    System.out.println(addAll(n1, n2));
  }
}