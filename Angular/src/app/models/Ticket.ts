import { BaseModel } from "./BaseModel";

export interface Ticket extends BaseModel {
    title: string;
    description: string;
    price: number;
}
