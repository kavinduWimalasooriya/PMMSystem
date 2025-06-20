import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncateString'
})
export class TruncateStringPipe implements PipeTransform {

  transform(value: string | null, ...args: unknown[]): string {
    if (value === null || value === undefined) {
      return '';
    };
    let truncated = value.substring(0, 100);
    return truncated + " ...";
  }

}
