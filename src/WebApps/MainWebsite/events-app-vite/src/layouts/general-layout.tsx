import { ThemeToggle } from '@/components/theme-toggle'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { AuthenticationService } from '@/services/AuthService'
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuContent,
  DropdownMenuItem,
} from '@radix-ui/react-dropdown-menu'
import { User } from 'lucide-react'
import { Link, Outlet } from 'react-router-dom'

export function GeneralLayout() {
  return (
    <div className="min-h-screen bg-background text-foreground">
      <header className="sticky top-0 z-50 flex w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60 justify-center">
        <div className="container flex h-14 mx-4 items-center justify-center">
          <div className="mr-4 hidden md:flex">
            <Link to="/" className="mr-6 flex items-center space-x-2">
              <span className="hidden font-bold sm:inline-block">
                EventsHub
              </span>
            </Link>
            <nav className="flex items-center space-x-6 text-sm font-medium">
              <Link
                to="/about"
                className="transition-colors hover:text-foreground/80 text-foreground/60"
              >
                About
              </Link>
              <Link
                to="/events"
                className="transition-colors hover:text-foreground/80 text-foreground"
              >
                Events
              </Link>
              <Link
                to="/contact"
                className="transition-colors hover:text-foreground/80 text-foreground/60"
              >
                Contact
              </Link>
            </nav>
          </div>
          <div className="flex flex-1 items-center justify-between space-x-2 md:justify-end">
            {/* <div className="w-full flex-1 md:w-auto md:flex-none">
              <Input
                type="search"
                placeholder="Search events..."
                className="h-9 md:w-[300px] lg:w-[300px]"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
              />
            </div> */}
            <ThemeToggle />
            {AuthenticationService.isAuthenticated() ? (
              <DropdownMenu>
                <DropdownMenuTrigger asChild>
                  <Button variant="outline" size="icon" className="h-9 w-9">
                    <User className="absolute h-4 w-4" />
                    <span className="sr-only">User menu</span>
                  </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="end">
                  <>
                    <DropdownMenuItem>
                      <Link to="/profile">Profile</Link>
                    </DropdownMenuItem>
                    <DropdownMenuItem>
                      <Link to="/settings">Settings</Link>
                    </DropdownMenuItem>
                    <DropdownMenuItem
                      onClick={() => AuthenticationService.logOff()}
                    >
                      Log out
                    </DropdownMenuItem>
                  </>
                </DropdownMenuContent>
              </DropdownMenu>
            ) : (
              <nav className="flex items-center space-x-6 text-sm font-medium">
                <Link
                  to="auth/organizer/login"
                  className="transition-colors hover:text-foreground/80 text-foreground"
                >
                  Sign-in
                </Link>
                <Link
                  to="auth/organizer/register"
                  className="transition-colors hover:text-chart-1 text-chart-1/70"
                >
                  Register
                </Link>
              </nav>
            )}
          </div>
        </div>
      </header>

      <div className="flex justify-center w-full bg-secondary min-h-screen">
        <Outlet />
      </div>
    </div>
  )
}
