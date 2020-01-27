import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms'; 
import { Validators } from '@angular/forms';
import { RestService } from '../service/rest.service';
import { Flights } from './Flights';

import { MatDialog } from '@angular/material';
import { DialogComponent } from '../dialog/dialog.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  formGroup: FormGroup;
  showFlights: boolean;
  showClasses: boolean;
  button: string;
  flights: Array<Flights>;
  selectedFlight: Flights; 


  constructor(private service: RestService, private dialog: MatDialog, public datepipe: DatePipe) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      NomUtilisateur : new FormControl('', Validators.required),
      PrenomUtilisateur : new FormControl('', Validators.required),
      Telephone : new FormControl('', Validators.required),
      Address : new FormControl('', Validators.required),
      Date: new FormControl('', Validators.required),
      Flight: new FormControl(''),
      ClasseId: new FormControl('')
    
    
    })

    this.showFlights = false, this.showClasses = false, this.selectedFlight = null
    this.button = 'chercher';

  }



  onFlightSelected(value: any) {
   
      this.selectedFlight = value;
    
  }

  validation() {
    
    //first submit (getting flights)
    if (!this.formGroup.get('Flight').value) {

      let date = this.datepipe.transform(new Date(), 'yyyy-MM-dd');
      if (this.formGroup.get('Date').value < date) {
        this.dialogOpen("La date doit etre supérieure ou égale à la date d'aujourd'hui", 'Oups');
      }
      else {
        this.service.datePost(this.formGroup.value).subscribe(data => {
          this.flights = data;
          this.button = 'réserver';

          this.showFlights = true;
          this.showClasses = true;
          this.selectedFlight = this.flights[0];
        }, error => {
          this.dialogOpen(error.message, 'Oups');

        });
      }
    }
    //second submit (making a reservation)
    else {

      if (!this.formGroup.get('Flight').value.flightId || !this.formGroup.get('ClasseId').value)
        this.dialogOpen('tout les champs doivent etre renseignés', 'Oups');
      else {

        this.service.ReservePost(this.formGroup.value).subscribe(data => {
/*          this.dialogOpen(data.message, 'Félicitations');
*/          const dialogRef = this.dialog.open(DialogComponent, {
              data: { title: data.message, contain: 'Félicitations' },
              height: '30%',
              width: '50%',
            });
          dialogRef.afterClosed().subscribe(() => {
            this.showFlights = false;
            this.showClasses = false;
            this.formGroup.controls['Flight'].setValue("");
            this.formGroup.controls['ClasseId'].setValue("");
            this.button = 'chercher';
          });


        }, error => {
          this.dialogOpen(error.message,'Oups');

        });
      }

    }

  }





  dialogOpen(contain : string, title : string) {
    this.dialog.open(DialogComponent, {
      data: { title : title, contain: contain },
      height: '40%',
      width: '50%',
    });

  }








}
