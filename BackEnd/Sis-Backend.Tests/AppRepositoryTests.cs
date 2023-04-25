using Moq;
using SiSApi.Data;
using SiSApi.Models;
using System;

namespace Sis_Backend.Tests
{
	public class AppRepositoryTests
	{
		Mock<IAppRepository> mockRepository;

		Retrospective _retrospective;

		[SetUp]
		public void Setup()
		{
			var guid = Guid.NewGuid();

			_retrospective = new Retrospective()
			{
				Id = guid,
				Name = "Retrospective 1",
				Date = DateTime.UtcNow,
				Participants = new string[] { "Gareth", "Mike" },
				Summary = "Post release retrospective",
				Feedbacks = new List<Feedback>() {
						new Feedback()
						{
							Id = Guid.NewGuid(),
							Name = "Gareth",
							Body = "Sprint objective met",
							FeedbackType = "Positive" , 
							RetrospectiveId = guid,
						},
						new Feedback()
						{
							Id = Guid.NewGuid(),
							Name = "Mike",
							Body = "We should be looking to start using VS2015",
							FeedbackType = "Idea" ,
							RetrospectiveId = guid,
						} }
			};

			mockRepository = new Mock<IAppRepository>();
			mockRepository.Setup(s => s.GetById(guid)).ReturnsAsync(_retrospective);

		}

		[Test]
		public async Task Add_DuplicateName_MustReturnFalse()
		{
			mockRepository.Setup(s => s.AddRetrospective(_retrospective)).ReturnsAsync(false) ;

			var add  = await mockRepository.Object.AddRetrospective(_retrospective);

			Assert.That(add, Is.EqualTo(false));

		}
	}
}