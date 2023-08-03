import { Component } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})

export class HomeComponent {
  showePage : string = 'SzemnelyPageDisplay';
  menuIconClick = false;

  constructor(private http:HttpClient){
  }

  btnClick() {
    const toggleBtn = document.querySelector('.toggle_btn')
    const toggleBtnIcon = document.querySelector('.toggle_btn i')
    const dropdownMenu = document.querySelector('.dropdown_menu')

    this.menuIconClick =!

    dropdownMenu.classList.toggle('open')
    const isOpen = dropdownMenu.classList.contains('open')   

  }

  activeButton(type : number) {
    const szemelyBtn = document.querySelector('.szemely_button')
    const rendezvenyBtn = document.querySelector('.rendezveny_button')


    if (type === 1){
      this.showePage = 'SzemnelyPageDisplay';
      this.menuIconClick = true
      if(szemelyBtn.classList.contains('clicked')){}
      else{
      szemelyBtn.classList.toggle('clicked')
      rendezvenyBtn.classList.remove('clicked')
      }
    }
    else {
      this.showePage = 'RendezvenyPageDisplay'
      this.menuIconClick = true
      if(rendezvenyBtn.classList.contains('clicked')){}
      else{
      rendezvenyBtn.classList.toggle('clicked')
      szemelyBtn.classList.remove('clicked')   
        }  
    }
    }} 
  
 