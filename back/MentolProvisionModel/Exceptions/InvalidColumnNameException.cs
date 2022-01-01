using System;

namespace MentolProvisionModel.Exceptions
{
	public class InvalidColumnNameException: Exception
	{
		public InvalidColumnNameException(string columnName, string message):base(message)
		{
			ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
		}

		public string ColumnName { get; }
	}
}
