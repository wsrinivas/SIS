import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRetrospectiveComponent } from './add-retrospective.component';

describe('AddRetrospectiveComponent', () => {
  let component: AddRetrospectiveComponent;
  let fixture: ComponentFixture<AddRetrospectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddRetrospectiveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddRetrospectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
