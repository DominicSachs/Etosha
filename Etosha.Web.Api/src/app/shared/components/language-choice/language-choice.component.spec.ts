import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguageChoiceComponent } from './language-choice.component';

describe('LanguageChoiceComponent', () => {
  let component: LanguageChoiceComponent;
  let fixture: ComponentFixture<LanguageChoiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LanguageChoiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguageChoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
