import { useClient } from '../../services/RootClient'
import { CommonLogin } from './CommonLogin'

export const GeneralLogin = () => {
  const { usersClient } = useClient()

  return (
    <CommonLogin
      getAuthTokensRequest={(x) => usersClient.getAuthTokens(x)}
      onSuccessfulLogin={() => console.log()}
    />
  )
}
