using System.Data.Common;
using Dapper;
using EventModularMonolith.Shared.Application.Data;
using EventModularMonolith.Shared.Application.EventBus;
using EventModularMonolith.Shared.Infrastructure.Inbox;
using EventModularMonolith.Shared.Infrastructure.Serialization;
using MassTransit;
using Newtonsoft.Json;

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Inbox;

internal sealed class IntegrationEventConsumer<TIntegrationEvent>(IDbConnectionFactory dbConnectionFactory)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            OccurredOnUtc = integrationEvent.OccurredOnUtc
        };

        const string sql =
            """
            INSERT INTO ticketing.inbox_messages(id, type, content, occurred_on_utc)
            VALUES (@VenueId, @Type, @Content::json, @OccurredOnUtc)
            """;

        await connection.ExecuteAsync(sql, inboxMessage);
    }
}
