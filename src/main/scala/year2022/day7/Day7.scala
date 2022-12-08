package year2022.day7

import year2021.day15.Day15.getClass
import year2022.day2.Day2.{parseInput, part1}
import year2022.day7.Day7.buildTree

import scala.collection.immutable.ListMap
import scala.io.Source

sealed trait Node

final case class File(size: Int) extends Node

final case class Directory() extends Node

final case class DirectoryContext(name: String, parent: String, nodes: Map[String, Node])

object Day7 {
  private val dir = """dir (\w+)""".r
  private val file = """(\d+) (.+)""".r
  private val cd = """\$ cd (\w+|\d+|\/)""".r
  private val cdSlash = """\$ cd \/""".r
  private val cdOut = """\$ cd \.\.""".r
  private val ls = """\$ ls""".r

  def part1(nodes: Map[String, Node]) = {
    val directories = nodes.filter(x => x._2 match {
      case file: File => false
      case dir: Directory => true
    })

    val sums = directories.map(key => nodes
      .view
      .filterKeys(x => x.startsWith(key._1))
      .map(_._2)
      .filter(x => x match {
        case _: File => true
        case _: Directory => false
      })
      .map(x => x.asInstanceOf[File].size).sum)

    sums.filter(_ < 100_000).sum
  }

  def parseInput(input: Seq[String]) = {
    input.foldLeft(DirectoryContext("", "", Map.empty[String, Node]))(buildTree).nodes
  }

  def buildTree(context: DirectoryContext, inputString: String): DirectoryContext = {
    val delimiter = "$"
    val DirectoryContext(contextName, contextParent, contextNodes) = context

    inputString match {
      case cdSlash() => context
      case ls() => context
      case dir(name) =>
        val updatesNodes = contextNodes + (s"$contextName$delimiter$name" -> Directory())
        DirectoryContext(contextName, contextParent, updatesNodes)
      case file(size, name) =>
        val updatesNodes = contextNodes + (s"$contextName$delimiter$name" -> File(size.toInt))
        DirectoryContext(contextName, contextParent, updatesNodes)
      case cd(name) =>
        val newName = s"$contextName$delimiter$name"
        val newCurrent = contextNodes(newName)
        DirectoryContext(newName, contextName, contextNodes)
      case cdOut() =>
        val last_index = contextName.lastIndexOf(delimiter)
        val parentName = contextName.splitAt(last_index)._1
        DirectoryContext(parentName, "", contextNodes)
    }
  }

  def main(args: Array[String]): Unit = {
    val input = Source
      .fromInputStream(getClass.getResourceAsStream("data.txt"))
      .getLines
      .toSeq

    val parsedInput = parseInput(input)
    val part1Res = part1(parsedInput)
    println(part1Res)
    //    assert(part1Res == )
  }
}
