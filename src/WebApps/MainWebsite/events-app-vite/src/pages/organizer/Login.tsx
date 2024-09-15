import { useClient } from '@/services/RootClient'
import CommonLogin from '../common/login/common-login'

export const OrganizerLogin = () => {
  const { usersClient } = useClient()

  return (
    <CommonLogin
      getAuthTokensRequest={(x) => usersClient.getAuthTokensForOrganizer(x)}
      onSuccessfulLogin={() => console.log()}
    />
  )
}
