import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from './user.service';
import { User } from './user.model';

@Component({
  templateUrl: './userdelete.component.html'
})
export class UserDeleteComponent implements OnInit, OnDestroy {
  user: User;
  private routeSubscription: any;

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
    this.userService.deleteUser(this.user.id).subscribe(user => {
        this.router.navigateByUrl('/users');
      });
  }
}
