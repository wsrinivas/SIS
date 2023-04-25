import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component'; 
import { LoadingDialogComponent } from './shared/loading-dialog/loading-dialog.component';
import { ErrorDialogComponent } from './shared/error-dialog/error-dialog.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from './home/home.component';
import { AddRetrospectiveComponent } from './add-retrospective/add-retrospective.component';

@NgModule({
  declarations: [
    AppComponent,
    FeedbackComponent,
    NavMenuComponent, 
    LoadingDialogComponent,
    ErrorDialogComponent,
    HomeComponent,
    AddRetrospectiveComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule ,
    // RouterModule.forRoot([
    //   { path: '', component: AppComponent, pathMatch: 'full' },
    //   { path: 'feedback', component: FeedbackComponent },
    //   { path: 'add-feedback', component: AddFeedbackComponent },
    // ]),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
