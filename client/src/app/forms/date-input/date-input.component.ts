import { Component, inject, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgbCalendar, NgbDatepickerModule, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import {CommonModule, JsonPipe}  from "@angular/common"


@Component({
  selector: 'app-date-input',
  imports: [NgbDatepickerModule, ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './date-input.component.html',
  styleUrl: './date-input.component.css'
})
export class DateInputComponent implements ControlValueAccessor{
  @Input() label!:string;
  @Input() maxDate!:NgbDateStruct;

  constructor(@Self() public ngControl:NgControl)
  {
    this.ngControl.valueAccessor = this;
  }

  get getFormControl(): FormControl | null {
    const control = this.ngControl?.control;
    return control instanceof FormControl ? control : null;
  }

  writeValue(obj: any): void {
    
  }
  registerOnChange(fn: any): void {
    
  }
  registerOnTouched(fn: any): void {
    
  }

}
