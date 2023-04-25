using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiSApi.Models
{
    public class Retrospective
	{

        public Retrospective()
        {
        this.Feedbacks = new List<Feedback>();
            
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }

		[Required]
        [Key] public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public string[] Participants { get; set; }
        public virtual List<Feedback> Feedbacks { get; set; }
    }
}
