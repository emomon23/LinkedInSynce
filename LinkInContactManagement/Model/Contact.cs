using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkInContactManagement.Model
{
    public class Contact
    {
        public Contact()
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }

        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Index]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string RawLinkedInName { get; set; }
        public DateTime? LastAutoContacted { get; set; }
        public DateTime? LastReachedOut { get; set; }
        public string LastReachOutMethod { get; set; }
        public string Notes { get; set; }
        public DateTime? ScheduleNextReachOut { get; set; }
        public bool ShouldSkipAutoReachOut { get; set; }
        public bool IsStillLinkedInContact { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
