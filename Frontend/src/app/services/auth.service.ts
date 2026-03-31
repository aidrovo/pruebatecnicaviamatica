import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private api = 'https://localhost:7238/api';

  constructor(private http: HttpClient, private router: Router) { }

  login(data: any) {
    return this.http.post<any>(`${this.api}/auth/login`, data);
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout() {
    this.http.post(`${this.api}/auth/logout`, {})
      .subscribe({
        next: () => this.finishLogout(),
        error: () => this.finishLogout()
      });
  }

  private finishLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }
}
