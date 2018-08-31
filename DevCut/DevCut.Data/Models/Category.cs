namespace DevCut.Data.Models
{
	/// <summary>
	/// Data class for Categories.
	/// </summary>
	public class Category
	{
		public Category(int id, string name, string description, string iconPath, bool isFavorite)
		{
			Id = id;
			Name = name;
			Description = description;
			IconPath = iconPath;
			IsFavorite = isFavorite;
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string IconPath { get; set; }
		public bool IsFavorite { get; set; }
	}
}
