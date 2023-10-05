import java.util.Arrays;

class Array {
  public static void main(String[] args) {
    String[] copyFrom = {"milan", "riya", "sauma"};
    String[] copyTo = new String[5];
    boolean[] b = new boolean[2];
    System.out.println(b[1]);

    System.arraycopy(copyFrom, 0, copyTo, 0, 3);
    copyTo = Arrays.copyOfRange(copyFrom, 1, 4);

    for(String name: copyTo) {
      System.out.print(name + " ");
    }
  }
} 