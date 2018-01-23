import { AdviseService } from './advise.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { AdviseComponent } from './advise.component';

describe('AdviseComponent', () => {
    let adviseService: AdviseService;
    let component: AdviseComponent;

    adviseService = <any>{
        getAdvice: () => Observable.of({})
    };

    beforeEach(() => {
        component = new AdviseComponent(adviseService);
    });

    it('should init an advice', () => {
        spyOn(adviseService, 'getAdvice').and.returnValue(Observable.of({}));
        component.ngOnInit();
        expect(adviseService.getAdvice).toHaveBeenCalled();
    });
});
