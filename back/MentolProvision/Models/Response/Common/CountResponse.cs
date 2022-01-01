namespace MentolProvision.Models.Response.Common
{
	public class CountResponse: IHasCollectionMetadata
	{
		public CollectionMetadata Meta { get; set; } = new CollectionMetadata();

		public OperationStatus Status { get; set; } = new OperationStatus();
	}
}
