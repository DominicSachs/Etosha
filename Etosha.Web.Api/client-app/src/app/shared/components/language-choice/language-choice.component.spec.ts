import { TranslateService } from '@ngx-translate/core';
import { Observable, of } from 'rxjs';
import { LanguageChoiceComponent } from './language-choice.component';

describe('LanguageChoiceComponent', () => {
  let component: LanguageChoiceComponent;
  let translateService: TranslateService;

  beforeEach(() => {
    translateService = <any>{
      setDefaultLang: function(language: string) { },
      use: (language: string) => of({})
    };

    component = new LanguageChoiceComponent(translateService);
  });

  it('should set default language on init', () => {
    jest.spyOn(translateService, 'setDefaultLang');

    component.ngOnInit();

    expect(component.selected).toBe('de-DE');
    expect(translateService.setDefaultLang).toHaveBeenCalledWith('de-DE');
  });

  it('should change the language with onLanguageChange', () => {
    jest.spyOn(translateService, 'use');

    component.onLanguageChange({ value: 'de-DE' });

    expect(translateService.use).toHaveBeenCalledWith('de-DE');
  });
});
