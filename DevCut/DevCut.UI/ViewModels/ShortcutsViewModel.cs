using DevCut.Contracts.BaseClasses;
using DevCut.Data.Models;
using DevCut.Logic.Processing;
using DevCut.UI.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DevCut.UI.ViewModels
{
	class ShortcutsViewModel : ViewModelBase, IContentControl
	{
		public string Name => "ShortcutsTemplate";
		public string DisplayName => "Shortcuts";
		public int CurrentApplication { get; set; }
		private ObservableCollection<ShortcutViewModel> _cards;
		public ObservableCollection<ShortcutViewModel> Cards
		{
			get
			{
				return _cards;
			}
			set
			{
				_cards = value;
				OnPropertyChanged(nameof(Cards));
			}
		}

		private readonly AbstractDataRepository<Shortcut> _shortcutDataRepository;

		public ShortcutsViewModel(int id)
		{
			CurrentApplication = id;
			Cards = new ObservableCollection<ShortcutViewModel>();
			_shortcutDataRepository = new ShortcutDataRepository($"Data Source={DatabaseManager.DatabaseName};Version=3;");
			LoadShortcuts(id);
			SaveCommand = new DelegateCommand(OnSave);
			AddCommand = new DelegateCommand(OnAdd);
			RemoveCommand = new DelegateCommand(OnRemove);
			EditCommand = new DelegateCommand(OnEdit);
			SaveEditCommand = new DelegateCommand(OnSaveEdit);
		}

		private void LoadShortcuts(int id)
		{
			Cards.Clear();
			var shortcuts = _shortcutDataRepository.List(id);
			foreach (var shortcut in shortcuts)
			{
				Cards.Add(new ShortcutViewModel
				{
					Shortcut = new ShortcutDetails
					{
						ShortcutId = shortcut.Id,
						Shortcut = shortcut.Name,
						ShortcutUsage = shortcut.Keys
					}
				});
			}
		}

		private string _shortcutName;
		public string NewShortcut
		{
			get => _shortcutName;
			set
			{
				_shortcutName = value;
				OnPropertyChanged(nameof(NewShortcut));
			}
		}

		private string _shortcutKeys;
		public string NewShortcutKeys
		{
			get => _shortcutKeys;
			set
			{
				_shortcutKeys = value;
				OnPropertyChanged(nameof(NewShortcutKeys));
			}
		}

		private string _shortcutNameUpdate;
		public string ShortcutNameUpdate
		{
			get => _shortcutNameUpdate;
			set
			{
				_shortcutNameUpdate = value;
				OnPropertyChanged(nameof(ShortcutNameUpdate));
			}
		}

		private string _shortcutKeysUpdate;
		public string ShortcutKeysUpdate
		{
			get => _shortcutKeysUpdate;
			set
			{
				_shortcutKeysUpdate = value;
				OnPropertyChanged(nameof(ShortcutKeysUpdate));
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

		private int _indexUpdate;
		public int IndexUpdate
		{
			get => _indexUpdate;
			set
			{
				_indexUpdate = value;
				OnPropertyChanged(nameof(IndexUpdate));
			}
		}

		public ICommand SaveCommand { get; set; }
		private void OnSave(object parameter)
		{
			if (string.IsNullOrEmpty(NewShortcut) || string.IsNullOrEmpty(NewShortcutKeys))
			{
				Index = 0;
				NewShortcut = string.Empty;
				NewShortcutKeys = string.Empty;
				return;
			}
			var shortcut = new Shortcut(0, NewShortcut, NewShortcutKeys, "kram", CurrentApplication);
			_shortcutDataRepository.Save(shortcut);
			Index = 0;
			NewShortcut = string.Empty;
			NewShortcutKeys = string.Empty;
			LoadShortcuts(CurrentApplication);
		}

		public ICommand AddCommand { get; set; }
		private void OnAdd(object parameter)
		{
			Index = 2;
		}

		private ICommand _removeCommand;
		public ICommand RemoveCommand
		{
			get { return _removeCommand; }
			set
			{
				_removeCommand = value;
				OnPropertyChanged(nameof(RemoveCommand));
			}
		}

		private void OnRemove(object parameter)
		{
			if (int.TryParse(parameter?.ToString(), out int shortcutId))
			{
				_shortcutDataRepository.Remove(shortcutId);
			}
			LoadShortcuts(CurrentApplication);
		}

		private ICommand _editCommand;
		public ICommand EditCommand
		{
			get => _editCommand;
			set
			{
				_editCommand = value;
				OnPropertyChanged(nameof(EditCommand));
			}
		}

		private ICommand _saveEditCommand;
		public ICommand SaveEditCommand
		{
			get => _saveEditCommand;
			set
			{
				_saveEditCommand = value;
				OnPropertyChanged(nameof(SaveEditCommand));
			}
		}

		private Shortcut _editShortcut = null;

		private void OnSaveEdit(object parameter)
		{
			if (string.IsNullOrEmpty(ShortcutNameUpdate) || string.IsNullOrEmpty(ShortcutKeysUpdate))
			{
				IndexUpdate = 0;
				return;
			}
			var shortcut = new Shortcut(_editShortcut.Id, ShortcutNameUpdate, ShortcutKeysUpdate, _editShortcut.Description, CurrentApplication);
			_shortcutDataRepository.Update(shortcut);
			IndexUpdate = 0;
			ShortcutNameUpdate = string.Empty;
			ShortcutKeysUpdate = string.Empty;
			_editShortcut = null;
			LoadShortcuts(CurrentApplication);
		}

		private void OnEdit(object parameter)
		{
			IndexUpdate = 3;
			if (int.TryParse(parameter?.ToString(), out int shortcutId))
			{
				_editShortcut = _shortcutDataRepository.Get(shortcutId);
				ShortcutKeysUpdate = _editShortcut.Keys;
				ShortcutNameUpdate = _editShortcut.Name;
				// geänderte Werte eintragen
				// persistieren
			}
			LoadShortcuts(CurrentApplication);
		}

	}
}
