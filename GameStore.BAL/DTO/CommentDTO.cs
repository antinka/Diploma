using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.DTO
{
    public class CommentDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public Guid? ParentCommentId { get; set; }
        public GameDTO Game { get; set; }

        public CommentDTO()
        { }
    }
}
