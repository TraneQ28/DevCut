using System;
using DevCut.Data.Models;

namespace DevCut.Logic.Processing
{
	/// <summary>
	/// Parser class that is responsible for parsing information from strings to type ShortcutDetails and vice versa.
	/// </summary>
	public class ShortcutInformationParser
	{
		/// <summary>
		/// Parses lines of strings to a shortcut object by splitting the entries with the delimiters comma and semicolon.
		/// </summary>
		/// <param name="lines">String array that contains shortcut information seperated by a delimiter.</param>
		/// <returns>Parsed shortcut object.</returns>
		public static ShortcutContainer Parse(string[] lines)
		{
			var shortcutContainer = new ShortcutContainer();

			foreach (var line in lines)
			{
				//TODO: Testen auf Fehler
				string[] shortcutInfo = line.Split(',', ';');
				shortcutContainer.ShortcutInformation.Add(new ShortcutDetails(0, shortcutInfo[0], shortcutInfo[1], shortcutInfo[2]));
			}

			return shortcutContainer;
		}
	}
}
