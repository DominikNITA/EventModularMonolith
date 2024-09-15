import { MinimalLayout } from '@/layouts/minimal-layout'
import { OrganizerLogin } from '@/pages/organizer/Login'
import OrganizerRegister from '@/pages/organizer/Register'
import { RouteObject } from 'react-router-dom'

const AuthRoutes: RouteObject[] = [
  {
    path: 'auth',
    element: <MinimalLayout />,
    children: [
      {
        path: 'organizer/login',
        element: <OrganizerLogin />,
      },
      {
        path: 'organizer/register',
        element: <OrganizerRegister />,
      },
    ],
  },
]

export default AuthRoutes
