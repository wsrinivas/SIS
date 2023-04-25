using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SiSApi.Models
{
    public class Feedback
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public String Name { get; set; }
        public String Body { get; set; }
        public String FeedbackType { get; set; }

		// public FeedbackType feedbackType { get; set; }
		[JsonIgnore]
		public Retrospective Retrospective { get; set; }
		public Guid RetrospectiveId { get; set; }

    }

    enum FeedbackType { positive, negative, idea, praise }
}