import { useAjax, getResponse } from '@/services/ApiHelper'
import { EventResponse, SpeakerDto } from '@/services/EventsClient'
import { useClient } from '@/services/RootClient'
import dayjs from 'dayjs'
import { LinkIcon, Calendar, Clock, MapPin } from 'lucide-react'
import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'

const EventDetailsPage: React.FC = () => {
  let { eventId } = useParams()
  //'c6db552f-978d-4011-927a-8b2682b3b125'
  const [event, setEvent] = useState<EventResponse>()
  const { eventsClient } = useClient()
  const result = useAjax(
    {
      request: () => eventsClient.getEvent(eventId ?? ''),
      setResult: (r) => {
        const response = getResponse(r)?.value
        setEvent(response)
      },
    },
    [],
  )

  const { speakersClient } = useClient()

  const [speakers, setSpeakers] = useState<SpeakerDto[]>()

  const result2 = useAjax(
    {
      request: () => speakersClient.getSpeakersForEvent(eventId ?? ''),
      setResult: (r) => {
        const response = getResponse(r)?.value
        setSpeakers(response)
        console.log(response, r)
      },
    },
    [],
  )

  if (!event) {
    return <div className="container mx-auto mt-8">Loading...</div>
  }

  return (
    <div className="bg-gray-100 min-h-screen">
      {/* Hero Section */}
      <section
        className="bg-cover bg-center min-h-screen flex items-center justify-center text-white"
        style={{ backgroundImage: `url(${event.backgroundImage})` }}
      >
        <div className="bg-black bg-opacity-50 p-8 rounded">
          <h1 className="text-4xl font-bold mb-4">{event.title}</h1>
          <p className="text-xl">
            {dayjs(event.startsAtUtc).format('MMMM D, YYYY')}
          </p>
        </div>
      </section>

      <div className="container mx-auto px-4 py-8">
        {/* Speakers Section */}
        <section className="mb-12">
          <h2 className="text-3xl font-bold mb-6">Speakers</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {speakers?.map((speaker) => (
              <div
                key={speaker.id}
                className="bg-white rounded-lg shadow-md p-6"
              >
                <img
                  src={speaker.imageUrl}
                  alt={speaker.name}
                  className="w-32 h-32 rounded-full mx-auto mb-4"
                />
                <h3 className="text-xl font-semibold mb-2">{speaker.name}</h3>
                <p className="text-gray-600 mb-4">{speaker.description}</p>
                <div className="flex justify-center space-x-4">
                  {speaker.links.map((link, index) => (
                    <a
                      key={index}
                      href={link.url}
                      target="_blank"
                      rel="noopener noreferrer"
                      className="text-blue-500 hover:text-blue-700"
                    >
                      <LinkIcon size={20} />
                    </a>
                  ))}
                </div>
              </div>
            ))}
          </div>
        </section>

        {/* Schedule Section */}
        <section className="mb-12">
          <h2 className="text-3xl font-bold mb-6">Schedule</h2>
          <div className="bg-white rounded-lg shadow-md p-6">
            <div className="flex items-center mb-4">
              <Calendar className="mr-2" />
              <span>{dayjs(event.startsAtUtc).format('MMMM D, YYYY')}</span>
            </div>
            <div className="flex items-center">
              <Clock className="mr-2" />
              <span>
                {dayjs(event.startsAtUtc).format('h:mm A')} -{' '}
                {event.endsAtUtc
                  ? dayjs(event.endsAtUtc).format('h:mm A')
                  : 'TBA'}
              </span>
            </div>
          </div>
        </section>

        {/* Venue Section */}
        <section className="mb-12">
          <h2 className="text-3xl font-bold mb-6">Venue</h2>
          <div className="bg-white rounded-lg shadow-md p-6">
            <h3 className="text-xl font-semibold mb-2">{event.venue.name}</h3>
            <p className="text-gray-600 mb-4">{event.venue.description}</p>
            <div className="flex items-center mb-4">
              <MapPin className="mr-2" />
              <address className="not-italic">
                {event.venue.address.streetAndNumber},{' '}
                {event.venue.address.city}, {event.venue.address.region},{' '}
                {event.venue.address.country}
              </address>
            </div>
            {event.venue.imageUrls.length > 0 && (
              <img
                src={event.venue.imageUrls[0]}
                alt={event.venue.name}
                className="w-full h-64 object-cover rounded-lg"
              />
            )}
          </div>
        </section>

        {/* Ticket Types Section */}
        <section>
          <h2 className="text-3xl font-bold mb-6">Ticket Types</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {event.ticketTypes.map((ticket) => (
              <div
                key={ticket.ticketTypeId}
                className="bg-white rounded-lg shadow-md p-6"
              >
                <h3 className="text-xl font-semibold mb-2">{ticket.name}</h3>
                <p className="text-2xl font-bold mb-4">
                  {ticket.price} {ticket.currency}
                </p>
                <p className="text-gray-600 mb-4">
                  {ticket.quantity} available
                </p>
                <button className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition-colors">
                  Buy Ticket
                </button>
              </div>
            ))}
          </div>
        </section>
      </div>
    </div>
  )
}

export default EventDetailsPage
