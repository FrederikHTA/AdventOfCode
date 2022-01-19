package lib

object LazyListImplicits {
  implicit class LazyListUnfoldOps(lazyList: LazyList.type) {
    def unfold0[A](a: A)(f: A => Option[A]): LazyList[A] =
      LazyList.unfold(a)(a => f(a).map(a => (a, a)))
  }

  implicit class CycleLazyListOps[A](list: LazyList[A]) {
    def cycle: LazyList[A] = {
      LazyList.continually(list).flatten
    }
  }
}
