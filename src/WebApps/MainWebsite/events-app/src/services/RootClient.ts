import React, { useContext } from 'react'
import { baseAppUrl } from './ClientBase'
import {
  EventsClient,
  IEventsClient,
  ISpeakersClient,
  IUsersClient,
  SpeakersClient,
  UsersClient,
} from './EventsClient'
import axios from 'axios'
import { AuthenticationService } from './AuthService'
import { log } from 'console'

export type RootClientArgs = {
  eventsClient?: IEventsClient
  usersClient?: IUsersClient
  speakersClient?: ISpeakersClient
}

export class RootClient {
  eventsClient: IEventsClient
  usersClient: IUsersClient
  speakersClient: ISpeakersClient

  constructor(args: RootClientArgs) {
    this.eventsClient = args.eventsClient!
    this.usersClient = args.usersClient!
    this.speakersClient = args.speakersClient!
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
          })
          originalRequest.headers.Authorization = `Bearer ${AuthenticationService.getAccessToken()}`
          return axios(originalRequest) //recall Api with new token
        } catch (error) {
          AuthenticationService.logOff()
          //TODO: Redirect to login page
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
  })
}
