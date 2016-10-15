using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test.Models
{

    public class GroupDbContext : DbContext
    {
        public DbSet<Quests> Group { get; set; }
    }

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