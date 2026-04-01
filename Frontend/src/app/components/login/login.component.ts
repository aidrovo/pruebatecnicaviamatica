import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  user = '';
  password = '';

  constructor(private auth: AuthService, private router: Router) { }

  login() {
    this.auth.login({
      user: this.user,
      password: this.password
    }).subscribe({
      next: (res: any) => {
        this.auth.saveToken(res.token);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        const message = err?.error?.message || err?.error || 'Error al iniciar sesión';
        alert(message);
      }
    });
  }
}
