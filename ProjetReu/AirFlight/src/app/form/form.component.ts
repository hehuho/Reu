import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms'; 
import { Validators } from '@angular/forms';
import { RestService } from '../service/rest.service';
import { Flights } from './Flights';
import { Form } from './Form';

import { MatDialog } from '@angular/material';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  formGroup: FormGroup;
  show: boolean = false;
  flights: Array<Flights>;
  


  constructor(private service: RestService, private dialog: MatDialog) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      Nom : new FormControl('', Validators.required),
      Prenom : new FormControl('', Validators.required),
      Tel : new FormControl('', Validators.required),
      Add1 : new FormControl('', Validators.required),
      Date : new FormControl('', Validators.required)
    
    
    })

  }

  validation() {
/*    let form: Form;
   form.Date = this.formGroup.get('Date').value;
*/     

    this.service.datePost(this.formGroup.value).subscribe(data => {
      console.log(data);
      this.flights = data;
      
      this.show = true;
    }, error => {
      this.dialog.open(DialogComponent, {
        data: { contain : error.message }, 
        height: '400px',
        width: '600px',
      });});


  }








}
//eric danglet alexandre hormigos gabriel du
