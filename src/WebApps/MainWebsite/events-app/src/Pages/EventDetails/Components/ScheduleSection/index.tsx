import React from 'react'

export const ScheduleSection = () => {
  return (
    <>
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
    </>
  )
}
