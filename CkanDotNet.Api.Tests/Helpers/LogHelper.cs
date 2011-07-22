using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using log4net.Layout;
using log4net;

namespace CkanDotNet.Api.Tests.Helpers
{
    public static class LogHelper
    {
        /// <summary>
        /// Configures a trace appender for log4net so that log output 
        /// is written to the test output.
        /// </summary>
        public static void ConfigureLoggingToTrace() {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders();

            TraceAppender appender = new TraceAppender();
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%d %-5p [%-10c]   %m%n%n";
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;
            appender.ActivateOptions();

            log4net.Config.BasicConfigurator.Configure(appender);
        }
    }
}
