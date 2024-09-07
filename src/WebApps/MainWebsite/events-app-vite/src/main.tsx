import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { DashboardLayout } from './components/dashboard-layout.tsx'
import { BrowserRouter } from 'react-router-dom'
import { ThemeProvider } from './components/theme-provider.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <ThemeProvider defaultTheme="light" storageKey="vite-ui-theme">
        <DashboardLayout />
      </ThemeProvider>   
    </BrowserRouter>
  </StrictMode>,
)
