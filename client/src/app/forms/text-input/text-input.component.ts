import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';
import {CommonModule} from '@angular/common'


@Component({
  selector: 'app-text-input',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.css'
})
export class TextInputComponent implements ControlValueAccessor{

    /*
    gives us acces to form control from the dom
    ControlValueAccessor: Defines an interface that acts as a bridge between the Angular forms API and a native element in the DOM.
    Implement this interface to create a custom form control directive that integrates with Angular forms.


    */

  @Input() label!:string;
  @Input() type ='text';

  /*
  injecting the control into the contructor of the component with the Self decorator
  we want this component to be selfcontained, we dont want angular to go fetch and inject another instance 
  of an injector that matches this from its container of dependency injections
  */
  constructor(@Self() public ngControl:NgControl)
  {
    this.ngControl.valueAccessor = this;
  }

    writeValue(obj: any): void {

    }
    registerOnChange(fn: any): void {

    }
    registerOnTouched(fn: any): void {

    }


    /*
      could not pass ngControl.control to [formControl] as it was coming in as AbstractControl<any, any> | null,
      [formControl] requires a type of FormControl to work. tried "instanceof" in the html, but angular templates
      do not support instanceof check like type script
      so i created this getter function that guarantees a return of FormControl and we passed that instead

    */
    get getFormControl(): FormControl | null {
    const control = this.ngControl?.control;
    return control instanceof FormControl ? control : null;
  }

    //this method is not necessary from the interface
    // setDisabledState?(isDisabled: boolean): void {
    //   throw new Error('Method not implemented.');
    // }



}
