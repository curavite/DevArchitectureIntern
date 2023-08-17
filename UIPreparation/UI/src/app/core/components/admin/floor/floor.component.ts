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

  machineTypeItems:LookUp[]=[];
  filteredMachineTypeItems: Observable<LookUp[]>;

  orderNumber:LookUp[]=[];
  filteredOrderItems: Observable<LookUp[]>;

  
  machineUserSelectedItems: LookUp[] = []; 
  filteredMachineItems: Observable<LookUp[]>;

  
  managerSelectedItems: LookUp[] = []; 
  filteredManagerItems: Observable<LookUp[]>;

  floorAddForm: FormGroup;


  groupUsers = new FormControl();
  orderNames = new FormControl();
  machineTypes = new FormControl();
  manager = new FormControl();


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
    this.getOrderMachineType();
    this.filteredMachineTypeItems = this.floorAddForm.controls.machineTypes.valueChanges.pipe(
      startWith(''),
      map(value => this._filterMachineType(value || '')),
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

  private _filterMachineType(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.machineTypeItems.filter(option => option.label.toLowerCase().includes(filterValue));
    
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
    console.log(this.selectedOrderName);
  });
}
  


  getOrderMachineType(){
    this.machineService.getMachineList().subscribe((data)=>{
      this.machineList=data;
      this.machineList.forEach((element)=>{
        this.machineTypeItems.push({
          id: element.id,
          label:element.machineType
        })
      })
    })
    console.log(this.machineTypeItems);
    
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
      console.log(this.orderNumber);

    })
    
   
  }

  getGroupUsers(){
    
   this.groupService.getGroupUsers(1).subscribe(data => {
     this.machineUserSelectedItems = data;
     console.log(this.machineUserSelectedItems);
     
   })
  }

  getManagers(){
    this.groupService.getGroupUsers(2).subscribe(data => {
      this.managerSelectedItems = data;
      console.log(this.managerSelectedItems);
      
      
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
    console.log(this.floor);
    this.floor.createdUserId=1;
    this.floor.managerName=this.floorAddForm.controls.manager.value;
    this.floor.jobRotation="Gündüz";
    this.floor.machineType=this.floorAddForm.controls.machineTypes.value;
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
      console.log(this.floor);
      

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
machineTypes: ["", Validators.required],
manager : ["", Validators.required],
sumProductAmount:[0, Validators.required],
brendaNumber : ["", Validators.required],

		})
	}


}
