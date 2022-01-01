namespace MentolProvision.Models.Request.User
{
	public class DeleteUserRoleRequest
	{
		public int? UserId { get; set; }

		public int? RoleId { get; set; }
	}
}
