import { useClient } from '../../services/RootClient'
import { CommonLogin } from './CommonLogin'

export const AdminLogin = () => {
  const { usersClient } = useClient()

  return (
    <CommonLogin
      getAuthTokensRequest={(x) => usersClient.getAuthTokensForOrganizer(x)}
      onSuccessfulLogin={() => console.log()}
    />
  )
}
