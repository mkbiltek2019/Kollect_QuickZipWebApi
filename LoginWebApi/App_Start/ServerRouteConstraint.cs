using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace LoginWebApi
{
    public class ServerRouteConstraint:IRouteConstraint
    {
        private readonly Func<Uri, bool> _predicate;
        public ServerRouteConstraint(Func<Uri, bool> predicate)
        {
            this._predicate = predicate;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return this._predicate(httpContext.Request.Url);
        }
    }
}