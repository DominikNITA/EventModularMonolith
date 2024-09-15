import React, { useState } from 'react'
import {
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Button } from '@/components/ui/button'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import {
  ArrowDownIcon,
  ArrowDownUpIcon,
  ArrowUpIcon,
  MoreHorizontal,
} from 'lucide-react'
import { CaretSortIcon } from '@radix-ui/react-icons'

export interface Column<T> {
  header: string
  accessor: keyof T | ((item: T) => React.ReactNode)
  className?: string
  sortable?: boolean
  filterable?: boolean | 'enum'
  filterOptions?: string[]
}

interface GenericTableProps<T> {
  data: T[]
  columns: Column<T>[]
  idField: keyof T
  actionsClassname?: string
  deleteOptions?: DeleteOptions<T>
  modifyOptions?: ModifyOptions<T>
  viewDetailsOptions?: ViewDetailsOptions<T>
  createOptions?: CreateOptions<T>
  customActions?: CustomActionsOptions<T>[]
}

export interface DeleteOptions<T> {
  onDelete?: (id: T[keyof T]) => void
  showDeleteButton: boolean | ((item: T) => boolean)
  deleteButtonText?: string
}

export interface ModifyOptions<T> {
  onModify?: (item: T) => void
  showModifyButton: boolean | ((item: T) => boolean)
  modifyButtonText?: string
  ModifyDialog?: React.ComponentType<{ item: T | null }>
}

export interface ViewDetailsOptions<T> {
  onViewDetails?: (item: T) => void
  showViewDetailsButton: boolean | ((item: T) => boolean)
  viewDetailsButtonText?: string
}

export interface CreateOptions<T> {
  onCreate?: (item: T) => void
  showCreateButton: boolean | (() => boolean)
  createButtonText?: string
  buttonPosition: 'top' | 'bottom' | 'both'
  CreateDialog?: React.ComponentType<{ item: T | null }>
}

export interface CustomActionsOptions<T> {
  onAction?: (item: T) => void
  showActionButton: boolean | (() => boolean)
  actionButtonText?: string | (() => string)
}

