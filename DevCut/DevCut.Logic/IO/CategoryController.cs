using System;
using DevCut.Data.Models;

namespace DevCut.Logic.IO
{
	/// <summary>
	/// Class that is able to return all installed programs on the windows pc.
	/// </summary>
	public class CategoryController
	{
		private ProgramContainer GetAllInstalledAppsInAlphabeticalOrder()
		{
			InstallFinder finder = new InstallFinder();
			var programContainer = finder.GetAllInstalledApplications();
			programContainer.ProgramInformation.Sort((c1, c2) => String.CompareOrdinal(c1.DisplayName, c2.DisplayName)); //sort descending alphabetically

			return programContainer;
		}


		/// <summary>
		/// Returns all programs with corresponding categories and stores them in a ProgramContainer object.
		/// </summary>
		/// <returns>List of program information.</returns>
		public ProgramContainer GetAppsWithCategories()
		{
			return JsonParser.DeserializeFromJson<ProgramContainer>(Properties.Resources.InstalledProgramInformationFile);
		}

		#region -------- helper methods -----------
		/// <summary>
		/// Writes the installed programs information to the JSON-file ProgramCategories.json.
		/// </summary>
		public void WriteToJsonFile()
		{
			var installedApps = GetAllInstalledAppsInAlphabeticalOrder();
			JsonParser.SerializeToJson(installedApps, @"Resources/ProgramCategories.json");
		}

		#endregion


	}
}
