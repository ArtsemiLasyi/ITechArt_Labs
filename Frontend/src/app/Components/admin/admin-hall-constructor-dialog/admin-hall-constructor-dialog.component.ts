import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { NgbModalConfig } from "@ng-bootstrap/ng-bootstrap";

@Component({
    selector : 'admin-hall-constructor-dialog',
    templateUrl : './admin-hall-constructor-dialog.component.html',
    styleUrls : ['./admin-hall-constructor-dialog.component.scss'],
    providers : []
})
export class AdminHallConstructorDialogComponent {
    
    constructor(
        private config : NgbModalConfig,
        private dialogRef : MatDialogRef<AdminHallConstructorDialogComponent>
    ) {
        config.backdrop = 'static';
        config.centered = true;
        config.keyboard = true;
        config.animation = true;
    }
    
    onNoClick(): void {
        this.dialogRef.close();
    }
}