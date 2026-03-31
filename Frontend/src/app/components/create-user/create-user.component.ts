import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html'
})
export class CreateUserComponent {

  constructor(private userService: UserService) { }

  user = {
    name: '',
    lastName: '',
    username: '',
    password: '',
    identification: ''
  };

  error = '';

  save() {
    this.error = '';

    this.userService.createUser(this.user)
      .subscribe({
        next: () => {
          alert('Usuario creado correctamente');
          this.reset();
        },
        error: (err) => {
          this.error = err.error; // 🔥 mensaje del backend
        }
      });
  }

  reset() {
    this.user = {
      name: '',
      lastName: '',
      username: '',
      password: '',
      identification: ''
    };
  }

  loadUsers() {
    this.userService.getUsers().subscribe((res: any) => {
      this.user = res;
    });
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];

    if (!file) return;

    this.userService.uploadExcel(file)
      .subscribe(() => {
        alert('Usuarios cargados');
        this.loadUsers();
      });
  }
}
