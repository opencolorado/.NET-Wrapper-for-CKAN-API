using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CkanDotNet.Api.Helper;

namespace CkanDotNet.Api.Model
{
    public class License
    {
        public string Status { get; set; }
        public string Maintainer { get; set; }
        public string Family { get; set; }
        public List<string> Tags { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public bool IsOkdCompliant { get; set; }
        public bool IsOsiCompliant { get; set; }
        public string DateCreated { get; set; }
        public string Id { get; set; }

        public DateTime DateCreatedAsDate
        {
            get
            {
                return DateHelper.Parse(DateCreated);
            }
        }
    }
}
