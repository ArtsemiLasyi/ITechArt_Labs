import { SessionSeatStatuses } from "./SessionSeatStatuses";

export class SessionSeatModel {
    userId : number = 0;
    seatId : number = 0;
    sessionId : number = 0;
    row : number = 0;
    place : number = 0;
    seatTypeId : number = 0;
    status : SessionSeatStatuses = SessionSeatStatuses.Free;
}