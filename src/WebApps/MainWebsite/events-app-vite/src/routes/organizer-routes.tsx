import { OrganizerLayout } from '@/layouts/organizer-layout'
import { Moderators } from '@/pages/organizer/Moderators'
import { RouteObject } from 'react-router-dom'
import ProtectedRoute from './private-route'
import { OrganizerEventsPage } from '@/pages/organizer/Events'
import { OrganizerVenuesPage } from '@/pages/organizer/Venues'

const OrganizerRoutes: RouteObject[] = [
  {
    path: 'organizer',
    element: (
      <ProtectedRoute>
        <OrganizerLayout />
      </ProtectedRoute>
    ),
    children: [
      {
        path: 'dashboard',
        element: <div>Dashboard</div>,
      },
      {
        path: 'events',
        element: <OrganizerEventsPage />,
      },
      {
        path: 'moderators',
        element: <Moderators />,
      },
      {
        path: 'venues',
        element: <OrganizerVenuesPage />,
      },
    ],
  },
]

export default OrganizerRoutes
