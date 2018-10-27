import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from './user.model';
import { UserService } from './user.service';

@Component({
  templateUrl: './userdelete.component.html'
})
export class UserDeleteComponent implements OnInit, OnDestroy {
  private routeSubscription: any;
  user: User;

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => {
      const id = +params['id'];
      if (id) {
        this.userService.getUser(id).subscribe(u => {
          this.user = u;
        });
      }
    });
  }

  ngOnDestroy() {
    this.routeSubscription.unsubscribe();
  }

  delete() {
    this.userService.deleteUser(this.user).subscribe(user => {
        this.router.navigateByUrl('/users');
      });
  }
}
