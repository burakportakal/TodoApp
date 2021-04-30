using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Helpers
{
    public static class ValidationHelper
    {
        public static List<ModelErrorCollection> GetModelErrors(ModelStateDictionary modelState)
        {
            return modelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
        }

    }
}
