import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RetrospectiveService } from '../services/retrospective.service';
import { Retrospective } from '../models/Retrospective';
import { Feedback } from '../models/Feedback';
import {  FormControl, FormGroup,  Validators } from '@angular/forms';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {


  submitted = false;
  success:boolean = false;
  failure:boolean = false;
  errorMessage?: string;

  feedbackForm = new FormGroup({
    name: new FormControl('', Validators.required),
    body: new FormControl(''),
    feedbackType:new FormControl('', Validators.required)
  });

  feedback: Feedback = {
    id: '',
    name: '',
    body: '',
    feedbackType: ''
  };

  retrospective: Retrospective = {
    name: "",
    summary: "",
    id: '',
    date: new Date().toLocaleString(),
    participants: [],
    feedbacks: []
  };

  constructor(
    private route: ActivatedRoute,
    private retrospectiveService: RetrospectiveService,
  ) { }

  ngOnInit(): void {
    this.getItem();
  }

  getItem(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    console.log('id=', id);
    this.retrospectiveService.getRetrospectiveById(id)
      .subscribe((retrospective) => {
        this.retrospective = retrospective;
      });
  }

  addFeedback() { 
    debugger;
    this.submitted = true;
   // if (!this.feedbackForm.valid) return;
    const id = String(this.route.snapshot.paramMap.get('id'));
    
    this.retrospectiveService.addFeedback(id, { 
      name: this.feedbackForm.controls.name.value, 
      body:  this.feedbackForm.controls.body.value , 
      feedbackType:  this.feedbackForm.controls.feedbackType.value  })
      .subscribe({
        next: () => {
          this.success = true;
          this.feedbackForm.disable();
        },
        error: error => {
          this.success = false;
          this.errorMessage = error.error;
        }
      });
  }


}
