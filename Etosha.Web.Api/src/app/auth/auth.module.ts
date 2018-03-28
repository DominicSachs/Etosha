import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/modules/shared.module';

@NgModule({
  imports: [
    SharedModule,
    ReactiveFormsModule
  ],
  declarations: [LoginComponent]
})
export class AuthModule { }
