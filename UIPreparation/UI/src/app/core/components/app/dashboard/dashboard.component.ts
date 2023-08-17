import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import * as Chartist from 'chartist';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  navigateToFloor(){
    this.router.navigateByUrl("/floor");
  }
}
