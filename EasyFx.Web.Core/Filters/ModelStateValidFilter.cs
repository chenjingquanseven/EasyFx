using EasyFx.Core;
using EasyFx.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text;

namespace EasyFx.Web.Core.Filters
{
    /// <summary>
    /// 数据校验
    /// </summary>
    public class ModelStateValidFilter : IActionFilter
    {
        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("参数校验错误");
                foreach (var item in context.ModelState)
                {
                    if (item.Value.ValidationState != ModelValidationState.Invalid)
                    {
                        continue;
                    }

                    var errors = item.Value.Errors;
                    if (!errors.Any())
                    {
                        continue;
                    }

                    builder.AppendLine($"{item.Key.ToCamcel()}:");
                    foreach (var error in errors)
                    {
                        builder.Append($"{error.ErrorMessage};");
                    }
                }

                throw new UserFriendlyException(99999, builder.ToString());
            }
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new System.NotImplementedException();
        }
    }
}