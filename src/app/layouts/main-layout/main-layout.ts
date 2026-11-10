import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './main-layout.html',
  styleUrls: ['./main-layout.scss']
})
export class MainLayoutComponent {

  sidebarCollapsed = false;
  customerExpanded = false;
  isSettingsOpen = false; // Biến quản lý trạng thái đóng/mở
  userName = '';
  avatarUrl = '';
  email = '';

  constructor(
    @Inject(PLATFORM_ID)
    private platformId: Object,
    private http: HttpClient    
  ) {}
  
    ngOnInit(): void {

      if (isPlatformBrowser(this.platformId)) {

        this.http
          .get<any>('http://localhost:5294/api/auth/me')
          .subscribe({

            next: (user) => {

              console.log('ME DATA=', user);

              this.userName =
                user.fullName || '';

              this.email =
                user.email || '';

              this.avatarUrl =
                user.avatarUrl || '';

            },

            error: (err) => {

              console.error(err);

            }

          });

      }

    }

    toggleSettings() {
      this.isSettingsOpen = !this.isSettingsOpen; // Đổi trạng thái qua lại khi click
      console.log('Settings state:', this.isSettingsOpen); // Dùng để kiểm tra trên Console (F12)
    }

    toggleSidebar(): void {

      this.sidebarCollapsed =
        !this.sidebarCollapsed;

    }
  
  toggleCustomerMenu(): void {
        
    this.customerExpanded =
      !this.customerExpanded;
  }

}