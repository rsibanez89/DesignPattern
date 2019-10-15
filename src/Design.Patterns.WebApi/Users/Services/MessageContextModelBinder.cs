using Design.Patterns.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
    public class MessageContextModelBinder : IModelBinder
    {
		private readonly IJwtBearerService jwtBearerService;

		public MessageContextModelBinder(IJwtBearerService jwtBearerService)
		{
			this.jwtBearerService = jwtBearerService;
		}

		public Task BindModelAsync(ModelBindingContext bindingContext)
        {
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			var headerValue = bindingContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
			var user = jwtBearerService.GetUserFromToken(headerValue.Replace("Bearer", "", StringComparison.OrdinalIgnoreCase).Trim());

			bindingContext.Model = new MessageContext { DateTimeStamp = DateTime.Now, UserId = user.Id };
			bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

			return Task.CompletedTask;
		}
    }
}
