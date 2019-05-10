import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {

  public wishList: any = null

  constructor(private http: HttpClient) { }

  ngOnInit() {
    // TODO: Move to service.
    this.http.get(`https://localhost:44327/api/WishList`, { headers: { 'Authorization': `bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vMmM1ZTA3YzctM2ZmNS00OTQ1LWI1M2ItMTA4OGFjNTc1OTdkL3YyLjAvIiwiZXhwIjoxNTU3NDgyNDkxLCJuYmYiOjE1NTc0Nzg4OTEsImF1ZCI6IjYxNmI4MDY4LWVlMDAtNDY4My1iNDA1LTNkYjI4M2U2MDdlOCIsIm9pZCI6ImY0NDhjNDgyLTFkYzAtNGJlOC05ODQxLWJiOTIwNzMxZDc0OCIsInN1YiI6ImY0NDhjNDgyLTFkYzAtNGJlOC05ODQxLWJiOTIwNzMxZDc0OCIsIm5hbWUiOiJMYXVyZW4gV2VzdCIsInRmcCI6IkIyQ18xX1NpZ25pblNpZ251cDEiLCJub25jZSI6IjAwYWRkMDY0LWFjMmYtNDA0NC04OWU4LTc3NGRjODZiNzc2NCIsInNjcCI6InVzZXJfaW1wZXJzb25hdGlvbiByZWFkIiwiYXpwIjoiYWEyZjk5ZWYtMTBhZS00MjE5LWFlMDctMjI0ZDg4MjAzMTUyIiwidmVyIjoiMS4wIiwiaWF0IjoxNTU3NDc4ODkxfQ.VVG2fF9T1GgJmCPkq-SuZpkJZpjPiZ8mJvki-DgclgmesZdgAnijNRhhzMdm6FudTo9DSzTqm3vPJbg9l9vtdG36CmBRsWVoKoCtKo4XSjZ06Ddkdl-YxqP8rXoir_C4Z-6bef4aME3PqVkHh-0NEOj5YCFwIg1hfmXD3jyaS769rtUmiTGfa6zMBhjH7L4T2EUx1PFnXi22ixt2yUqSov4eX6EonFSowCdjGYJ0xGQmnzdtGiVUcg8YPXCQ1srCAPkuOVR9JLrB-QfEOsX0X24CYAULlQffAXXdWotTGsG0wxs5rhuJCLn5GWoJ_E3Gr01CO2eYgSgeyavln6e3zg` } })
      .subscribe((data) => {
      this.wishList = data;
    });
  }

}
