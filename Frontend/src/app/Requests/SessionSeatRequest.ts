export class SessionSeatRequest {
    seatId : number;
    sessionId : number;
    userId : number;

    constructor(seatId : number, sessionId : number, userId : number) {
        this.seatId = seatId;
        this.sessionId = sessionId;
        this.userId = userId;
    }
}