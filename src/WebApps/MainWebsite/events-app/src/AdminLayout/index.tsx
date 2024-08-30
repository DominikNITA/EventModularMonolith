import React from 'react'
import {
  Button,
  Collapse,
  Container,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  DropdownToggle,
  Nav,
  Navbar,
  NavItem,
} from 'react-bootstrap'
import { Link, Outlet, useLocation } from 'react-router-dom'

import './AdminLayout.css'
import {
  Building,
  Calendar,
  People,
  PersonFillGear,
  PersonLinesFill,
  PersonRaisedHand,
  Speaker,
  Speedometer,
} from 'react-bootstrap-icons'

export const AdminLayout = () => {
  return (
    <main>
      {/********header**********/}
      <Header />
      <div className="pageWrapper d-lg-flex">
        {/********Sidebar**********/}
        <aside className="sidebarArea shadow showSidebar" id="sidebarArea">
          <Sidebar />
        </aside>
        {/********Content Area**********/}
        <div className="contentArea">
          {/********Middle Content**********/}
          <Container className="p-4" fluid>
            <Outlet />
          </Container>
        </div>
      </div>
    </main>
  )
}

const Header = () => {
  const [isOpen, setIsOpen] = React.useState(false)

  const [dropdownOpen, setDropdownOpen] = React.useState(false)

  const toggle = () => setDropdownOpen((prevState) => !prevState)
  const Handletoggle = () => {
    setIsOpen(!isOpen)
  }
  const showMobilemenu = () => {
    document.getElementById('sidebarArea')?.classList.toggle('showSidebar')
  }
  return (
    <Navbar expand="md" className="fix-header">
      <div className="d-flex align-items-center">
        <div className="d-lg-block d-none me-5 pe-3">
          <Logo />
        </div>
        <Navbar.Brand href="/">Event</Navbar.Brand>
        <Button
          color="primary"
          className=" d-lg-none"
          onClick={() => showMobilemenu()}
        >
          <i className="bi bi-list"></i>
        </Button>
      </div>
      <div className="hstack gap-2">
        <Button
          color="primary"
          size="sm"
          className="d-sm-block d-md-none"
          onClick={Handletoggle}
        >
          {isOpen ? (
            <i className="bi bi-x"></i>
          ) : (
            <i className="bi bi-three-dots-vertical"></i>
          )}
        </Button>
      </div>

      <Collapse>
        <>
          <Nav className="me-auto" navbar>
            <NavItem>
              <Link to="/starter" className="nav-link">
                Starter
              </Link>
            </NavItem>
            <NavItem>
              <Link to="/about" className="nav-link">
                About
              </Link>
            </NavItem>
          </Nav>
          <Dropdown>
            <DropdownToggle>
              My account
              <PersonLinesFill></PersonLinesFill>
            </DropdownToggle>
            <DropdownMenu>
              <DropdownItem>Edit Profile</DropdownItem>
              <DropdownItem>Logout</DropdownItem>
            </DropdownMenu>
          </Dropdown>
        </>
      </Collapse>
    </Navbar>
  )
}

const Logo = () => {
  return <Link to="/">TheEvent</Link>
}

const navigation = [
  {
    title: 'Dashboard',
    href: '/dashboard',
    icon: <Speedometer />,
  },
  {
    title: 'Events',
    href: '/events',
    icon: <Calendar />,
  },
  {
    title: 'Moderators',
    href: '/moderators',
    icon: <People />,
  },
  {
    title: 'Speakers',
    href: '/speakers',
    icon: <PersonRaisedHand />,
  },
  {
    title: 'Venues',
    href: '/venues',
    icon: <Building />,
  },
]

const Sidebar = () => {
  const showMobilemenu = () => {
    document.getElementById('sidebarArea')?.classList.toggle('showSidebar')
  }
  let location = useLocation()

  return (
    <div className="bg-dark">
      <div className="d-flex">
        <Button
          color="white"
          className="ms-auto text-white d-lg-none"
          onClick={() => showMobilemenu()}
        >
          <i className="bi bi-x"></i>
        </Button>
      </div>
      <div className="p-3 mt-2">
        <Nav className="sidebarNav flex-column">
          {navigation.map((navi, index) => (
            <NavItem key={index} className="sidenav-bg">
              <Link
                to={`/organizer${navi.href}`}
                className={
                  location.pathname === navi.href
                    ? 'active nav-link py-3'
                    : 'nav-link py-3'
                }
              >
                {navi.icon}
                <span className="ms-3 d-inline-block">{navi.title}</span>
              </Link>
            </NavItem>
          ))}
        </Nav>
      </div>
    </div>
  )
}
