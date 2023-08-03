import { MainModel } from "./main-model";
import { Permission } from "./permission";
import { User } from "./user";

export interface Role extends MainModel{
    id: string;
    name: string;
    users: User[];
    permissions: Permission[];
}