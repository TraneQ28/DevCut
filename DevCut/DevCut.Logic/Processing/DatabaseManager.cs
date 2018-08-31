using System.Data.SQLite;
using System.IO;

namespace DevCut.Logic.Processing
{
	/// <summary>
	/// Class that manages the sqlite database connection and its initialization.
	/// </summary>
	public class DatabaseManager
	{
		public const string DatabaseNamePath = "shortcuts.sqlite";
		public const string DatabaseName = "shortcuts.sqlite";

		public DatabaseManager()
		{
			InitializeDatabase();
		}

		/// <summary>
		/// Establishes the connection to the sqlite database using the given connection string.
		/// </summary>
		/// <param name="connectionString">String that is required for connecting which contains the datasource and version.</param>
		/// <returns>Established database connection.</returns>
		public SQLiteConnection GetConnection(string connectionString)
		{
			var connection = new SQLiteConnection(connectionString);
			connection.Open();
			return connection;
		}

		/// <summary>
		/// Initializes the database by creating the database file if it isn't existing and creates all needed tables.
		/// </summary>
		private void InitializeDatabase()
		{
			if (!File.Exists(DatabaseName))
			{
				SQLiteConnection.CreateFile(DatabaseName);

				var connection = GetConnection($"Data Source={DatabaseName};Version=3;");
				var commandText = @"CREATE TABLE `CATEGORY` ( `ID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `NAME` TEXT NOT NULL )";
				CreateTable(connection, commandText);
				commandText = @"CREATE TABLE `APPLICATION` ( `ID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `NAME` TEXT NOT NULL, `DESCRIPTION` TEXT )";
				CreateTable(connection, commandText);
				commandText = @"CREATE TABLE `SHORTCUT` ( `ID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `NAME` TEXT NOT NULL, `KEYS` TEXT NOT NULL, `DESCRIPTION` TEXT )";
				CreateTable(connection, commandText);
				commandText = @"CREATE TABLE `APPLICATION_TO_CATEGORY` ( `APP_TO_CAT_ID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `APPLICATION_ID` INTEGER NOT NULL, `CATEGORY_ID` INTEGER NOT NULL )";
				CreateTable(connection, commandText);
				commandText = @"CREATE TABLE `SHORTCUT_TO_APPLICATION` ( `SC_TO_APP_ID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `SHORTCUT_ID` INTEGER NOT NULL, `APPLICATION_ID` INTEGER NOT NULL )";
				CreateTable(connection, commandText);
			}
		}

		/// <summary>
		/// Creates a table in the database using the specified command.
		/// </summary>
		/// <param name="connection">Database connection to sqlite daatabase.</param>
		/// <param name="commandText">Command to execute.</param>
		private void CreateTable(SQLiteConnection connection, string commandText)
		{
			var command = connection.CreateCommand();
			command.CommandText = commandText;
			command.ExecuteNonQuery();
		}
	}
}
