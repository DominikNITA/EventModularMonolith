import { createBrowserRouter } from 'react-router-dom'
import OrganizerRoutes from './organizer-routes'
import AuthRoutes from './auth-routes'
import GeneralRoutes from './general-routes'

const router = createBrowserRouter(
  [...OrganizerRoutes, ...AuthRoutes, ...GeneralRoutes],
  {},
)

export default router
