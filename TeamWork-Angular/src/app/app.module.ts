import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MenuComponent } from './components/landing-page/menu/menu.component';
import { LoginPageComponent } from './components/account/login-page/login-page.component';
import { RegisterPageComponent } from './components/account/register-page/register-page.component';
import { MainPageComponent } from './components/landing-page/main-page/main-page.component';
import { JwtModule } from '@auth0/angular-jwt';

import {MatDialogModule} from '@angular/material/dialog';
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
import { MatTableModule } from '@angular/material/table';
import {MatButtonToggleModule} from '@angular/material/button-toggle';

import { MatPaginatorModule } from '@angular/material/paginator';
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
import { GroupDetailsSectionComponent } from './components/group-section/group-details/group-details-section/group-details-section.component';
import { EditGroupComponent } from './components/group-section/group-details/edit-group/edit-group.component';
import { AddMemberComponent } from './components/group-section/group-details/add-member/add-member.component';
import { ViewMembersComponent } from './components/group-section/group-details/view-members/view-members.component';
import { MessageCardComponent } from './components/landing-page/landing-pages-components/message-card/message-card.component';
import { GroupInvitationsComponent } from './components/landing-page/landing-pages-components/notification/group-invitations/group-invitations.component';
import { GroupInvitationComponent } from './components/landing-page/landing-pages-components/notification/group-invitation/group-invitation.component';
import { HomeLoggedComponent } from './components/landing-page/home-logged/home-logged.component';
import { MessageComponent } from './components/group-section/chat/message/message.component';
import { SentMessageComponent } from './components/group-section/chat/sent-message/sent-message.component';
import { ChatComponent } from './components/group-section/chat/chat/chat.component';
import { MessageChangeComponent } from './components/group-section/chat/message-change/message-change.component';
import { ListComponent } from './components/assignment-list/list/list.component';
import { SideMenuComponent } from './components/assignment-list/side-menu/side-menu.component';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTabsModule} from '@angular/material/tabs';
import { AngMusicPlayerModule } from  'ang-music-player';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { CarouselComponent } from './components/landing-page/landing-pages-components/carousel/carousel.component';
import {VgCoreModule} from '@videogular/ngx-videogular/core';
import { ShareButtonsModule } from 'ngx-sharebuttons/buttons';
import { ShareIconsModule } from 'ngx-sharebuttons/icons';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faFacebookSquare } from '@fortawesome/free-brands-svg-icons';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { ContactPageComponent } from './components/contact-page/contact-page.component';
import { ProfilePageComponent } from './components/account/profile-page/profile-page.component';
import { NgxImageZoomModule } from 'ngx-image-zoom';
import {LeafletModule} from '@asymmetrik/ngx-leaflet';
import { MapComponent } from './components/landing-page/landing-pages-components/map/map.component';


export function tokenGetter() {
  return localStorage.getItem('access_token');
}
const icons = [
  // ... other icons
  faFacebookSquare
];

const shareProp = {
  facebook: {
    icon: ['fab', 'facebook-square']
  }
};
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
    GroupDetailsSectionComponent,
    EditGroupComponent,
    AddMemberComponent,
    ViewMembersComponent,
    MessageCardComponent,
    GroupInvitationsComponent,
    GroupInvitationComponent,
    HomeLoggedComponent,
    MessageComponent,
    SentMessageComponent,
    ChatComponent,
    MessageChangeComponent,
    ListComponent,
    SideMenuComponent,
    CarouselComponent,
    AboutPageComponent,
    ContactPageComponent,
    ProfilePageComponent,
    MapComponent,
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
    MatDialogModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
      tokenGetter: tokenGetter
    }}),
    MatTableModule,
    MatPaginatorModule,
    MatButtonToggleModule,
    MatGridListModule,
    MatTabsModule,
    AngMusicPlayerModule,
    SlickCarouselModule,
    VgCoreModule,
    ShareIconsModule,
    ShareButtonsModule.withConfig({ prop: shareProp }),
    NgxImageZoomModule,
    LeafletModule
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
export class AppModule {
  constructor(iconLibrary: FaIconLibrary) {
    iconLibrary.addIcons(...icons);
  }
 }
