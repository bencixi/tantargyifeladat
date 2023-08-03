import { MainModel } from "./main-model";
import { Role } from "./role";

export interface User extends MainModel {
    id: string;
    email: string;
    name: string;
    fullName: string;
    role: Role;
    password: string;
    theme: string;
}