import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { HomeComponent } from './components/home/home.component';
import { NotAuthGuardService } from './auth/not-auth-guard.service';
import {PersonComponent} from './components/person/person.component';
import {PersonByIdComponent} from './components/personbyid/personbyid.component'
import { MainComponent } from './components/main/main.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: { only: 'Admin' },
    }
  },
  {
    path: 'dashboard/user',
    component: UserEditComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: { only: 'Admin' },
    },
  },
  { path: 'home', component: HomeComponent, canActivate: [NotAuthGuardService] },
  { path: 'person', component: PersonComponent},
  { path: 'personbyid/:id', component: PersonByIdComponent },
  { path: 'main', component: MainComponent}  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
