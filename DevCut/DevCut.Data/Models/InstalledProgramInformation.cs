using System;
using DevCut.Contracts.Interfaces;

namespace DevCut.Data.Models
{
	/// <summary>
	/// Data class that holds information of a installed program containing the name, version, install date, publisher, icon path and category.
	/// </summary>
	public class InstalledProgramInformation : IProgram
	{
		public InstalledProgramInformation(Guid id, string displayName, string displayVersion, string installDate, string publisher, string displayIconPath, string category)
		{
			Id = id;
			DisplayName = displayName;
			DisplayVersion = displayVersion;
			InstallDate = installDate;
			Publisher = publisher;
			DisplayIconPath = displayIconPath;
			Category = category;
		}

		public Guid Id { get; set; }
		public string DisplayName { get; set; }
		public string DisplayVersion { get; set; }
		public string InstallDate { get; set; }
		public string Publisher { get; set; }
		public string DisplayIconPath { get; set; }
		public string Category { get; set; }
	}
}
