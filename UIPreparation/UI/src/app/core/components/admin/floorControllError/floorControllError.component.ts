import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/Alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { FloorControllError } from './models/FloorControllError';
import { FloorControllErrorService } from './services/FloorControllError.service';
import { environment } from 'environments/environment';
import { getuid } from 'process';
import { FloorErrorDto } from './models/floorerrorDto';

declare var jQuery: any;

@Component({
	selector: 'app-floorControllError',
	templateUrl: './floorControllError.component.html',
	styleUrls: ['./floorControllError.component.scss']
})
export class FloorControllErrorComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['createdDate','errorName','amount','wsHfloorControllId', 'update','delete'];


	floorErrorDtoList:FloorErrorDto[];

	floorControllErrorList:FloorControllError[];
	floorControllError:FloorControllError=new FloorControllError();

	floorControllErrorAddForm: FormGroup;



	floorControllErrorId:number;

	constructor(private floorControllErrorService:FloorControllErrorService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getFloorErrorDtoList();
    }

	ngOnInit() {
		const userid=this.authService.getCurrentUserId();

		console.log(this.authService.getCurrentUserId());
		console.log("userid: "+userid);
		this.createFloorControllErrorAddForm();
		this.authService.getCurrentUserId();
		console.log(this.authService.getCurrentUserId());
		console.log("userid: "+userid);
		
		

	}
	getUserıD(){
		this.authService.getCurrentUserId();

	}

	// getFloorControllErrorList() {
	// 	this.floorControllErrorService.getFloorControllErrorList().subscribe(data => {
	// 		this.floorControllErrorList = data;
	// 		this.dataSource = new MatTableDataSource(data);
    //         this.configDataTable();
	// 	});
	// }
	getFloorErrorDtoList() {
		this.floorControllErrorService.getFloorErrorDtoList().subscribe(data => {
			this.floorErrorDtoList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.floorControllErrorAddForm.valid) {
			this.floorControllError = Object.assign({}, this.floorControllErrorAddForm.value)

			if (this.floorControllError.id == 0)
				this.addFloorControllError();
			else
				this.updateFloorControllError();
		}

	}

	addFloorControllError(){

		this.floorControllErrorService.addFloorControllError(this.floorControllError).subscribe(data => {
			this.getFloorErrorDtoList();
			this.floorControllError = new FloorControllError();
			jQuery('#floorcontrollerror').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.floorControllErrorAddForm);

		})

	}

	updateFloorControllError(){

		this.floorControllErrorService.updateFloorControllError(this.floorControllError).subscribe(data => {

			var index=this.floorControllErrorList.findIndex(x=>x.id==this.floorControllError.id);
			this.floorControllErrorList[index]=this.floorControllError;
			this.dataSource = new MatTableDataSource(this.floorControllErrorList);
            this.configDataTable();
			this.floorControllError = new FloorControllError();
			jQuery('#floorcontrollerror').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.floorControllErrorAddForm);

		})

	}

	createFloorControllErrorAddForm() {
		this.floorControllErrorAddForm = this.formBuilder.group({		
			id : [0],
createdUserId : [0, Validators.required],
createdDate : [null, Validators.required],
lastUpdatedUserId : [0, Validators.required],
lastUpdatedDate : [null, Validators.required],
status : [false, Validators.required],
isDeleted : [false, Validators.required],
errorName : ["", Validators.required],
amount : [0, Validators.required],
wSH_floorControllId : [0, Validators.required]
		})
	}


	getFloorControllErrorById(floorControllErrorId:number){
		this.clearFormGroup(this.floorControllErrorAddForm);
		this.floorControllErrorService.getFloorControllErrorById(floorControllErrorId).subscribe(data=>{
			this.floorControllError=data;
			this.floorControllErrorAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
