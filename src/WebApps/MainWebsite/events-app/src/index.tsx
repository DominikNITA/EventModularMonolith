import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import './main.css'
import 'react-notifications-component/dist/theme.css'
import theme from './theme'
import reportWebVitals from './reportWebVitals'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import { MainLayout } from './MainLayout'
import { EventDetails } from './Pages/EventDetails'
import { ClientContext, createRootClient } from './services/RootClient'
import { ReactNotifications } from 'react-notifications-component'
import { LandingPage } from './Pages/LandingPage'
import { AdminLayout } from './AdminLayout'
import { GeneralLogin } from './Pages/Login/GeneralLogin'
import { AdminLogin } from './Pages/Login/AdminLogin'
import { Moderators } from './AdminPages/Moderators'
import { ThemeProvider } from '@mui/material/styles'
import CssBaseline from '@mui/material/CssBaseline'

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement)

const router = createBrowserRouter([
  {
    element: <MainLayout />,
    children: [
      {
        path: '/',
        element: <LandingPage />,
      },
      {
        path: 'events/:eventId',
        element: <EventDetails />,
      },
      {
        path: 'login',
        element: <GeneralLogin />,
      },
    ],
  },
  {
    path: 'organizer',
    element: <AdminLayout></AdminLayout>,
    children: [
      {
        path: 'login',
        element: <AdminLogin />,
      },
      {
        path: 'dashboard',
        element: <div>Dashboard</div>,
      },
      {
        path: 'events',
        element: <div>Events</div>,
      },
      {
        path: 'moderators',
        element: <Moderators />,
      },
    ],
  },
])

const rootClient = createRootClient()

root.render(
  <React.StrictMode>
    <ClientContext.Provider value={rootClient}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <RouterProvider router={router} />
      </ThemeProvider>
    </ClientContext.Provider>
    <ReactNotifications />
  </React.StrictMode>,
)

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals()
