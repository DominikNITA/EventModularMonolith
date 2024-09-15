import { GeneralLayout } from '@/layouts/general-layout'
import { MinimalLayout } from '@/layouts/minimal-layout'
import EventDetailsPage from '@/pages/general/EventDetails'
import EventsPage from '@/pages/general/Events'
import { OrganizerLogin } from '@/pages/organizer/Login'
import OrganizerRegister from '@/pages/organizer/Register'
import { RouteObject } from 'react-router-dom'

const GeneralRoutes: RouteObject[] = [
  {
    path: '/',
    element: <GeneralLayout />,
    children: [
      {
        path: '',
        element: <EventsPage />,
      },
      {
        path: 'events/:eventId',
        element: <EventDetailsPage />,
      },
    ],
  },
]

export default GeneralRoutes
