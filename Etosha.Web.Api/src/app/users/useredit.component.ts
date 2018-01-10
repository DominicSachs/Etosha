import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from './user.service';
import { User } from './user.model';

@Component({
  selector: 'app-useredit',
  templateUrl: './useredit.component.html',
  styleUrls: ['./useredit.component.scss']
})
export class UserEditComponent implements OnInit, OnDestroy {
  user: User;
  userForm: FormGroup;
  private routeSubscription: any;

  constructor(private route: ActivatedRoute, private userService: UserService, private formBuilder: FormBuilder) {
    this.userForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => {
      let id = +params['id']; // (+) converts string 'id' to a number
      this.userService.getUser(id)
        .subscribe(u => {
          console.log(u);
          this.userForm.patchValue({
            firstName: u.firstName,
            lastName: u.lastName,
            email: u.email
          });
          this.user = u;
        });
   });
  }

  ngOnDestroy() {
    this.routeSubscription.unsubscribe();
  }
}
