import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_service/account.service';
import { Inject, inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
//javascript code so the injection done by other way
export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toaster = inject(ToastrService);

  if(accountService.currentUser())
  {
    return true;
  }
  else 
  {
    toaster.error("You shall not pass");
    return false;
  }
  
};
