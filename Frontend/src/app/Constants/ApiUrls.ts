import * as Config from '../../../config.json';

export class ApiUrls {

    private static readonly _users : string = '/users';
    private static readonly _cities : string = '/cities';
    private static readonly _films : string = '/films';
    private static readonly _authentification : string = '/account';
    private static readonly _signIn : string = '/signin';
    private static readonly _signUp : string = '/signup';
    
    static readonly Users = Config.ApiUrl + ApiUrls._users;
    static readonly Films = Config.ApiUrl + ApiUrls._films;
    static readonly Cities = Config.ApiUrl + ApiUrls._cities;
    static readonly SignIn = Config.ApiUrl 
        + ApiUrls._authentification
        + ApiUrls._signIn;
    static readonly SignUp = Config.ApiUrl 
        + ApiUrls._authentification
        + ApiUrls._signUp;
}