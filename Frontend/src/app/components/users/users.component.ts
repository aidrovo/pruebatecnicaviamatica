import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {

  users: any[] = [];
  search = '';

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers().subscribe((res: any) => {
      this.users = res;
    });
  }

  searchUsers() {
    if (!this.search) {
      this.loadUsers();
      return;
    }

    this.userService.searchUsers(this.search)
      .subscribe((res: any) => {
        this.users = res;
      });
  }

  selectedUser: any = null;

  editUser(user: any) {
    this.selectedUser = { ...user }; // copia
  }

  updateUser() {
    this.userService.updateUser(this.selectedUser.id, this.selectedUser)
      .subscribe(() => {
        alert('Usuario actualizado');
        this.selectedUser = null;
        this.loadUsers();
      });
  }

  deleteUser(id: number) {
    if (!confirm('¿Seguro que deseas eliminar?')) return;

    this.userService.deleteUser(id)
      .subscribe(() => {
        alert('Usuario eliminado');
        this.loadUsers();
      });
  }
}
