using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deasy.Test.Mvc3.Services.Foo
{
    public class FooService : IFooService
    {
        public string Foo()
        {
            return "Foo";
        }
    }
}