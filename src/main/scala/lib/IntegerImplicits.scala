package lib

object IntegerImplicits {
  extension (start: Int) {
    def rangeWithDirection(end: Int): Range.Inclusive = {
      Range.inclusive(start, end, if (start > end) -1 else 1)
    }
  }
}
