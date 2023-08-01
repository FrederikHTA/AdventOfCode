package scala-lib

object StringImplicits {
  extension (binary: String) {
    def binaryToBigInt(): BigInt = BigInt.apply(binary, 2)
  }

  extension (input: String) {
    def toGrid: Grid[Int] =
      input.linesIterator.map(_.split("").toVector.map(x => x.toInt)).toVector
  }
}