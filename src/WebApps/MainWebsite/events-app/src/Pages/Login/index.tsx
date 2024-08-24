import React, { ChangeEvent, FormEvent, useState } from 'react'
import {
  AuthenticationResult,
  AuthenticationService,
} from '../../services/AuthService'
import { useClient } from '../../services/RootClient'
import { GetAuthTokensRequest } from '../../services/EventsClient'

export const Login = () => {
  const [login, setLogin] = useState('')
  const [password, setPassword] = useState('')
  const [errorMessage, setErrorMessage] = useState<string | null>(null)

  const { usersClient } = useClient()

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    // var formdata = new URLSearchParams({
    //   client_id: process.env.REACT_APP_AUTH_CLIENT_ID!,
    //   grant_type: 'password',
    //   scope: 'email openid',
    //   username: login,
    //   password: password,
    // })

    // let headers: Record<string, string> = {}
    // headers['Content-Type'] = 'application/x-www-form-urlencoded'
    // headers['Accept'] = 'application/json'

    // var requestOptions: RequestInit = {
    //   method: 'POST',
    //   body: formdata,
    //   redirect: 'follow',
    //   headers: headers,
    //   mode: 'no-cors',
    // }
    // console.log(process.env.REACT_APP_AUTH_BASE_URL)
    // console.log(process.env.REACT_APP_GOOGLE_MAPS_API)
    // console.log(process.env.REACT_APP_AUTH_CLIENT_ID)

    usersClient
      .getAuthTokens(
        new GetAuthTokensRequest({ email: login, password: password }),
      )
      .then((x) => {
        if (x.isFailure) {
          throw x.error
        }
        {
          AuthenticationService.authenticate(x.value!, login)
        }
      })
      .catch(() => {
        setErrorMessage('Invalid username or password')
        setPassword('')
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
    <div>
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
