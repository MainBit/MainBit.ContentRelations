using System.Web.Mvc;
using Orchard;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
using MainBit.ContentRelations.Services;
using MainBit.ContentRelations.Models;

namespace MainBit.ContentRelations.Controllers
{
    [ValidateInput(false), Admin]
    public class ContentRelationAdminController : Controller 
    {
        private readonly IContentRelationService _contentRelationService;

        public ContentRelationAdminController(IContentRelationService contentRelationService, IOrchardServices orchardServices)
        {
            _contentRelationService = contentRelationService;
            Services = orchardServices;
            T = NullLocalizer.Instance;
        }

        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }

        [HttpGet]
        public ActionResult Index() {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage Content Relation")))
                return new HttpUnauthorizedResult();

            var listOfRecords = _contentRelationService.Get();
            if (listOfRecords == null || listOfRecords.Count == 0)
                ViewBag.EmptyMessage = T("No data");
            return View(listOfRecords);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage Content Relation")))
                return new HttpUnauthorizedResult();

            var model = new ContentRelationRecord();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContentRelationRecord model)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage Content Relation")))
                return new HttpUnauthorizedResult();

            model.Id = 0;

            if (!ModelState.IsValid)
                return View();

            _contentRelationService.Create(model);
            Services.Notifier.Information(T("Content Relation successfully added"));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int contentRelationId)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage Content Relation")))
                return new HttpUnauthorizedResult();

            return View(_contentRelationService.Get(contentRelationId));
        }

        [HttpPost]
        public ActionResult Edit(int contentRelationId, ContentRelationRecord model)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage Content Relation")))
                return new HttpUnauthorizedResult();

            model.Id = contentRelationId;

            if (!ModelState.IsValid)
                return View(model);

            _contentRelationService.Update(model);
            Services.Notifier.Information(T("Content Relation successfully saved"));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int contentRelationId)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Cannot manage Content Relation")))
                return new HttpUnauthorizedResult();

            _contentRelationService.Delete(contentRelationId);
            Services.Notifier.Information(T("Content Relation successfully deleted"));

            return RedirectToAction("Index");
        }

    }
}