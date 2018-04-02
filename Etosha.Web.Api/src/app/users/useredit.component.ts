import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from './user.service';
import { User } from './user.model';
import { Regex } from '../shared/validation/regex.model';
import { RoleService } from '../shared/services/role.service';
import { UserRole } from '../shared/models/role.model';

@Component({
  templateUrl: './useredit.component.html',
  styleUrls: ['./useredit.component.scss']
})
export class UserEditComponent implements OnInit, OnDestroy {
  userForm: FormGroup;
  roles: Array<UserRole>;
  private routeSubscription: any;

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
