using DevCut.Contracts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCut.UI.Events
{
	public class NavigationEventArgs : EventArgs
	{
		public NavigationEventArgs(int id)
		{
			Id = id;
		}

		public NavigationEventArgs(IContentControl target)
		{
			Target = target;
		}

		public int Id { get; set; }
		public IContentControl Target { get; set; }
	}
}
