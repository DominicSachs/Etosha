import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../shared/modules/material.module';
import { SharedModule } from '../shared/modules/shared.module';
import { UserService } from './user.service';
import { UserDeleteComponent } from './userdelete.component';
import { UserEditComponent } from './useredit.component';
import { UsersComponent } from './users.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    SharedModule,
    MaterialModule,
    FlexLayoutModule
  ],
  declarations: [UsersComponent, UserEditComponent, UserDeleteComponent],
  providers: [UserService]
})
export class UsersModule { }
