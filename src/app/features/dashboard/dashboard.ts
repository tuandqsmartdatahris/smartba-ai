import { Component, OnInit } from '@angular/core';
import { MeService } from '../../../services/me.service';
import { UserMeProfile } from '../../../types/meprofiles';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})


export class DashboardComponent
  implements OnInit {

  user?: UserMeProfile;

  constructor(
    private meService: MeService
  ) {}

  ngOnInit(): void {

    this.meService.getMe()
      .subscribe({
        next: (data) => {
          console.log('ME DATA=', data);
          this.user = data;
        },
        error: (err) => {
          console.error(err);
        }
      });
  }
}