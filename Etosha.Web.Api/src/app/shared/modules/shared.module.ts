import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from './material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        CommonModule,
        TranslateModule,
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
