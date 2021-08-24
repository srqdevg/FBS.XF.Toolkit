using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// Database Helper.
	/// </summary>
	public static class DatabaseHelper
	{
		#region Public methods
		/// <summary>
		/// Executes the SQL command.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="query">The query.</param>
		/// <returns>List&lt;Dictionary&lt;System.String, System.String&gt;&gt;.</returns>
		public static List<Dictionary<string, string>> ExecuteSQLCommand(DbContext context, string query)
		{
			// Create row container
			var rows = new List<Dictionary<string, string>>();

			using (var command = context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = query;
				command.CommandType = CommandType.Text;

				context.Database.OpenConnection();

				using (var dbDataReader = command.ExecuteReader())
				{
					if (dbDataReader.HasRows)
					{
						// Iterate rows
						while (dbDataReader.Read())
						{
							// Create column container
							var columns = new Dictionary<string, string>();

							// Iterate columns
							for (var i = 0; i < dbDataReader.FieldCount; i++)
							{
								var columnValue = dbDataReader.GetFieldType(i);

								if (columnValue != null)
								{
									if (dbDataReader.IsDBNull(i))
									{
										columns.Add(dbDataReader.GetName(i), "(null)");
										continue;
									}

									if (columnValue.Name.Equals("String"))
									{
										columns.Add(dbDataReader.GetName(i), dbDataReader.GetString(i));
										continue;
									}

									if (columnValue.Name.Equals("Int32"))
									{
										columns.Add(dbDataReader.GetName(i), dbDataReader.GetInt32(i).ToString());
										continue;
									}

									if (columnValue.Name.Equals("Int64"))
									{
										columns.Add(dbDataReader.GetName(i), dbDataReader.GetInt64(i).ToString());
										continue;
									}

									Console.WriteLine($"Column {columnValue.Name}");
								}
							}

							// Add row
							rows.Add(columns);
						}
					}
				}
			}

			return rows;
		}
		#endregion
	}
}
