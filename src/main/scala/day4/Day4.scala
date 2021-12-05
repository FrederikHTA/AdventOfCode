package day4

import java.io
import scala.io.Source

final case class Board(rows: Array[Row])

final case class Row(row: Array[(Int, Boolean)])

object Day4 extends App {
  val testData = Source.fromResource("day4/testdata.txt")
    .getLines()
    .toList
    .mkString("\n")

  val testData2 = Source.fromResource("day4/testdata2.txt")
    .getLines()
    .toList
    .mkString("\n")

  val realData = Source.fromResource("day4/data.txt")
    .getLines()
    .toList
    .mkString("\n")

  val (inputNumbers, boards) = parseData(realData)

  part1(inputNumbers, boards)
  part2(inputNumbers, boards)

  def part1(inputNumbers: Array[Int], board: Array[Board]): Unit = {

    val (_, winnerNumber, winnerBoard) = findWinningBoard(inputNumbers)

    val result = calculateWinnerScore(winnerNumber, winnerBoard.head)

    println(s"Part 1: $result")
    //    println(s"Part 1: WinningNumber: $winnerNumber \n${winnerBoard.rows.map(_.row.map(_._1).mkString(" ")).mkString("\n")}")
  }

  def part2(inputNumbers: Array[Int], board: Array[Board]): Unit = {

    val (_, winnerNumber, winnerBoard) = findLastWinningBoard(inputNumbers)

    val result = calculateWinnerScore(winnerNumber, winnerBoard)

    println(s"Part 2: $result")
  }

  def findLastWinningBoard(inputNumbers: Array[Int]) = {
    val emptyBoard = Board(Array(Row(Array((0, false)))))

    inputNumbers.foldLeft((boards, 0, emptyBoard)) {
      case ((boards, winnerNumber, winnerBoard), number) =>
        val newBoards = boards.map(board => {
          val newRow = board.rows.map(row => {
            val newRow = row.row.map(cell => {
              if (cell._1 == number) {
                (cell._1, true)
              } else {
                cell
              }
            })
            Row(newRow)
          })
          Board(newRow)
        })

        val (isAnyWinners, winnerBoards) = checkBoardsForWinners(newBoards)

        if (isAnyWinners) {
          if (newBoards.length == 1) {
            (boards, winnerNumber, winnerBoard)
          } else {
            val newBoardsFiltered = newBoards.filter(board => !winnerBoards.contains(board))
            (newBoardsFiltered, number, winnerBoards.head)
          }
        } else {
          (newBoards, winnerNumber, winnerBoard)
        }
    }
  }

  def findWinningBoard(inputNumbers: Array[Int]) = {
    val emptyBoard = Board(Array(Row(Array((0, false)))))

    inputNumbers.foldLeft((boards, 0, Array(emptyBoard))) {
      case ((boards, winnerNumber, winnerBoard), number) =>
        if (winnerNumber > 0) (boards, winnerNumber, winnerBoard)
        else {
          val newBoards = boards.map(board => {
            val newRow = board.rows.map(row => {
              val newRow = row.row.map(cell => {
                if (cell._1 == number) {
                  (cell._1, true)
                } else {
                  cell
                }
              })
              Row(newRow)
            })
            Board(newRow)
          })

          val (isAnyWinners, winnerBoard) = checkBoardsForWinners(newBoards)
          (newBoards, if (isAnyWinners) number else 0, winnerBoard)
        }
    }
  }

  def calculateWinnerScore(winnerNumber: Int, winningBoard: Board): Int = {
    val sum = winningBoard.rows.map(_.row.filter(_._2 == false).map(_._1).sum).sum

    winnerNumber * sum
  }

  def checkBoardsForWinners(boards: Array[Board]): (Boolean, Array[Board]) = {
    val isHorizontalWinner = boards.filter(board => {
      board.rows.exists(row => {
        row.row.forall(cell => cell._2)
      })
    })

    val transposedBoards = transposeBoards(boards)

    val isVerticalWinner = transposedBoards.filter(board => {
      board.rows.exists(row => {
        row.row.forall(cell => cell._2)
      })
    })

    val winners = isHorizontalWinner ++ isVerticalWinner

    if (winners.length > 0) (true, winners)
    else (false, Array(Board(Array(Row(Array((0, false)))))))
  }

  def transposeBoards(boards: Array[Board]) = {
    boards.map(board => {
      val mappedRows = board.rows.map(rows => {
        rows.row.map(cell => {
          (cell._1, cell._2)
        })
      })
      mappedRows.transpose
    }).map(board => {
      Board(board.map(row => {
        Row(row.map(cell => {
          (cell._1, cell._2)
        }))
      }))
    })
  }

  def parseData(input: String): (Array[Int], Array[Board]) = {
    val splitData = input.split("\n\n")

    val inputNumbers = splitData.head.split(",").map(x => x.toInt)

    val boards = splitData
      .tail
      .map(x => {
        Board(
          x.split("\n")
            map (y => Row(y
            .trim
            .replace("  ", " ")
            .split(" ")
            .map(z => (z.toInt, false))))
        )
      })

    (inputNumbers, boards)
  }
}
