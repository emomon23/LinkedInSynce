using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkInContactManagement.Model
{
    public class ToDo
    {
        [Key]
        public int ToDoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DateCompleted { get; set; }
        
        public bool IsExpired
        {
            get { return DueDate.HasValue ? DueDate.Value < DateTime.Now : false; }
        }

        public Color UrgencyColor
        {
            get
            {
                if (DateCompleted.HasValue)
                {
                    return Color.Aqua;
                }

                if (DueDate.HasValue == false || DueDate.Value >= DateTime.Now.AddDays(1))
                {
                    return Color.DarkGreen;
                }

                if (DueDate.Value < DateTime.Parse(DateTime.Now.AddDays(1).ToString("d") + " 12:00 am"))
                {
                    return Color.DarkRed;
                }

                return Color.DarkOrange;
            }
        }
    }
}
