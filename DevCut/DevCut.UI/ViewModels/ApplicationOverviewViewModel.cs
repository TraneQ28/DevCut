using DevCut.Contracts.BaseClasses;
using DevCut.Logic.Processing;
using DevCut.Data.Models;
using DevCut.UI.Events;
using DevCut.UI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevCut.UI.Commands;
using System.Collections.ObjectModel;

namespace DevCut.UI.ViewModels
{
	/// <summary>
	/// ViewModel class that provides logic for the application overview.
	/// </summary>
	class ApplicationOverviewViewModel : ViewModelBase, IContentControl
	{
		public string Name => "ApplicationsOverviewTemplate";
		public string DisplayName => "Application subview";
		public int CurrentViewId { get; set; }
		public ObservableCollection<CardModel> Cards { get; set; }

		private readonly AbstractDataRepository<Application> _applicationDataRepository;
		private ICommand _selectEntryCommand;
		public ICommand SelectEntryCommand => _selectEntryCommand ?? (_selectEntryCommand = new DelegateCommand(SelectCategory));

		public ApplicationOverviewViewModel(int id)
		{
			_applicationDataRepository = new ApplicationDataRepository($"Data Source={DatabaseManager.DatabaseName};Version=3;");
			Cards = new ObservableCollection<CardModel>();
			CurrentViewId = id;
			SaveCommand = new DelegateCommand(OnSave);
			LoadViewData(id);
			AddApplication();
		}

		private void LoadViewData(int id)
		{
			var applications = _applicationDataRepository.List(id);
			foreach (var application in applications)
			{
				Cards.Add(new CardModel
				{
					DisplayName = application.Name,
					Id = application.Id
				});
			}
		}

		private int _index;
		public int Index

		{
			get => _index;
			set
			{
				_index = value;
				OnPropertyChanged(nameof(Index));
			}
		}

		private void AddApplication()
		{
			Cards.Add(new CardModel
			{
				DisplayName = "Hinzufügen",
				Id = 0,
				ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-plus-math-100.png"
			});

		}

		public EventHandler<NavigationEventArgs> Navigate { get; set; }

		private void SelectCategory(object parameter)
		{
			if (int.TryParse(parameter.ToString(), out int id))
			{
				if (id == 0)
				{
					Index = 3;
					return;
				}
				var args = new NavigationEventArgs(id) {Target = new ShortcutsViewModel(id)};
				Navigate?.Invoke(this, args);
			}
		}

		private string _newName;
		public string NewName
		{
			get => _newName;
			set
			{
				_newName = value;
				OnPropertyChanged(nameof(NewName));
			}
		}

		public ICommand SaveCommand { get; set; }
		private void OnSave(object parameter)
		{
			if (string.IsNullOrEmpty(NewName))
			{
				Index = 0;
				NewName = string.Empty;
				return;
			}
			var application = new Application(0, NewName, CurrentViewId, "kram", "", false);
			_applicationDataRepository.Save(application);
			Index = 0;
			NewName = string.Empty;
			LoadViewData(CurrentViewId);
		}
	}
}
