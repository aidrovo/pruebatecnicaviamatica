import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class UserService {

  private api = 'https://localhost:7238/api';

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get(`${this.api}/users`);
  }

  searchUsers(term: string) {
    return this.http.get(`${this.api}/users/search?term=${term}`);
  }

  createUser(user: any) {
    return this.http.post(`${this.api}/users`, user);
  }

  getDashboard() {
    return this.http.get(`${this.api}/dashboard`);
  }

  updateUser(id: number, user: any) {
    return this.http.put(`${this.api}/users/${id}`, user);
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.api}/users/${id}`);
  }

  uploadExcel(data: FormData) {
    return this.http.post(`${this.api}/dashboard/upload`, data);
  }
}
