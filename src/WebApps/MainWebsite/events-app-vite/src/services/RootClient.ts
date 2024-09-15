import React, { useContext } from 'react'
import { baseAppUrl } from './ClientBase'
import {
  CategoriesClient,
  EventsClient,
  ICategoriesClient,
  IEventsClient,
  IOrganizersClient,
  ISpeakersClient,
  IUsersClient,
  OrganizersClient,
  SpeakersClient,
  UsersClient,
} from './EventsClient'
import axios from 'axios'
import { AuthenticationService } from './AuthService'
import { redirect } from 'react-router-dom'

export type RootClientArgs = {
  eventsClient?: IEventsClient
  usersClient?: IUsersClient
  speakersClient?: ISpeakersClient
  organizersClient?: IOrganizersClient
  categoriesClient?: ICategoriesClient
}

export class RootClient {
  eventsClient: IEventsClient
  usersClient: IUsersClient
  speakersClient: ISpeakersClient
  organizersClient: IOrganizersClient
  categoriesClient: ICategoriesClient

  constructor(args: RootClientArgs) {
    this.eventsClient = args.eventsClient!
    this.usersClient = args.usersClient!
    this.speakersClient = args.speakersClient!
    this.organizersClient = args.organizersClient!
    this.categoriesClient = args.categoriesClient!
  }
}

export const ClientContext = React.createContext<RootClient | undefined>(
  undefined,
)

export function useClient(): RootClient {
  const rootClient = useContext(ClientContext)
  if (!rootClient) {
    throw new Error('RootClient nie został zainicjowany!')
  }

  return rootClient
}

const axiosConfig = axios.create({
  baseURL: baseAppUrl,
  headers: {
    'Content-Type': 'application/json',
  },
})

axiosConfig.interceptors.request.use(
  (config) => {
    const accessToken = AuthenticationService.getAccessToken()
    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  },
)

axiosConfig.interceptors.response.use(
  (response) => {
    return response
  },
  async (error) => {
    const originalRequest = error.config
    if (error?.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true
      const refreshToken = AuthenticationService.getRefreshToken()
      if (refreshToken) {
        try {
          const response = await axios.post(
            AuthenticationService.getRefreshUrl(),
            { refreshToken },
          )
          AuthenticationService.authenticate({
            access_token: response.data.access_token,
            refresh_token: response.data.refresh_token,
            organizer_id: response.data.organizer_id,
          })
          originalRequest.headers.Authorization = `Bearer ${AuthenticationService.getAccessToken()}`
          return axios(originalRequest) //recall Api with new token
        } catch (error) {
          const redirectToOrganizerLogin =
            !!AuthenticationService.getOrganizerId()
          AuthenticationService.logOff()
          redirect(redirectToOrganizerLogin ? '/organizer/login' : '/login')
        }
      }
    }
    return Promise.reject(error)
  },
)

export default axiosConfig

export function createRootClient(): RootClient {
  return new RootClient({
    eventsClient: new EventsClient(undefined, axiosConfig),
    usersClient: new UsersClient(undefined, axiosConfig),
    speakersClient: new SpeakersClient(undefined, axiosConfig),
    organizersClient: new OrganizersClient(undefined, axiosConfig),
    categoriesClient: new CategoriesClient(undefined, axiosConfig),
  })
}
