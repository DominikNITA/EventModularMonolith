// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

namespace EventModularMonolith.Modules.Events.Application.Speakers.CreateSpeaker;

public class CreateSpeakerCommandValidator : AbstractValidator<CreateSpeakerCommand>
{
        public CreateSpeakerCommandValidator()
        {          
            RuleFor(v => v.Name)
                 .MaximumLength(256)
                 .NotEmpty();      
        }    
}

