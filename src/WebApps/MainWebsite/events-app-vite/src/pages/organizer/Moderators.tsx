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
  ModeratorDto,
  ResultOfIReadOnlyCollectionOfModeratorDto,
} from '@/services/EventsClient'
import { useClient } from '@/services/RootClient'
import { useEffect, useState } from 'react'

export const Moderators = () => {
  const [moderators, setModerators] = useState<ModeratorDto[]>([])
  const { organizersClient } = useClient()
  useEffect(() => {
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
    <Card className="max-w-screen-lg">
      <CardHeader>
        <CardTitle>Moderator List</CardTitle>
        <CardDescription>
          A list of all moderators in your organization
        </CardDescription>
      </CardHeader>
      <CardContent>
        <GenericTable
          data={moderators}
          columns={[
            {
              header: 'Name',
              accessor: (moderator) =>
                `${moderator.firstName} ${moderator.lastName}`,
              className: 'text-center',
              sortable: true,
            },
            {
              header: 'Email',
              accessor: 'email',
              className: 'text-center ',
              sortable: true,
            },
            {
              header: 'Is active',
              accessor: (moderator) =>
                moderator.isActive ? '✅ Active' : '❌ Inactive',
              className: 'text-center w-36',
              sortable: true,
            },
          ]}
          actionsClassname="text-center w-64"
          idField="userId"
          createOptions={{
            buttonPosition: 'both',
            showCreateButton: true,
            createButtonText: 'Add new moderator',
          }}
          modifyOptions={{
            showModifyButton: false,
          }}
          deleteOptions={{
            showDeleteButton: (moderator) => moderator.isOwner === false,
          }}
        />
      </CardContent>
      {/* <Table>
          <TableHeader>
            <TableRow>
              <TableHead className="text-center">Name</TableHead>
              <TableHead className="text-center">Email</TableHead>
              <TableHead className="text-center w-28">Status</TableHead>
              <TableHead className="text-center w-64">Actions</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {moderators.map((moderator) => (
              <TableRow key={moderator.userId}>
                <TableCell>{`${moderator.firstName} ${moderator.lastName}`}</TableCell>
                <TableCell>{moderator.email}</TableCell>
                <TableCell>
                  {moderator.isActive ? '✅ Active' : '❌ Inactive'}
                </TableCell>
                <TableCell>
                  <div className="space-x-2">
                    <Button variant="outline" size="sm">
                      View logs
                    </Button>
                    {moderator.isOwner === false && (
                      <>
                        <Button variant="outline" size="sm">
                          {moderator.isActive ? 'Deactivate' : 'Activate'}
                        </Button>
                        <Button
                          variant="destructive"
                          size="sm"
                          onClick={() => openPopup(moderator)}
                        >
                          Delete
                        </Button>
                      </>
                    )}
                  </div>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </CardContent>
      <CardFooter>
        <Button variant="default" size="sm">
          Add new moderator
        </Button>
      </CardFooter>
      {popupState.isOpen && (
        <div className="fixed inset-0 bg-opacity-50 flex items-center justify-center">
          <div className="p-6 bg-secondary rounded-lg shadow-lg">
            <h2 className="text-xl font-bold mb-4">Confirm Deletion</h2>
            <p className="mb-4">
              Are you sure you want to delete the moderator{' '}
              {popupState.moderator?.firstName} {popupState.moderator?.lastName}
              ? This action cannot be undone.
            </p>
            <div className="flex justify-end space-x-2">
              <Button variant="outline" onClick={closePopup}>
                Cancel
              </Button>
              <Button variant="destructive" onClick={handleDeleteConfirm}>
                Delete
              </Button>
            </div>
          </div>
        </div>
      )} */}
    </Card>
  )
}
