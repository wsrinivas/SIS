using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SiSApi.Models; 

namespace SiSApi.Data
{

	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{ 
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging(); 
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Retrospective>()
				.Property(p => p.Participants)
					.HasConversion(
								 v => string.Join(',', v),
								 v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
								);

			//modelBuilder.Entity<Retrospective>()
			//.HasMany(r => r.Feedbacks)
			////.WithOne(f => f.Retrospective)
			////.HasForeignKey(fk=>fk.RetrospectiveId)
			////.HasPrincipalKey(pk=>pk.Id);


			//modelBuilder.Entity<Retrospective>().HasData(
			////new Retrospective()
			////{
			////	Id = Guid.NewGuid(),
			////	Name = "Retrospective 1",
			////	Date = DateTime.UtcNow,
			////	Participants = new string[] { "Gareth", "Mike" },
			////	Summary = "Post release retrospective",
			////	Feedbacks = new List<Feedback>() {
			////		new Feedback()
			////		{
			////			Id = Guid.NewGuid(),
			////			Name = "Gareth",
			////			Body = "Sprint objective met",
			////			FeedbackType = "Positive" //, Retrospective = new Retrospective() { Name = "Retrospective 1" }
			////		},
			////		new Feedback()
			////		{
			////			Id = Guid.NewGuid(),
			////			Name = "Mike",
			////			Body = "We should be looking to start using VS2015",
			////			FeedbackType = "Idea"
			////		},
			////	}
			////},
			//	new Retrospective()
			//	{
			//		Id = Guid.NewGuid(),
			//		Name = "Retrospective 1",
			//		Date = DateTime.UtcNow.AddDays(-1),
			//		Participants = new string[] { "Viktor" },
			//		Summary = "Summary 1 "
			//	},
			//	new Retrospective()
			//	{
			//		Id = Guid.NewGuid(),
			//		Name = "Retrospective 2",
			//		Date = DateTime.UtcNow.AddDays(-2),
			//		Participants = new string[] { "Viktor" , "Sri"},
			//		Summary = "Summary 2"
			//	});


			base.OnModelCreating(modelBuilder);
		}

		// DBSets

		public DbSet<Retrospective> Retrospectives { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }



	}
}
