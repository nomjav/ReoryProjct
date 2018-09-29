namespace AcademyLockSmith.Data.Helpers
{
	public enum LineItemType
	{
		Parts = 1,
		Labor = 2,
		Other = 3,
		Service = 4
	}

	public enum JobStatus
	{
		NotAssigned = 22,

		TechDispatchedInfo = 7,
		Assigned = 8,
		PendingTechAcceptance = 27,

		FollowUp = 5,

		Going = 6,
		Completed = 4,
		Return = 9,
		PartsOnOrder = 23,
		Cub = 10,
		HotCub = 11,
		InvoiceReceived = 24,
		PayableCreated = 17,
		CloseOuts = 25,
		MarkUpCreated = 14,
		Reconciled = 18,
		Cancelled = 3,
		Lost = 2,
		CompletedCallBack = 1,

		PrepaidJobs = 26,

		FollowupWithTechOnly = 31,
		FollowupWithStoreOnly = 32,
		ActionRequired = 36,


	}

	public enum FormOfPayment
	{
		Billing = 1,
		Cash = 2,
		Check = 3,
		Visa = 4,
		Amex = 5,
		Discovery = 6
	}

}