using DevCut.Contracts.BaseClasses;
using DevCut.Data.Models;

namespace DevCut.UI.ViewModels
{
	class ShortcutViewModel : ViewModelBase
	{
		private ShortcutDetails _shortcut;
		public ShortcutViewModel()
		{
		}

		public ShortcutDetails Shortcut
		{
			get => _shortcut;
			set
			{
				if (_shortcut == value)
					return;
				_shortcut = value;
				OnPropertyChanged(nameof(Shortcut));
			}
		}
	}
}
