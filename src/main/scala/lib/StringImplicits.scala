package lib

object StringImplicits {
  implicit class BinaryOperations(binary: String) {
    def binaryToBigInt(): BigInt = {
      BigInt.apply(binary, 2)
    }
  }
}
