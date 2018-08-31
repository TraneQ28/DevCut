using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using DevCut.Data.Models;
using DevCut.Logic.Processing;

namespace DevCut.Logic.IO
{
	/// <summary>
	/// Class that provides methods for reading and writing shortcuts from and to csv- and xml-files.
	/// </summary>
	public class CsvXmlConverter
	{
		/// <summary>
		/// Reads the
		/// </summary>
		/// <param name="csvPath"></param>
		/// <returns></returns>
		public ShortcutContainer ReadFromCsv(string csvPath)
		{
			string[] lines = File.ReadAllLines(csvPath, Encoding.UTF8);

			return ShortcutInformationParser.Parse(lines);
		}

		public void SerializeToXml(ShortcutContainer listOfShortcutInformation, string xmlPath)
		{
			try
			{
				var serializer = new XmlSerializer(typeof(ShortcutContainer));
				using (var writer = File.Open(xmlPath, FileMode.OpenOrCreate, FileAccess.Write))
				{
					serializer.Serialize(writer, listOfShortcutInformation);
				}
			}
			catch (InvalidOperationException invalidOperationException)
			{
				Console.WriteLine(invalidOperationException.Message);
			}
			catch (ArgumentNullException argumentNullException)
			{
				Console.WriteLine(argumentNullException.Message);
			}
			catch (ArgumentOutOfRangeException argumentOutOfRangeException)
			{
				Console.WriteLine(argumentOutOfRangeException.Message);
			}
			catch (ArgumentException argumentException)
			{
				Console.WriteLine(argumentException.Message);
			}
			catch (PathTooLongException pathTooLongException)
			{
				Console.WriteLine(pathTooLongException.Message);
			}
			catch (DirectoryNotFoundException directoryNotFoundException)
			{
				Console.WriteLine(directoryNotFoundException.Message);
			}
			catch (FileNotFoundException fileNotFoundException)
			{
				Console.WriteLine(fileNotFoundException.Message);
			}
			catch (IOException ioException)
			{
				Console.WriteLine(ioException.Message);
			}
			catch (UnauthorizedAccessException unauthorizedAccessException)
			{
				Console.WriteLine(unauthorizedAccessException.Message);
			}
			catch (NotSupportedException notSupportedException)
			{
				Console.WriteLine(notSupportedException.Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xmlPath"></param>
		/// <returns></returns>
		public ShortcutContainer DeserializeFromXml(string xmlPath)
		{
			ShortcutContainer listOfShortcutInformation = null;

			try
			{
				var deserializer = new XmlSerializer(typeof(ShortcutContainer));

				using (TextReader reader = new StreamReader(xmlPath))
				{
					object obj = deserializer.Deserialize(reader);
					ShortcutContainer xmlData = (ShortcutContainer)obj;
					listOfShortcutInformation = xmlData;
				}
			}
			catch (InvalidOperationException invalidOperationException)
			{
				Console.WriteLine(invalidOperationException.Message);
			}

			return listOfShortcutInformation;
		}

		/// <summary>
		/// Reads the shorcuts from the given csv file and writes them to the given xml file.
		/// </summary>
		/// <param name="csvPath">Csv file to read.</param>
		/// <param name="xmlPath">Xml file to write to.</param>
		public void ReadFromCsvAndWriteToXml(string csvPath, string xmlPath)
		{
			SerializeToXml(ReadFromCsv(csvPath), xmlPath);
		}
	}
}
