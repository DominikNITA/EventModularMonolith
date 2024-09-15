import { ThemeToggle } from '@/components/theme-toggle'
import { ajax, NetworkState } from '@/services/ApiHelper'
import { AuthenticationService } from '@/services/AuthService'
import {
  GetAuthTokensRequest,
  ResultOfAuthTokenWithRefresh,
} from '@/services/EventsClient'
import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'

interface IProps {
  getAuthTokensRequest: (
    request: GetAuthTokensRequest,
  ) => Promise<ResultOfAuthTokenWithRefresh>
  onSuccessfulLogin: () => void
}

export default function Login(props: IProps) {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [errorMessage, setErrorMessage] = useState<string | null>(null)
  const navigate = useNavigate()

  const handleSubmit = (e: React.FormEvent) => {
    ajax({
      request: () =>
        props.getAuthTokensRequest(
          new GetAuthTokensRequest({ email: email, password: password }),
        ),
      setResult: (result: NetworkState<ResultOfAuthTokenWithRefresh>) => {
        console.log(result)
        if (result.state === 'success') {
          AuthenticationService.authenticate(result.response.value!, email)
          const redirectToOrganizerDashboard =
            !!AuthenticationService.getOrganizerId()
          navigate(redirectToOrganizerDashboard ? '/organizer/dashboard' : '/')
        } else if (result.state === 'failed') {
          setErrorMessage('Invalid username or password')
          setPassword('')
        }
      },
      showSuccessNotification: true,
      showErrorNotification: true,
      successMessage: 'Successfully logged in',
    })
    e.preventDefault()
  }

  return (
    <div className="w-full max-w-md">
      <form
        onSubmit={handleSubmit}
        className="bg-gray-200 shadow-md rounded px-8 pt-6 pb-8 mb-4"
      >
        <h2 className="text-2xl font-bold mb-6 text-center dark:text-gray-800">
          Login
        </h2>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="email"
          >
            Email
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 bg-white text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="email"
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="mb-6">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="password"
          >
            Password
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 bg-white text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
            id="password"
            type="password"
            placeholder="**************"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="flex items-center justify-between">
          <button
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            type="submit"
          >
            Sign In
          </button>
          <ThemeToggle />
          <Link
            to="/organizer/register"
            className="inline-block align-baseline font-bold text-sm text-blue-500 hover:text-blue-800"
          >
            Need an account?
          </Link>
        </div>
      </form>
    </div>
  )
}
