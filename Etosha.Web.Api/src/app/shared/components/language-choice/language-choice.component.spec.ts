import { LanguageChoiceComponent } from './language-choice.component';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs/Observable';

describe('LanguageChoiceComponent', () => {
  let component: LanguageChoiceComponent;
  let translateService: TranslateService;

  beforeEach(() => {
    translateService = <any>{
      setDefaultLang(language: string) { },
      use: (language: string) => Observable.of({})
    };

    component = new LanguageChoiceComponent(translateService);
  });

  it('should set default language on init', () => {
    spyOn(translateService, 'setDefaultLang');

    component.ngOnInit();

    expect(component.selected).toBe('de-DE');
    expect(translateService.setDefaultLang).toHaveBeenCalledWith('de-DE');
  });

  it('should change the language with onLanguageChange', () => {
    spyOn(translateService, 'use');

    component.onLanguageChange({ value: 'de-DE' });

    expect(translateService.use).toHaveBeenCalledWith('de-DE');
  });
});
