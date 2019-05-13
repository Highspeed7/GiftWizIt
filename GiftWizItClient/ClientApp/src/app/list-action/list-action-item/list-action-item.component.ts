import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChange,
  SimpleChanges
} from '@angular/core';

@Component({
  selector: 'gw-list-action-item',
  templateUrl: './list-action-item.component.html',
  styleUrls: ['./list-action-item.component.css']
})
export class ListActionItemComponent implements OnInit {
    

  @Input()
  icon: string;

  @Input()
  title: string;

  @Input('isActive')
  public isActive: boolean;

  @Output()
  onActionClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
    console.log()
  }

  public actionClicked(evt: Event) {
    this.onActionClicked.emit({ 'action': this.title });
  }
}
