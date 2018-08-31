using System;
using DevCut.Data.Models;
using Microsoft.Win32;

namespace DevCut.Logic.IO
{
	/// <summary>
	/// Class that is responsible for getting all installed applications on the operating system.
	/// </summary>
	public class InstallFinder
	{
		/// <summary>
		/// Reads all installed applications with registry entry under Windows and saves them in a
		/// </summary>
		/// <returns></returns>
		public ProgramContainer GetAllInstalledApplications()
		{
			var listOfAllApplications = new ProgramContainer();
			string registryKey = Properties.Resources.RegistryKeyPath;
			using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
			{
				//Add operating system to the list
				//TODO: fill empty parameters and extract to own method
				listOfAllApplications.ProgramInformation.Add(new InstalledProgramInformation(new Guid(),
					GetFriendlyOperatingSystemName(),
					string.Empty, string.Empty, "Microsoft", string.Empty, "OperatingSystem"));

				if (key != null)
				{
					foreach (string subkeyName in key.GetSubKeyNames())
					{
						using (RegistryKey subkey = key.OpenSubKey(subkeyName))
						{
							if (subkey != null)
							{
								PrintNameAndVersionToConsole(subkey);

								var displayName = HKLM_GetString(subkey, "DisplayName");
								if (displayName != null)
								{
									var installedProgramInformation =
										new InstalledProgramInformation(new Guid(), displayName,
											HKLM_GetString(subkey, "DisplayVersion"),
											HKLM_GetString(subkey, "InstallDate"),
											HKLM_GetString(subkey, "Publisher"),
											HKLM_GetString(subkey, "DisplayIcon"), string.Empty);
									listOfAllApplications.ProgramInformation.Add(installedProgramInformation);
								}
							}
						}
					}
				}
			}
			return listOfAllApplications;
		}

		/// <summary>
		/// Returns the Windows registry value using the key.
		/// </summary>
		/// <param name="registryKey">Registry node in Windows.</param>
		/// <param name="key">Key to search for.</param>
		/// <returns>Returns the value of the registry key.</returns>
		private string HKLM_GetString(RegistryKey registryKey, string key)
		{
			try
			{
				if (registryKey == null) return "";
				return (string)registryKey.GetValue(key);
			}
			catch { return ""; }
		}

		/// <summary>
		/// Returns the Windows registry value using the key and the registry path.
		/// </summary>
		/// <param name="path">Path to registry.</param>
		/// <param name="key">Key to search for.</param>
		/// <returns>Returns the value of the registry key.</returns>
		private string HKLM_GetString(string path, string key)
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(path);
				if (registryKey == null) return "";
				return (string)registryKey.GetValue(key);
			}
			catch { return ""; }
		}


		/// <summary>
		/// Get Friendly name of operating system
		/// </summary>
		/// <returns>Friendly name.</returns>
		private string GetFriendlyOperatingSystemName()
		{
			string productName = HKLM_GetString(Properties.Resources.RegistryKeyWindowsCurrentVersion, "ProductName");
			string csdVersion = HKLM_GetString(Properties.Resources.RegistryKeyWindowsCurrentVersion, "CSDVersion");
			if (productName != "")
			{
				return (productName.StartsWith("Microsoft") ? "" : "Microsoft ") + productName +
					   (csdVersion != "" ? " " + csdVersion : "");
			}
			return "";
		}

		/// <summary>
		/// Prints name and version of registry subkey to console.
		/// </summary>
		/// <param name="subkey"></param>
		private void PrintNameAndVersionToConsole(RegistryKey subkey)
		{
			Console.WriteLine(subkey.GetValue("DisplayName"));
			Console.WriteLine(subkey.GetValue("DisplayVersion"));
		}
	}
}
