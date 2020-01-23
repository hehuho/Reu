import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
import { HttpClient } from '@angular/common/http';
import { MatSelectChange } from '@angular/material/select';
import { EventEmitter } from '@angular/core';

export class Flight {
  value: string;
  viewValue: string;
}

export class Classe {
  value: string;
  viewValue: string;
  price: string;
  flightId: string;
}

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
  
  dateInput : string;

  selectedValue: string;
  selectedValueClasse: string;
  flights: Flight[] = [];
  classes: Classe[] = [];
  classesSelected: Classe[] = [];

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

      for(let result of res){
        
        let flight : Flight = { value : result['flightId'].toString(), viewValue :  result['flightName'] };

        this.flights.push(flight);

        for(let resultItem of result['classeList']){
          let classe : Classe = { value : resultItem['classeId'], 
                                  viewValue : resultItem['name'], 
                                  price : resultItem['price'],
                                  flightId : resultItem['flightId']
                                  };
          
          this.classes.push(classe);
        }

      }

      console.log(this.selectedValueClasse);

    });
  }

  
  onFlightSelectionChange(event: EventEmitter<MatSelectChange>){
    console.log("FlightId : " + `${this.selectedValue}`);

    console.log(this.classes);

    this.classesSelected = [];

    for(let classeItem of this.classes){
      if(classeItem.flightId == this.selectedValue){
        this.classesSelected.push(classeItem);
      }
    }

    console.log(this.classesSelected);
    
    console.log(this.selectedValueClasse);

  }

  onSubmit(){
    alert("test !");
  }

  isNumber(val) { return typeof val === 'number'; }

}
