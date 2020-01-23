import { Component, OnInit } from '@angular/core';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-date-reservation',
  templateUrl: './date-reservation.component.html',
  styleUrls: ['./date-reservation.component.css'],
  providers: [DatePipe]
})
export class DateReservationComponent implements OnInit {

  startDate = new Date(Date.now());
  minDate = new Date(Date.now() - 1);
  
  //events: string[] = [];
  dateInput : string;

  constructor(private datePipe: DatePipe) { }

  ngOnInit() {
  }

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    //this.events.push(`${type}: ${event.value}`);
    this.dateInput = this.datePipe.transform(new Date(`${event.value}`),'yyyy/MM/dd');

    console.log(this.datePipe.transform(new Date(`${event.value}`),'yyyy/MM/dd'));
  }

}
