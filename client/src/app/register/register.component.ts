import { Component, EventEmitter, inject, Inject, Input, input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_service/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  toaster = inject(ToastrService);
  cancelRegister = output<boolean>();
model : any ={}

register()
{
  this.accountService.register(this.model).subscribe({
    next : response => 
    {
      console.log(response);
    },
    error : error => {this.toaster.error(error.error)}
  })
}

cancle()
{
  this.cancelRegister.emit(false);
}
}
