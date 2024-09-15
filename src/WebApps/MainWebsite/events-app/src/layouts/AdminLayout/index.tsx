import React from 'react'
import { Link, Outlet, useLocation } from 'react-router-dom'

import './AdminLayout.css'
import Box from '@mui/material/Box'
import AppBar from '@mui/material/AppBar'
import Toolbar from '@mui/material/Toolbar'
import { useTheme } from '@mui/material'
import { Header } from './Header'

export const AdminLayout = () => {
  const theme = useTheme()
  return (
    <Box sx={{ display: 'flex' }}>
      <Header />
    </Box>
    // <main>
    //   {/********header**********/}
    //   <Header />
    //   <div className="pageWrapper d-lg-flex">
    //     {/********Sidebar**********/}
    //     <aside className="sidebarArea shadow showSidebar" id="sidebarArea">
    //       <Sidebar />
    //     </aside>
    //     {/********Content Area**********/}
    //     <div className="contentArea">
    //       {/********Middle Content**********/}
    //       <Container className="p-4" fluid>
    //         <Outlet />
    //       </Container>
    //     </div>
    //   </div>
    // </main>
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
