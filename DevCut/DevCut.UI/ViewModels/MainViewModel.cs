using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevCut.Contracts.BaseClasses;
using DevCut.UI.Commands;

namespace DevCut.UI.ViewModels
{
	/// <summary>
	/// Main viewmodel that is responsible for processing information shown on the main view of the application (home site).
	/// </summary>
	class MainViewModel : ViewModelBase
	{
		private readonly CategorieOverviewViewModel _categorieOverviewView;

		public MainViewModel()
		{
			//ButtonViewModel = new ButtonViewModel();
			ShortcutViewModel = new ShortcutViewModel();
			InitMenuItems();
			_categorieOverviewView = new CategorieOverviewViewModel();
			_categorieOverviewView.NavigateToSubView += (sender, args) =>
			{
				var nextViewId = args.Id;
				var nextView = new ApplicationOverviewViewModel(nextViewId);
				nextView.Navigate += (sender2, targetArgs) =>
				{
					CurrentView = targetArgs.Target;
				};
				CurrentView = nextView;
			};
			CurrentView = _categorieOverviewView;
		}

		private void InitMenuItems()
		{
			var homeMenuItem = new MenuItemViewModel();
			homeMenuItem.Navigate += NavigateToOverview;
			homeMenuItem.DisplayName = "Home";
			homeMenuItem.ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-home-filled-100.png";

			//TODO: add settings
			//var settingsMenuItem = new MenuItemViewModel();
			//settingsMenuItem.Navigate += NavigateToSettings;
			//settingsMenuItem.DisplayName = "Settings";
			//settingsMenuItem.ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-Settings Filled-100.png";

			//TODO: link to database
			var favoritesMenuItem = new MenuItemViewModel();
			favoritesMenuItem.Navigate += NavigateToFavorites;
			favoritesMenuItem.DisplayName = "Favorites";
			favoritesMenuItem.ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-star-filled-100.png";

			var menuItems = new List<MenuItemViewModel>
			{
				homeMenuItem,
				//settingsMenuItem,
				favoritesMenuItem
			};
			MenuItems = menuItems;
		}

		private void NavigateToSubview(Guid id)
		{
			var overview = new CategorieOverviewViewModel();
			overview.NavigateToSubView += (sender, args) =>
			{
				var nextViewId = args.Id;
				var nextView = new ApplicationOverviewViewModel(nextViewId);
				nextView.Navigate += (sender2, targetArgs) =>
				{
					CurrentView = targetArgs.Target;
				};
				CurrentView = nextView;
			};
		}

		private void NavigateToOverview(object sender, EventArgs args)
		{
			CurrentView = _categorieOverviewView;
		}

		private void NavigateToSettings(object sender, EventArgs args)
		{
			CurrentView = _categorieOverviewView;
		}

		private void NavigateToFavorites(object sender, EventArgs args)
		{
			CurrentView = _categorieOverviewView;
		}

		/*View Models*/
		public ShortcutViewModel ShortcutViewModel { get; }
		public CategorieOverviewViewModel ApplicationsOverviewViewModel { get; set; }
		public ApplicationOverviewViewModel NavigationViewModel { get; set; }

		private IContentControl _currentView;
		public IContentControl CurrentView
		{
			get => _currentView;
			set
			{
				if (_currentView == value)
					return;
				_currentView = value;
				OnPropertyChanged(nameof(CurrentView));
			}
		}

		private List<MenuItemViewModel> _menuItems;
		public List<MenuItemViewModel> MenuItems
		{
			get => _menuItems;
			set
			{
				_menuItems = value;
				OnPropertyChanged(nameof(MenuItems));
			}
		}

		/*Properties*/
		private string _search;
		public string Search
		{
			get => _search;
			set
			{
				if (_search == value)
					return;
				_search = value;
				OnPropertyChanged(nameof(Search));
			}
		}

		//obsolete
		//private ICommand _osButtonClickCommand;
		//private ICommand _devButtonClickCommand;
		//private ICommand _designButtonClickCommand;
		//private ICommand _gamesButtonClickCommand;
		//private ICommand _webButtonClickCommand;
		//private ICommand _miscellaneousButtonClickCommand;
		//private ICommand _searchCommand;

		//public ICommand OsButtonClickCommand => _osButtonClickCommand ?? (_osButtonClickCommand = new DelegateCommand(ClickOsButton));
		////public ICommand ButtonClickCommand => new DelegateCommand(ClickOsButton);
		//public ICommand DevButtonClickCommand => _devButtonClickCommand ?? (_devButtonClickCommand = new DelegateCommand(ClickDevButton));
		//public ICommand DesignButtonClickCommand => _designButtonClickCommand ?? (_designButtonClickCommand = new DelegateCommand(ClickDesignButton));
		//public ICommand GamesButtonClickCommand => _gamesButtonClickCommand ?? (_gamesButtonClickCommand = new DelegateCommand(ClickGamesButton));
		//public ICommand WebButtonClickCommand => _webButtonClickCommand ?? (_webButtonClickCommand = new DelegateCommand(ClickWebButton));
		//public ICommand MiscellaneousButtonClickCommand => _miscellaneousButtonClickCommand ?? (_miscellaneousButtonClickCommand = new DelegateCommand(ClickMiscellaneousButton));
		//public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(DoSearch));

		private void DoSearch(object parameter)
		{
			//TODO: dynamic search while typing as optional feature (on hold) 
			if (parameter is string searchKey && searchKey.Length > 2)
			{
				// do search
			}
		}
	}
}
