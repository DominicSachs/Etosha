import { NgModule } from '@angular/core';
import { MaterialModule } from '../shared/modules/material.module';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { UsersComponent } from './users.component';
import { UserService } from './user.service';
import { HttpClientModule } from '@angular/common/http';
import { UserEditComponent } from './useredit.component';
import { UserDeleteComponent } from './userdelete.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule,
    FlexLayoutModule
  ],
  declarations: [UsersComponent, UserEditComponent, UserDeleteComponent],
  providers: [UserService]
})
export class UsersModule { }
