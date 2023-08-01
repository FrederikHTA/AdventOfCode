package scala-lib

object LazyListImplicits {
  extension[A] (list: LazyList[A]) {
    def cycle: LazyList[A] = LazyList.continually(list).flatten
  }
}
