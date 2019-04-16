
import { AuthService } from './_service/Auth.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';



import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';


import { ErrorInterceptorProvide } from './_service/error.intercepter';





@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      HomeComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvide

   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
