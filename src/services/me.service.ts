import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserMeProfile } from '../types/meprofiles';

@Injectable({
  providedIn: 'root'
})
export class MeService {

  private apiUrl =
    'http://localhost:5294/api/auth/me';

  constructor(
    private http: HttpClient
  ) 
      {
      }

getMe(): Observable<UserMeProfile> 
{

  return this.http.get<UserMeProfile>(
    this.apiUrl  );
}

}