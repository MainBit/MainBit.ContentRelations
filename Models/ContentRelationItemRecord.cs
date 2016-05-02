using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace MainBit.ContentRelations.Models
{
    public class ContentRelationItemRecord
    {
        public virtual int Id { get; set; }
        public virtual int ContentRelationRecord_Id { get; set; }
        public virtual int ContentItemRecord1_Id { get; set; }
        public virtual int ContentItemRecord2_Id { get; set; }
    }
}