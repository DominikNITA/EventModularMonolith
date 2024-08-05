import React, { useEffect, useState } from 'react'
import { useClient } from '../../../../services/RootClient'
import { Link } from 'react-router-dom'
import { SpeakerDto } from '../../../../services/EventsClient'
import { getResponse, useAjax } from '../../../../services/ApiHelper'

interface IProps {
  eventId: string
}

export const SpeakersSection = (props: IProps) => {
  const { mainClient } = useClient()

  const [speakers, setSpeakers] = useState<SpeakerDto[]>()

  const result = useAjax(
    {
      request: () => mainClient.getSpeakersForEvent(props.eventId ?? ''),
      setResult: (r) => {
        const response = getResponse(r)?.value
        setSpeakers(response)
        console.log(response, r)
      },
    },
    [],
  )

  return (
    <>
      <section id="speakers" className="speakers section">
        <div className="container section-title">
          <h2>
            Event Speakers
            <br />
          </h2>
        </div>

        <div className="container">
          <div className="row gy-4">
            {speakers?.map((speaker) => (
              <div className="col-xl-3 col-lg-4 col-md-6">
                <div className="member">
                  <img src={speaker.imageUrl} className="img-fluid" alt="" />
                  <div className="member-info">
                    <div className="member-info-content">
                      <h4>
                        <Link to={`/speakers/${speaker.id}`}>
                          {speaker.name}
                        </Link>
                      </h4>
                      <span>{speaker.description}</span>
                    </div>
                    <div className="social">
                      {speaker.links?.map((link) => (
                        <a href={link.url}>
                          <i className={`bi bi-twitter-x`}></i>
                        </a>
                      ))}
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
    </>
  )
}
