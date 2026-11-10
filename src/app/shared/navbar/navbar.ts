import { Component } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-navbar',
  styleUrl: './navbar.css',
  templateUrl: './navbar.html',
})
export class Navbar {

  isSettingsOpen = false;

  toggleSettings() {
    this.isSettingsOpen = !this.isSettingsOpen;
  }

}
