import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-users',
  template: `
  <h2>Usuarios</h2>
  <div *ngFor="let u of users">
    {{u.username}}
  </div>
  `
})
export class UsersComponent implements OnInit {
  users: any[] = [];

  constructor(private auth: AuthService) { }

  ngOnInit() {
    this.auth.getUsers().subscribe((res: any) => this.users = res);
  }

}
