using MediatR;

namespace Blazor.Markdown.Core.Mediator
{
    public class QueryRequest<TResponse> : IRequest<TResponse>
    {

    }

    public class QueryRequest<TQueryParams, TResponse> : IRequest
    {
        public TQueryParams QueryParams { get; set; }

        public QueryRequest()
        {

        }

        public QueryRequest(TQueryParams queryParams)
        {
            this.QueryParams = queryParams;
        }
    }
}
