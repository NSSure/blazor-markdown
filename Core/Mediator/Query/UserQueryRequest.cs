using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;

namespace Blazor.Markdown.Core.Mediator.Query
{
    public class UserQueryRequest : IRequest<UserQueryResponse>
    {
        public UserQueryOptions QueryOptions { get; set; } = new UserQueryOptions();

        public UserQueryRequest(UserQueryOptions queryOptions)
        {
            this.QueryOptions = queryOptions;
        }
    }
}
