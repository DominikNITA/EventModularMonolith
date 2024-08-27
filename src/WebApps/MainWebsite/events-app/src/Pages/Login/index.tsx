import React, { ChangeEvent, FormEvent, useState } from 'react'
import {
  AuthenticationResult,
  AuthenticationService,
} from '../../services/AuthService'
import { useClient } from '../../services/RootClient'
import {
  GetAuthTokensRequest,
  ResultOfAuthTokenWithRefresh,
} from '../../services/EventsClient'
import { ajax, NetworkState } from '../../services/ApiHelper'

export const Login = () => {
  const [login, setLogin] = useState('')
  const [password, setPassword] = useState('')
  const [errorMessage, setErrorMessage] = useState<string | null>(null)

  const { usersClient } = useClient()

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    ajax({
      request: () =>
        usersClient.getAuthTokens(
          new GetAuthTokensRequest({ email: login, password: password }),
        ),
      setResult: (result: NetworkState<ResultOfAuthTokenWithRefresh>) => {
        console.log(result)
        if (result.state === 'success') {
          AuthenticationService.authenticate(result.response.value!, login)
        } else if (result.state === 'failed') {
          setErrorMessage('Invalid username or password')
          setPassword('')
        }
      },
      showSuccessNotification: true,
      successMessage: 'Successfully logged in',
    })
    event.preventDefault()
  }

  function handleLoginChange(event: ChangeEvent<HTMLInputElement>) {
    setLogin(event.target.value)
  }

  function handlePasswordChange(event: ChangeEvent<HTMLInputElement>) {
    setPassword(event.target.value)
  }

  return (
    <div style={{ marginTop: '150px', marginLeft: '150px' }}>
      {errorMessage !== null && <div>{errorMessage}</div>}

      <form onSubmit={handleSubmit}>
        <div>
          <div>Login: </div>
          <div>
            <input type="text" value={login} onChange={handleLoginChange} />
          </div>
          <div>Password: </div>
          <div>
            <input
              type="password"
              autoComplete="password"
              value={password}
              onChange={handlePasswordChange}
            />
          </div>
          <div>
            <input type="submit" value="Log in" />
          </div>
        </div>
      </form>
    </div>
  )
}
