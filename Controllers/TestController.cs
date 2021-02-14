using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using LangBuilder.Data;

namespace LangBuilder.Web.Controllers
{
    public class TestController : ApiController
    {
        public TestController(LangBuilderContext context)
        {
            var c = context;
        }

        [System.Web.Http.HttpGet]
        public EmptyResult Action()
        {
            return new EmptyResult();
        }
    }
}
