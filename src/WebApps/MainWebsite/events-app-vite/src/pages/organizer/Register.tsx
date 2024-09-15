import { ThemeToggle } from '@/components/theme-toggle'
import { ajax, NetworkState } from '@/services/ApiHelper'
import { AuthenticationService } from '@/services/AuthService'
import {
  CreateOrganizerRequest,
  RegisterUserCommand,
  ResultOfGuid,
} from '@/services/EventsClient'
import { useClient } from '@/services/RootClient'
import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'

export default function OrganizerRegister() {
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')

  const { organizersClient } = useClient()
  const navigate = useNavigate()

  const handleSubmit = (e: React.FormEvent) => {
    ajax({
      request: () =>
        organizersClient.createOrganizer(
          CreateOrganizerRequest.fromJS({
            name: name,
            description: 'Test',
            registerUserCommand: RegisterUserCommand.fromJS({
              firstName: 'test',
              lastName: 'test',
              email: email,
              password: password,
            }),
          }),
        ),
      setResult: (result: NetworkState<ResultOfGuid>) => {
        console.log(result)
        if (result.state === 'success') {
          navigate('/organizer/confirm-registration')
        } else if (result.state === 'failed') {
          //  setErrorMessage('Invalid username or password')
          setPassword('')
        }
      },
      showSuccessNotification: true,
      successMessage: 'Successfully created organizer',
    })
    // Here you would typically handle the registration logic
    console.log('Registration attempt', {
      name,
      email,
      password,
      confirmPassword,
    })
    e.preventDefault()
  }

  return (
    <div className="w-full max-w-md">
      <form
        onSubmit={handleSubmit}
        className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4"
      >
        <h2 className="text-2xl font-bold mb-6 text-center">Register</h2>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="name"
          >
            Organization name
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="name"
            type="text"
            placeholder="Full Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="email"
          >
            Email
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
            id="email"
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="mb-4">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="password"
          >
            Password
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
            id="password"
            type="password"
            placeholder="******************"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="mb-6">
          <label
            className="block text-gray-700 text-sm font-bold mb-2"
            htmlFor="confirm-password"
          >
            Confirm Password
          </label>
          <input
            className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
            id="confirm-password"
            type="password"
            placeholder="******************"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            required
          />
        </div>
        <div className="flex items-center justify-between">
          <button
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
            type="submit"
          >
            Register
          </button>
          <ThemeToggle />
          <Link
            to="/auth/organizer/login"
            className="inline-block align-baseline font-bold text-sm text-blue-500 hover:text-blue-800"
          >
            Already have an account?
          </Link>
        </div>
      </form>
    </div>
  )
}
