package lib

object LazyListImplicits {
  implicit class CycleLazyListOps[A](list: LazyList[A]) {
    def cycle: LazyList[A] = {
      LazyList.continually(list).flatten
    }
  }
}
