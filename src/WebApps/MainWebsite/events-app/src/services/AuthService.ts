export class AuthenticationService {
  public static authenticate(
    authenticationResult: AuthenticationResult,
    login?: string,
  ): void {
    window.localStorage.setItem(
      LoginConsts.ACCESS_TOKEN_KEY,
      authenticationResult.access_token,
    )
    window.localStorage.setItem(
      LoginConsts.REFRESH_TOKEN_KEY,
      authenticationResult.refresh_token,
    )
    if (!!authenticationResult.organizer_id) {
      window.localStorage.setItem(
        LoginConsts.ORGANIZER_ID,
        authenticationResult.organizer_id,
      )
    }
    if (!!login) {
      window.localStorage.setItem(LoginConsts.USERNAME_KEY, login)
    }
  }

  public static isAuthenticated(): boolean {
    return window.localStorage.getItem(LoginConsts.ACCESS_TOKEN_KEY) != null
  }

  public static getUserId(): string | null {
    return window.localStorage.getItem(LoginConsts.USER_ID)
  }

  public static getOrganizerId(): string | null {
    return window.localStorage.getItem(LoginConsts.ORGANIZER_ID)
  }

  public static getUsername(): string | null {
    return window.localStorage.getItem(LoginConsts.USERNAME_KEY)
  }

  public static getAccessToken(): string | null {
    return window.localStorage.getItem(LoginConsts.ACCESS_TOKEN_KEY)
  }

  public static getRefreshToken(): string | null {
    return window.localStorage.getItem(LoginConsts.REFRESH_TOKEN_KEY)
  }

  public static getRefreshUrl(): string {
    return process.env.REACT_APP_REFRESH_URL!
  }

  public static logOff(): void {
    window.localStorage.removeItem(LoginConsts.ACCESS_TOKEN_KEY)
    window.localStorage.removeItem(LoginConsts.REFRESH_TOKEN_KEY)
    window.localStorage.removeItem(LoginConsts.USER_ID)
    window.localStorage.removeItem(LoginConsts.USERNAME_KEY)
    window.localStorage.removeItem(LoginConsts.ORGANIZER_ID)
  }

  public static setUserId(userId: string): void {
    window.localStorage.setItem(LoginConsts.USER_ID, userId)
  }
}

export interface AuthenticationResult {
  access_token: string
  refresh_token: string
  organizer_id: string
}

export class LoginConsts {
  public static readonly ACCESS_TOKEN_KEY: string = 'access_token'
  public static readonly REFRESH_TOKEN_KEY: string = 'refresh_token'
  public static readonly USERNAME_KEY: string = 'username'
  public static readonly USER_ID: string = 'userid'
  public static readonly ORGANIZER_ID: string = 'organizerid'
}
