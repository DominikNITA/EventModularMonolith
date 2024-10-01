'use client'

import { ThemeToggle } from '@/components/theme-toggle'
import {
  Users,
  Home,
  Settings,
  BarChart,
  FileText,
  Building2,
} from 'lucide-react'
import { Link, Outlet } from 'react-router-dom'

export function OrganizerLayout() {
  return (
    <div className="flex h-screen">
      {/* Sidebar */}
      <aside className="w-64 dark:bg-gray-400 bg-gray-200 shadow-md border-gray-300 border">
        <nav className="mt-5">
          <Link
            to="dashboard"
            className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200"
          >
            <Home className="mr-3 h-5 w-5" />
            Dashboard
          </Link>
          <Link
            to="moderators"
            className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200"
          >
            <Users className="mr-3 h-5 w-5" />
            Moderators
          </Link>
          <Link
            to="events"
            className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200"
          >
            <BarChart className="mr-3 h-5 w-5" />
            Events
          </Link>
          <Link
            to="venues"
            className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200"
          >
            <Building2 className="mr-3 h-5 w-5" />
            Venues
          </Link>
          <Link
            to="settings"
            className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200"
          >
            <Settings className="mr-3 h-5 w-5" />
            Settings
          </Link>
          <ThemeToggle />
        </nav>
      </aside>

      {/* Main Content */}
      <main className="flex-1 p-8">
        <Outlet />
      </main>
    </div>
  )
}
