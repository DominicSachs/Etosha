import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/modules/shared.module';
import { UsersComponent } from './users.component';
import { UserService } from './user.service';
import { HttpClientModule } from '@angular/common/http';
import { UserEditComponent } from './useredit.component';
import { UserDeleteComponent } from './userdelete.component';

@NgModule({
  imports: [
    SharedModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule
  ],
  declarations: [UsersComponent, UserEditComponent, UserDeleteComponent],
  providers: [UserService]
})
export class UsersModule { }
