
import { Observable,  throwError as observableThrowError } from 'rxjs';

export abstract class BaseService {
    constructor() { }

    protected handleError(error: any) {
        const applicationError = error.headers.get('Application-Error');

        if (applicationError) {
            return observableThrowError(applicationError);
        }

        let modelStateErrors = '';
        const serverError = error.error;

        if (!serverError.type) {
            for (const key in serverError) {
                if (serverError[key]) {
                    modelStateErrors += serverError[key] + '\n';
                }
            }
        }

        modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
        return observableThrowError(modelStateErrors || 'Server error');
    }
}
