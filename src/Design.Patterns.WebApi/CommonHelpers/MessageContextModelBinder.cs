using Design.Patterns.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.CommonHelpers
{
    public class MessageContextModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bindingContext.Model = new MessageContext { DateTimeStamp = new DateTime(), UserId = 23 };
            return Task.FromResult(true);
        }
    }
}
