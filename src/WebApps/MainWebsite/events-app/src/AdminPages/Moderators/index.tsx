import { useState } from 'react'
import {
  ModeratorDto,
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
      if (result.state === 'success') {
        setModerators(result.response.value!)
      }
    },
    showSuccessNotification: true,
  })

  return (
    <>
      <h1>Moderators</h1>
      <Table striped bordered hover variant="light">
        <thead>
          <tr>
            <th>#</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Is active</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {moderators.map((moderator, index) => (
            <tr>
              <td>{index + 1}</td>
              <td>{moderator.firstName}</td>
              <td>{moderator.lastName}</td>
              <td>TODO@todo.com</td>
              <td>
                {moderator.isActive ? (
                  <Check size={25} color="green" />
                ) : (
                  <Crosshair size={25} color="red" />
                )}
              </td>
              <td></td>
            </tr>
          ))}
        </tbody>
      </Table>
    </>
  )
}
