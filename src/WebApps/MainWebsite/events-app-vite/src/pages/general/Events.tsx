import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
// import 'mapbox-gl/dist/mapbox-gl.css'
import { Moon, Sun, Search, User } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import { useTheme } from '@/components/theme-provider'
import { AuthenticationService } from '@/services/AuthService'
import { ThemeToggle } from '@/components/theme-toggle'
import { useClient } from '@/services/RootClient'
import { EventResponse } from '@/services/EventsClient'
import { useAjax, getResponse } from '@/services/ApiHelper'

// You would need to get an access token from Mapbox
const MAPBOX_ACCESS_TOKEN = 'your_mapbox_access_token_here'

type Event = {
  id: number
  title: string
  date: string
  location: string
  latitude: number
  longitude: number
}

const events: Event[] = [
  {
    id: 1,
    title: 'Tech Conference 2023',
    date: '2023-09-15',
    location: 'San Francisco',
    latitude: 37.7749,
    longitude: -122.4194,
  },
  {
    id: 2,
    title: 'Music Festival',
    date: '2023-10-01',
    location: 'Los Angeles',
    latitude: 34.0522,
    longitude: -118.2437,
  },
  {
    id: 3,
    title: 'Art Exhibition',
    date: '2023-09-22',
    location: 'New York',
    latitude: 40.7128,
    longitude: -74.006,
  },
  {
    id: 4,
    title: 'Food & Wine Convention',
    date: '2023-11-05',
    location: 'Chicago',
    latitude: 41.8781,
    longitude: -87.6298,
  },
  {
    id: 5,
    title: 'Sports Tournament',
    date: '2023-10-10',
    location: 'Miami',
    latitude: 25.7617,
    longitude: -80.1918,
  },
  {
    id: 6,
    title: 'Sports Tournament',
    date: '2023-10-10',
    location: 'Miami',
    latitude: 25.7617,
    longitude: -80.1918,
  },
  {
    id: 7,
    title: 'Sports Tournament',
    date: '2023-10-10',
    location: 'Miami',
    latitude: 25.7617,
    longitude: -80.1918,
  },
  {
    id: 8,
    title: 'Sports Tournament',
    date: '2023-10-10',
    location: 'Miami',
    latitude: 25.7617,
    longitude: -80.1918,
  },
  {
    id: 9,
    title: 'Sports Tournament',
    date: '2023-10-10',
    location: 'Miami',
    latitude: 25.7617,
    longitude: -80.1918,
  },
  {
    id: 10,
    title: 'Sports Tournament',
    date: '2023-10-10',
    location: 'Miami',
    latitude: 25.7617,
    longitude: -80.1918,
  },
]

const EventsPage: React.FC = () => {
  const [searchTerm, setSearchTerm] = useState('')
  const [userLocation, setUserLocation] = useState({
    latitude: 40,
    longitude: -98,
  })

  useEffect(() => {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        setUserLocation({
          latitude: position.coords.latitude,
          longitude: position.coords.longitude,
        })
      })
    }
  }, [])

  const filteredEvents = events.filter(
    (event) =>
      event.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
      event.location.toLowerCase().includes(searchTerm.toLowerCase()),
  )

  const { eventsClient } = useClient()

  const [eventss, setEvents] = useState<EventResponse[]>()

  const result2 = useAjax(
    {
      request: () => eventsClient.getEvents(),
      setResult: (r) => {
        const response = getResponse(r)?.value
        setEvents(response)
        console.log(response, r)
      },
    },
    [],
  )

  return (
    <main className="container py-6 grid gap-6 md:grid-cols-2 justify-center">
      <section>
        <h2 className="text-2xl font-bold mb-4">Upcoming Events</h2>
        <div className="space-y-3">
          {eventss?.map((event) => (
            <div
              key={event.id}
              className="bg-card text-card-foreground rounded-lg shadow-sm p-4"
            >
              <h3 className="text-lg font-semibold">{event.title}</h3>
              <p className="text-sm text-muted-foreground">
                {event.description}
              </p>
              <p className="text-sm">{event.venue.address.city}</p>
              <Button>
                <Link to={`events/${event.id}`}>Details</Link>
              </Button>
            </div>
          ))}
        </div>
      </section>

      <section className="h-[600px]">
        {/* <Map
            mapboxAccessToken={MAPBOX_ACCESS_TOKEN}
            initialViewState={{
              longitude: userLocation.longitude,
              latitude: userLocation.latitude,
              zoom: 3.5,
            }}
            style={{ width: '100%', height: '100%' }}
            mapStyle="mapbox://styles/mapbox/streets-v11"
          >
            {filteredEvents.map((event) => (
              <Marker
                key={event.id}
                longitude={event.longitude}
                latitude={event.latitude}
                color="#FF0000"
              />
            ))}
          </Map> */}
      </section>
    </main>
  )
}

export default EventsPage
