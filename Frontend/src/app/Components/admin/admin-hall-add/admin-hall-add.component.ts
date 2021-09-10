import { Component } from "@angular/core";
import { ErrorModel } from "src/app/Models/ErrorModel";
import { HallModel } from "src/app/Models/HallModel";
import { SuccessModel } from "src/app/Models/SuccessModel";
import { HallRequest } from "src/app/Requests/HallRequest";
import { HallService } from "src/app/Services/HallService";

@Component({
    selector: 'admin-hall-add',
    templateUrl: './admin-hall-add.component.html',
    providers: [HallService]
})
export class AdminHallAddComponent {

    readonly defaultFileName : string = "Add photo";

    photo : File | undefined;
    model : HallModel = new HallModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();
    selectedFileName : string = this.defaultFileName;

    constructor(private hallService : HallService) { }

    loadPhoto(event : any) : any {
        this.photo = event.target.files[0];
        if (this.photo === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.photo.name; 
    }

    addHall() {
        const request = new HallRequest(
            this.model.cinemaId,
            this.model.name,
        );
        this.hallService.addHall(request).subscribe(
            async (data : any) => {
                const id = data.id;
                const formData = new FormData();
                formData.append("formFile", this.photo!);  
                await this.hallService.addPhoto(id, formData);
                this.success.flag = true;
            }
        )
    }
}