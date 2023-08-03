import { Component, Input, OnInit } from '@angular/core';
import { PersonService } from 'src/app/services/person.service';
import { Response } from 'src/app/models/response';
import { ActivatedRoute } from '@angular/router';



@Component({
    selector: 'app-personbyid',
    templateUrl: './personbyid.component.html',
    styleUrls: ['./personbyid.component.css']
  })

  
  
  export class PersonByIdComponent implements OnInit  {
    public users : any;
    public userid : any;
    public user : any;
    

    constructor(
        private userService: PersonService,
        private activatedRoute: ActivatedRoute
      ) {}

      ngOnInit(): void {
        this.userid = this.activatedRoute.snapshot.paramMap.get('id');  
        this.getPersons();
        this.getPersonById(this.userid)      
      }
      //Felhasználó lekérése

      getPersons() {
        this.userService.getPerson().subscribe({
          next: (response: Response<any>) => {
            this.users = response      
        }})
      }
    
    //Felhasználó törlése

    deletePerson(id : number) {
      this.userService.deletePerson(id).subscribe({
        next: (response: Response<any>) => {      
           
        }});
    }

    //Felhasználó lekérése id-alapján

    getPersonById(id : number) {
      this.userService.getPersonById(id).subscribe({
        next: (response: Response<any>) => {      
          this.user = response 
          console.log(this.user)  
        }});
    
      }  

      

  
    
  }