import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {

  loginWithGoogle() {

    window.location.href =
      'https://localhost:5294/api/auth/google-login';

  }

}