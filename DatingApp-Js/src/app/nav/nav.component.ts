import { Component, OnInit } from '@angular/core';
import { AuthService } from './../_service/Auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
 model: any = {};
  constructor(private auth: AuthService) { }

  ngOnInit() {
  }

  login() {
    // console.log("OK");
    this.auth.login(this.model).subscribe(next => {
      console.log('Successfully loged in....');
    }, error => {
      console.log(error);
    });
  }

  logedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logedOut() {
    console.log('All Ok.......');
    localStorage.removeItem('token');

  }
}
