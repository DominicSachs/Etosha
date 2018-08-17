import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Regex } from '../shared/validation/regex.model';
import { User } from './user.model';
import { UserService } from './user.service';

@Component({
  templateUrl: './useredit.component.html',
  styleUrls: ['./useredit.component.scss']
})
export class UserEditComponent implements OnInit, OnDestroy {
  private routeSubscription: any;
  userForm: FormGroup;

  constructor(private route: ActivatedRoute, private userService: UserService, private formBuilder: FormBuilder, private router: Router) {
    this.userForm = this.formBuilder.group({
      id: '',
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.pattern(Regex.EMAIL_REGEX)]]
    });
  }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => {
      const id = +params['id'];
      if (id) {
        this.userService.getUser(id)
          .subscribe(u => {
            this.userForm.patchValue({
              firstName: u.firstName,
              lastName: u.lastName,
              email: u.email
            });
          });
      }

      this.userForm.patchValue({ id: id || 0 });
    });
  }

  ngOnDestroy() {
    this.routeSubscription.unsubscribe();
  }

  // use object destructing
  onSubmit({ value, valid }: { value: User, valid: boolean }) {
    if (valid) {
      this.userService.saveUser(value)
        .subscribe(user => {
          this.router.navigateByUrl('/users');
        });
    }
  }
}
