import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MenuComponent } from './components/main-menu/menu/menu.component';
import { MenuHomeComponent } from './components/main-menu//menu-home/menu-home.component';
import { MenuGroupComponent } from './components/main-menu/menu-group/menu-group.component';
import { LoginPageComponent } from './components/account/login-page/login-page.component';
import { RegisterPageComponent } from './components/account/register-page/register-page.component';
import { MainPageComponent } from './components/landing-page/main-page/main-page.component';

import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FlexLayoutModule } from "@angular/flex-layout";
import { AppRoutingModule } from './app-routing.module';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import {UserRegisterModule} from '../app/modules/user-register.module';
import {UserLoginModule} from '../app/modules/user-login.module';
import { HttpClientModule } from '@angular/common/http';
import { CreateGroupComponent } from './components/group-section/create-group/create-group.component';

import { AlertService } from 'src/app/services/alert.service'; 
import { ToastrModule } from 'ngx-toastr';
import { AppErrorHandler } from './handler-error/app-error-handler';
@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    MenuHomeComponent,
    MenuGroupComponent,
    LoginPageComponent,
    RegisterPageComponent,
    MainPageComponent,
    CreateGroupComponent
  ],
  imports: [
    ReactiveFormsModule,
    MatSelectModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    MatInputModule,
    MatSidenavModule,
    FlexLayoutModule,
    AppRoutingModule,
    UserRegisterModule,
    UserLoginModule,
    HttpClientModule,
    ToastrModule.forRoot()
  ],
  providers: [
    AlertService,
    AppErrorHandler
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
