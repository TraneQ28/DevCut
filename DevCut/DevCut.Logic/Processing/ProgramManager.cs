using System.Linq;
using DevCut.Contracts.Interfaces;
using DevCut.Data.Models;
using DevCut.Logic.IO;

namespace DevCut.Logic.Processing
{
	public class ProgramManager : IProgramManager
	{
		public void CreateNewProgram(IProgram create)
		{
			var programs = JsonParser.DeserializeFromJson<ProgramContainer>(Properties.Resources.InstalledProgramInformationFile);
			programs.ProgramInformation.Add(create);
			JsonParser.SerializeToJson(programs, Properties.Resources.ShortcutInformationFile);
		}

		public void EditProgram(IProgram edit)
		{
			var programs = JsonParser.DeserializeFromJson<ProgramContainer>(Properties.Resources.InstalledProgramInformationFile);
			var firstOrDefault = programs.ProgramInformation.FirstOrDefault(x => x.Id == edit.Id);
			if (firstOrDefault != null)
			{
				var index = programs.ProgramInformation.IndexOf(firstOrDefault);
				programs.ProgramInformation[index] = edit;
			}
			JsonParser.SerializeToJson(programs, Properties.Resources.ShortcutInformationFile);
		}

		public void DeleteExistingProgram(IProgram delete)
		{
			var programs = JsonParser.DeserializeFromJson<ProgramContainer>(Properties.Resources.InstalledProgramInformationFile);
			programs.ProgramInformation.Remove(delete);
			JsonParser.SerializeToJson(programs, Properties.Resources.ShortcutInformationFile);
		}
	}
}
