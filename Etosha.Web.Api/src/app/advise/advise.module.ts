import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdviseComponent } from './advise.component';
import { AdviseService } from './advise.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [AdviseComponent],
  providers: [AdviseService]
})
export class AdviseModule { }
