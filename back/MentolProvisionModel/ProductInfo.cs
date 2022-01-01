using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
	public class ProductInfo
	{
		[Column("Idr")]
		public int Id { get; set; }

		public string Product { get; set; }

		public string Name { get; set; }

		public string Version { get; set; }

		[Column("DNumber")] 
		public string DecimalNumber { get; set; }

		public string Contacts { get; set; }

		public string AddInfo { get; set; }
	}
}