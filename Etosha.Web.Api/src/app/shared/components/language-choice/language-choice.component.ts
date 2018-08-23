import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'language-choice',
  templateUrl: './language-choice.component.html',
  styleUrls: ['./language-choice.component.scss']
})
export class LanguageChoiceComponent implements OnInit {
  selected: string;

  constructor(private translateService: TranslateService) { }

  ngOnInit() {
    this.selected = 'de-DE';
    this.translateService.setDefaultLang('de-DE');
  }

  onLanguageChange(event) {
    this.translateService.use(event.value);
  }
}
