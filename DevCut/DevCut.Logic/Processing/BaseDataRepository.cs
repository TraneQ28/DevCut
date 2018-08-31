using System.Collections.Generic;
using System.Data.SQLite;

namespace DevCut.Logic.Processing
{
	/// <summary>
	/// Generic abstract base class that provides methods for altering the database and establishing a connection using the given type.
	/// </summary>
	/// <typeparam name="T">Datatype that interacts with the database.</typeparam>
	public abstract class AbstractDataRepository<T>
	{
		protected SQLiteConnection Connection { get; set; }
		protected string ConnectionString { get; set; }
		protected AbstractDataRepository(string connectionString)
		{
			Connection = new SQLiteConnection
			{
				ConnectionString = connectionString
			};
			ConnectionString = connectionString;
		}

		protected void Open()
		{
			Connection.Open();
		}

		public abstract T Get(int idToFind);
		public abstract IEnumerable<T> List(int id);
		public abstract long Save(T entity);
		public abstract void Update(T entity);
		public abstract void Remove(int idToRemove);
	}
}
