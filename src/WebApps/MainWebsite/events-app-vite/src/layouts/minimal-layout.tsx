'use client'

import { Users, Home, Settings, BarChart, FileText } from 'lucide-react'
import { Link, Outlet } from 'react-router-dom'
import './minimal-layout.css'

export function MinimalLayout() {
  return (
    <div className="minimalRoot">
      <Outlet />
    </div>
  )
}
