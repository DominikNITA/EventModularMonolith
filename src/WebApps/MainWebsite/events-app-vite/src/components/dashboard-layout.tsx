'use client'

import { Users, Home, Settings, BarChart, FileText } from 'lucide-react'

import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Link } from 'react-router-dom'

export function DashboardLayout() {
  const users = [
    { id: 1, name: 'John Doe', email: 'john@example.com' },
    { id: 2, name: 'Jane Smith', email: 'jane@example.com' },
    { id: 3, name: 'Bob Johnson', email: 'bob@example.com' },
    { id: 4, name: 'Alice Brown', email: 'alice@example.com' },
    { id: 5, name: 'Charlie Davis', email: 'charlie@example.com' },
  ]

  return (
    <div className="flex h-screen bg-gray-100">
      {/* Sidebar */}
      <aside className="w-64 bg-white shadow-md">
        <nav className="mt-5">
          <Link to="/dashboard" className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200">
            <Home className="mr-3 h-5 w-5" />
            Dashboard
          </Link>
          <Link to="/users" className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200">
            <Users className="mr-3 h-5 w-5" />
            Users
          </Link>
          <Link to="/reports" className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200">
            <BarChart className="mr-3 h-5 w-5" />
            Reports
          </Link>
          <Link to="/documents" className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200">
            <FileText className="mr-3 h-5 w-5" />
            Documents
          </Link>
          <Link to="/settings" className="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-200">
            <Settings className="mr-3 h-5 w-5" />
            Settings
          </Link>
        </nav>
      </aside>

      {/* Main Content */}
      <main className="flex-1 p-8">
        <Card>
          <CardHeader>
            <CardTitle>User List</CardTitle>
            <CardDescription>A list of all registered users.</CardDescription>
          </CardHeader>
          <CardContent>
            <div className="divide-y divide-gray-200">
              {users.map((user) => (
                <div key={user.id} className="py-4 flex justify-between items-center">
                  <div>
                    <p className="text-sm font-medium text-gray-900">{user.name}</p>
                    <p className="text-sm text-gray-500">{user.email}</p>
                  </div>
                  <Button variant="outline" size="sm">View Profile</Button>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </main>
    </div>
  )
}