export class Utilities {
  public isEmpty(obj) {
    return Object.keys(obj).length === 0 && obj.constructor === Object
  }
}
