using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test.Models
{

    public class QuestDBContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; }

        public QuestDBContext()
        {
            Database.SetInitializer<QuestDBContext>(
                new DropCreateDatabaseIfModelChanges<QuestDBContext>()
                );
        }
    }



    [Table("Quests")]
    public class Quest
    {
        [Key]
        public int QuestsId { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public DateTime Date { get; set; }
    }
}