export function GenericTable<T>({
  data,
  columns,
  idField,
  actionsClassname,
  deleteOptions,
  modifyOptions,
  viewDetailsOptions,
  createOptions,
}: GenericTableProps<T>) {
  const [itemToModify, setItemToModify] = useState<T | null>(null)
  const [itemToDelete, setItemToDelete] = useState<T | null>(null)

  const handleModify = (item: T) => {
    setItemToModify(item)
    modifyOptions!.onModify!(item)
  }

  const closeModifyDialog = () => {
    setItemToModify(null)
  }

  const [popupState, setPopupState] = useState<{
    isOpen: boolean
    entity: T | null
  }>({
    isOpen: false,
    entity: null,
  })

  const openPopup = (entity: T) => {
    setPopupState({ isOpen: true, entity })
  }

  const closePopup = () => {
    setPopupState({ isOpen: false, entity: null })
  }

  const shouldShowViewDetailsButton = (item: T) =>
    typeof viewDetailsOptions?.showViewDetailsButton === 'function'
      ? viewDetailsOptions?.showViewDetailsButton(item)
      : (viewDetailsOptions?.showViewDetailsButton ?? true)

  const shouldShowModifyButton = (item: T) =>
    typeof modifyOptions?.showModifyButton === 'function'
      ? modifyOptions?.showModifyButton(item)
      : (modifyOptions?.showModifyButton ?? true)

  const shouldShowDeleteButton = (item: T) =>
    typeof deleteOptions?.showDeleteButton === 'function'
      ? deleteOptions.showDeleteButton(item)
      : (deleteOptions?.showDeleteButton ?? true)

  const [sortConfig, setSortConfig] = useState<{
    key: keyof T | ((item: T) => string)
    direction: 'asc' | 'desc'
  } | null>(null)
  const [filters, setFilters] = useState<Partial<Record<keyof T, string>>>({})

  const sortedAndFilteredData = data
    .filter((item) =>
      Object.entries(filters).every(([key, value]) =>
        String(item[key as keyof T])
          .toLowerCase()
          .includes((value as string).toLowerCase()),
      ),
    )
    .sort((a, b) => {
      if (sortConfig) {
        const aValue =
          typeof sortConfig.key === 'function'
            ? sortConfig.key(a)
            : a[sortConfig.key]
        const bValue =
          typeof sortConfig.key === 'function'
            ? sortConfig.key(b)
            : b[sortConfig.key]

        if (aValue < bValue) return sortConfig.direction === 'asc' ? -1 : 1
        if (aValue > bValue) return sortConfig.direction === 'asc' ? 1 : -1
        return 0
      }
      return 0
    })

  return (
    <Table>
      <TableHeader>
        <TableRow>
          {columns.map((column) => (
            <TableHead
              key={String(column.accessor)}
              className={column.className}
            >
              {column.header}
              {column.sortable && (
                <Button
                  variant="outline"
                  className="ml-3 p-1"
                  onClick={() =>
                    setSortConfig((current) => {
                      if (
                        current?.key === column.accessor &&
                        current.direction === 'desc'
                      ) {
                        return null
                      }
                      return {
                        key: column.accessor as keyof T,
                        direction:
                          current?.key === column.accessor &&
                          current.direction === 'asc'
                            ? 'desc'
                            : 'asc',
                      }
                    })
                  }
                >
                  {sortConfig?.key === column.accessor ? (
                    sortConfig.direction === 'asc' ? (
                      <ArrowUpIcon
                        className="h-[1.2rem] w-[1.2rem"
                        color="#fcba03"
                      />
                    ) : (
                      <ArrowDownIcon
                        className="h-[1.2rem] w-[1.2rem]"
                        color="#fcba03"
                      />
                    )
                  ) : (
                    <ArrowDownUpIcon className="h-[1.2rem] w-[1.2rem]" />
                  )}
                </Button>
              )}
              {column.filterable &&
                (column.filterable === 'enum' ? (
                  <select
                    onChange={(e) =>
                      setFilters({
                        ...filters,
                        [String(column.accessor)]: e.target.value,
                      })
                    }
                  >
                    <option value="">All</option>
                    {column.filterOptions?.map((option) => (
                      <option key={option} value={option}>
                        {option}
                      </option>
                    ))}
                  </select>
                ) : (
                  <input
                    type="text"
                    placeholder="Filter"
                    onChange={(e) =>
                      setFilters({
                        ...filters,
                        [String(column.accessor)]: e.target.value,
                      })
                    }
                  />
                ))}
            </TableHead>
          ))}
          <TableHead className={actionsClassname}>Actions</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {sortedAndFilteredData.map((item) => (
          <TableRow key={String(item[idField])}>
            {columns.map((column, index) => (
              <TableCell key={index} className={column.className}>
                {typeof column.accessor === 'function'
                  ? column.accessor(item)
                  : String(item[column.accessor])}
              </TableCell>
            ))}
            <TableCell className={actionsClassname}>
              <div className="space-x-2">
                {shouldShowViewDetailsButton(item) && (
                  <Button variant="outline" size="sm">
                    {viewDetailsOptions?.viewDetailsButtonText ||
                      'View Details'}
                  </Button>
                )}
                {shouldShowModifyButton(item) && (
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => handleModify(item)}
                  >
                    {modifyOptions?.modifyButtonText || 'Modify'}
                  </Button>
                )}
                {shouldShowDeleteButton(item) && (
                  <Button
                    variant="destructive"
                    size="sm"
                    onClick={() => openPopup(item)}
                  >
                    {deleteOptions?.deleteButtonText || 'Delete'}
                  </Button>
                )}
              </div>
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
