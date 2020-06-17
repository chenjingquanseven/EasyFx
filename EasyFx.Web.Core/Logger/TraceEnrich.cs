using EasyFx.Core.Extensions;
using EasyFx.Web.Core.Abstracts;
using Serilog.Core;
using Serilog.Events;
using System;

namespace EasyFx.Web.Core.Logger
{
    /// <summary>
    /// trace
    /// </summary>
    public class TraceEnrich:ILogEventEnricher
    {
        /// <summary>Enrich the log event.</summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TraceId",
                WebContext.Current?.GetHeaderValue("trace-id", Guid.NewGuid().ToString("N"))));

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserIp",
                WebContext.Current?.GetHeaderValue("user-ip", WebContext.Current?.GetUserOrCurrentIp())));
        }
    }
}