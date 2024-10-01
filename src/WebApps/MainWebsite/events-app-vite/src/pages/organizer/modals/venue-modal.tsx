import { Button } from '@/components/ui/button'
import { useEffect, useState } from 'react'
import { GenericModal } from '@/components/generic-form-modal'
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
import { useFieldArray, useForm, UseFormReturn } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import {
  AddressDto,
  CategoryDto,
  CreateVenueRequest,
  FileParameter,
  Result,
  ResultOfIReadOnlyCollectionOfCategoryDto,
  ResultOfStringOf,
} from '@/services/EventsClient'
import { useClient } from '@/services/RootClient'
import { ajax, NetworkState } from '@/services/ApiHelper'
import { Textarea } from '@/components/ui/textarea'
import { BlobServiceClient } from '@azure/storage-blob'
import { AuthenticationService } from '@/services/AuthService'

const VenueForm = ({ form }: { form: UseFormReturn<VenueFormValues> }) => {
  const [categories, setCategories] = useState<CategoryDto[]>([])
  const [imageBlobs, setImageBlobs] = useState<Blob[]>([])
  const { categoriesClient, venuesClient, filesClient } = useClient()
  const { fields, append, remove } = useFieldArray({
    control: form.control,
    name: 'imageUrls',
    keyName: 'imageId',
  })

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
      showSuccessNotification: true,
    })
  }, [])

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const uploadedFiles = e.target.files ? Array.from(e.target.files) : []
    console.log('uploadedFiles', uploadedFiles)
    append(uploadedFiles)
  }

  return (
    <div className="space-y-4">
      <FormField
        control={form.control}
        name="title"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Title</FormLabel>
            <FormControl>
              <Input placeholder="Your venue's title" {...field} />
            </FormControl>
            <FormDescription>This is the venue name</FormDescription>
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
      <div className="space-y-4">
        <h3 className="text-lg font-semibold">Address</h3>
        <FormField
          control={form.control}
          name="address.streetAndNumber"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Street and Number</FormLabel>
              <FormControl>
                <Input placeholder="123 Main St" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <FormField
            control={form.control}
            name="address.city"
            render={({ field }) => (
              <FormItem>
                <FormLabel>City</FormLabel>
                <FormControl>
                  <Input placeholder="New York" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="address.country"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Country</FormLabel>
                <FormControl>
                  <Input placeholder="United States" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>
        <FormField
          control={form.control}
          name="address.region"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Region (Optional)</FormLabel>
              <FormControl>
                <Input placeholder="New York" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="address.longitude"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Longitude (Optional)</FormLabel>
              <FormControl>
                <Input placeholder="-73.935242" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="address.latitude"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Latitude (Optional)</FormLabel>
              <FormControl>
                <Input placeholder="40.730610" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
      </div>
      <input
        type="file"
        multiple
        accept="image/*"
        onChange={handleFileChange}
      />
    </div>
  )
}

export const createVenueSchema = z.object({
  title: z.string().min(1).max(100),
  description: z.string().min(1).max(1000),
  address: z.object({
    streetAndNumber: z.string().min(3).max(100),
    city: z.string().min(2).max(100),
    country: z.string().min(2).max(100),
    region: z.string().min(2).max(100).optional(),
    longitude: z.string().optional(),
    latitude: z.string().optional(),
  }),
  imageUrls: z.array(z.any()).optional(),
})
type VenueFormValues = z.infer<typeof createVenueSchema>

const defaultValues: Partial<VenueFormValues> = {
  imageUrls: [],
}

interface VenueModalProps {}

export const VenueModal = () => {
  const [isModalOpen, setIsModalOpen] = useState(false)
  const { filesClient, venuesClient } = useClient()

  useEffect(() => {
    form.reset()
  }, [isModalOpen])

  const form = useForm<VenueFormValues>({
    resolver: zodResolver(createVenueSchema),
    mode: 'all',
    defaultValues,
    shouldUnregister: false,
  })

  form.register('imageUrls')

  const handleSubmit = async (values: VenueFormValues) => {
    console.log('In submit', values)
    let imageContainers: string[] = []
    await ajax({
      request: () => {
        return filesClient.uploadManyFiles(
          values.imageUrls!.map((imageFile) => ({
            data: imageFile,
            fileName: imageFile.name,
          })),
        )
      },
      setResult: (result: NetworkState<ResultOfStringOf>) => {
        if (result.state === 'success') {
          console.log(result.response.value)
          imageContainers = result.response.value!
        }
      },
      avoidExecution: !values.imageUrls,
      showSuccessNotification: true,
    })

    ajax({
      request: () => {
        return venuesClient.postApiVenues(
          CreateVenueRequest.fromJS({
            organizerId: AuthenticationService.getOrganizerId(),
            name: values.title,
            description: values.description,
            address: AddressDto.fromJS(values.address),
            imageContainers: imageContainers.map((c) => c.split('/')[0]),
          }),
        )
      },
      setResult: (result) => {
        if (result.state === 'success') {
          handleClose()
        }
      },
    })
  }

  const handleClose = () => {
    form.reset(defaultValues)
    setIsModalOpen(false)
  }

  return (
    <>
      <Button onClick={() => setIsModalOpen(true)}>Create Venue</Button>
      <GenericModal<VenueFormValues>
        isOpen={isModalOpen}
        onClose={handleClose}
        title="Create Venue"
        FormComponent={VenueForm}
        form={form}
        onSubmit={handleSubmit}
      />
    </>
  )
}
