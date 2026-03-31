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

  uploadExcel(file: File) {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.api}/users/upload`, formData);
  }

  getDashboard() {
    return this.http.get(`${this.api}/dashboard`);
  }
}
