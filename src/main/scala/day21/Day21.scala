package day21

import scala.annotation.tailrec
import lib.LazyListImplicits._

object Day21 {
  final case class Players(p1: Player, p2: Player)

  final case class DeterministicDie(rolls: Int = 0, values: LazyList[Int] = LazyList.from(1).take(100).cycle) {
    def roll: (DeterministicDie, Int) = {
      val (roll, newValueList) = values.splitAt(3)
      (DeterministicDie(rolls + 3, newValueList), roll.sum)
    }
  }

  case class Player(position: Int, score: Int = 0) {
    def move(amount: Int): Player = {
      val playerPosition = (position + amount - 1) % 10 + 1
      Player(playerPosition, score + playerPosition)
    }
  }

  @tailrec
  def play(p1: Player, p2: Player, die: DeterministicDie): (Player, DeterministicDie) = {
    val (newDie, roll) = die.roll
    val newP1 = p1.move(roll)

    if (newP1.score >= 1000) {
      (p2, newDie)
    } else {
      play(p2, newP1, newDie)
    }
  }


  def part1(players: Players): Int = {
    val (player, die) = play(players.p1, players.p2, DeterministicDie())
    player.score * die.rolls
  }

  def main(args: Array[String]): Unit = {
    val example: Players = Players(Player(4), Player(8))
    val data: Players = Players(Player(5), Player(6))

    println(s"Example: ${part1(example)}")
    println(s"Part1: ${part1(data)}")

    assert(part1(example) == 739785)
    assert(part1(data) == 1002474)
  }
}
