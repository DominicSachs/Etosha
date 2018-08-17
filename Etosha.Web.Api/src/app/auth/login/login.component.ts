import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginModel } from '../../shared/models/login.model';
import { AuthService } from '../../shared/services/auth.service';
import { Regex } from '../../shared/validation/regex.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private returnUrl: string;

  loginForm: FormGroup;
  hasError = false;
  hide = true;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private route: ActivatedRoute, private router: Router) {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.pattern(Regex.EMAIL_REGEX)])],
      password: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  onSubmit({ value, valid }: { value: LoginModel, valid: boolean }) {
    if (valid) {
      this.hasError = false;
      this.authService.login(value)
        .subscribe(result => {
          if (result) {
            this.router.navigateByUrl(this.returnUrl);
          }
        },
        e => this.hasError = true);
    }
  }
}
