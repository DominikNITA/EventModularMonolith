import React, { useEffect, useState } from 'react'
import { useClient } from '../../../../services/RootClient'
import { Link } from 'react-router-dom'

interface IProps {
  eventId: string
}

interface Speaker {
  id: string
  image_src: string
  name: string
  description: string
  links: Link[]
}

interface Link {
  type: string
  link: string
}

export const SpeakersSection = (props: IProps) => {
  const { mainClient } = useClient()

  const [speakers, setSpeakers] = useState<Speaker[]>()

  useEffect(() => {
    setSpeakers([
      {
        id: 'speaker1',
        description: 'Quas alias incidunt',
        image_src: '/img/speakers/speaker-1.jpg',
        name: 'Walter White',
        links: [
          { link: 'https://sport.tvp.pl/', type: 'facebook' },
          { link: 'https://sport.tvp.pl/', type: 'instagram' },
        ],
      },
      {
        id: 'speaker2',
        description: 'Quel bg',
        image_src: '/img/speakers/speaker-2.jpg',
        name: 'Hubert Hirthe',
        links: [{ link: 'https://sport.tvp.pl/', type: 'twitter' }],
      },
      {
        id: 'speaker3',
        description: 'Testing description',
        image_src: '/img/speakers/speaker-3.jpg',
        name: 'Amanda Jepson',
        links: [{ link: 'https://sport.tvp.pl/', type: 'linkedin' }],
      },
      {
        id: 'speaker4',
        description: 'Testing description',
        image_src: '/img/speakers/speaker-3.jpg',
        name: 'Amanda Jepson',
        links: [{ link: 'https://sport.tvp.pl/', type: 'linkedin' }],
      },
    ])
  }, [])

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
                  <img src={speaker.image_src} className="img-fluid" alt="" />
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
                      {speaker.links.map((link) => (
                        <a href={link.link}>
                          <i className={`bi bi-${link.type}-x`}></i>
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
