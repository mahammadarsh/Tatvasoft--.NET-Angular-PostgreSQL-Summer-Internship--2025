using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models.CommonModels
{
    public class UserSkill
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Skill { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
