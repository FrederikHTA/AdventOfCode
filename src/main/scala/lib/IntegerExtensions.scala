package lib

object IntegerExtensions {
  implicit class RangeOperations(start: Int) {
    def createRangeWithDirection(end: Int): Range.Inclusive = {
      Range.inclusive(start, end, if (start > end) -1 else 1)
    }
  }
}
