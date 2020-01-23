import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-date-reservation',
  templateUrl: './date-reservation.component.html',
  styleUrls: ['./date-reservation.component.css']
})
export class DateReservationComponent implements OnInit {

  startDate = new Date(Date.now());
  minDate = new Date(Date.now() - 1);

  constructor() { }

  ngOnInit() {
  }

}
