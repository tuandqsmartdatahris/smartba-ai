import { Injectable } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { PLATFORM_ID, inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly ACCESS_TOKEN_KEY = 'accessToken';
  private readonly REFRESH_TOKEN_KEY = 'refreshToken';
  private platformId = inject(PLATFORM_ID);

  saveTokens(
    accessToken: string,
    refreshToken: string
  ): void {

      if (!isPlatformBrowser(this.platformId)) {
          return;
        }

        localStorage.setItem(
          'accessToken',
          accessToken
        );

        localStorage.setItem(
          'refreshToken',
          refreshToken
        );
  }

  getAccessToken(): string | null {

      if (!isPlatformBrowser(this.platformId)) {
        return null;
      }

      return localStorage.getItem(
        'accessToken'
      );
  }

  getRefreshToken(): string | null {

      if (!isPlatformBrowser(this.platformId)) {
        return null;
      }

      return localStorage.getItem(
        'refreshToken'
      );
  }

  clearTokens(): void {

      if (!isPlatformBrowser(this.platformId)) {
        return;
      }

      localStorage.removeItem(
        'accessToken'
      );

      localStorage.removeItem(
        'refreshToken'
      );
  }

  isLoggedIn(): boolean {

    return !!this.getAccessToken();
  }
}