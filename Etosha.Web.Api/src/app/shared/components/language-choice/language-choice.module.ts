import { NgModule } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { LanguageChoiceComponent } from './language-choice.component';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [LanguageChoiceComponent],
  exports: [
    LanguageChoiceComponent
  ]
})
export class LanguageChoiceModule { }
