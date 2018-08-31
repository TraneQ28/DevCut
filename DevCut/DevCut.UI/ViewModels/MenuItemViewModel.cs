using DevCut.Contracts.BaseClasses;
using DevCut.UI.Commands;
using System;
using System.Windows.Input;

namespace DevCut.UI.ViewModels
{
	/// <summary>
	/// ViewModel class that provides logic for the menu items.
	/// </summary>
	public class MenuItemViewModel : ViewModelBase, IContentControl
	{
		public MenuItemViewModel()
		{
			ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-home-filled-100.png";
			// ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-Settings Filled-100.png"; <--- SettingsMenuItem
			ClickCommand = new DelegateCommand(OnNavigate);
		}

		private void OnNavigate(object parameter)
		{
			Navigate?.Invoke(this, new EventArgs());
		}

		public string Name => "HomeTabItemTemplate";
		public string DisplayName { get; set; }

		private string _imageSource;
		public string ImageSource
		{
			get => _imageSource;
			set
			{
				_imageSource = value;
				OnPropertyChanged(nameof(ImageSource));
			}
		}

		private ICommand _clickCommand;
		public ICommand ClickCommand
		{
			get => _clickCommand;
			set
			{
				_clickCommand = value;
				OnPropertyChanged(nameof(ClickCommand));
			}
		}

		public EventHandler Navigate { get; set; }
	}
}
