using System;

namespace DevCut.Contracts.Interfaces
{
	public interface IShortcut
	{
		Guid ShortcutId { get; set; }
		string Shortcut { get; set; }
		string ShortcutDescription { get; set; }
		string ShortcutUsage { get; set; }
	}
}
