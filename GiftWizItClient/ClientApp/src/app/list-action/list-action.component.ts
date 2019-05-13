import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'gw-list-action',
  templateUrl: './list-action.component.html',
  styleUrls: ['./list-action.component.css']
})
export class ListActionComponent implements OnInit {

  public moveActionActive: boolean = false;
  public trashActionActive: boolean = false;

  @Output()
  onActionClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  public actionClicked(linkInfo: any) {
    console.log("Action clicked");
    switch (linkInfo.action) {
      case "Move": {
        this.trashActionActive = false;
        this.moveActionActive = true;
        break;
      }
      case "Delete": {
        this.moveActionActive = false;
        this.trashActionActive = true;
        break;
      }
      case "Cancel": {
        this.moveActionActive = false;
        this.trashActionActive = false;
        break;
      }
    }
    this.onActionClicked.emit(linkInfo);
  }
}
