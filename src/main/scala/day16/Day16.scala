package day16

import scala.io.Source
import lib.StringImplicits._

final case class PacketResult(packetVersion: Int, packetTypeId: Int, packetValue: Int)

sealed trait Packet {
  def packetVersion: Int
}

case class OperatorPacket(packetVersion: Int, packetTypeId: Int, subPackets: List[Packet]) extends Packet

case class LiteralPacket(packetVersion: Int, packetTypeId: Int, packetValue: Int) extends Packet

object Day16 {

  def part1(input: String): Int = {
    val result = parsePacket(input)
    val sum = sumVersions(result._1)

    println(s"Part 1 result: $result")
    println(s"Part 1 version sum result: $sum")

    sum
  }

  def sumVersions(packet: Packet): Int = {
    packet match {
      case OperatorPacket(packetVersion, _, subPackets) => packetVersion + subPackets.map(sumVersions).sum
      case LiteralPacket(packetVersion, _, _) => packetVersion
    }
  }

  def parsePacket(input: String): (Packet, String) = {
    val (packetVersion, tail) = input.splitAt(3)
    val (packetTypeId, tail2) = tail.splitAt(3)

    packetTypeId.binaryToInteger() match {
      case 4 =>
        val (literalValue, tail) = parseLiteralValue(tail2)
        (LiteralPacket(packetVersion.binaryToInteger(), packetTypeId.binaryToInteger(), 0), tail)
      case _ =>
        val (packetLengthTypeId, tail3) = tail2.splitAt(1)

        packetLengthTypeId match {
          case "0" =>
            val (bits, tail4) = tail3.splitAt(15)
            val length = bits.binaryToInteger()
            val (subPacketBits, tail5) = tail4.splitAt(length)
            val subPackets = parseSubPackets(subPacketBits)
            (OperatorPacket(packetVersion.binaryToInteger(), packetTypeId.binaryToInteger(), subPackets), tail5)
          case _ =>
            val (bits, tail4) = tail3.splitAt(11)
            val number = bits.binaryToInteger()
            val (subPackets, tail5) = parseSubPacketsNumber(tail4, number)
            (OperatorPacket(packetVersion.binaryToInteger(), packetTypeId.binaryToInteger(), subPackets), tail5)
        }
    }
  }

  def parseSubPacketsNumber(binaryString: String, number: Int): (List[Packet], String) = {
    if (number == 0) {
      (Nil, binaryString)
    } else {
      val (packet, tail) = parsePacket(binaryString)
      val (subPackets, tail2) = parseSubPacketsNumber(tail, number - 1)
      (packet :: subPackets, tail2)
    }
  }

  def parseSubPackets(binaryString: String): List[Packet] = {
    binaryString match {
      case "" => List()
      case _ =>
        val (packet, tail) = parsePacket(binaryString)
        val subPackets = parseSubPackets(tail)
        packet :: subPackets
    }
  }

  def parseLiteralValue(binaryString: String): (String, String) = {
    val (value, tail) = binaryString.splitAt(5)
    val (prefixBit, bitGroup) = value.splitAt(1)

    prefixBit match {
      case "0" => (bitGroup, tail)
      case _ =>
        val (value2, tail2) = parseLiteralValue(tail)
        (value.tail + value2, tail2)
    }
  }

  def hexToBinaryMap: Map[String, String] = Map[String, String](
    "0" -> "0000",
    "1" -> "0001",
    "2" -> "0010",
    "3" -> "0011",
    "4" -> "0100",
    "5" -> "0101",
    "6" -> "0110",
    "7" -> "0111",
    "8" -> "1000",
    "9" -> "1001",
    "A" -> "1010",
    "B" -> "1011",
    "C" -> "1100",
    "D" -> "1101",
    "E" -> "1110",
    "F" -> "1111",
  )

  def parseHex(input: String): String = {
    input.split("").map(x => hexToBinaryMap(x)).mkString
  }

  def main(arwgs: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .mkString
      .trim

    assert(part1(parseHex(input)) == 886)

    input.linesIterator.foreach(x => part1(parseHex(x)))

    //    tests()
    //    println(part1(input))
  }

  def tests(): Unit = {
    val test = Source
      .fromInputStream(getClass.getResourceAsStream("testdata.txt"))
      .mkString
      .trim

    val test1 = Source
      .fromInputStream(getClass.getResourceAsStream("testdata1.txt"))
      .mkString
      .trim

    val test2 = Source
      .fromInputStream(getClass.getResourceAsStream("testdata2.txt"))
      .mkString
      .trim

    val test3 = Source
      .fromInputStream(getClass.getResourceAsStream("testdata3.txt"))
      .mkString
      .trim

    assert(test == "6")
    assert(test1 == "9")
    assert(test2 == "14")
    //    assert(test3 == "7")
  }
}
