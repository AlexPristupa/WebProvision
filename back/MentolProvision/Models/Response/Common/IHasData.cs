using System.Collections.Generic;

namespace MentolProvision.Models.Response
{
	public interface IHasData<T> where T: class
	{
		public List<T> Data { get; set; }
	}
}
