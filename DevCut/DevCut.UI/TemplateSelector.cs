using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCut.UI
{
	public class TemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var isIContentControl = item as IContentControl;
			var isFrameworkElement = item as FrameworkElement;

			if (container is FrameworkElement frameworkElement && item != null)
			{
				var name = ((IContentControl)item).Name;
				var template = frameworkElement.FindResource(name) as DataTemplate;
				return template;
			}

			return null;
		}
	}
}
