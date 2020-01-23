import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-date-reservation',
  templateUrl: './date-reservation.component.html',
  styleUrls: ['./date-reservation.component.css'],
  providers: [DatePipe]
})

export class DateReservationComponent implements OnInit {

  startDate = new Date(Date.now());
  minDate = new Date(Date.now() - 1);
  configUrl = 'assets/config.json';
  
  //events: string[] = [];
  dateInput : string;

  constructor(private datePipe: DatePipe, private http: HttpClient) { }

  ngOnInit() {
  }

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    //this.events.push(`${type}: ${event.value}`);
    this.dateInput = this.datePipe.transform(new Date(`${event.value}`),'yyyy/MM/dd');

    console.log(this.datePipe.transform(new Date(`${event.value}`),'yyyy/MM/dd'));
  }

  onClick(){
    alert(this.dateInput);

    let dateInputPost:JSON = <JSON><unknown>{
      "Date": this.dateInput
    }

    console.log(dateInputPost);

    this.http.post<any>('http://localhost:63708/api/reservation/ReturnList', dateInputPost).subscribe(res => {
      console.log(res);
    });
  }

}
