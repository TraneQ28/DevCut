namespace DevCut.Data.Models
{
	/// <summary>
	/// Data class that represents a shortcut with all its necessary information.
	/// </summary>
	public class Shortcut
	{
		public Shortcut(int id, string name, string keys, string description, int applicationId)
		{
			Id = id;
			Name = name;
			Keys = keys;
			Description = description;
			ApplicationId = applicationId;
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Keys { get; set; }
		public string Description { get; set; }
		public int ApplicationId { get; set; }
	}
}
