import * as Config from '../../../config.json';
import { UrlSegments } from './UrlSegments';

export class ApiUrls {
    
    static readonly Users = Config.ApiUrl + UrlSegments.Users;
    static readonly Films = Config.ApiUrl + UrlSegments.Films;
    static readonly Cities = Config.ApiUrl + UrlSegments.Cities;
    static readonly Cinemas = Config.ApiUrl + UrlSegments.Cinemas;
    static readonly Halls = Config.ApiUrl + UrlSegments.Halls;
    static readonly SignIn = Config.ApiUrl 
        + UrlSegments.Authentification
        + UrlSegments.SignIn;
    static readonly SignUp = Config.ApiUrl 
        + UrlSegments.Authentification
        + UrlSegments.SignUp;
}