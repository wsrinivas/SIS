import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'; 
import { AppComponent } from './app.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { HomeComponent } from './home/home.component';
import { AddRetrospectiveComponent } from './add-retrospective/add-retrospective.component';

const routes: Routes = [ 
 
  { path: '', component: HomeComponent, pathMatch: 'full'  },
  { path: 'retrospective/:id', component: FeedbackComponent }  ,
  { path: 'add-retrospective', component: AddRetrospectiveComponent }  ,

 ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
