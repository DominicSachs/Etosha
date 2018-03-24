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
    MatToolbarModule,
    MatSelectModule
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
        MatToolbarModule,
        MatSelectModule
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
        MatToolbarModule,
        MatSelectModule
    ]
})
export class MaterialModule { }
