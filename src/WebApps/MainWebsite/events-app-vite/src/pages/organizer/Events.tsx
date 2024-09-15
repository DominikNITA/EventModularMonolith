import { GenericTable } from '@/components/generic-table'
import { Button } from '@/components/ui/button'
import {
  Card,
  CardHeader,
  CardTitle,
  CardDescription,
  CardContent,
  CardFooter,
} from '@/components/ui/card'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { ajax, NetworkState } from '@/services/ApiHelper'
import { AuthenticationService } from '@/services/AuthService'
import {
  EventResponse,
  ModeratorDto,
  ResultOfIReadOnlyCollectionOfEventResponse,
  ResultOfIReadOnlyCollectionOfModeratorDto,
} from '@/services/EventsClient'
import { useClient } from '@/services/RootClient'
import { useEffect, useState } from 'react'
import { EventModal } from './modals/event-modal'

export const OrganizerEventsPage = () => {
  const [events, setEvents] = useState<EventResponse[]>([])
  const { organizersClient } = useClient()
  useEffect(() => {
    ajax({
      request: () =>
        organizersClient.getEventsForOrganizer(
          AuthenticationService.getOrganizerId()!,
        ),
      setResult: (
        result: NetworkState<ResultOfIReadOnlyCollectionOfEventResponse>,
      ) => {
        if (result.state === 'success') {
          setEvents(result.response.value!)
        }
      },
      showSuccessNotification: true,
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
        <CardTitle>Events list</CardTitle>
        <CardDescription>
          A list of all events in your organization
        </CardDescription>
      </CardHeader>
      <CardContent>
        <GenericTable
          data={events}
          columns={[
            {
              header: 'Title',
              accessor: 'title',
              className: 'text-center ',
            },
            {
              header: 'Description',
              accessor: 'description',
              className: 'text-center ',
            },
            {
              header: 'Dates',
              accessor: (event) => `${event.startsAtUtc} - ${event.endsAtUtc}`,
              className: 'text-center ',
            },
            {
              header: 'Status',
              accessor: (event) => event.id,
              className: 'text-center w-28',
            },
          ]}
          actionsClassname="text-center w-64"
          idField="id"
          createOptions={{
            buttonPosition: 'both',
            showCreateButton: true,
            createButtonText: 'Add new event',
          }}
        />
        <EventModal />
      </CardContent>
    </Card>
  )
}
