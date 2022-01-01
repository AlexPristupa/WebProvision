namespace MentolProvision.Models.Response.User
{
	public class RecoverOrCancelUserCreationResponse
	{
		public RecoverOrCancelUserCreationMeta Meta { get; set; } = new RecoverOrCancelUserCreationMeta();

		public RecoverOrCancelUserData User { get; set; } = new RecoverOrCancelUserData();
	}
}
