import { Injectable } from "@angular/core";

@Injectable()
export class PageService {

    readonly DEFAULT_PAGE_SIZE : number = 10;
    readonly INITIAL_PAGE_NUMBER : number = 1;
    
    private pageSize : number;
    private pageNumber : number;


    constructor () {
        this.pageSize = this.DEFAULT_PAGE_SIZE;
        this.pageNumber = this.INITIAL_PAGE_NUMBER;
    }

    nextPage() {
        this.pageNumber++;
    }

    setPageSize(pageSize : number) {
        this.pageSize = pageSize;
    }

    previousPage() {
        if (this.pageNumber !== this.INITIAL_PAGE_NUMBER) {
            this.pageNumber--;
        }
    }

    clearPageNumber() {
        this.pageNumber = this.INITIAL_PAGE_NUMBER;
    }

    setPageNumber(pageNumber : number) {
        if (pageNumber >= this.INITIAL_PAGE_NUMBER) {
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