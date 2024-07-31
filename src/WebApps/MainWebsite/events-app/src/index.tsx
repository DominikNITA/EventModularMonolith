import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import './main.css'
import reportWebVitals from './reportWebVitals'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import { MainLayout } from './MainLayout'
import { EventDetails } from './Pages/EventDetails'
import { ClientContext, createRootClient } from './services/RootClient'

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement)

const router = createBrowserRouter([
  {
    element: <MainLayout />,
    children: [
      {
        path: 'events/:eventId',
        element: <EventDetails />,
      },
    ],
  },
])

const rootClient = createRootClient()

root.render(
  <React.StrictMode>
    <ClientContext.Provider value={rootClient}>
      <RouterProvider router={router} />
    </ClientContext.Provider>
  </React.StrictMode>,
)

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals()
