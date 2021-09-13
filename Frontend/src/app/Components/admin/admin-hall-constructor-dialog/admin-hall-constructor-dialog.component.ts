import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
    selector: 'admin-hall-constructor-dialog',
    templateUrl: './admin-hall-constructor-dialog.component.html',
    providers: []
})
export class AdminHallConstructorDialogComponent {
    
    constructor(
        public dialogRef: MatDialogRef<AdminHallConstructorDialogComponent>) {}
    
    onNoClick(): void {
        this.dialogRef.close();
    }
}