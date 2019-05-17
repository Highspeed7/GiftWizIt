export class GiftList {
  constructor(name: string, userId: string) {
    this.name = name;
    this.userId = userId;
  }
  public id: number;
  public name: string;
  public giftItems: GiftItemQuery[];
  public userId?: string;
  public isExpanded: boolean;
}

export class GiftItemQuery {
  public item_Id: number;
  public gift_List_Id: number;
  public partner_Id?: number;
  public afflt_Link?: string;
  public itm_Name?: string;
  public glst_Name?: string;
  public image?: string;
}
