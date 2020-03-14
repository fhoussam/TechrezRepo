import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;

  constructor(private http: HttpClient) { }

  public incrementCounter() {
    this.currentCount++;
  }

  public challengeAngularUser() {
    window.location.replace('api/security/challengeAngularUser');
  }

  public getProducts() {
    this.http.get('api/products').subscribe(x => {
      console.log(x);
    });
  }
}
