using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkanDotNet.Web.Models
{
    public class ViewDataFactory
    {
        public static T Create<T>() where T : SearchResultsSideBarModel, new()
        {
            var model = new T();
            model.Categories = new List<string>();
            model.Categories.Add("Whee");

            return model;
        }
    }
}