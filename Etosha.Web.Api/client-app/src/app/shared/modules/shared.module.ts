import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { MaterialModule } from './material.module';

@NgModule({
    imports: [
        CommonModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient]
            }
        }),
        MaterialModule,
        FlexLayoutModule
    ],
    exports: [
        CommonModule,
        TranslateModule,
        MaterialModule,
        FlexLayoutModule
    ]
})
export class SharedModule { }

export function HttpLoaderFactory(http: HttpClient) {
    return new TranslateHttpLoader(http);
}
