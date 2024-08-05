// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventModularMonolith.Modules.Events.Application.Abstractions.Data;
using EventModularMonolith.Modules.Events.Domain.Speakers;
using EventModularMonolith.Shared.Application.Messaging;
using EventModularMonolith.Shared.Domain;


namespace EventModularMonolith.Modules.Events.Application.Speakers.CreateSpeaker;
    
public class CreateSpeakerCommandHandler(
    ISpeakerRepository speakerRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateSpeakerCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
        {
           Result<Speaker> speaker = Speaker.Create(
                request.Name,
                request.Description,
                request.Links.Select(l => new Link(new Uri(l.Url))).ToList()
           );

           if(speaker.IsFailure)
           {
                return Result.Failure<Guid>(speaker.Error);
           }

           speakerRepository.Insert(speaker.Value);

           await unitOfWork.SaveChangesAsync(cancellationToken);

           return speaker.Value.Id;
        }
    }

