import { UserRoles } from "../Constants/UserRoles";

export class UserModel {
    id : number = 0;
    email : string = "";
    role : number = UserRoles.USER;
}