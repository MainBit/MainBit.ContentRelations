using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace MainBit.ContentRelations
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Route = new Route(
                        "Admin/ContentRelations",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"},
                                                    {"controller", "ContentRelationAdmin"},
                                                    {"action", "Index"}
                                                },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"}
                        },
                        new MvcRouteHandler())
                },

                new RouteDescriptor {
                    Route = new Route(
                        "Admin/ContentRelations/Create",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"},
                                                    {"controller", "ContentRelationAdmin"},
                                                    {"action", "Create"}
                                                },
                        new RouteValueDictionary (),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"}
                                                },
                        new MvcRouteHandler())
                },

                new RouteDescriptor {
                    Route = new Route(
                        "Admin/ContentRelations/{contentRelationId}/Edit",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"},
                                                    {"controller", "ContentRelationAdmin"},
                                                    {"action", "Edit"}
                                                },
                        new RouteValueDictionary (),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"}
                                                },
                        new MvcRouteHandler())
                },

                new RouteDescriptor {
                    Route = new Route(
                        "Admin/ContentRelations/{contentRelationId}/Delete",
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"},
                                                    {"controller", "ContentRelationAdmin"},
                                                    {"action", "Delete"}
                                                },
                        new RouteValueDictionary (),
                        new RouteValueDictionary {
                                                    {"area", "MainBit.ContentRelations"}
                                                },
                        new MvcRouteHandler())
                },

            };
        }
    }
}