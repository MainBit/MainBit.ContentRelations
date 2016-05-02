using System.Collections.Generic;
using Orchard.ContentManagement;
using MainBit.ContentRelations.Fields;

namespace MainBit.ContentRelations.ViewModels
{
    public class ContentRelationFieldViewModel
    {
        public ICollection<ContentItem> ContentItems { get; set; }
        public string SelectedIds { get; set; }
        public ContentRelationField Field { get; set; }
        public ContentPart Part { get; set; }
    }
}