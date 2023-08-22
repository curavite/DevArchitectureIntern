import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Error } from './models/error';
import { ErrorService } from './services/error.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-error',
	templateUrl: './error.component.html',
	styleUrls: ['./error.component.scss']
})
export class ErrorComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['createdDate','errorName','isError','rowNumber','departmant','colorCode', 'update','delete'];

	errorList:Error[];
	error:Error=new Error();

	errorAddForm: FormGroup;
	selectedColor: string = "#ffffff";

	errorId:number;

	constructor(private errorService:ErrorService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getErrorList();
    }

	ngOnInit() {

		this.createErrorAddForm();
	}


	getErrorList() {
		this.errorService.getErrorList().subscribe(data => {
			this.errorList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}
	updateBackgroundColor() {
		this.selectedColor = this.errorAddForm.get('colorCode').value;
	  }

	save(){
		if (this.errorAddForm.valid) {
			this.error = Object.assign({}, this.errorAddForm.value)

			if (this.error.id == 0)
				this.addError();
			else
				this.updateError();
		}

	}

	addError(){

		this.errorService.addError(this.error).subscribe(data => {
			this.getErrorList();
			this.error = new Error();
			jQuery('#error').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.errorAddForm);

		})

	}

	updateError(){

		this.errorService.updateError(this.error).subscribe(data => {

			var index=this.errorList.findIndex(x=>x.id==this.error.id);
			this.errorList[index]=this.error;
			this.dataSource = new MatTableDataSource(this.errorList);
            this.configDataTable();
			this.error = new Error();
			jQuery('#error').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.errorAddForm);

		})

	}

	createErrorAddForm() {
		this.errorAddForm = this.formBuilder.group({		
			id : [0],

errorName : ["", Validators.required],
isError : [false],
rowNumber : [0, Validators.required],
departmant : ["", Validators.required],
colorCode : ["", Validators.required]
		})
	}

	deleteError(errorId:number){
		this.errorService.deleteError(errorId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.errorList=this.errorList.filter(x=> x.id!=errorId);
			this.dataSource = new MatTableDataSource(this.errorList);
			this.configDataTable();
		})
	}

	getErrorById(errorId:number){
		this.clearFormGroup(this.errorAddForm);
		this.errorService.getErrorById(errorId).subscribe(data=>{
			this.error=data;
			this.errorAddForm.patchValue(data);
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
