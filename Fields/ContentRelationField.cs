using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using System.Linq;

namespace MainBit.ContentRelations.Fields
{
    public class ContentRelationField : ContentField {

        internal LazyField<IEnumerable<ContentItem>> _contentItems = new LazyField<IEnumerable<ContentItem>>();

        public IEnumerable<ContentItem> ContentItems {
            get {
                return _contentItems.Value;
            }
        }
    }
}