import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../../components/members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component, currentRoute, currentState, nextState) => {


  if(component.editForm.dirty){
    return confirm("Unsaved changes will be lost, are you sure you want to leave this page?")
  }
  return true;
};
