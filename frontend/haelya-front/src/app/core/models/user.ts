import { UserRole } from "./user-role";

export interface User{
    id: number;
    email : string;
    lastName : string;
    firstName : string;
    phoneNumber : string | null;
    birthDate : Date | null;
    registerDate : Date;
    role : UserRole;
}