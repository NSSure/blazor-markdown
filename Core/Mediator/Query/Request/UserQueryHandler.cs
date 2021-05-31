using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator.Query;
using Blazor.Markdown.Shared.Model;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class UserQueryHandler : IRequestHandler<UserQueryRequest, UserQueryResponse>
    {
        public readonly UserRepository UserRepository;

        public UserQueryHandler(UserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public async Task<UserQueryResponse> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            List<User> _users = await this.UserRepository.ListAll();

            return new UserQueryResponse()
            {
                Users = _users.Select(a => new UserModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    RoleIds = a.RoleIds,
                    ActionIds = a.ActionIds,
                    DateAdded = a.DateAdded,
                    DateLastUpdated = a.DateLastUpdated
                })
                .ToList()
            };
        }
    }
}
