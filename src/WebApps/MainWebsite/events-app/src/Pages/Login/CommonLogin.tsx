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
import { Row, Col, Form, Container, Button } from 'react-bootstrap'
import { redirect, useNavigate } from 'react-router-dom'

interface IProps {
  getAuthTokensRequest: (
    request: GetAuthTokensRequest,
  ) => Promise<ResultOfAuthTokenWithRefresh>
  onSuccessfulLogin: () => void
}

export const CommonLogin = (props: IProps) => {
  const [login, setLogin] = useState('')
  const [password, setPassword] = useState('')
  const [errorMessage, setErrorMessage] = useState<string | null>(null)
  const navigate = useNavigate()
  const { usersClient } = useClient()

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    ajax({
      request: () =>
        props.getAuthTokensRequest(
          new GetAuthTokensRequest({ email: login, password: password }),
        ),
      setResult: (result: NetworkState<ResultOfAuthTokenWithRefresh>) => {
        console.log(result)
        if (result.state === 'success') {
          AuthenticationService.authenticate(result.response.value!, login)
          const redirectToOrganizerDashboard =
            !!AuthenticationService.getOrganizerId()
          navigate(redirectToOrganizerDashboard ? '/organizer/dashboard' : '/')
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
    <Col lg={2}>
      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="email"
            placeholder="name@example.com"
            value={login}
            onChange={handleLoginChange}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Password</Form.Label>
          <Col>
            <Form.Control
              type="password"
              autoComplete="password"
              placeholder="Password"
              value={password}
              onChange={handlePasswordChange}
            />
          </Col>
        </Form.Group>
        {errorMessage !== null && <div>{errorMessage}</div>}

        <Button variant="primary" type="submit">
          Submit
        </Button>
      </Form>
    </Col>
  )
}
