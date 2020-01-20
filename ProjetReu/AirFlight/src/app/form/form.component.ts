import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms'; 
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
formGroup : FormGroup;


  constructor() { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      nom : new FormControl('', Validators.required),
      prenom : new FormControl('', Validators.required),
      tel : new FormControl('', Validators.required),
      add1 : new FormControl('', Validators.required),
      add2 : new FormControl('', Validators.required),
      add3 : new FormControl('', Validators.required),
      date : new FormControl('', Validators.required)
    
    
    })

  }

  validation(){



  }








}
//eric danglet alexandre hormigos gabriel du