import React, { useState } from 'react'
import { useAjax, getResponse } from '../../services/ApiHelper'
import { EventResponse } from '../../services/EventsClient'
import { useParams } from 'react-router-dom'
import { useClient } from '../../services/RootClient'
import { SpeakersSection } from './Components/SpeakersSection'

import dayjs from 'dayjs'

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
      <section id="hero" className="hero section dark-background">
        <img src={'/img/hero-bg.jpg'} alt="" className="" />

        <div className="container d-flex flex-column align-items-center text-center mt-auto">
          <h2 className="">{event?.title}</h2>
          <p>
            {dayjs(event?.startsAtUtc).date()}{' '}
            {dayjs(event?.startsAtUtc).format('MMMM')}, {event?.location}
          </p>
          <div className="">
            <a
              href="https://www.youtube.com/watch?v=LXb3EKWsInQ"
              className="glightbox pulsating-play-btn mt-3"
            ></a>
          </div>
        </div>

        <div className="about-info mt-auto position-relative">
          <div className="container position-relative">
            <div className="row">
              <div className="col-lg-6">
                <h2>About The Event</h2>
                <p>{event?.description}</p>
              </div>
              <div className="col-lg-3">
                <h3>Where</h3>
                <p>{event?.location}</p>
              </div>
              <div className="col-lg-3">
                <h3>When</h3>
                <p>
                  {dayjs(event?.startsAtUtc).format('dddd')}
                  <br />
                  {dayjs(event?.startsAtUtc).date()}{' '}
                  {dayjs(event?.startsAtUtc).format('MMMM')}
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <SpeakersSection eventId={eventId!} />

      <section id="schedule" className="schedule section">
        <div className="container section-title">
          <h2>
            Event Schedule
            <br />
          </h2>
          <p>
            Necessitatibus eius consequatur ex aliquid fuga eum quidem sint
            consectetur velit
          </p>
        </div>

        <div className="container">
          <ul className="nav nav-tabs" role="tablist">
            <li className="nav-item">
              <a
                className="nav-link active"
                href="#day-1"
                role="tab"
                data-bs-toggle="tab"
              >
                Day 1
              </a>
            </li>
            <li className="nav-item">
              <a
                className="nav-link"
                href="#day-2"
                role="tab"
                data-bs-toggle="tab"
              >
                Day 2
              </a>
            </li>
            <li className="nav-item">
              <a
                className="nav-link"
                href="#day-3"
                role="tab"
                data-bs-toggle="tab"
              >
                Day 3
              </a>
            </li>
          </ul>

          <div className="tab-content row justify-content-center">
            <h3 className="sub-heading">
              Voluptatem nulla veniam soluta et corrupti consequatur neque
              eveniet officia. Eius necessitatibus voluptatem quis labore
              perspiciatis quia.
            </h3>

            <div
              role="tabpanel"
              className="col-lg-9 tab-pane fade show active"
              id="day-1"
            >
              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>09:30 AM</time>
                </div>
                <div className="col-md-10">
                  <h4>Registration</h4>
                  <p>
                    Fugit voluptas iusto maiores temporibus autem numquam
                    magnam.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>10:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-1-2.jpg"
                      alt="Brenden Legros"
                    />
                  </div>
                  <h4>
                    Keynote <span>Brenden Legros</span>
                  </h4>
                  <p>Facere provident incidunt quos voluptas.</p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>11:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-2-2.jpg"
                      alt="Hubert Hirthe"
                    />
                  </div>
                  <h4>
                    Et voluptatem iusto dicta nobis. <span>Hubert Hirthe</span>
                  </h4>
                  <p>
                    Maiores dignissimos neque qui cum accusantium ut sit sint
                    inventore.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>12:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-3-2.jpg"
                      alt="Cole Emmerich"
                    />
                  </div>
                  <h4>
                    Explicabo et rerum quis et ut ea. <span>Cole Emmerich</span>
                  </h4>
                  <p>
                    Veniam accusantium laborum nihil eos eaque accusantium
                    aspernatur.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>02:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-4-2.jpg"
                      alt="Jack Christiansen"
                    />
                  </div>
                  <h4>
                    Qui non qui vel amet culpa sequi.{' '}
                    <span>Jack Christiansen</span>
                  </h4>
                  <p>Nam ex distinctio voluptatem doloremque suscipit iusto.</p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>03:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-5.jpg"
                      alt="Alejandrin Littel"
                    />
                  </div>
                  <h4>
                    Quos ratione neque expedita asperiores.{' '}
                    <span>Alejandrin Littel</span>
                  </h4>
                  <p>
                    Eligendi quo eveniet est nobis et ad temporibus odio quo.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>04:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-6.jpg"
                      alt="Willow Trantow"
                    />
                  </div>
                  <h4>
                    Quo qui praesentium nesciunt <span>Willow Trantow</span>
                  </h4>
                  <p>
                    Voluptatem et alias dolorum est aut sit enim neque
                    veritatis.
                  </p>
                </div>
              </div>
            </div>

            <div role="tabpanel" className="col-lg-9  tab-pane fade" id="day-2">
              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>10:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-1-2.jpg"
                      alt="Brenden Legros"
                    />
                  </div>
                  <h4>
                    Libero corrupti explicabo itaque.{' '}
                    <span>Brenden Legros</span>
                  </h4>
                  <p>Facere provident incidunt quos voluptas.</p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>11:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-2-2.jpg"
                      alt="Hubert Hirthe"
                    />
                  </div>
                  <h4>
                    Et voluptatem iusto dicta nobis. <span>Hubert Hirthe</span>
                  </h4>
                  <p>
                    Maiores dignissimos neque qui cum accusantium ut sit sint
                    inventore.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>12:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-3-2.jpg"
                      alt="Cole Emmerich"
                    />
                  </div>
                  <h4>
                    Explicabo et rerum quis et ut ea. <span>Cole Emmerich</span>
                  </h4>
                  <p>
                    Veniam accusantium laborum nihil eos eaque accusantium
                    aspernatur.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>02:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-4-2.jpg"
                      alt="Jack Christiansen"
                    />
                  </div>
                  <h4>
                    Qui non qui vel amet culpa sequi.{' '}
                    <span>Jack Christiansen</span>
                  </h4>
                  <p>Nam ex distinctio voluptatem doloremque suscipit iusto.</p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>03:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-5.jpg"
                      alt="Alejandrin Littel"
                    />
                  </div>
                  <h4>
                    Quos ratione neque expedita asperiores.{' '}
                    <span>Alejandrin Littel</span>
                  </h4>
                  <p>
                    Eligendi quo eveniet est nobis et ad temporibus odio quo.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>04:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-6.jpg"
                      alt="Willow Trantow"
                    />
                  </div>
                  <h4>
                    Quo qui praesentium nesciunt <span>Willow Trantow</span>
                  </h4>
                  <p>
                    Voluptatem et alias dolorum est aut sit enim neque
                    veritatis.
                  </p>
                </div>
              </div>
            </div>

            <div role="tabpanel" className="col-lg-9  tab-pane fade" id="day-3">
              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>10:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-2-2.jpg"
                      alt="Hubert Hirthe"
                    />
                  </div>
                  <h4>
                    Et voluptatem iusto dicta nobis. <span>Hubert Hirthe</span>
                  </h4>
                  <p>
                    Maiores dignissimos neque qui cum accusantium ut sit sint
                    inventore.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>11:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-3-2.jpg"
                      alt="Cole Emmerich"
                    />
                  </div>
                  <h4>
                    Explicabo et rerum quis et ut ea. <span>Cole Emmerich</span>
                  </h4>
                  <p>
                    Veniam accusantium laborum nihil eos eaque accusantium
                    aspernatur.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>12:00 AM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-1-2.jpg"
                      alt="Brenden Legros"
                    />
                  </div>
                  <h4>
                    Libero corrupti explicabo itaque.{' '}
                    <span>Brenden Legros</span>
                  </h4>
                  <p>Facere provident incidunt quos voluptas.</p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>02:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-4-2.jpg"
                      alt="Jack Christiansen"
                    />
                  </div>
                  <h4>
                    Qui non qui vel amet culpa sequi.{' '}
                    <span>Jack Christiansen</span>
                  </h4>
                  <p>Nam ex distinctio voluptatem doloremque suscipit iusto.</p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>03:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-5.jpg"
                      alt="Alejandrin Littel"
                    />
                  </div>
                  <h4>
                    Quos ratione neque expedita asperiores.{' '}
                    <span>Alejandrin Littel</span>
                  </h4>
                  <p>
                    Eligendi quo eveniet est nobis et ad temporibus odio quo.
                  </p>
                </div>
              </div>

              <div className="row schedule-item">
                <div className="col-md-2">
                  <time>04:00 PM</time>
                </div>
                <div className="col-md-10">
                  <div className="speaker">
                    <img
                      src="/img/speakers/speaker-6.jpg"
                      alt="Willow Trantow"
                    />
                  </div>
                  <h4>
                    Quo qui praesentium nesciunt <span>Willow Trantow</span>
                  </h4>
                  <p>
                    Voluptatem et alias dolorum est aut sit enim neque
                    veritatis.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

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
              <iframe
                src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d12097.433213460943!2d-74.0062269!3d40.7101282!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0xb89d1fe6bc499443!2sDowntown+Conference+Center!5e0!3m2!1smk!2sbg!4v1539943755621"
                frameBorder="0"
              ></iframe>
            </div>

            <div className="col-lg-6 venue-info">
              <div className="row justify-content-center">
                <div className="col-11 col-lg-8 position-relative">
                  <h3>Downtown Conference Center, New York</h3>
                  <p>
                    Iste nobis eum sapiente sunt enim dolores labore accusantium
                    autem. Cumque beatae ipsam. Est quae sit qui voluptatem
                    corporis velit. Qui maxime accusamus possimus. Consequatur
                    sequi et ea suscipit enim nesciunt quia velit.
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="container-fluid venue-gallery-container">
          <div className="row g-0">
            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-1.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-1.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>

            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-2.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-2.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>

            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-3.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-3.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>

            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-4.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-4.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>

            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-5.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-5.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>

            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-6.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-6.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>

            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-7.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-7.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>

            <div className="col-lg-3 col-md-4">
              <div className="venue-gallery">
                <a
                  href="/img/venue-gallery/venue-gallery-8.jpg"
                  className="glightbox"
                  data-gall="venue-gallery"
                >
                  <img
                    src="/img/venue-gallery/venue-gallery-8.jpg"
                    alt=""
                    className="img-fluid"
                  />
                </a>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  )
}
