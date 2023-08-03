import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonByIdComponent } from './personbyid.component';

describe('PersonByIdComponent', () => {
  let component: PersonByIdComponent;
  let fixture: ComponentFixture<PersonByIdComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PersonByIdComponent]
    });
    fixture = TestBed.createComponent(PersonByIdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
