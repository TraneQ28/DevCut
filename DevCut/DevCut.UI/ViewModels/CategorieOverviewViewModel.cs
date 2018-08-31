using DevCut.Contracts.BaseClasses;
using DevCut.Data.Models;
using DevCut.Logic.Processing;
using DevCut.UI.Commands;
using DevCut.UI.Events;
using DevCut.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DevCut.UI.ViewModels
{
	/// <summary>
	/// ViewModel class that provides logic for the category overview.
	/// </summary>
	public class CategorieOverviewViewModel : ViewModelBase, IContentControl
	{
		public string Name => "ApplicationsOverviewTemplate";
		public string DisplayName => "Application overview";
		public EventHandler<NavigationEventArgs> NavigateToSubView { get; set; }
		public List<CardModel> Cards { get; set; }

		private readonly AbstractDataRepository<Category> _categoryDataRepository;
		private ICommand _selectEntryCommand;
		public ICommand SelectEntryCommand => _selectEntryCommand ?? (_selectEntryCommand = new DelegateCommand(SelectCategory));

		public CategorieOverviewViewModel()
		{
			_categoryDataRepository = new CategoryDataRepository($"Data Source={DatabaseManager.DatabaseName};Version=3;");
			Cards = new List<CardModel>();
			LoadCategories();
			AddCategory();
			//Cards = new List<CardModel>
			//{
			//	new CardModel
			//	{
			//		DisplayName = "Operating systems",
			//		ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-System Task Filled-100.png"
			//	},
			//	new CardModel
			//	{
			//		DisplayName = "Development",
			//		ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-Code Filled-100.png"
			//	},
			//	new CardModel
			//	{
			//		DisplayName = "Design",
			//		ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-Design Filled-100.png"
			//	},
			//	new CardModel
			//	{
			//		DisplayName = "Games",
			//		ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-Game Controller Filled-100.png"
			//	},
			//	new CardModel
			//	{
			//		DisplayName = "Web",
			//		ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-Internet Filled-100.png"
			//	},
			//	new CardModel
			//	{
			//		DisplayName = "Miscellaneous",
			//		ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-Device Manager Filled-100.png"
			//	}
			//};
		}

		private void LoadCategories()
		{
			var categories = _categoryDataRepository.List(0);
			foreach (var category in categories)
			{
				Cards.Add(new CardModel
				{
					DisplayName = category.Name,
					Id = category.Id,
					ImageSource = category.IconPath
				});
			}
		}

		private void AddCategory()
		{
			Cards.Add(new CardModel
			{
				DisplayName = "Hinzufügen",
				Id = _categoryDataRepository.List(0).Count() + 1,
				ImageSource = "pack://application:,,,/DevCut.UI;component/Resources/icons8-plus-math-100.png"
			});
			
		}

		private void SelectCategory(object parameter)
		{
			if (int.TryParse(parameter.ToString(), out int id))
			{
				NavigateToSubView?.Invoke(this, new NavigationEventArgs(id));
			}
		}


	}
}
