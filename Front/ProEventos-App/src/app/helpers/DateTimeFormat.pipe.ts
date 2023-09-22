import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constantes } from '../util/constantes';
import * as moment from 'moment';

@Pipe({
  name: 'DateTimeFormat'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    const dateMoment: moment.Moment = moment(value, 'DD/MM/YYYY hh:mm');
    const dateJS: Date = dateMoment.toDate();
    return super.transform(dateJS, Constantes.DATE_TIME_FMT);
  }

}
