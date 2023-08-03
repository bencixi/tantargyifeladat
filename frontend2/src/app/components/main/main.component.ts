import { Component } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
  constructor(private http:HttpClient){

  }

  //Új felhasználó hozzáadása

  onUserCreate(users: {email: string, telefonszam: string,
  erzekenysegek: string, vallas: string, fogyatekossag: string,
  titulus: string, userType: number, ProfilePic: string,
   utolsoesemeny: string, name: string}){
    console.log(users);
    this.http.post(`${environment.baseURL}/api/Person`, users)
    .subscribe((response)=> {
      console.log(response)
    });
  }
}
