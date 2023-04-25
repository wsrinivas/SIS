import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Retrospective } from '../models/Retrospective';
import { RetrospectiveService } from '../services/retrospective.service';
import { Feedback } from '../models/Feedback';

@Component({
  selector: 'app-add-retrospective',
  templateUrl: './add-retrospective.component.html',
  styleUrls: ['./add-retrospective.component.css']
})
export class AddRetrospectiveComponent implements OnInit {



constructor(
  private retrospectiveService: RetrospectiveService,){   }

  submitted = false;
  success:boolean = false;
  failure:boolean = false;
  errorMessage?: string;

  retrospectiveForm = new FormGroup({
    name: new FormControl('', Validators.required),
    summary: new FormControl('', Validators.required),
    date: new FormControl(''),
    participants: new FormControl('', Validators.required),
    feedbacks: new FormControl('')
  });  

  ngOnInit(): void {
    this.retrospectiveForm.controls.name.setValue('');
    this.retrospectiveForm.controls.summary.setValue('');
    this.retrospectiveForm.controls.date.setValue(new Date().toDateString());
    this.retrospectiveForm.controls.participants.setValue('');
    this.retrospectiveForm.controls.feedbacks.setValue('');

  }

  addRetrospective(){    
    console.warn(this.retrospectiveForm.value);
    var participantsArray  = this.retrospectiveForm.controls.participants.value?.split(',');
    
    var _feedbacks: Array<Feedback> = [] ;
    
    this.retrospectiveService.addRetrospective(
      { 
        name: this.retrospectiveForm.controls.name.value,
        summary: this.retrospectiveForm.controls.summary.value,
        date: this.retrospectiveForm.controls.date.value, 
        participants: participantsArray, 
        feedbacks:  _feedbacks
      }).subscribe({
        next: x => {
          console.log('Observer got a next value: ' + x);
          this.success = true;
          this.failure = false;
        },
        error: err => {
          console.error('Observer got an error: ' + err);
          this.success = false;
          this.failure = true;
          this.errorMessage = err;
        },
        complete: () => console.log('Observer got a complete notification'),         
      });

  }
}
