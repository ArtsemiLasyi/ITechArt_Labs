import { Component } from '@angular/core';
import { HallService } from 'src/app/Services/HallService';

@Component({
    selector: 'admin-hall-info',
    templateUrl: './admin-hall-info.component.html',
    providers: [HallService]
})
export class AdminHallInfoComponent {

}
