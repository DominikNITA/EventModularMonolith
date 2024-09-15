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
  //   schema: z.ZodType<T>
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
  //   schema,
  onSubmit,
  cancelButtonText = 'Cancel',
  saveButtonText = 'Save',
  form,
}: GenericModalProps<T>) {
  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
            <FormComponent form={form} />
            <DialogFooter>
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
