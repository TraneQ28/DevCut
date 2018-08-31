using System;
using DevCut.Logic.IO;

namespace DevCut.TestProject
{
	class Program
	{
		static void Main(string[] args)
		{

			//var jsonParser = new JsonParser();
			//var csvXmlConverter = new CsvXmlConverter();
			//var shortcuts = csvXmlConverter.ReadFromCsv(@"Resources/Shortcuts.csv");

			//JsonParser.SerializeToJson(shortcuts, @"Resources/test.json");

			//var deserialize = JsonParser.DeserializeFromJson<Program>(@"Resources/test.json");


			CategoryController categoryController = new CategoryController();
			categoryController.WriteToJsonFile();

			Console.ReadLine();
		}
	}
}
