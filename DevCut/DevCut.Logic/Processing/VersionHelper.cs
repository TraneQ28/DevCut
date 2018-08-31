using System.Diagnostics;

namespace DevCut.Logic.Processing
{
	class VersionHelper
	{
		public static string GetFileVersion()
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
			string version = fileVersionInfo.FileVersion;

			return version;
		}
	}
}
