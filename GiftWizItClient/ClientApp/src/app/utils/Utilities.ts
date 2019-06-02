export class Utilities {
  public isEmpty(obj) {
    return Object.keys(obj).length === 0 && obj.constructor === Object
  }

  public arrayRemove(arr, value) {
    return arr.filter((elem) => {
      return elem != value;
    })
  }

  public getCurrTimeInSeconds() {
    return Math.round(Date.now() / 1000);
  }
}
