import { DependencyList, useEffect, useState } from "react";
import { Result } from "./EventsClient";


export interface AjaxArgs<T> {
   request: () => Promise<T | null>;
   setResult?: (v: NetworkState<T>) => void;
   showErrorNotification?: boolean;
   showSuccessNotification?: boolean;
   successMessage?: string;
   avoidExecution?: boolean;
 }

 export async function ajax<T extends Result>({
   request,
   setResult,
   showErrorNotification = true,
   showSuccessNotification = true,
   successMessage,
 }: AjaxArgs<T>): Promise<NetworkState<T>> {
   setResult?.(NetworkLoading<T>());
   try {
     const response: T | null = await request();
     let result: NetworkState<T>;
     if (response == null) result = NetworkNone<T>();
     else {
       result = NetworkSuccess(response);
     }
 
     setResult?.(result);
     return result;
   } catch (err) {
     return Promise.reject(err);
   }
 }

 export async function ajaxGet<T extends Result>({
   showErrorNotification = true,
   showSuccessNotification = false,
   ...rest
 }: AjaxArgs<T>): Promise<NetworkState<T>> {
   return ajax({ ...rest, showErrorNotification, showSuccessNotification });
 }

 export function useAjax<T extends Result>(args: AjaxArgs<T>, dependency?: DependencyList): NetworkState<T> {
   const [result, setResult] = useState<NetworkState<T>>(NetworkNone());
   useEffect(() => {
     if (!args.avoidExecution) {
       ajaxGet({ ...args,
         setResult(v) {
           args.setResult?.(v);
           setResult(v);
         },
       });
     }
   }, dependency ?? []);
 
   return result;
 }



 export type NetworkNoneState = {
   state: 'none';
 };
 
 export type NetworkLoadingState = {
   state: 'loading';
 };
 
 export type NetworkFailedState = {
   state: 'failed';
   code: number;
   message?: string;
   response?: string;
 };
 
 export type NetworkSuccessState<T> = {
   state: 'success';
   response: T;
 };
 
 export type NetworkState<T> =
   | NetworkNoneState
   | NetworkLoadingState
   | NetworkFailedState
   | NetworkSuccessState<T>;
 
 export function NetworkNone<T>(): NetworkState<T> {
   return { state: 'none' };
 }
 
 export function NetworkSuccess<T>(response: T): NetworkState<T> {
   return { state: 'success', response };
 }
 
 export function NetworkFailed<T>(code: number, message?: string, response?: string): NetworkState<T> {
   return { state: 'failed', code, message, response };
 }
 
 export function NetworkLoading<T>(): NetworkState<T> {
   return { state: 'loading' };
 }
 
 export function getResponse<T>(state: NetworkState<T>) : T | undefined {
   switch (state.state) {
     case 'success':
       return state.response;
     default:
       return undefined;
   }
 }
 
 export function responseAvaliable<T>(state: NetworkState<T>) : boolean {
   switch (state.state) {
     case 'success':
       return true;
     default:
       return false;
   }
 }
 
 export function combineNetworkFailed<T>(states: NetworkState<T>[]): NetworkState<T> {
   const message = states.reduce((prev, curr) => (curr.state === 'failed' ? `${prev} ${curr}}` : prev), '');
   return { state: 'failed', code: 500, message };
 }
 
 