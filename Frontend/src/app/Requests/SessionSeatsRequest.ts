import { SessionSeatRequest } from "./SessionSeatRequest";

export class SessionSeatsRequest {
    value : SessionSeatRequest[];

    constructor(value : SessionSeatRequest[]) {
        this.value = value;
    }
}