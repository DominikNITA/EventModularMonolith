import { APIProvider, Marker, Map } from '@vis.gl/react-google-maps'
import { VenueDto } from '../../../../services/EventsClient'
import { useState, useEffect } from 'react'

export interface IProps {
  venue: VenueDto | undefined
}

export const VenueSection = ({ venue }: IProps) => {
  const [imageSources, setImageSources] = useState<string[]>([])
  const apiKey = process.env.REACT_APP_GOOGLE_MAPS_API
  // Placeholder
  useEffect(() => {
    setImageSources([
      '/img/venue-gallery/venue-gallery-1.jpg',
      'http://127.0.0.1:10000/devstoreaccount1/images/0002.png',
      '/img/venue-gallery/venue-gallery-3.jpg',
      '/img/venue-gallery/venue-gallery-4.jpg',
      '/img/venue-gallery/venue-gallery-5.jpg',
      '/img/venue-gallery/venue-gallery-6.jpg',
      '/img/venue-gallery/venue-gallery-7.jpg',
      '/img/venue-gallery/venue-gallery-8.jpg',
    ])
  }, [])

  return (
    <>
      <section id="venue" className="venue section">
        <div className="container section-title">
          <h2>
            Event Venue
            <br />
          </h2>
          <p>
            Necessitatibus eius consequatur ex aliquid fuga eum quidem sint
            consectetur velit
          </p>
        </div>

        <div className="container-fluid">
          <div className="row g-0">
            <div className="col-lg-6 venue-map">
              {venue && apiKey && (
                <APIProvider apiKey={apiKey}>
                  <Map
                    defaultCenter={{
                      lat: venue.address.latitude,
                      lng: venue.address.longitude,
                    }}
                    defaultZoom={13}
                    gestureHandling={'greedy'}
                    disableDefaultUI={true}
                  >
                    <Marker
                      title={venue.name}
                      position={{
                        lat: venue.address.latitude,
                        lng: venue.address.longitude,
                      }}
                      onClick={() =>
                        window.open(
                          `https://maps.google.com/?q=${venue.address.latitude},${venue.address.longitude}`,
                          '_blank',
                        )
                      }
                    />
                  </Map>
                </APIProvider>
              )}
            </div>

            <div className="col-lg-6 venue-info">
              <div className="row justify-content-center">
                <div className="col-11 col-lg-8 position-relative">
                  <h3>
                    {venue?.name}, {venue?.address.city}
                  </h3>
                  <p>{venue?.description}</p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="container-fluid venue-gallery-container">
          <div className="row g-0">
            {imageSources.map((image) => (
              <div className="col-lg-3 col-md-4">
                <div className="venue-gallery">
                  <a
                    href={image}
                    className="glightbox"
                    data-gall="venue-gallery"
                  >
                    <img src={image} alt="" className="img-fluid" />
                  </a>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
    </>
  )
}
