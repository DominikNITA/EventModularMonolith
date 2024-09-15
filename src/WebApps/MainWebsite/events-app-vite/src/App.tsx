import './App.css'
import 'react-notifications-component/dist/theme.css'
import { ThemeProvider } from './components/theme-provider'
import { ClientContext, createRootClient } from './services/RootClient'
import router from './routes'
import { RouterProvider } from 'react-router-dom'
import { Toaster } from 'sonner'

function App() {
  const rootClient = createRootClient()

  return (
    <>
      <ClientContext.Provider value={rootClient}>
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
          <RouterProvider router={router} />
        </ThemeProvider>
      </ClientContext.Provider>
      <Toaster position="bottom-right" richColors />
    </>
  )
}

export default App
