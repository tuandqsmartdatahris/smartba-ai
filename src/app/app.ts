import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  imports: [RouterOutlet, MatTooltipModule],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('smartba-web');
  
  sidebarCollapsed = false;

  toggleSidebar() {
      
    this.sidebarCollapsed = !this.sidebarCollapsed;
  }
}
