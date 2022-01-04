package lib

object StringImplicits {
  implicit class BinaryOperations(binary: String) {
    def binaryToInteger(): Int = {
      Integer.parseInt(binary, 2)
    }
  }
}
