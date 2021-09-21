import { Observable } from "rxjs";
import { debounceTime, skip, switchMap, takeUntil } from "rxjs/operators";

export const autocomplete = (time : number, selector : any) => (source$ : any) =>
  source$.pipe(
    debounceTime(time),
    switchMap((...args: any[]) => 
      selector(...args)
        .pipe(
            takeUntil(
                source$
                    .pipe(
                        skip(1)
                    )
            )
        )
    )
  )