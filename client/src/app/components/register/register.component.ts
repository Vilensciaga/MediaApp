import { Component, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common'
import {AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators} from '@angular/forms'
import { Router, RouterModule } from '@angular/router';
import { AccountService } from '../../services/userServices/account.service';
import { ToastrService } from 'ngx-toastr';
import { TextInputComponent } from "../../forms/text-input/text-input.component";
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { DateInputComponent } from "../../forms/date-input/date-input.component";
@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, RouterModule, ReactiveFormsModule, TextInputComponent, DateInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  model:any = {};

  maxDateStruct!:NgbDateStruct;


  registerForm!: FormGroup<{
    username: FormControl<string>;
    password: FormControl<string>;
    confirmPassword: FormControl<string>;
    gender: FormControl<string>;
    knownAs: FormControl<string>;
    dateOfBirth: FormControl<string>;
    city: FormControl<string>;
    country: FormControl<string>;
}>;


  constructor(private accountService:AccountService, private router:Router,
     private toastr:ToastrService, private fb:FormBuilder)
  {

  }

  ngOnInit()
  {
    this.initializeForm();
    this.matchingPasswordUpdate();
    this.maxDateStruct = this.toNgbDateStruct();
  }

  matchValues(matchTo:string):ValidatorFn
  {
    //getting access to the the control we are attaching the validator to
    //attching it to the confirm password control
    //compare to what is in the match, comparing the 2 values
    //if they match we return null meaning validation pass. 
    // Else we return a validation error iMatching equals true and validation fails
    return (control: AbstractControl) => {
    const parent = control.parent as FormGroup;
    if (!parent) return null;

    const matchingControl = parent.controls[matchTo];
    return control.value === matchingControl?.value ? null : { isMatching: true };
  };
  }

  toNgbDateStruct(): NgbDateStruct {
    var date = new Date();
    date.setFullYear(date.getFullYear()-18);
    return {
      year: date.getFullYear(),
      month: date.getMonth() + 1, // JS months are 0-based
      day: date.getDate()
    };
}

  register()
  {
    // this.accountService.register(this.model).subscribe({
    //   next: response =>{
    //     //console.log(response);
    //     //setTimeout(()=>{
    //       this.router.navigate(["/home"]);
    //     //}, 500)
    //   },
    //   error: error =>
    //   {
    //     this.toastr.error(error.error);
    //   }
    // });

    console.log(this.registerForm.value);
  }

  matchingPasswordUpdate()
  {
    //making form becomes invalid when password field changes
    this.registerForm.get('password')?.valueChanges.subscribe({
      next:next =>{
        this.registerForm.get('confirmPassword')?.updateValueAndValidity();
      }
    })
  }



  initializeForm()
  {
    this.registerForm = this.fb.nonNullable.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
      gender: ['male'],
      knownAs: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      city: ['', [Validators.required]],
      country: ['', [Validators.required]],


    });
  }

}
