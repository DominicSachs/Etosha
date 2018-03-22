import { NgModule } from '@angular/core';
import {
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatSidenavModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule
} from '@angular/material';

@NgModule({
    imports: [
        MatButtonModule,
        MatCardModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatSidenavModule,
        MatSortModule,
        MatTableModule,
        MatToolbarModule
    ],
    exports: [
        MatButtonModule,
        MatCardModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatSidenavModule,
        MatSortModule,
        MatTableModule,
        MatToolbarModule
    ]
})
export class MaterialModule { }
