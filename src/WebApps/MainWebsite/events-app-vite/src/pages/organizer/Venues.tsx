import { GenericTable } from '@/components/generic-table'
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
} from '@/components/ui/card'
import { ajax, NetworkState } from '@/services/ApiHelper'
import { AuthenticationService } from '@/services/AuthService'
import {
  ModeratorDto,
  ResultOfIReadOnlyCollectionOfVenueGridDto,
  VenueGridDto,
} from '@/services/EventsClient'
import { useClient } from '@/services/RootClient'
import { useEffect, useState } from 'react'
import { EventModal } from './modals/event-modal'
import { VenueModal } from './modals/venue-modal'

export const OrganizerVenuesPage = () => {
  const [venues, setEvents] = useState<VenueGridDto[]>([])
  const { organizersClient } = useClient()
  useEffect(() => {
    ajax({
      request: () =>
        organizersClient.getVenuesForOrganizer(
          AuthenticationService.getOrganizerId()!,
        ),
      setResult: (
        result: NetworkState<ResultOfIReadOnlyCollectionOfVenueGridDto>,
      ) => {
        if (result.state === 'success') {
          setEvents(result.response.value!)
        }
      },
      showSuccessNotification: false,
    })
  }, [])

  const [popupState, setPopupState] = useState<{
    isOpen: boolean
    moderator: ModeratorDto | null
  }>({
    isOpen: false,
    moderator: null,
  })

  const openPopup = (moderator: ModeratorDto) => {
    setPopupState({ isOpen: true, moderator })
  }

  const closePopup = () => {
    setPopupState({ isOpen: false, moderator: null })
  }

  const handleDeleteConfirm = () => {
    if (popupState.moderator) {
      // Perform delete operation here
      console.log(
        `Deleting moderator: ${popupState.moderator.firstName} ${popupState.moderator.lastName}`,
      )
      // After deletion, close the popup
      closePopup()
    }
  }

  return (
    <Card className="w-full">
      <CardHeader>
        <CardTitle>Venues list</CardTitle>
        <CardDescription>
          A list of all venues in your organization
        </CardDescription>
      </CardHeader>
      <CardContent>
        <GenericTable
          data={venues}
          columns={[
            {
              header: 'Name',
              accessor: 'name',
              className: 'text-center ',
            },
            {
              header: 'Description',
              accessor: 'description',
              className: 'text-center ',
            },
            {
              header: 'Address',
              accessor: 'shortAddress',
              className: 'text-center w-28',
            },
          ]}
          actionsClassname="text-center w-64"
          idField="venueId"
          createOptions={{
            buttonPosition: 'both',
            showCreateButton: true,
            createButtonText: 'Add new event',
          }}
        />
        <VenueModal />
      </CardContent>
    </Card>
  )
}
