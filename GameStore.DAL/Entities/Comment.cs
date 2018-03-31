using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Comment: BaseEntity
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public Guid? ParentCommentId { get; set; }

        [ForeignKey("Game")]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }
}
