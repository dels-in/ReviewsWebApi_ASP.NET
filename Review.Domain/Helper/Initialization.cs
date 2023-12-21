using Review.Domain.Models;

namespace Review.Domain.Helper
{
    public static class Initialization
    {
        private const string LoremIpsum =
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        private const int Capacity = 100;
        private static readonly Random _random = new();

        public static Feedback[] GetFeedbacks()
        {
            var result = new List<Feedback>(Capacity);
            for (var i = 1; i <= Capacity; i++)
            {
                var feedback = CreateFeedback(i);
                result.Add(feedback);
            }

            return result.ToArray();
        }

        private static Feedback CreateFeedback(int id)
        {
            return new Feedback
            {
                Id = id,
                CreateDate = DateTime.Now.AddDays(_random.Next(-100, 0)),
                Grade = _random.Next(0, 6),
                ProductId = _random.Next(1, 12),
                Text = LoremIpsum.Substring(0, _random.Next(20, 100)),
                UserId = _random.Next(1, 10),
                RatingId = _random.Next(1, 10),
                Status = (Status)_random.Next(0, 2)
            };
        }

        public static Rating[] GetRatings()
        {
            var result = new List<Rating>(Capacity);
            for (var id = 1; id <= Capacity; id++)
            {
                var rating = CreateRating(id);
                result.Add(rating);
            }

            return result.ToArray();
        }

        private static Rating CreateRating(int feedbackId)
        {
            CreateFeedback(feedbackId);
            var randomCapacity = _random.Next(1, 10);
            var feedbacks = new List<Feedback>(randomCapacity);
            for (var id = 1; id <= randomCapacity; id++)
            {
                feedbacks.Add(CreateFeedback(id));
            }

            var feedbacksAverage = feedbacks.Select(x => x.Grade).Average();
            var rating = new Rating
            {
                Id = feedbackId,
                CreateDate = DateTime.Now.AddDays(_random.Next(-100, 0)),
                ProductId = _random.Next(1, 10),
                Grade = Math.Round(feedbacksAverage, 2)
            };
            return rating;
        }

        public static Login[] GetLogins()
        {
            var users = new List<Login>();
            var admin = new Login
            {
                Id = 0,
                UserName = "admin",
                Password = "admin"
            };
            users.Add(admin);
            return users.ToArray();
        }
    }
}