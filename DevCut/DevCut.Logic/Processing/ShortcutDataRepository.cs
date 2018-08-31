using DevCut.Data.Models;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DevCut.Logic.Processing
{
	/// <summary>
	/// Class that implements the AbstractDataRepository with type of Shortcut and provides methods for database handling (add, update, remove).
	/// </summary>
	public class ShortcutDataRepository : AbstractDataRepository<Shortcut>
	{
		private readonly DatabaseManager _databaseManager;
		private const int ID_COLUMN = 0;
		private const int NAME_COLUMN = 1;
		private const int KEYS_COLUMN = 2;
		private const int DESCRIPTION_COLUMN = 3;
		private const int APPLICATION_ID_COLUMN = 4;
		private const string TABLE_NAME = "SHORTCUT";

		public ShortcutDataRepository(string connectionString) : base(connectionString)
		{
			_databaseManager = new DatabaseManager();
		}

		public override Shortcut Get(int idToFind)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"SELECT ID, NAME, KEYS, DESCRIPTION, APPLICATION_ID FROM {TABLE_NAME} WHERE ID=@id";
				command.Parameters.Add(new SQLiteParameter("@id", idToFind));
				command.Prepare();
				var reader = command.ExecuteReader();
				if (reader.Read())
				{
					return Map(reader);
				}
			}
			return null;
		}

		public override IEnumerable<Shortcut> List(int applicationId)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = $"SELECT ID, NAME, KEYS, DESCRIPTION, APPLICATION_ID FROM {TABLE_NAME} WHERE APPLICATION_ID=@id";
				command.Parameters.Add(new SQLiteParameter("@id", applicationId));
				command.Prepare();
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					yield return Map(reader);
				}
			}
		}

		public override long Save(Shortcut newShortcut)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = "INSERT INTO SHORTCUT(NAME, KEYS, DESCRIPTION, APPLICATION_ID) VALUES (@name, @keys, @description, @applicationId)";
				command.Parameters.Add(new SQLiteParameter("@name", newShortcut.Name));
				command.Parameters.Add(new SQLiteParameter("@keys", newShortcut.Keys));
				command.Parameters.Add(new SQLiteParameter("@description", newShortcut.Description));
				command.Parameters.Add(new SQLiteParameter("@applicationId", newShortcut.ApplicationId));
				command.Prepare();
				command.ExecuteNonQuery();
				var lastId = connection.LastInsertRowId;
				return lastId;
			}
		}

		public override void Update(Shortcut newShortcut)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = "UPDATE SHORTCUT SET NAME = @name, KEYS = @keys, DESCRIPTION = @description, APPLICATION_ID = @applicationId WHERE ID = @id";
				command.Parameters.Add(new SQLiteParameter("@name", newShortcut.Name));
				command.Parameters.Add(new SQLiteParameter("@keys", newShortcut.Keys));
				command.Parameters.Add(new SQLiteParameter("@description", newShortcut.Description));
				command.Parameters.Add(new SQLiteParameter("@id", newShortcut.Id));
				command.Parameters.Add(new SQLiteParameter("@applicationId", newShortcut.ApplicationId));
				command.Prepare();
				command.ExecuteNonQuery();
			}
		}

		public override void Remove(int idToRemove)
		{
			using (var connection = _databaseManager.GetConnection(ConnectionString))
			{
				var command = connection.CreateCommand();
				command.CommandText = "DELETE FROM SHORTCUT WHERE ID = @id";
				command.Parameters.Add(new SQLiteParameter("@id", idToRemove));
				command.Prepare();
				command.ExecuteNonQuery();
			}
		}

		private Shortcut Map(SQLiteDataReader reader)
		{
			var id = reader.GetInt32(ID_COLUMN);
			var name = reader.GetString(NAME_COLUMN);
			var keys = reader.GetString(KEYS_COLUMN);
			var description = reader.GetString(DESCRIPTION_COLUMN);
			var applicationId = reader.GetInt32(APPLICATION_ID_COLUMN);
			var shortcut = new Shortcut(id, name, keys, description, applicationId);
			return shortcut;
		}
	}
}
