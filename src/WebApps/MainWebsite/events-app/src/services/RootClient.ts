import React, { useContext } from 'react'
import { ClientBase } from './ClientBase'
import { Client, IClient } from './EventsClient'

export type RootClientArgs = {
  mainClient?: IClient
}

export class RootClient {
  mainClient: IClient

  /* eslint-disable @typescript-eslint/no-non-null-assertion */
  constructor(args: RootClientArgs) {
    this.mainClient = args.mainClient!
  }
  /* eslint-enable @typescript-eslint/no-non-null-assertion */
}
/* eslint-enable @typescript-eslint/no-non-null-assertion */

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

export function createRootClient(): RootClient {
  return new RootClient({
    mainClient: new Client(),
  })
}
