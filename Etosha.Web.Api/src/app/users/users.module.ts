import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { UsersComponent } from './users.component';
import { UserService } from './user.service';
import { HttpClientModule } from '@angular/common/http';
import { UserEditComponent } from './useredit.component';
import { UserDeleteComponent } from './userdelete.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule
  ],
  declarations: [UsersComponent, UserEditComponent, UserDeleteComponent],
  providers: [UserService]
})
export class UsersModule { }
