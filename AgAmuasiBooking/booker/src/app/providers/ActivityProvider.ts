import { Injectable, signal } from '@angular/core';

interface IStates { isProcessing: boolean, message?: string }

@Injectable({ providedIn: 'root' })
export class ActivityProvider {
  activity = signal<IStates>({ isProcessing: false, message: '' })

  beginProc() {
    this.activity.update(() => {
      return {
        isProcessing: true
      }
    })
  }


  endProc() {
    this.activity.update(() => {
      return {
        isProcessing: false
      }
    })
  }
}
