using DOL.WHD.Section14c.Domain;
using DOL.WHD.Section14c.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.WHD.Section14c.DataAccess.Models
{

    public class UserActivity : BaseEntity
    {

        public UserActivity()
        {
            Id = Id ?? Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        //[ForeignKey("ActionId")]
        public int ActionId { get; set; }
        public string ActionType { get; set; }
        public string HistoryId { get; set; }
    }

}
