import { Injectable } from "@angular/core";

@Injectable()
export class PageService {

    readonly DEFAULT_PAGE_SIZE : number = 10;
    
    private pageSize : number;
    private pageNumber : number;


    constructor () {
        this.pageSize = this.DEFAULT_PAGE_SIZE;
        this.pageNumber = 1;
    }

    nextPage() {
        this.pageNumber++;
    }

    setPageSize(pageSize : number) {
        this.pageSize = pageSize;
    }

    previousPage() {
        if (this.pageNumber !== 1) {
            this.pageNumber--;
        }
    }

    setPageNumber(pageNumber : number) {
        if (pageNumber >= 1) {
            this.pageNumber = pageNumber;
        }
    }

    getPageNumber() : number {
        return this.pageNumber;
    }

    getPageSize() : number {
        return this.pageSize;
    }
}