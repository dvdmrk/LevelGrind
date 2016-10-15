using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace test.Models
{
    [Table("Quests")]
    public class Quests
    {
        [Key]
        public int QuestsId { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string Quest { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public string AmOrPm { get; set; }
    }
}