import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { User } from './user.model';
import { UserService } from './user.service';

@Component({
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  displayedColumns = ['firstName', 'lastName', 'email', 'userName', 'actions'];
  dataSource: MatTableDataSource<User>;
  @ViewChild(MatSort, { static: false })
  sort: MatSort;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.dataSource = new MatTableDataSource([]);
    this.userService.getUsers().subscribe((users: User[]) => {
      this.dataSource = new MatTableDataSource(users);
      this.dataSource.sort = this.sort;
    });
  }
}
