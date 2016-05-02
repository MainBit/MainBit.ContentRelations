using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MainBit.ContentRelations.Models
{
    public class ContentRelationRecord
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Title { get; set; }
    }
}