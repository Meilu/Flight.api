using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;

namespace flight.api.Providers
{
    public interface ISimpleAuthorizationServerProvider : IOAuthAuthorizationServerProvider
    {
    }
}