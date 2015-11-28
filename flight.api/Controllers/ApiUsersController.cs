using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using flight.BLL.Services;
using flight.DAL.Entities;
using Microsoft.Data.OData;

namespace flight.api.Controllers
{
    public class ApiUsersController : ODataController
    {

        private readonly IApiUserService _apiUserService;

        public ApiUsersController(IApiUserService apiUserService)
        {
            _apiUserService = apiUserService;
        }

        public IQueryable<ApiUserEntity> Get()
        {
            return this._apiUserService.GetUsers();
        }

        public HttpResponseMessage Post(ApiUserEntity apiUserEntity)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ODataError { ErrorCode = "2", Message = "Modelstate is invalid" });

            var result = this._apiUserService.RegisterUser(apiUserEntity);

            if (result == UserCreateResult.Success)
                return Request.CreateResponse(HttpStatusCode.OK, apiUserEntity);

            // http://www.odata.org/documentation/odata-version-3-0/json-verbose-format/#representingerrorsinaresponse
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new ODataError { ErrorCode = result.ToString(), Message = "todo: generate error messages!" });
        }
    }
}