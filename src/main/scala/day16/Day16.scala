package day16

import scala.io.Source
import lib.StringImplicits._

final case class PacketResult(packetVersion: Int, packetTypeId: Int, packetValue: Int)

sealed trait Packet {
  def packetVersion: Int
}

final case class OperatorPacket(packetVersion: Int, packetTypeId: Int, packetValue: Int) extends Packet
final case class LiteralPacket(packetVersion: Int, packetTypeId: Int, packetValue: Int) extends Packet

object Day16 {

  def part1(input: String): Unit = {
    val result = parsePacket(input)
    println(s"Part 1: $result")
  }

  def parsePacket(input: String): List[PacketResult] = {
    val packetVersion = getPacketVersion(input)
    val packetTypeId = getPacketTypeId(input)

    if(packetTypeId == 4) {
      val packetData = getPacketData(input)
      val res = parsePacketData(packetData)
      List(PacketResult(packetVersion, packetTypeId, res))
    } else {
      val packetLengthTypeId = getPacketLengthTypeId(input)
      val subPacketData = getSubPacketData(input)

      val subPacketVersionSum = if(packetLengthTypeId == 0) {
        parseLengthTypeId0(subPacketData)
      } else {
        parseLengthTypeId1(subPacketData)
      }

      subPacketVersionSum :+ PacketResult(packetVersion, packetTypeId, 0)
    }
  }

  private def parseLengthTypeId0(subPacketData: String): List[PacketResult] = {
    val totalLengthOfSubPackets = subPacketData.take(15).binaryToInteger()
    val subPackets = subPacketData.substring(15, 15 + totalLengthOfSubPackets).grouped(11).toList

    val subPacketGroups = if(subPackets.last.length != 11) {
      subPackets.take(subPackets.size - 2) :+ subPackets.takeRight(2).mkString("")
    } else {
      subPackets
    }

    val parsedPackets = subPacketGroups.map(parsePacket)

    parsedPackets.flatten
  }

  private def parseLengthTypeId1(subPacketData: String): List[PacketResult] = {
    val numberOfSubPackets = subPacketData.take(11).binaryToInteger()

    if(subPacketData.length >= 11) {
      val subPackets = subPacketData
        .substring(11)
        .grouped(11)
        .take(numberOfSubPackets)
        .toList

      //    val res = parsePacket(subPacketData.substring(11))
      val subPacketResult = subPackets.map(parsePacket)
      subPacketResult.flatten
    } else {
      List(PacketResult(0, 0, 0))
    }
  }

  def parsePacketData(packetBinaryData: String): Int = {
    val packetData = packetBinaryData.grouped(5).filter(_.length == 5).toList

    val packetValue = packetData.map(_.substring(1)).mkString("").binaryToInteger()

    packetValue
  }

  def getSubPacketData(binaryString: String): String = binaryString.substring(7)

  def getPacketData(binaryString: String): String = binaryString.substring(6)

  def getPacketLengthTypeId(binaryString: String): Int = binaryString(6).asDigit

  def getPacketVersion(binaryString: String): Int = binaryString.substring(0,3).binaryToInteger()

  def getPacketTypeId(binaryString: String): Int = binaryString.substring(3,6).binaryToInteger()

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

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("testdata3.txt"))
      .mkString
      .trim


    input.linesIterator.foreach(x => println(part1(parseHex(x))))

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
