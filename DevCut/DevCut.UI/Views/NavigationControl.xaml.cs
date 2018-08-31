using System.Windows;
using System.Windows.Controls;
using DevCut.Contracts.Interfaces;
using DevCut.UI.ViewModels;

namespace DevCut.UI.Views
{
	/// <summary>
	/// Interaktionslogik für NavigationControl.xaml
	/// </summary>
	public partial class NavigationControl : UserControl, IPrevious
	{
		public NavigationControl()
		{
			InitializeComponent();
			//Previous = this;
			//DataContext = new MainViewModel();
		}

		//TODO: use memento design pattern
		public IPrevious SelectedItem
		{
			get => (IPrevious)GetValue(SelectedItemProperty);
			set => SetValue(SelectedItemProperty, value);
		}

		// Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SelectedItemProperty =
			DependencyProperty.Register("SelectedItem", typeof(IPrevious), typeof(NavigationControl), new PropertyMetadata(null, OnSelectedItemChanged));

		private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((NavigationControl)d).BuildPath(e.NewValue as IPrevious);
		}

		private void BuildPath(IPrevious node)
		{
			BreadcrumbNavigation.Children.Clear();
			while (node != null)
			{
				Button b = new Button {Content = node.ToString()};
				b.Click += NavigationButtonClicked;
				b.Tag = node;
				BreadcrumbNavigation.Children.Insert(0, b);
				node = node.Previous;
				//if we have more parents then we want a seperator
				if (node != null)
				{
					Label seperator = new Label {Content = ">>"};
					BreadcrumbNavigation.Children.Insert(0, seperator);
				}
			}
		}

		private void NavigationButtonClicked(object sender, RoutedEventArgs e)
		{
			IPrevious selectedItem = ((Button)sender).Tag as IPrevious;
			SelectedItem = selectedItem;
		}

		public IPrevious Previous { get; }
	}
}
