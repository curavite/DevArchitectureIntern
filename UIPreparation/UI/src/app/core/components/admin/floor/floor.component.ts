import { Component, OnInit } from '@angular/core';
import { GroupService } from '../group/Services/group.service';
import { LookUp } from 'app/core/models/lookUp';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { OrderService } from '../order/services/order.service';
import { Order } from '../order/models/order';
import { element } from 'protractor';
import { MachineService } from '../machine/services/machine.service';
import { Machine } from '../machine/models/Machine';
import { FloorService } from './services/wscFloor.service';
import { WashingControll_Floor } from './models/wscFloor';
import { AlertifyService } from 'app/core/services/alertify.service';
import { Router } from '@angular/router';
declare var jQuery: any;

@Component({
  selector: 'app-floor',
  templateUrl: './floor.component.html',
  styleUrls: ['./floor.component.css']
})
export class FloorComponent implements OnInit {



  constructor(private groupService:GroupService,private router: Router,private alertifyService:AlertifyService,private orderService:OrderService,private floorService:FloorService,private machineService:MachineService,private formBuilder: FormBuilder) { }
  selectedOrderId: number;
  selectedOrderName:string;
  selectedOrderMaterialName:string;

  orderList:Order[];
  order:Order=new Order;
  floor:WashingControll_Floor=new WashingControll_Floor;
  machineList:Machine[];

  machineWashingTypeItems:LookUp[]=[];
  filteredWashingMachineTypeItems: Observable<LookUp[]>;

  machineDryingTypeItems:LookUp[]=[];
  filteredDryingMachineTypeItems: Observable<LookUp[]>;

  machineSquezeeTypeItems:LookUp[]=[];
  filteredSquezeeMachineTypeItems: Observable<LookUp[]>;

  orderNumber:LookUp[]=[];
  filteredOrderItems: Observable<LookUp[]>;

  
  machineUserSelectedItems: LookUp[] = []; 
  filteredMachineItems: Observable<LookUp[]>;

  
  managerSelectedItems: LookUp[] = []; 
  filteredManagerItems: Observable<LookUp[]>;

  floorAddForm: FormGroup;


  

  ngOnInit(): void {


    this.createFloorAddForm();

    

    this.getGroupUsers();
    this.filteredMachineItems = this.floorAddForm.controls.groupUsers.valueChanges.pipe(
      startWith(''),
      map(value => this._filterMachinePersonel(value || '')),
    );

    this.getorderNumber();
    this.filteredOrderItems = this.floorAddForm.controls.orderNames.valueChanges.pipe(
      startWith(''),
      map(value => this._filterOrderName(value || '')),
    );
    this.getMachineType();
    this.filteredWashingMachineTypeItems = this.floorAddForm.controls.washingMachine.valueChanges.pipe(
      startWith(''),
      map(value => this._filterWashingMachineType(value || '')),
    );
    this.filteredDryingMachineTypeItems = this.floorAddForm.controls.dryingMachine.valueChanges.pipe(
      startWith(''),
      map(value => this._filterDryingMachineType(value || '')),
    );
    this.filteredSquezeeMachineTypeItems = this.floorAddForm.controls.squeezMachine.valueChanges.pipe(
      startWith(''),
      map(value => this._filterSquezeeMachineType(value || '')),
    );
    this.getManagers();
    this.filteredManagerItems = this.floorAddForm.controls.manager.valueChanges.pipe(
      startWith(''),
      map(value => this._filterManager(value || '')),
    );
  }

  private _filterMachinePersonel(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.machineUserSelectedItems.filter(option => option.label.toLowerCase().includes(filterValue));
    
  }


  private _filterManager(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.managerSelectedItems.filter(option => option.label.toLowerCase().includes(filterValue));
    
  }


  private _filterOrderName(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.orderNumber.filter(option => option.label.toLowerCase().includes(filterValue));
    
  }

  private _filterWashingMachineType(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.machineWashingTypeItems.filter(option => option.label.toLowerCase().includes(filterValue));
    
  }
  private _filterDryingMachineType(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.machineDryingTypeItems.filter(option => option.label.toLowerCase().includes(filterValue));
    
  }
  private _filterSquezeeMachineType(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.machineSquezeeTypeItems.filter(option => option.label.toLowerCase().includes(filterValue));
    
  }
 onOrderOptionClick(event) {
  const selectedId = event.target.value;
  this.getOrderNameById(selectedId);
}

getOrderNameById(id: number) {
  this.orderService.getOrderById(id).subscribe((data) => {
    this.order = data;
    this.selectedOrderName = data.orderModelName;
    this.selectedOrderMaterialName= data.orderMaterialName;
  });
}
  


getMachineType() {
  this.machineService.getMachineList().subscribe((data) => {
    this.machineList = data;
    
    this.machineList.forEach((element) => {
      if (element.machineType === "Yıkama") {
        this.machineWashingTypeItems.push({
          id: element.id,
          label: element.machineName
        });}
        if (element.machineType === "Kurulama") {
          this.machineDryingTypeItems.push({
            id: element.id,
            label: element.machineName
          });
        }
        if (element.machineType === "Sıkma") {
          this.machineSquezeeTypeItems.push({
            id: element.id,
            label: element.machineName
          });
        }
    }) 
    });
  }



  getorderNumber(){
    this.orderService.getOrderList().subscribe((data)=>{
      this.orderList=data;
      this.orderList.forEach((element)=>{
        this.orderNumber.push({
          id: element.id,
          label:element.orderNumber
        })
      })

    })
    
   
  }

  getGroupUsers(){
    
   this.groupService.getGroupUsers(1).subscribe(data => {
     this.machineUserSelectedItems = data;
     
   })
  }

  getManagers(){
    this.groupService.getGroupUsers(2).subscribe(data => {
      this.managerSelectedItems = data;
      
      
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
  



  addFloor(){
    this.floor.createdUserId=1;
    this.floor.managerName=this.floorAddForm.controls.manager.value;
    this.floor.jobRotation="Gündüz";
    this.floor.washingMachine=this.floorAddForm.controls.washingMachine.value;
    this.floor.dryingMachine=this.floorAddForm.controls.dryingMachine.value;
    this.floor.squeezMachine=this.floorAddForm.controls.squeezMachine.value;
    this.floor.machineEmployee=this.floorAddForm.controls.groupUsers.value;
    this.floor.orderName=this.floorAddForm.controls.orderNames.value;
    
		this.floorService.addWashingControll_Floor(this.floor).subscribe(data => {
			this.floor = new WashingControll_Floor();
      jQuery('#order').modal('hide');
			this.clearFormGroup(this.floorAddForm);

		})

	}
  save(){
    
		if (this.floorAddForm.valid) {
			this.floor = Object.assign({}, this.floorAddForm.value)
      

			if (this.floor.id == 0){
				this.addFloor();
        this.router.navigateByUrl("/floorcontroll");

      }
			
		}

	}

  

  createFloorAddForm() {
		this.floorAddForm = this.formBuilder.group({		
			id : [0],
      groupUsers : ["", Validators.required],
orderNames : ["", Validators.required],
washingMachine: ["", Validators.required],
dryingMachine: ["", Validators.required],
squeezMachine: ["", Validators.required],
manager : ["", Validators.required],
sumProductAmount:[0, Validators.required],
brendaNumber : ["", Validators.required],

		})
	}


}
