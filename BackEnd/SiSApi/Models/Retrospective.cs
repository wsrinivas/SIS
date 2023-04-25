using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiSApi.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Retrospective
	{
        public Retrospective()
        {
        this.Feedbacks = new List<Feedback>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

		[Required]
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public string[] Participants { get; set; }
        public virtual List<Feedback> Feedbacks { get; set; }
    }
}
