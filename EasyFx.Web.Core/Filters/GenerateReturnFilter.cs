using EasyFx.Core.Models;
using EasyFx.Web.Core.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace EasyFx.Web.Core.Filters
{
    /// <summary>
    /// 生成返回对象
    /// </summary>
    public class GenerateReturnFilter:IResultFilter
    {
        /// <summary>Called before the action result executes.</summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext" />.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var controllerType = context.Controller.GetType();
            var attributeType = typeof(GenerateReturnIgnoreAttribute);
            var actionAttribute = context.ActionDescriptor.ActionConstraints
                .Select(p => p.GetType())
                .FirstOrDefault(type => type == attributeType);
            var isDefined = controllerType.IsDefined(attributeType,false);
            if (actionAttribute != null || isDefined)
            {
                return;
            }


            if (context.Result is ObjectResult result)
            {
                context.Result = new ObjectResult(new ApiResult()
                {
                    Code = 0,
                    Message = null,
                    Data = result.Value
                });
            }
        }

        /// <summary>Called after the action result executes.</summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResultExecutedContext" />.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }
    }
}