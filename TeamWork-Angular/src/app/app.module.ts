import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MenuComponent } from './components/main-menu/menu/menu.component';
import { LoginPageComponent } from './components/account/login-page/login-page.component';
import { RegisterPageComponent } from './components/account/register-page/register-page.component';
import { MainPageComponent } from './components/landing-page/main-page/main-page.component';
import { JwtModule } from '@auth0/angular-jwt';

import {MatListModule} from '@angular/material/list';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import {MatCardModule} from '@angular/material/card';
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
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CreateGroupComponent } from './components/group-section/create-group/create-group.component';

import {ClipboardModule} from '@angular/cdk/clipboard';
import { AlertService } from 'src/app/services/alert.service'; 
import { ToastrModule } from 'ngx-toastr';
import { AppErrorHandler } from './handler-error/app-error-handler';
import {AuthconfigInterceptor} from 'src/app/shared/authconfig.interceptor';
import { JoinGroupComponent } from './components/group-section/join-group/join-group.component';
import { GroupComponent } from './components/group-section/my-groups/group/group.component';
import { GroupsComponent } from './components/group-section/my-groups/groups/groups.component';
import { SheetKeyComponent } from './components/group-section/my-groups/sheet-key/sheet-key.component';
import { GroupMainPageComponent } from './components/group-section/group-details/group-main-page/group-main-page.component';
import { GroupMenuComponent } from './components/group-section/group-details/group-menu/group-menu.component';
import { GroupDetailsSectionComponent } from './components/group-section/group-details/group-details-section/group-details-section.component';
import { EditGroupComponent } from './components/group-section/group-details/edit-group/edit-group.component';

export function tokenGetter() {
  return localStorage.getItem('access_token');
}

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    LoginPageComponent,
    RegisterPageComponent,
    MainPageComponent,
    CreateGroupComponent,
    JoinGroupComponent,
    GroupComponent,
    GroupsComponent,
    SheetKeyComponent,
    GroupMainPageComponent,
    GroupMenuComponent,
    GroupDetailsSectionComponent,
    EditGroupComponent
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
    HttpClientModule,
    MatBottomSheetModule,
    ClipboardModule,
    MatListModule,
    MatCardModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
      tokenGetter: tokenGetter
    }}),
  ],
  providers: [
    AlertService,
    AppErrorHandler,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthconfigInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
