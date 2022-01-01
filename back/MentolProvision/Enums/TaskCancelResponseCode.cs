namespace MentolProvision.Enums
{
	// дублируем некоторые коды ответа из TaskPostResponseCode
	public enum TaskCancelResponseCode
	{
		TaskNotExistAndNotBlockedByAnotherUser = 1122,
		TaskCreatedAndNotBlockedByAnotherUser = 1232,
		TaskIsBlockedByAnotherUser = 3001,
		TaskAlreadyExistsAndNonBlockedByAnotherUser = 3030,
		TaskAlreadyExistsAndBlockedByAnotherUser = 3031,
		ActionIsCanceled = 3200
	}
}
