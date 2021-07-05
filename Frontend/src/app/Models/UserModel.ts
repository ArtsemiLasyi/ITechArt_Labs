import { UserRoles } from "./UserRoles";

export class UserModel {
    id : number = 0;
    email : string = '';
    role : number = UserRoles.User;
}
