/* eslint-disable class-methods-use-this */

import dayjs, { Dayjs } from 'dayjs'
import { ErrorDto, IResult, Result } from './EventsClient'

export const baseAppUrl = 'https://localhost:5001'
// (
//    document.getElementsByTagName('base')[0] || {}
//  ).href?.slice(0, -1);

interface ISessionStore {
  updateLastRequest(date: Dayjs): void
}

export class ClientBase {
  public archive = false

  protected transformOptions(options: any) {
    const transformedOptions = { ...options } // transformedOptions.headers!.Pragma = 'no-cache';
    // transformedOptions.headers!['Cache-Control'] = 'no-cache';
    // const um = GlobalUserManager.UserManager();
    // if (um) {
    //   const promise = um.getUser().then((user: User | null) => {
    //     if (user) {
    //       const token = user.access_token;
    //         transformedOptions.headers!.Authorization = [`Bearer ${token}`];
    //     }
    //     return Promise.resolve(transformedOptions);
    //   });
    //   return promise;
    // }

    // transformedOptions.headers['Access-Control-Allow-Origin'] = '*';

    return Promise.resolve(options)
  }

  protected getBaseUrl(arg1: string, base?: string) {
    return baseAppUrl
  }

  protected async transformResult(
    url: string,
    response: Response,
    processor: (response: Response) => any,
  ) {
    const status = response.status
    let _headers: any = {}
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v))
    }
    if (status === 200) {
      var res = await response.json()
      console.log(res)
      var result: IResult & { value: any } = {
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
