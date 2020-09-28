import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { LoginPageComponent } from './components/account/login-page/login-page.component';
import { RegisterPageComponent } from './components/account/register-page/register-page.component';
import { CreateGroupComponent } from './components/group-section/create-group/create-group.component';
import { MainPageComponent } from './components/landing-page/main-page/main-page.component';
import { AuthGuard } from 'src/app/shared/auth.guard';
import { JoinGroupComponent } from './components/group-section/join-group/join-group.component';

const routes: Routes=[

  {
    path:'login',
    component: LoginPageComponent
  },
  {
    path:'register',
    component: RegisterPageComponent
  },
  {
    path:'landing-page',
    component: MainPageComponent
  },
  {
    path: '',   
    redirectTo: '/landing-page',
    pathMatch: 'full' 
  },
  {
    path:'create-group',
    component: CreateGroupComponent,
    canActivate: [AuthGuard]
  },
  {
    path:'join-group',
    component: JoinGroupComponent,
    canActivate: [AuthGuard]
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports:[RouterModule]
})
export class AppRoutingModule { }
