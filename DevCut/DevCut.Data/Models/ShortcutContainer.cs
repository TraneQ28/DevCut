using System.Collections.Generic;

namespace DevCut.Data.Models
{

	/// <summary>
	/// 
	/// </summary>
	public class ShortcutContainer
	{
		/// <summary>
		/// 
		/// </summary>
		public ShortcutContainer()
		{
			ShortcutInformation = new List<ShortcutDetails>();
		}

		//TODO: zusätzliche Informationen finden, die hier sinnvoll wären

		/// <summary>
		/// 
		/// </summary>
		public List<ShortcutDetails> ShortcutInformation { get; set; }
	}
}
