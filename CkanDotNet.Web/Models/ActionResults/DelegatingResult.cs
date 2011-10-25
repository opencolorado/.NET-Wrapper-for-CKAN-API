using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CkanDotNet.Web.Models.ActionResults
{
    public class DelegatingResult : ActionResult
    {

        public Action<ControllerContext> Command
        {
            get;
            private set;
        }

        public DelegatingResult(Action<ControllerContext> command)
        {
            this.Command = command;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            Command(context);
        }
    }
}