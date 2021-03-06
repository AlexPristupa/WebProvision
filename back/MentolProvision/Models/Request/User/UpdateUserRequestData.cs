namespace MentolProvision.Models.Request.User
{
	public class UpdateUserRequestData
	{
		public string UserName { get; set; }

		public string UserDisplayName { get; set; }

		public string UserEmail { get; set; }

		public string UserSid { get; set; }

		public string UserProvider { get; set; }

		public string UserPosition { get; set; }

		public string UserPassword { get; set; }

		public int? UserId { get; set; }
	}
}
