using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	public interface ISimilarityScorer
	{
		double Calculate(UserProfile current, UserProfile other);
	}
}

