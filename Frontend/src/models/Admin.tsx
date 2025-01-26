import { List } from "immutable";

export interface AdminData {
    adminId: number;
    username: string;
    //password: string;
    email: string;
}

export interface AdminDataWrapper {
    adminData: AdminData
}
