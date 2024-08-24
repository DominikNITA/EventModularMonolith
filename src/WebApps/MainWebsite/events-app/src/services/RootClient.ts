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
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true
      const refreshToken = localStorage.getItem('refreshToken')
      if (refreshToken) {
        try {
          const response = await axios.post(`/refreshToken`, { refreshToken })
          // don't use axious instance that already configured for refresh token api call
          const newAccessToken = response.data.accessToken
          localStorage.setItem('accessToken', newAccessToken) //set new access token
          originalRequest.headers.Authorization = `Bearer ${newAccessToken}`
          return axios(originalRequest) //recall Api with new token
        } catch (error) {
          // Handle token refresh failure
          // mostly logout the user and re-authenticate by login again
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
