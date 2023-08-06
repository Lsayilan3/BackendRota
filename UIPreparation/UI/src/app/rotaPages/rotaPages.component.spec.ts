/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RotaPagesComponent } from './rotaPages.component';

describe('RotaPagesComponent', () => {
  let component: RotaPagesComponent;
  let fixture: ComponentFixture<RotaPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RotaPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RotaPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
