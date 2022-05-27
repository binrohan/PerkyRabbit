import { BaseModel } from "./BaseModel";
import { Ticket } from "./Ticket";

export interface TicketDialogResult extends BaseModel, Ticket {
    isSucceeded: boolean;
}
