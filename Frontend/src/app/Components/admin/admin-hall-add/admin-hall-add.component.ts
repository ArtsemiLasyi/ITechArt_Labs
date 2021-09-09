import { Component } from "@angular/core";
import { HallService } from "src/app/Services/HallService";

@Component({
    selector: 'admin-hall-add',
    templateUrl: './admin-hall-add.component.html',
    providers: [HallService]
})
export class AdminHallAddComponent {

    readonly defaultFileName : string = "Add photo";

    photo : File | undefined;
    model : FilmModel = new FilmModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();
    selectedFileName : string = this.defaultFileName;

    constructor(private filmService : FilmService) { }

    loadPhoto(event : any) : any {
        this.photo = event.target.files[0];
        if (this.photo === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.photo.name; 
    }

    addHall() {
        const request = new FilmRequest(
            this.model.name,
            this.model.description,
            this.model.durationInMinutes,
            this.model.releaseYear
        );
        this.filmService.addFilm(request).subscribe(
            async (data : any) => {
                const id = data.id;
                const formData = new FormData();
                formData.append("formFile", this.photo!);  
                await this.filmService.addPoster(id, formData);
                this.success.flag = true;
            }
        )
    }
}