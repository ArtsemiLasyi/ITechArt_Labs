import { Injectable } from "@angular/core";
import { StorageKeyNames } from "../Constants/StorageKeyNames";
import { CityModel } from "../Models/CityModel";

@Injectable()
export class StorageService {

    getCurrentCity() : CityModel | null {
        let name = sessionStorage.getItem(StorageKeyNames.CURRENT_CITY_NAME);
        let id = sessionStorage.getItem(StorageKeyNames.CURRENT_CITY_ID);
        if (id && name) {
            let model = new CityModel();
            model.name = name;
            model.id = Number.parseInt(id);
            if (!isNaN(model.id)) {
                return model;
            }
        }
        return null;
    }

    setNewCity(city : CityModel) {
        sessionStorage.setItem(StorageKeyNames.CURRENT_CITY_NAME, city.name);
        sessionStorage.setItem(StorageKeyNames.CURRENT_CITY_ID, city.id.toString());
    }
}