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
    public class SettingsQueryHandler : IRequestHandler<SettingsQueryRequest, SettingsQueryReturn>
    {
        public readonly SettingsRepository SettingsRepository;

        public SettingsQueryHandler(SettingsRepository settingsRepository)
        {
            this.SettingsRepository = settingsRepository;
        }

        public async Task<SettingsQueryReturn> Handle(SettingsQueryRequest request, CancellationToken cancellationToken)
        {
            List<Settings> _settings = await this.SettingsRepository.ListAll();

            return new SettingsQueryReturn()
            {
                Settings = _settings.Select(a => new SettingsModel()
                {
                    Id = a.Id,
                    ConnectionString = a.ConnectionString,
                    DateAdded = a.DateAdded,
                    DateLastAdded = a.DateLastUpdated
                })
                .ToList()
            };
        }
    }
}
