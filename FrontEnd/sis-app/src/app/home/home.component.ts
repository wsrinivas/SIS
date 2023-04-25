import { Component } from '@angular/core';
import { RetrospectiveService } from './../services/retrospective.service';
import { BehaviorSubject, Subject } from 'rxjs';
import { Retrospective } from './../models/Retrospective';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  private readonly _retrospectives = new BehaviorSubject<Retrospective[]>([]);
  readonly retrospectives$ = this._retrospectives.asObservable();

  get retrospectives(): Retrospective[] {
    return this._retrospectives.getValue();
  }

  set retrospectives(val: Retrospective[]) {
    this._retrospectives.next(val);
  }

  title = 'sis-app';

  constructor(
    public service: RetrospectiveService) { }

  getFilteredList(dateString: string): void {
    this.service.getRetrospectiveByDate(dateString).subscribe(results => {
      this.retrospectives = results
    });
  }
  getList(): void {
    this.loadData();
  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData() {
    this.service.getRetrospectives().subscribe(results => {
      this.retrospectives = results;
    });
  }
}
