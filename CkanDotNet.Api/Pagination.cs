using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkanDotNet.Api
{
    public class Pagination
    {
        private int limit;

        public Pagination(int limit)
        {
            this.limit = limit;
        }

        public int GetOffset(int page)
        {
            return (page * limit) - limit;
        }

    }
}