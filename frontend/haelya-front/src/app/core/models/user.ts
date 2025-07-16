import { UserRole } from "./user-role";

export interface User{
    id: number;
    email : string;
    lastname : string;
    firstname : string;
    phonenumber : string | null;
    birthdate : Date | null;
    registerdate : Date;
    role : UserRole;
}