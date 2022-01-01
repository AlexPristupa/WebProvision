namespace MentolProvision.Enums
{
	public enum TaskPostResponseCode
	{
		TaskCreatedAndNotBlockedByAnotherUser = 1232,
		TaskNotExistAndNotBlockedByAnotherUser = 1122,
		TaskIsExistAndNotBlockedByAnotherUser = 1132,
		TaskIsExistAndResourceNotBlockedByAnotherUser = 2132,
		ResourceBlockedByAnotherUser = 3001,
		TaskIsExistAndResourceBlockedByAnotherUser = 3031,
		ActionIsCanceled = 3200
	}
}
