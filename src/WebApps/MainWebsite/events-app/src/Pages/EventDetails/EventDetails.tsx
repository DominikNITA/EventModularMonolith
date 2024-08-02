import React, { useState } from 'react'
import { useAjax, getResponse } from '../../services/ApiHelper'
import { EventResponse } from '../../services/EventsClient'
import { useParams } from 'react-router-dom'
import { useClient } from '../../services/RootClient'
import { SpeakersSection } from './Components/SpeakersSection'

import dayjs from 'dayjs'
import { HeroSection } from './Components/HeroSection'
import { ScheduleSection } from './Components/ScheduleSection'
import { VenueSection } from './Components/VenueSection'

export const EventDetails = () => {
  let { eventId } = useParams()
  //'c6db552f-978d-4011-927a-8b2682b3b125'
  const [event, setEvent] = useState<EventResponse>()
  const { mainClient } = useClient()
  const result = useAjax(
    {
      request: () => mainClient.getEvent(eventId ?? ''),
      setResult: (r) => {
        const response = getResponse(r)?.value
        setEvent(response)
        console.log(response, r)
      },
    },
    [],
  )
  return (
    <>
      <HeroSection event={event} />
      <SpeakersSection eventId={eventId!} />
      <ScheduleSection />
      <VenueSection venue={event?.venue} />
    </>
  )
}
