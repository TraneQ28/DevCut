using System;

namespace DevCut.Contracts.Interfaces
{
	public interface IProgram
	{
		Guid Id { get; set; }
		string DisplayName { get; set; }
		string DisplayVersion { get; set; }
		string InstallDate { get; set; }
		string Publisher { get; set; }
		string DisplayIconPath { get; set; }
		string Category { get; set; }
	}
}
