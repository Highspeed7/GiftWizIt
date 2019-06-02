export class SharedList {
  public giftList: GiftList;
}

class GiftList {
  public id: number;
  public name: string;
  public giftItems: Items[];
}

class Items {
  public item_Id: number;
  public item: Item
}

class Item {
  public name: string;
  public image: string;
  public linkItemPartners: Link[]; 
}

class Link {
  public affliateLink: string;
}
