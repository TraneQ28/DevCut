using System.ComponentModel;

namespace DevCut.Contracts.BaseClasses
{
	/// <summary>
	/// Base class that implements the INotifyPropertyChanged interface and is responsible for handling changes of properties in the view.
	/// </summary>
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
