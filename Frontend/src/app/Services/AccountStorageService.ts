import { Injectable } from "@angular/core";
import { StorageKeyNames } from "../Constants/StorageKeyNames";
import { CityModel } from "../Models/CityModel";

@Injectable()
export class AccountStorageService {

    saveToken(token : string) {
        localStorage.setItem(StorageKeyNames.TOKEN, token);
    }

    deleteToken() {
        localStorage.removeItem(StorageKeyNames.TOKEN);
    }
}