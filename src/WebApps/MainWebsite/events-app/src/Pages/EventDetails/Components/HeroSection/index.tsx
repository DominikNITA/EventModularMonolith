import React from 'react'
import { EventResponse } from '../../../../services/EventsClient'
import dayjs from 'dayjs'

interface IProps {
  event: EventResponse
}

export const HeroSection = ({ event }: IProps) => {
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
    </>
  )
}
