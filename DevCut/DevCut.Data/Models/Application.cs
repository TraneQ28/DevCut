namespace DevCut.Data.Models
{
	/// <summary>
	/// Data class for Application with necessary attributes.
	/// </summary>
	public class Application
	{
		public Application(int id, string name, int categoryId, string description, string iconPath, bool isFavorite)
		{
			Id = id;
			Name = name;
			Description = description;
			IconPath = iconPath;
			IsFavorite = isFavorite;
			CategoryId = categoryId;
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public string IconPath { get; set; }
		public bool IsFavorite { get; set; }
	}
}
