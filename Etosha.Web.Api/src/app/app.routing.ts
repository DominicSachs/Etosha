import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdviseComponent } from './advise/advise.component';
import { UsersComponent } from './users/users.component';
import { UserEditComponent } from './users/useredit.component';

export const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full', },
  { path: 'users', component: UsersComponent },
  { path: 'users/create', component: UserEditComponent },
  { path: 'users/edit/:id', component: UserEditComponent },
  { path: 'advice/:id', component: AdviseComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
