using Blazor.Markdown.Core.Attributes;
using Blazor.Markdown.Core.Exceptions;
using Blazor.Markdown.Core.Utility;
using MediatR;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Behavior
{
    public class AuthorizeActionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public AuthorizeActionBehavior()
        {

        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            AuthorizeActionAttribute _authorizeActionAttribute = typeof(TRequest).GetCustomAttribute(typeof(AuthorizeActionAttribute)) as AuthorizeActionAttribute;

            if (_authorizeActionAttribute != null)
            {
                if (!PermissionsUtility.HasAction(_authorizeActionAttribute.Action))
                {
                    throw new MissingRequiredActionException("You do not have permissions to permform that action.");
                }
            }

            return await next();
        }
    }
}
