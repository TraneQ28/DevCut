using System;
using DevCut.Contracts.BaseClasses;
using DevCut.Contracts.Interfaces;

namespace DevCut.Data.Models
{
	/// <summary>
	/// ShortcutModel
	/// </summary>
	public class ShortcutDetails : ViewModelBase
	{
		public ShortcutDetails()
		{

		}

		public ShortcutDetails(int id, string shortcut, string shortcutDescription, string shortcutUsage)
		{
			ShortcutId = id;
			Shortcut = shortcut;
			ShortcutDescription = shortcutDescription;
			ShortcutUsage = shortcutUsage;
		}

		private int _shortcutId;

		//TODO: hier vllt mit IDs arbeiten --> zur besseren Vergleichbarkeit und Filterung 
		public int ShortcutId
		{
			get => _shortcutId;
			set
			{
				if (_shortcutId != value)
				{
					_shortcutId = value;
					OnPropertyChanged(nameof(ShortcutId));
				}
			}
		}

		private string _shortcut;

		public string Shortcut
		{
			get => _shortcut;
			set
			{
				if (_shortcut != value)
				{
					_shortcut = value;
					OnPropertyChanged(nameof(Shortcut));
				}
			}
		}

		private string _shortcutDescription;

		public string ShortcutDescription
		{
			get => _shortcutDescription;
			set
			{
				if (_shortcutDescription != value)
				{
					_shortcutDescription = value;
					OnPropertyChanged(nameof(_shortcutDescription));
				}
			}
		}

		private string _shortcutUsage;

		public string ShortcutUsage
		{
			get => _shortcutUsage;
			set
			{
				if (_shortcutUsage != value)
				{
					_shortcutUsage = value;
					OnPropertyChanged(nameof(ShortcutUsage));
				}
			}
		}
	}
}
