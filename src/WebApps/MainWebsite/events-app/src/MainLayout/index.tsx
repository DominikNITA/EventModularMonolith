import React from 'react'
import { Navbar, Nav, NavDropdown, Container, Button } from 'react-bootstrap'
import { Link } from 'react-router-dom'
import { Outlet } from 'react-router-dom'

export const MainLayout = () => {
  return (
    <>
      <Navbar expand="xl" fixed="top" className="header dark-background">
        <Container fluid>
          <Navbar.Brand as={Link} to="/">
            <img src="/img/logo.png" alt="" />
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link as={Link} to="/" className="active">
                Home
              </Nav.Link>
              <Nav.Link href="#speakers">Speakers</Nav.Link>
              <Nav.Link href="#schedule">Schedule</Nav.Link>
              <Nav.Link href="#venue">Venue</Nav.Link>
              <Nav.Link href="#hotels">Hotels</Nav.Link>
              <Nav.Link href="#gallery">Gallery</Nav.Link>
              <NavDropdown title="Dropdown" id="basic-nav-dropdown">
                <NavDropdown.Item href="#">Dropdown 1</NavDropdown.Item>
                <NavDropdown title="Deep Dropdown" id="basic-nav-dropdown-deep">
                  <NavDropdown.Item href="#">Deep Dropdown 1</NavDropdown.Item>
                  <NavDropdown.Item href="#">Deep Dropdown 2</NavDropdown.Item>
                  <NavDropdown.Item href="#">Deep Dropdown 3</NavDropdown.Item>
                  <NavDropdown.Item href="#">Deep Dropdown 4</NavDropdown.Item>
                  <NavDropdown.Item href="#">Deep Dropdown 5</NavDropdown.Item>
                </NavDropdown>
                <NavDropdown.Item href="#">Dropdown 2</NavDropdown.Item>
                <NavDropdown.Item href="#">Dropdown 3</NavDropdown.Item>
                <NavDropdown.Item href="#">Dropdown 4</NavDropdown.Item>
              </NavDropdown>
              <Nav.Link href="#contact">Contact</Nav.Link>
            </Nav>
            <Button
              variant="primary"
              className="cta-btn d-none d-sm-block"
              href="#buy-tickets"
            >
              Buy Tickets
            </Button>
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <Outlet />
    </>
  )
}
