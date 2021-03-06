import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
 flag = false;
 values: any = {};
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  regiToggle() {
    this.flag = true;
  }
  getValues() {
    this.http.get( 'http://localhost:5000/api/values' ).subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

  cancelRegiEmit(flag: boolean) {
  this.flag = flag;
  }

}
