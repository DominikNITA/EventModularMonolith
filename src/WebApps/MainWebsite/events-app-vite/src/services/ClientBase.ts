/* eslint-disable class-methods-use-this */
import { ErrorDto, IResult } from './EventsClient'
import { AxiosResponse } from 'axios'

export const baseAppUrl = 'https://localhost:5001'

export class ClientBase {
  public archive = false

  protected transformOptions(options: any) {
    return Promise.resolve(options)
  }

  protected getBaseUrl(arg1: string, base?: string) {
    return baseAppUrl
  }

  protected async transformResult(
    url: string,
    response: AxiosResponse,
    processor: (response: AxiosResponse) => any,
  ) {
    const status = response.status
    let _headers: any = {}
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v))
    }
    if (status === 200) {
      var res = await response.data
      const result: IResult & { value: any } = {
        isSuccess: true,
        isFailure: false,
        error: {} as ErrorDto,
        value: res,
      }
      return result
    } else if (status !== 200 && status !== 204) {
      return processor(response)
    }
  }
}
