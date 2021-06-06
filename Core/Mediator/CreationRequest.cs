using MediatR;

namespace Blazor.Markdown.Core.Mediator
{
    public class CreationRequest : IRequest
    {

    }

    public class CreationRequest<TResponse> : IRequest<TResponse>
    {

    }


    public class CreationRequest<TOptions, TResponse> : IRequest<TResponse>
    {
        public TOptions Options { get; set; }

        public CreationRequest()
        {

        }

        public CreationRequest(TOptions options)
        {
            this.Options = options;
        }
    }
}
