import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[gwInsertion]'
})
export class InsertionDirective {
  constructor(public viewContainerRef: ViewContainerRef) { }
}
