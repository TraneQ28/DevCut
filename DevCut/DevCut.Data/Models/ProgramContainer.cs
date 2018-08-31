using System.Collections.Generic;
using DevCut.Contracts.Interfaces;

namespace DevCut.Data.Models
{
	/// <summary>
	/// Wrapper class for a list of programs and its information.
	/// </summary>
	public class ProgramContainer
	{
		public ProgramContainer()
		{
			ProgramInformation = new List<IProgram>();
		}

		public List<IProgram> ProgramInformation { get; set; }
	}
}
