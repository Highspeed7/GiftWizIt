export class GuestInfo {
  constructor() {
    this.lists = [];
  }
  public name: string;
  public email: string;
  public lists: GuestList[];
}

export class GuestList {
  public giftListId: number;
  public giftListName: string;
  public giftListPass: string;
  public dataExpireTime: number;
  public storedTime: number;
}
