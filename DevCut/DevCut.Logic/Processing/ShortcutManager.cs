using System;
using System.IO;
using DevCut.Data.Models;
using DevCut.Logic.IO;

namespace DevCut.Logic.Processing
{
	public class ShortcutManager
	{
		public void Add(ShortcutDetails create)
		{
			ShortcutContainer shortcuts = null;
			if (File.Exists(Properties.Resources.ShortcutInformationFile))
			{
				shortcuts = JsonParser.DeserializeFromJson<ShortcutContainer>(Properties.Resources.ShortcutInformationFile);
			}
			else
			{
				shortcuts = new ShortcutContainer();
			}
			
			shortcuts.ShortcutInformation.Add(create);
			JsonParser.SerializeToJson(shortcuts, Properties.Resources.ShortcutInformationFile);
		}

		public void Edit(ShortcutDetails edit)
		{
			var shortcuts = JsonParser.DeserializeFromJson<ShortcutContainer>(Properties.Resources.ShortcutInformationFile);
			var index = shortcuts.ShortcutInformation.FindIndex(x => x.ShortcutId == edit.ShortcutId);
			if (index >= 0)
			{
				shortcuts.ShortcutInformation[index] = edit;
			}
			JsonParser.SerializeToJson(shortcuts, Properties.Resources.ShortcutInformationFile);
		}

		public void Delete(int delete)
		{
			var shortcuts = JsonParser.DeserializeFromJson<ShortcutContainer>(Properties.Resources.ShortcutInformationFile);
			shortcuts.ShortcutInformation.RemoveAll(x => x.ShortcutId == delete);
			JsonParser.SerializeToJson(shortcuts, Properties.Resources.ShortcutInformationFile);
		}

		public ShortcutContainer Get()
		{
			var shortcuts = JsonParser.DeserializeFromJson<ShortcutContainer>(Properties.Resources.ShortcutInformationFile) ?? new ShortcutContainer();
			return shortcuts;
		}
	}
}
