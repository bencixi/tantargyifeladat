import { Component, Input } from '@angular/core';
import { PersonService } from 'src/app/services/person.service';
import { Response } from 'src/app/models/response';
import { Router } from '@angular/router';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.css']
})
export class PersonComponent{

  public users : any;
  public id : any;
  private router: Router;
  public showePage : string = 'All'

  constructor(
    private personService: PersonService,
  ) {}
  


  ngOnInit(): void {
    this.getUsers();
  }

  // Felhasználó lekérése

  getUsers() {
    this.personService.getPerson().subscribe({
      next: (response: Response<any>) => {      
        this.users = response
    }})
  }

  // Felhasználó lekérése id-alapján

  getUserById(id : number) {
    this.personService.getPersonById(id).subscribe({
      next: (response: Response<any>) => {      
        this.id = response    
      }});
  
    }
}
