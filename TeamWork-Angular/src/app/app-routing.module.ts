import { NgModule, OnInit } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { LoginPageComponent } from './components/account/login-page/login-page.component';
import { RegisterPageComponent } from './components/account/register-page/register-page.component';
import { CreateGroupComponent } from './components/group-section/create-group/create-group.component';
import { MainPageComponent } from './components/landing-page/main-page/main-page.component';
import { AuthGuard } from 'src/app/shared/auth.guard';
import { JoinGroupComponent } from './components/group-section/join-group/join-group.component';
import { GroupsComponent } from './components/group-section/my-groups/groups/groups.component';
import { AuthService } from './shared/auth.service';
import { GroupMainPageComponent } from './components/group-section/group-details/group-main-page/group-main-page.component';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { ProfilePageComponent } from './components/account/profile-page/profile-page.component';
import { CreateListComponent } from './components/assignment-list/create-list/create-list.component';
import { ListsComponent } from './components/assignment-list/lists/lists.component';
import { RandomSelectionMainComponent } from './components/group-section/random-selection-main/random-selection-main.component';

const routes: Routes = [

  {
    path: 'login',
    component: LoginPageComponent
  },
  {
    path: 'register',
    component: RegisterPageComponent
  },
  {
    path: 'landing-page',
    component: MainPageComponent
  },
  {
    path: '',
    redirectTo: '/landing-page',
    pathMatch: 'full'
  },
  {
    path: 'create-group',
    component: CreateGroupComponent,
    canActivate: [AuthGuard],
    data: { roles: ["student"]}
  },
  {
    path: 'join-group',
    component: JoinGroupComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'my-groups',
    component: GroupsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'group-details',
    component: GroupMainPageComponent,
    canActivate: [AuthGuard],
    data: { roles: ["student"]}
  },
  {
    path: 'list',
    component: ListsComponent,
    canActivate: [AuthGuard],
    data: { roles: ["teacher"]}
  },
  {
    path: 'create-list',
    component: CreateListComponent,
    canActivate: [AuthGuard],
    data: { roles: ["teacher"]}
  },
  {
    path: 'about',
    component: AboutPageComponent
    
  },
  {
    path: 'profile',
    component: ProfilePageComponent,
    canActivate: [AuthGuard]
    
  },
  {
    path: 'random',
    component: RandomSelectionMainComponent,
    canActivate: [AuthGuard],
    data: { roles: ["teacher"]}
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule implements OnInit {
  constructor(private authService: AuthService) {
  }

  ngOnInit() {
    this.authService.doLogout();
  }
}
