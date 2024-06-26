export class HallDrawingParameters {
    static readonly CAPTION_COLOR_RGB : string = 'rgb(0, 0, 200)';
    static readonly COMMON_COLOR_RGB : string = '#000000';
    static readonly MAX_ANGLE : number = 2 * Math.PI;
    static readonly MIN_ANGLE : number = 0;
    static readonly FONT : string = 'roboto';
    static readonly SEAT_RADIUS : number = HallDrawingParameters.getRadius();
    static readonly EMPTY_PLACE_RADIUS : number = 5;
    static readonly INDENT_BETWEEN_SEATS : number = HallDrawingParameters.SEAT_RADIUS * 2;
    static readonly INDENT_BETWEEN_SEAT_AND_HALL_BORDER = HallDrawingParameters.SEAT_RADIUS * 2;

    static getRadius() : number {
        if (window.innerWidth > 1000) {
            return 20;
        }
        if (window.innerWidth > 300) {
            return 10;
        }
        return 5;
    }
}