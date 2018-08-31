using DevCut.Data.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DevCut.Logic.Processing
{
	/// <summary>
	/// Class that implements the AbstractDataRepository with type of Application and provides methods for database handling (add, update, remove).
	/// </summary>
	public class ApplicationDataRepository : AbstractDataRepository<Application>
	{
		private readonly DatabaseManager _databaseManager;
		private const int ID_COLUMN = 0;
		private const int NAME_COLUMN = 1;
		private const int CATEGORY_ID_COLUMN = 2;
		private const int DESCRIPTION_COLUMN = 3;
		private const int ICON_PATH_COLUMN = 4;
		private const int IS_FAVORITE_COLUMN = 5;
		private const string TABLE_NAME = "APPLICATION";

		
		public ApplicationDataRepository(string connectionString) : base(connectionString)
		{
			_databaseManager = new DatabaseManager();
		}

		/// <summary>
		/// Returns the application with the given id from the database.
		/// </summary>
		/// <param name="idToFind">Id of the application to find.</param>
		/// <returns>Returns the application if it is found, otherwise null is returned.</returns>
		public override Application Get(int idToFind)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var applicationCommand = connection.CreateCommand();
				applicationCommand.CommandText = $"SELECT ID, NAME, CATEGORY_ID, DESCRIPTION, ICON_PATH, IS_FAVORITE FROM {TABLE_NAME} WHERE ID=@id";
				applicationCommand.Parameters.Add(new SQLiteParameter("@id", idToFind));
				var reader = applicationCommand.ExecuteReader();
				if (reader.Read())
				{
					return Map(reader);
				}
			}
			return null;
		}

		public override IEnumerable<Application> List(int categoryId)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"SELECT ID, NAME, CATEGORY_ID, DESCRIPTION, ICON_PATH, IS_FAVORITE FROM {TABLE_NAME} WHERE CATEGORY_ID = @id";
				command.Parameters.Add(new SQLiteParameter("@id", categoryId));
				command.Prepare();
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					yield return Map(reader);
				}
			}
		}

		public override long Save(Application newApplication)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"INSERT INTO {TABLE_NAME}(NAME, CATEGORY_ID, DESCRIPTION, ICON_PATH, IS_FAVORITE) VALUES (@name, @categoryId, @description, @iconPath, @isFavorite)";
				command.Parameters.Add(new SQLiteParameter("@name", newApplication.Name));
				command.Parameters.Add(new SQLiteParameter("@categoryId", newApplication.CategoryId));
				command.Parameters.Add(new SQLiteParameter("@description", newApplication.Description));
				command.Parameters.Add(new SQLiteParameter("@iconPath", newApplication.IconPath));
				command.Parameters.Add(new SQLiteParameter("@isFavorite", newApplication.IsFavorite ? 1 : 0));
				command.Prepare();
				command.ExecuteNonQuery();
				var lastId = connection.LastInsertRowId;
				return lastId;
			}
		}

		public override void Update(Application changedApplication)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"UPDATE {TABLE_NAME} SET NAME = @name, CATEGORY_ID = @categoryId, DESCRIPTION = @description, ICON_PATH = @iconPath, IS_FAVORITE = @isFavorite WHERE ID = @id";
				command.Parameters.Add(new SQLiteParameter("@name", changedApplication.Name));
				command.Parameters.Add(new SQLiteParameter("@categoryId", changedApplication.CategoryId));
				command.Parameters.Add(new SQLiteParameter("@description", changedApplication.Description));
				command.Parameters.Add(new SQLiteParameter("@iconPath", changedApplication.IconPath));
				command.Parameters.Add(new SQLiteParameter("@isFavorite", changedApplication.IsFavorite ? 1 : 0));
				command.Parameters.Add(new SQLiteParameter("@id", changedApplication.Id));
				command.Prepare();
				command.ExecuteNonQuery();
			}
		}

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

		private Application Map(SQLiteDataReader reader)
		{
			var id = reader.GetInt32(ID_COLUMN);
			var name = reader.GetString(NAME_COLUMN);
			var categoryId = reader.GetInt32(CATEGORY_ID_COLUMN);
			var description = reader.GetString(DESCRIPTION_COLUMN);
			var iconPath = reader.GetString(ICON_PATH_COLUMN);
			var isFavorite = reader.GetInt32(IS_FAVORITE_COLUMN) > 0;
			var application = new Application(id, name, categoryId, description, iconPath, isFavorite);
			return application;
		}
	}
}
