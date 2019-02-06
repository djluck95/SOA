import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DecimalPipe } from '@angular/common';
import { DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { GridJoggingComponent } from './grid-jogging/grid-jogging.component';
import { AddOrUpdateJoggingComponent } from './add-or-update-jogging/add-or-update-jogging.component';

import { WorkoutService } from './workout.service';

import {
  OktaAuthModule,
  OktaCallbackComponent,
  OktaAuthGuard
} from '@okta/okta-angular';
import * as _ from 'lodash';
import { HomeComponent } from './home/home.component';





const appRoutes: Routes = [
  { path: '', component: HomeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    GridJoggingComponent,
    AddOrUpdateJoggingComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
  ],
  providers: [WorkoutService],
  bootstrap: [AppComponent]
})
export class AppModule { }
