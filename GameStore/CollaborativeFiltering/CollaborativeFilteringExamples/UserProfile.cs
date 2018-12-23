using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	public class UserProfile
	{
		public Guid UserId { get; private set; }
		public Guid[] ItemHistory { get; private set; }

		public UserProfile (Guid userId, Guid[] itemIds)
		{
			UserId = userId;
			ItemHistory = itemIds;
		}
	}
}

