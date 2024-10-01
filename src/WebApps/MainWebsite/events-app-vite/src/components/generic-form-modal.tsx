import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from '@/components/ui/dialog'
import { Button } from '@/components/ui/button'
import { FieldValues, useForm, UseFormReturn } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { z } from 'zod'
import { Form } from './ui/form'

interface GenericModalProps<T extends FieldValues> {
  isOpen: boolean
  onClose: () => void
  title: string
  FormComponent: React.ComponentType<{ form: UseFormReturn<T> }>
  onSubmit: (values: T) => void
  cancelButtonText?: string
  saveButtonText?: string
  form: UseFormReturn<T>
}

export function GenericModal<T extends FieldValues>({
  isOpen,
  onClose,
  title,
  FormComponent,
  onSubmit,
  cancelButtonText = 'Cancel',
  saveButtonText = 'Save',
  form,
}: GenericModalProps<T>) {
  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="max-h-[80vh] flex flex-col">
        <DialogHeader className="flex-shrink-0">
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="flex flex-col flex-grow overflow-hidden"
          >
            <div className="flex-grow overflow-y-auto pr-4 -mr-4">
              <FormComponent form={form} />
            </div>
            <DialogFooter className="flex-shrink-0 mt-4">
              <Button type="button" variant="outline" onClick={onClose}>
                {cancelButtonText}
              </Button>
              <Button type="submit">{saveButtonText}</Button>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}
