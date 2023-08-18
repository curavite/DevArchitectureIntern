import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { WashingControll_FloorControllComponent } from '../washingControll_FloorControll/washingControll_FloorControll.component';

@Component({
  selector: 'app-open-dialog',
  templateUrl: './open-dialog.component.html',
  styleUrls: ['./open-dialog.component.css']
})
export class OpenDialogComponent implements OnInit {

  constructor(private router:Router,private dialogRef: MatDialogRef<WashingControll_FloorControllComponent>) { }

  ngOnInit(): void {
  }

  goToHomePage(){
    this.router.navigateByUrl("/dashboard");
    this.dialogRef.close(); 

  }
  onContinueClicked(){
      this.dialogRef.close(); 

    
  }

}
