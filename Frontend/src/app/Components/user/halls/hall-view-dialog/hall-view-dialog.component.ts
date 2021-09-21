import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HallModel } from 'src/app/Models/HallModel';

@Component({
    selector : 'hall-view-dialog',
    templateUrl : './hall-view-dialog.component.html',
    providers : []
})
export class HallViewDialogComponent {
    
    constructor(
        public dialogRef : MatDialogRef<HallViewDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private model : HallModel
    ) { }
    
    onNoClick(): void {
        this.dialogRef.close();
    }
}