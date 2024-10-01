import { Button } from '@/components/ui/button'
import { useEffect, useState } from 'react'
import { GenericModal } from '@/components/generic-form-modal'
import dayjs, { Dayjs } from 'dayjs'
import {
  FormField,
  FormItem,
  FormLabel,
  FormControl,
  FormDescription,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { z } from 'zod'
import { useForm, UseFormReturn } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { cn } from '@/lib/utils'
import { CaretSortIcon } from '@radix-ui/react-icons'
import {
  Popover,
  PopoverTrigger,
  PopoverContent,
} from '@radix-ui/react-popover'
import {
  CommandInput,
  CommandList,
  CommandEmpty,
  CommandGroup,
  CommandItem,
} from 'cmdk'
import { CheckIcon } from 'lucide-react'
import {
  CategoryDto,
  CreateEventRequest,
  ResultOfGuid,
  ResultOfIReadOnlyCollectionOfCategoryDto,
  ResultOfIReadOnlyCollectionOfVenueGridDto,
  VenueGridDto,
} from '@/services/EventsClient'
import { useClient } from '@/services/RootClient'
import { ajax, NetworkState } from '@/services/ApiHelper'
import { Command } from '@/components/ui/command'
import { Textarea } from '@/components/ui/textarea'
import { AuthenticationService } from '@/services/AuthService'

const EventForm = ({ form }: { form: UseFormReturn<EventFormValues> }) => {
  const [categories, setCategories] = useState<CategoryDto[]>([])
  const [venues, setVenues] = useState<VenueGridDto[]>([])
  const { categoriesClient, organizersClient } = useClient()
  useEffect(() => {
    ajax({
      request: () => categoriesClient.getCategories(),
      setResult: (
        result: NetworkState<ResultOfIReadOnlyCollectionOfCategoryDto>,
      ) => {
        if (result.state === 'success') {
          setCategories(result.response.value!)
        }
      },
    })
    ajax({
      request: () =>
        organizersClient.getVenuesForOrganizer(
          AuthenticationService.getOrganizerId() ?? '',
        ),
      setResult: (
        result: NetworkState<ResultOfIReadOnlyCollectionOfVenueGridDto>,
      ) => {
        if (result.state === 'success') {
          setVenues(result.response.value!)
        }
      },
    })
  }, [])

  return (
    <div className="space-y-4">
      <FormField
        control={form.control}
        name="title"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Title</FormLabel>
            <FormControl>
              <Input placeholder="Your event's title" {...field} />
            </FormControl>
            <FormDescription>This is the events name</FormDescription>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={form.control}
        name="description"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Description</FormLabel>
            <FormControl>
              <Textarea placeholder="Your event's description" {...field} />
            </FormControl>
            <FormDescription>This is the events description</FormDescription>
            <FormMessage />
          </FormItem>
        )}
      />
      <div className="flex space-x-4">
        <FormField
          control={form.control}
          name="startsAtUtc"
          render={({ field }) => (
            <FormItem className="flex flex-col w-[200px]">
              <FormLabel>Start date</FormLabel>
              <FormControl>
                <Input
                  type="datetime-local"
                  placeholder="Your event's description"
                  {...field}
                  value={
                    field.value ? field.value.format('YYYY-MM-DDThh:mm') : ''
                  }
                  onChange={(e) => field.onChange(dayjs(e.target.value))}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="endsAtUtc"
          render={({ field }) => (
            <FormItem className="flex flex-col w-[200px]">
              <FormLabel>End date (optional)</FormLabel>
              <FormControl>
                <Input
                  type="datetime-local"
                  placeholder="Your event's description"
                  {...field}
                  value={
                    field.value ? field.value.format('YYYY-MM-DDThh:mm') : ''
                  }
                  onChange={(e) => field.onChange(dayjs(e.target.value))}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </div>
      <FormField
        control={form.control}
        name="categoryId"
        render={({ field }) => (
          <FormItem className="flex flex-col">
            <FormLabel>Category</FormLabel>
            <Popover>
              <PopoverTrigger asChild>
                <FormControl>
                  <Button
                    variant="outline"
                    role="combobox"
                    className={cn(
                      'w-[200px] justify-between',
                      !field.value && 'text-muted-foreground',
                    )}
                  >
                    {field.value
                      ? categories.find(
                          (category) => category.id === field.value,
                        )?.name
                      : 'Select Category'}
                    <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                  </Button>
                </FormControl>
              </PopoverTrigger>
              <PopoverContent className="w-[200px] p-0">
                <Command>
                  <CommandInput
                    placeholder="Search category..."
                    className="bg-accent"
                  />
                  <CommandList>
                    <CommandEmpty>No category found.</CommandEmpty>
                    <CommandGroup>
                      {categories.map((category) => (
                        <CommandItem
                          value={category.name}
                          key={category.id}
                          onSelect={() => {
                            form.setValue('categoryId', category.id)
                          }}
                        >
                          <div className="flex items-center">
                            <CheckIcon
                              className={cn(
                                'mr-2 h-4 w-4',
                                category.id === field.value
                                  ? 'opacity-100'
                                  : 'opacity-0',
                              )}
                            />
                            {category.name}
                          </div>
                        </CommandItem>
                      ))}
                    </CommandGroup>
                  </CommandList>
                </Command>
              </PopoverContent>
            </Popover>
            <FormDescription>
              This is the category of the event.
            </FormDescription>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={form.control}
        name="venueId"
        render={({ field }) => (
          <FormItem className="flex flex-col">
            <FormLabel>Venue</FormLabel>
            <Popover>
              <PopoverTrigger asChild>
                <FormControl>
                  <Button
                    variant="outline"
                    role="combobox"
                    className={cn(
                      'w-[350px] justify-between',
                      !field.value && 'text-muted-foreground',
                    )}
                  >
                    {field.value
                      ? venues.find((venue) => venue.venueId === field.value)
                          ?.name
                      : 'Select Venue'}
                    <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                  </Button>
                </FormControl>
              </PopoverTrigger>
              <PopoverContent className="w-[350px] p-0">
                <Command>
                  <CommandInput
                    placeholder="Search venue..."
                    className="bg-accent"
                  />
                  <CommandList>
                    <CommandEmpty>No venue found.</CommandEmpty>
                    <CommandGroup>
                      {venues.map((venue) => (
                        <CommandItem
                          value={venue.name}
                          key={venue.venueId}
                          onSelect={() => {
                            form.setValue('venueId', venue.venueId)
                          }}
                        >
                          <div className="flex">
                            <CheckIcon
                              className={cn(
                                'mr-2 h-4 w-4',
                                venue.venueId === field.value
                                  ? 'opacity-100'
                                  : 'opacity-0',
                              )}
                            />
                            {venue.name}, {venue.shortAddress}
                          </div>
                        </CommandItem>
                      ))}
                    </CommandGroup>
                  </CommandList>
                </Command>
              </PopoverContent>
            </Popover>
            <FormDescription>This is the venue of the event.</FormDescription>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  )
}

export const createEventSchema = z
  .object({
    categoryId: z.string().uuid(),
    title: z.string().min(3).max(100),
    description: z.string().min(10).max(1000),
    venueId: z.string().uuid().optional(),
    startsAtUtc: z.instanceof(dayjs as unknown as typeof Dayjs),
    endsAtUtc: z.instanceof(dayjs as unknown as typeof Dayjs).optional(),
    //   speakersIds: z.array(z.string().uuid()),
  })
  .refine(
    (data) =>
      data.endsAtUtc === undefined || data.endsAtUtc.isAfter(data.startsAtUtc),
    {
      message: 'End date must be after start date',
      path: ['endsAtUtc'],
    },
  )
type EventFormValues = z.infer<typeof createEventSchema>

const defaultValues: Partial<EventFormValues> = {
  categoryId: undefined,
  title: '',
  description: '',
  startsAtUtc: undefined,
  endsAtUtc: undefined,
  venueId: undefined,
}

export const EventModal = () => {
  const [isModalOpen, setIsModalOpen] = useState(false)
  const { categoriesClient, organizersClient } = useClient()

  //   const initialValues: EventFormValues = {
  //     categoryId: '',
  //     title: 'Test',
  //     description: '',
  //     venueId: '',
  //     startsAtUtc: dayjs(),
  //     endsAtUtc: undefined,
  //     speakersIds: [],
  //   }

  const form = useForm<EventFormValues>({
    resolver: zodResolver(createEventSchema),
    mode: 'all',
    defaultValues,
  })

  const handleSubmit = (values: EventFormValues) => {
    console.log(values)
    ajax({
      request: () =>
        organizersClient.postApiOrganizerEvents(
          AuthenticationService.getOrganizerId() ?? '',
          CreateEventRequest.fromJS(values),
        ),
      setResult: (result: NetworkState<ResultOfGuid>) => {
        if (result.state === 'success') {
          setIsModalOpen(false)
        }
      },
      showErrorNotification: true,
      showSuccessNotification: true,
    })
  }

  const handleClose = () => {
    form.reset(defaultValues)
    setIsModalOpen(false)
  }

  return (
    <>
      <Button onClick={() => setIsModalOpen(true)}>Create Event</Button>
      <GenericModal<EventFormValues>
        isOpen={isModalOpen}
        onClose={handleClose}
        title="Create Event"
        FormComponent={EventForm}
        form={form}
        //   schema={createEventSchema}
        onSubmit={handleSubmit}
      />
    </>
  )
}
