import { AuthenticationService } from '@/services/AuthService'
import React from 'react'
import { Navigate, useLocation } from 'react-router-dom'

interface ProtectedRouteProps {
  children: React.ReactNode
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  const isAuthenticated = AuthenticationService.isAuthenticated()
  const location = useLocation()

  if (!isAuthenticated) {
    return (
      <Navigate to="/auth/organizer/login" state={{ from: location }} replace />
    )
  }

  return <>{children}</>
}

export default ProtectedRoute
