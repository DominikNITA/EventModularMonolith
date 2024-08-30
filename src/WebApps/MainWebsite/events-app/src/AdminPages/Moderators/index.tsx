import React, { useState } from 'react'
import {
  ModeratorDto,
  OrganizersClient,
  ResultOfIReadOnlyCollectionOfModeratorDto,
} from '../../services/EventsClient'
import { ajax, NetworkState } from '../../services/ApiHelper'
import { useClient } from '../../services/RootClient'
import { AuthenticationService } from '../../services/AuthService'

export const Moderators = () => {
  const [moderators, setModerators] = useState<ModeratorDto[]>([])
  const { organizersClient } = useClient()
  ajax({
    request: () =>
      organizersClient.getModerators(AuthenticationService.getOrganizerId()!),
    setResult: (
      result: NetworkState<ResultOfIReadOnlyCollectionOfModeratorDto>,
    ) => {
      console.log(result)
      if (result.state === 'success') {
        setModerators(result.response.value!)
      }
    },
    showSuccessNotification: true,
  })

  return (
    <>
      <h1>Moderators</h1>
      {moderators.map((moderator) => (
        <div key={moderator.userId}>
          {moderator.firstName}
          {moderator.lastName}
        </div>
      ))}
    </>
  )
}
