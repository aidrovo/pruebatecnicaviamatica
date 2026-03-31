import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  data: any;

  constructor(private auth: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.userService.getDashboard()
      .subscribe(res => this.data = res);
  }

  logout() {
    this.auth.logout();
  }
}
