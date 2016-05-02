using MainBit.ContentRelations.Services;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.ContentRelations
{
    public class Shapes : IShapeTableProvider
    {
        private readonly Work<IContentRelationService> _contentRelationService;
        public Shapes(Work<IContentRelationService> contentRelationService)
        {
            _contentRelationService = contentRelationService;
		}
 
		public void Discover(ShapeTableBuilder builder) {
            builder.Describe("ContentRelationPicker").OnDisplaying(context =>
            {
                context.Shape.ContentRelations = _contentRelationService.Value.Get();
			});
		}
    }
}