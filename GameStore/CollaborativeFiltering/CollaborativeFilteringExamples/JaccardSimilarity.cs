using System;
using System.Linq;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	public class JaccardSimilarity : ISimilarityScorer
	{		
		public double Calculate(UserProfile current, UserProfile other)
		{
			int intersectionCount = 
				current.ItemHistory.Intersect(other.ItemHistory).Count();

			int unionCount = 
				current.ItemHistory.Length
				+ other.ItemHistory.Length
				- intersectionCount;

			return intersectionCount / (double)unionCount;
		}			
	}
}

