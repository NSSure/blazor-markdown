using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Repository;
using Blazor.Markdown.Core.Mediator.Request;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Markdown.Core.Mediator.Handler
{
    public class SettingsCreationHandler : IRequestHandler<SettingsCreationRequest, SettingsCreationResponse>
    {
        public readonly SettingsRepository SettingsRepository;

        public SettingsCreationHandler(SettingsRepository settingsRepository)
        {
            this.SettingsRepository = settingsRepository;
        }

        public async Task<SettingsCreationResponse> Handle(SettingsCreationRequest request, CancellationToken cancellationToken)
        {
            Settings _settings = new Settings()
            {
                ConnectionString = request.Options.ConnectionString
            };

            await this.SettingsRepository.AddAsync(_settings);

            return new SettingsCreationResponse()
            {
                Id = _settings.Id
            };
        }
    }
}
