import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Retrospective } from '../models/Retrospective'; 
import { environment } from 'src/environments/environment';
import { Feedback } from '../models/Feedback';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class RetrospectiveService {



  itemsUrl = `${environment.apiUrl}/Retrospective`;

  constructor( private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  getRetrospectives() : Observable<Retrospective[]>{
    return this.http.get<Retrospective[]>(this.itemsUrl);
  }

  getRetrospectiveById(id: string) : Observable<Retrospective>{ 
    return this.http.get<Retrospective>(this.itemsUrl + '/GetById/' + id);
  }

  getRetrospectiveByDate(date: string) : Observable<Retrospective[]>{ 
    return this.http.get<Retrospective[]>(this.itemsUrl + '/GetByDate/' + date);
  }

  addRetrospective(retrospective: Retrospective){
    return this.http.post<Retrospective>(this.itemsUrl , retrospective, this.httpOptions);}

  updateRetrospective(id: string, retrospective: Retrospective) { 
    return this.http.put<Retrospective>(this.itemsUrl + '/' + id, retrospective, this.httpOptions);
  }

  addFeedback(id: string, _feedback: { name: string | null; body: string | null; feedbackType: string | null; }) {
   var feedbackDto : Feedback = {};

   feedbackDto.name = _feedback.name;
   feedbackDto.body = _feedback.body;
   feedbackDto.feedbackType = _feedback.feedbackType;

    return this.http.put<Retrospective>(this.itemsUrl + '/AddFeedback/' + id, feedbackDto, this.httpOptions);
  }

}
