import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-otp',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './otp.html',
  styleUrl: './otp.scss'
})
export class Otp {

  otp = '';

  email = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) { }

  resendOtp(): void {

    console.log('Resend OTP');

  }
  
  verifyOtp() {

    this.http.post(
      'http://localhost:5294/api/auth/verify-otp',
      {
        email: this.email,
        otp: this.otp
      })
      .subscribe({
        next: (response: any ) => {

          console.log('Access Token:', response.accessToken);
          console.log('Refresh Token:', response.refreshToken);

          this.authService.saveTokens(
            response.accessToken,
            response.refreshToken
          );

          this.router.navigate(['/dashboard']);
          
        },
        error: err => {

          alert('OTP không hợp lệ');

          console.error(err);
          
          alert(
            JSON.stringify(err.error)
          );
          
        }
      });

  }

}