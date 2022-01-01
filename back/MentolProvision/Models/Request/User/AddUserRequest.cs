using MentolProvision.Models.Request.Metadata;

namespace MentolProvision.Models.Request.User
{
	public class AddUserRequest
	{
		public AddUserRequestMetadata Meta { get; set; } = new AddUserRequestMetadata();

		public AddUserRequestData User { get; set; } = new AddUserRequestData();
	}
}
