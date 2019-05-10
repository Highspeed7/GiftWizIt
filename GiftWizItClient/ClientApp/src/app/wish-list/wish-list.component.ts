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
    this.http.get(`https://localhost:44327/api/WishList`, { headers: { 'Authorization': `bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vMmM1ZTA3YzctM2ZmNS00OTQ1LWI1M2ItMTA4OGFjNTc1OTdkL3YyLjAvIiwiZXhwIjoxNTU3NDYyOTEzLCJuYmYiOjE1NTc0NTkzMTMsImF1ZCI6IjYxNmI4MDY4LWVlMDAtNDY4My1iNDA1LTNkYjI4M2U2MDdlOCIsIm5hbWUiOiJCcmlhbiBXZXN0IiwiaWRwIjoiZmFjZWJvb2suY29tIiwib2lkIjoiYzJlMmJmZDEtYzNkMy00Yzk5LWFiNjMtOGU4YTAxYTk1ZmE3Iiwic3ViIjoiYzJlMmJmZDEtYzNkMy00Yzk5LWFiNjMtOGU4YTAxYTk1ZmE3IiwidGZwIjoiQjJDXzFfU2lnbmluU2lnbnVwMSIsIm5vbmNlIjoiYWE5YWQ1M2MtZTE1Mi00NzJjLTgyZjktN2RhZmI1YTA4ZTQ3Iiwic2NwIjoidXNlcl9pbXBlcnNvbmF0aW9uIHJlYWQiLCJhenAiOiJhYTJmOTllZi0xMGFlLTQyMTktYWUwNy0yMjRkODgyMDMxNTIiLCJ2ZXIiOiIxLjAiLCJpYXQiOjE1NTc0NTkzMTN9.CqWEOmEBqIkF3kRGShdrx2q-YrMZzTR25i36K1rYM9A5OmJeMOW1Yv6IdCgoJiDAtfap4j5VXZAdUEDWvBXRPmrfrr8Y7caEGbmDPaHYB3nJ0ay7LaKEJpktbTR-Qpohz7H3X4IcjJoWNlKb5xfgEDredgxB3UIhQWy4cC6vlrKIxsRlMjwFRHDYwzwUZlOp8cmuYdHpzfBLLbUskOc6wIlJfm1KFZHvMpAWs7VUIlYQu_8ET0E3DreFT3vzi7IQwekdqk1fnVZVZ2exxS4P7AlABcv5BHBxHOMWkQCslDmXqNdF5j_85eS317vJBXX7yaWd4V_AJby2P1GnvuYxFg` } })
      .subscribe((data) => {
      this.wishList = data;
    });
  }

}
