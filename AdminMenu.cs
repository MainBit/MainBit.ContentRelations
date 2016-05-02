using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace MainBit.ContentRelations
{
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Settings"),
                menu => menu.Add(T("Content Relations"), "1.1", item => item.Action("Index", "ContentRelationAdmin", new { area = "MainBit.ContentRelations" })
                    .Permission(StandardPermissions.SiteOwner))
            );
        }
    }
}
