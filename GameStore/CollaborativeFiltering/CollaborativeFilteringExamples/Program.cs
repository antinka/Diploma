using System;
using System.Collections.Generic;

namespace CollaborativeFilteringExamples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var profiles = GetAllUserProfiles(); 		
			var simiarity = new JaccardSimilarity();
			var engine = new CollaborativeFiltering();
			foreach (var profile in profiles) {
				var results = engine.recommend(profiles, simiarity, profile);
				PrintResults(profile.UserId, results);
			}

            Console.ReadLine();
        }

		private static void PrintResults(Guid userId, IEnumerable<KeyValuePair<Guid, double>> results) {
			Console.Write("user{0}: [ ", userId);
			foreach (var result in results) {
				Console.Write (String.Format ("(item {0}, {1}) ", result.Key, result.Value));
			}
			Console.WriteLine ("]");
		}

		private static IEnumerable<UserProfile> GetAllUserProfiles() {
			var profiles = new List<UserProfile> ();

            var id = Guid.NewGuid();


            profiles.Add(new UserProfile(Guid.NewGuid(), new Guid[] { id, Guid.NewGuid(), Guid.NewGuid() }));
            profiles.Add(new UserProfile(Guid.NewGuid(), new Guid[] { id }));
            profiles.Add(new UserProfile(Guid.NewGuid(), new Guid[] { id }));
            return profiles;
		}
	}
}
