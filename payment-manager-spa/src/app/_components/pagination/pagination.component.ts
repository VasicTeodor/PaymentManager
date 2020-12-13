import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {
  @Input() totalItems: any;
  @Input() pageSize: any;
  @Input() currentPage: any;
  @Output() pageChanged: EventEmitter<any>  = new EventEmitter();

  totalPages: number = 0;
  startDocNum: number = 0;
  endDocNum: number = 0;
  pages: any;
  canShowNewPagination: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  getItems(): any {
    this.pageChanged.emit({
      contentPage: this.currentPage,
     });
  }

  getPage(pageNumber: number) {
    this.currentPage = pageNumber;
    this.getItems();
  }

  init() {
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
    this.startDocNum = ((this.pageSize * this.currentPage) - this.pageSize);
    this.startDocNum = this.startDocNum === 0 ? 1 : this.startDocNum;
    this.endDocNum = ((this.pageSize * this.currentPage) > this.totalItems) ? this.totalItems : (this.pageSize * this.currentPage);

    let startPage: number;
    let endPage: number;
    let startDots: boolean;
    let endDots: boolean;

    if (this.totalPages <= 10) {
      startPage = 1;
      endPage = this.totalPages;
      startDots = false;
      endDots = false;
    } else {
      if (this.currentPage - 6 >= 1) {
          startPage = this.currentPage - 4;
          startDots = true;
      } else {
          startPage = 1;
          startDots = false;
      }
      if (this.currentPage + 6 <= this.totalPages) {
          endPage = this.currentPage + 4;
          endDots = true;
      } else {
          endPage = this.totalPages;
          endDots = false;
      }
    }

    this.pages = this.range(startPage, endPage, 1);
  }

  range(startValue: number, endValue: number, step: number) {
    endValue += 1;
    return Array(Math.ceil((endValue - startValue) / step)).fill(startValue).map((x, y) => x + y * step);
  }

}
