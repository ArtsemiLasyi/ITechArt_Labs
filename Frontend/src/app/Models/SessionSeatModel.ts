import { SessionSeatStatuses } from "./SessionSeatStatuses";

export class SessionSeatModel {
    seatId : number = 0;
    sessionId : number = 0;
    status : SessionSeatStatuses = SessionSeatStatuses.Free;
}