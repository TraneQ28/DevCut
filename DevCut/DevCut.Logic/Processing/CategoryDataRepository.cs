using DevCut.Data.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DevCut.Logic.Processing
{
	/// <summary>
	/// Class that implements the BaseDataRepository with the type Category and therefore is able to alter the data in the database.
	/// </summary>
	public class CategoryDataRepository : AbstractDataRepository<Category>
	{
		private readonly DatabaseManager _databaseManager;
		private const int ID_COLUMN = 0;
		private const int NAME_COLUMN = 1;
		private const int DESCRIPTION_COLUMN = 2;
		private const int ICON_PATH_COLUMN = 3;
		private const int IS_FAVORITE_COLUMN = 4;
		private const string TABLE_NAME = "CATEGORY";

		public CategoryDataRepository(string connectionString) : base(connectionString)
		{
			_databaseManager = new DatabaseManager();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idToFind"></param>
		/// <returns></returns>
		public override Category Get(int idToFind)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"SELECT ID, NAME, DESCRIPTION, ICON_PATH, IS_FAVORITE FROM {TABLE_NAME} WHERE ID=@id";
				command.Parameters.Add(new SQLiteParameter("@id", idToFind));
				var reader = command.ExecuteReader();
				if (reader.Read())
				{
					return Map(reader);
				}
			}
			return null;
		}

		/// <summary>
		/// Returns a enumeration of categories from the database.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public override IEnumerable<Category> List(int id)
		{
			var connection = _databaseManager.GetConnection(ConnectionString);
			var command = connection.CreateCommand();
			command.CommandText = $"SELECT ID, NAME, DESCRIPTION, ICON_PATH, IS_FAVORITE FROM {TABLE_NAME}";
			var reader = command.ExecuteReader();
			while (reader.Read())
			{
				yield return Map(reader);
			}
		}

		/// <summary>
		/// Saves the category object to the database.
		/// </summary>
		/// <param name="newCategory"></param>
		/// <returns></returns>
		public override long Save(Category newCategory)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"INSERT INTO {TABLE_NAME}(NAME, DESCRIPTION, ICON_PATH, IS_FAVORITE) VALUES (@name, @description, @iconPath, @isFavorite)";
				command.Parameters.Add(new SQLiteParameter("@name", newCategory.Name));
				command.Parameters.Add(new SQLiteParameter("@description", newCategory.Description));
				command.Parameters.Add(new SQLiteParameter("@iconPath", newCategory.IconPath));
				command.Parameters.Add(new SQLiteParameter("@isFavorite", newCategory.IsFavorite ? 1 : 0));
				command.Prepare();
				command.ExecuteNonQuery();
				var lastId = connection.LastInsertRowId;
				return lastId;
			}
		}

		/// <summary>
		/// Updates the given Category in the corresponding table in the sqlite database.
		/// </summary>
		/// <param name="changedCategory">Category to change in the table.</param>
		public override void Update(Category changedCategory)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"UPDATE {TABLE_NAME} SET NAME = @name, DESCRIPTION = @description, ICON_PATH = @iconPath, IS_FAVORITE = @isFavorite WHERE ID = @id";
				command.Parameters.Add(new SQLiteParameter("@name", changedCategory.Name));
				command.Parameters.Add(new SQLiteParameter("@description", changedCategory.Description));
				command.Parameters.Add(new SQLiteParameter("@iconPath", changedCategory.IconPath));
				command.Parameters.Add(new SQLiteParameter("@isFavorite", changedCategory.IsFavorite ? 1 : 0));
				command.Parameters.Add(new SQLiteParameter("@id", changedCategory.Id));
				command.Prepare();
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Removes the Category with the id.
		/// </summary>
		/// <param name="idToRemove"></param>
		public override void Remove(int idToRemove)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"DELETE FROM {TABLE_NAME} WHERE ID = @id";
				command.Parameters.Add(new SQLiteParameter("@id", idToRemove));
				command.Prepare();
				command.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Maps the database types to usable data types.
		/// </summary>
		/// <param name="reader">Database reader.</param>
		/// <returns>New category object.</returns>
		private Category Map(SQLiteDataReader reader)
		{
			var id = reader.GetInt32(ID_COLUMN);
			var name = reader.GetString(NAME_COLUMN);
			var description = reader.GetString(DESCRIPTION_COLUMN);
			var iconPath = reader.GetString(ICON_PATH_COLUMN);
			var isFavorite = reader.GetInt32(IS_FAVORITE_COLUMN) > 0;
			var category = new Category(id, name, description, iconPath, isFavorite);
			return category;
		}
	}
}
