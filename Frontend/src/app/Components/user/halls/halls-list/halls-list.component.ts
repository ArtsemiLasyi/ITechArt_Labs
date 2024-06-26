import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { HallModel } from 'src/app/Models/HallModel';
import { HallService } from 'src/app/Services/HallService';
import { MatDialog } from '@angular/material/dialog';
import { HallViewDialogComponent } from '../hall-view-dialog/hall-view-dialog.component';

@Component({
    selector : 'halls-list',
    templateUrl : './halls-list.component.html',
    styleUrls : ['./halls-list.component.scss'],
    providers : [
        HallService
    ]
})
export class HallsListComponent implements OnInit {

    halls : Observable<HallModel[]> | undefined;
    @Input() cinemaId = 0;

    constructor (
        private hallService: HallService,
        private dialog: MatDialog
    ) { }

    getHalls(cinemaId : number) {
        this.halls = this.hallService
            .getHalls(cinemaId);
    }

    getPhoto(id : number) {
        return this.hallService.getPhoto(id);
    }

    ngOnInit() {
        this.getHalls(this.cinemaId);
    }

    openDialog(model : HallModel) {
        this.dialog.open(
            HallViewDialogComponent, {
                restoreFocus: true,
                data : { 
                    id : model.id,
                    cinemaId : model.cinemaId,
                    name : model.name 
                }
            }
        );
    }
}
