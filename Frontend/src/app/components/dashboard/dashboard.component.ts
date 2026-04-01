import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  users: any[] = [];
  data: any;

  constructor(private auth: AuthService, private userService: UserService) { }

  ngOnInit() {
    this.userService.getDashboard()
      .subscribe(res => this.data = res);
  }

  logout() {
    this.auth.logout();
  }

  selectedFile!: File;

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  loadUsers() {
    this.userService.getUsers().subscribe((res: any) => {
      this.users = res;
    });
  }

  upload() {
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.userService.uploadExcel(formData)
      .subscribe({
        next: (res: any) => {
          alert(res.message); // 👈 ahora sí funciona
          this.loadUsers();
        },
        error: (err) => {
          console.error(err);
          alert('Error al subir archivo');
        }
      });
  }

}
