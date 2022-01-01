using System.Collections.Generic;

namespace MentolProvisionModel.Infrastructure
{
	public class FilterColumn
	{
		public string ColumnName { get; set; }
		public List<(string Operand, string Value)> ValuesToOperand { get; set; }
	}
}
