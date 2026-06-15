import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoveHistoyComponent } from './move-histoy.component';

describe('MoveHistoyComponent', () => {
  let component: MoveHistoyComponent;
  let fixture: ComponentFixture<MoveHistoyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MoveHistoyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MoveHistoyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
