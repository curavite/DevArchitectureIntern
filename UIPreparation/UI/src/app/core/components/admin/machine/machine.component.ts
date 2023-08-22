import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Machine } from './models/Machine';
import { MachineService } from './services/machine.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-machine',
	templateUrl: './machine.component.html',
	styleUrls: ['./machine.component.scss']
})
export class MachineComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['createdDate','machineName','machineType', 'update','delete'];

	machineList:Machine[];
	machine:Machine=new Machine();

	machineAddForm: FormGroup;


	machineId:number;

	constructor(private machineService:MachineService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getMachineList();
    }

	ngOnInit() {

		this.createMachineAddForm();
	}


	getMachineList() {
		this.machineService.getMachineList().subscribe(data => {
			this.machineList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.machineAddForm.valid) {
			this.machine = Object.assign({}, this.machineAddForm.value)

			if (this.machine.id == 0)
				this.addMachine();
			else
				this.updateMachine();
		}

	}

	addMachine(){

		this.machineService.addMachine(this.machine).subscribe(data => {
			this.getMachineList();
			this.machine = new Machine();
			jQuery('#machine').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.machineAddForm);

		})

	}

	updateMachine(){

		this.machineService.updateMachine(this.machine).subscribe(data => {

			var index=this.machineList.findIndex(x=>x.id==this.machine.id);
			this.machineList[index]=this.machine;
			this.dataSource = new MatTableDataSource(this.machineList);
            this.configDataTable();
			this.machine = new Machine();
			jQuery('#machine').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.machineAddForm);

		})

	}

	createMachineAddForm() {
		this.machineAddForm = this.formBuilder.group({		
			id : [0],
machineName : ["", Validators.required],
machineType : ["", Validators.required]
		})
	}

	deleteMachine(machineId:number){
		this.machineService.deleteMachine(machineId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.machineList=this.machineList.filter(x=> x.id!=machineId);
			this.dataSource = new MatTableDataSource(this.machineList);
			this.configDataTable();
		})
	}

	getMachineById(machineId:number){
		this.clearFormGroup(this.machineAddForm);
		this.machineService.getMachineById(machineId).subscribe(data=>{
			this.machine=data;
			this.machineAddForm.patchValue(data);
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
