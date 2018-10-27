import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserRole } from '../shared/models/role.model';
import { RoleService } from '../shared/services/role.service';
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
  roles: UserRole[];

  constructor(private route: ActivatedRoute, private userService: UserService, private formBuilder: FormBuilder,
              private router: Router, private roleService: RoleService) {
    this.userForm = this.formBuilder.group({
      id: '',
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.pattern(Regex.EMAIL_REGEX)]],
      roleId: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => {
      const id = +params['id'];

      this.roleService.getRoles().subscribe(r => this.roles = r);

      if (id) {
        this.userService.getUser(id)
          .subscribe(u => {
            this.userForm.patchValue({
              firstName: u.firstName,
              lastName: u.lastName,
              email: u.email,
              roleId: u.roleId
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
