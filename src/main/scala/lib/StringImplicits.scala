package lib

object StringImplicits {
  implicit class BinaryOperations(binary: String) {
    def binaryToBigInt(): BigInt = {
      BigInt.apply(binary, 2)
    }
  }

  implicit class StringOperations(input: String) {
    def toGrid: Grid[Int] = {
      input.linesIterator.map(_.split("").toVector.map(x => x.toInt)).toVector
    }
  }
}
