// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace EventModularMonolith.Modules.Events.Application.Speakers.DTOs;

public class SpeakerDto
{
   public Guid Id { get; internal set; }
   public string Name { get; internal set; } = string.Empty;
   public string Description { get; internal set; } = string.Empty;
   public string ImageUrl { get; internal set; } = string.Empty;
   public List<SpeakerLinkDto> Links { get; internal set; } = [];
}

public class SpeakerLinkDto
{
   private SpeakerLinkDto(){}
#pragma warning disable CA1054
   public SpeakerLinkDto(string url)
#pragma warning restore CA1054
   {
      Url = url;
   }

   public string Url { get; internal set; }
}

