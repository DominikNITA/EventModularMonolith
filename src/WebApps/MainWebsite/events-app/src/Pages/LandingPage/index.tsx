import React from 'react'
import { Col } from 'react-bootstrap'
import { Link } from 'react-router-dom'

export const LandingPage = () => {
  return (
    <>
      <Col md={24}>
        <h1>LandingPage</h1>
      </Col>
      <footer>
        <Link to="/organizer/login">Login for organizers</Link>
      </footer>
    </>
  )
}
