import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdviseComponent } from './advise/advise.component';
import { UsersComponent } from './users/users.component';
import { UserEditComponent } from './users/useredit.component';
import { UserDeleteComponent } from './users/userdelete.component';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './shared/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full', },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
  { path: 'users/create', component: UserEditComponent, canActivate: [AuthGuard] },
  { path: 'users/edit/:id', component: UserEditComponent, canActivate: [AuthGuard] },
  { path: 'users/delete/:id', component: UserDeleteComponent, canActivate: [AuthGuard] },
  { path: 'advice/:id', component: AdviseComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
