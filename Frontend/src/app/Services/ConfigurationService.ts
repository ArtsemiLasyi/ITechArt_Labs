import * as Config from '../../../config.json';

export class ConfigurationService {

    private static readonly _users : string = '/users';
    private static readonly _authentification : string = '/account';
    private static readonly _signIn : string = '/signin';
    private static readonly _signUp : string = '/signup';
    
    static readonly URL_USERS = Config.ApiUrl + ConfigurationService._users;
    static readonly URL_SIGNIN = Config.ApiUrl 
        + ConfigurationService._authentification
        + ConfigurationService._signIn;
    static readonly URL_SIGNUP = Config.ApiUrl 
        + ConfigurationService._authentification
        + ConfigurationService._signUp;
}