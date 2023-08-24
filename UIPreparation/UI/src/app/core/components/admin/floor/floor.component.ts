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
import { AlertifyService } from 'app/core/services/Alertify.service';
import { Router } from '@angular/router';
import { AuthService } from '../login/services/auth.service';
declare var jQuery: any;

@Component({
  selector: 'app-floor',
  templateUrl: './floor.component.html',
  styleUrls: ['./floor.component.css']
})
export class FloorComponent implements OnInit {



  constructor(private groupService:GroupService,private router: Router,private alertifyService:AlertifyService,private orderService:OrderService,private floorService:FloorService,private machineService:MachineService,private formBuilder: FormBuilder,private authService:AuthService) { }
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

  selectedOrder:Order[];
  selectedOrderNumber:string;

  

  ngOnInit(): void {

    this.authService.getCurrentUserId();

    this.createFloorAddForm();

    

    this.getGroupUsers();
  

    this.getorderNumber();

    this.getMachineType();
 
   
 
    this.getManagers();

    
  
  }


  private _filter(value: string): LookUp[] {
    const filterValue = value.toLowerCase();
    return this.machineUserSelectedItems.filter(option => option.label.toLowerCase().includes(filterValue));
  }
  private _filterManager(value: string): LookUp[] {
    const filterValue = value.toLowerCase();

    return this.managerSelectedItems.filter(option => option.label.toLowerCase().includes(filterValue));
    
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
  //  const selectedLabel = this.orderList.find(number => number.id === selectedId)?.orderNumber;
   this.selectedOrder = this.orderList.filter(x => x.id == selectedId);

  // this.floorAddForm.controls['orderNames'].setValue(selectedLabel);
  // this.floor.orderName=selectedLabel;
  this.selectedOrder.forEach((item)=>{
    this.selectedOrderNumber=item.orderNumber
  });
  
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
        });
        this.filteredWashingMachineTypeItems = this.floorAddForm.controls.washingMachine.valueChanges.pipe(
          startWith(''),
          map(value => typeof value === 'string' ? value : value?.label),
          map(name => name ? this._filterWashingMachineType(name) : this.machineWashingTypeItems.slice())
        );}
        if (element.machineType === "Kurulama") {
          this.machineDryingTypeItems.push({
            id: element.id,
            label: element.machineName
          });
          this.filteredDryingMachineTypeItems = this.floorAddForm.controls.dryingMachine.valueChanges.pipe(
            startWith(''),
            map(value => typeof value === 'string' ? value : value?.label),
            map(name => name ? this._filterDryingMachineType(name) : this.machineDryingTypeItems.slice())
          );
        }
        if (element.machineType === "Sıkma") {
          this.machineSquezeeTypeItems.push({
            id: element.id,
            label: element.machineName
          });
          this.filteredSquezeeMachineTypeItems = this.floorAddForm.controls.squeezMachine.valueChanges.pipe(
            startWith(''),
            map(value => typeof value === 'string' ? value : value?.label),
            map(name => name ? this._filterSquezeeMachineType(name) : this.machineSquezeeTypeItems.slice())
          );
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

         this.filteredMachineItems = this.floorAddForm.controls.machineEmployee.valueChanges.pipe(
      startWith(''),
        map(value => typeof value === 'string' ? value : value?.label),

				map(name => name ? this._filter(name) : this.machineUserSelectedItems.slice())
     );
     
   })
  }

  getManagers(){
    this.groupService.getGroupUsers(2).subscribe(data => {
      this.managerSelectedItems = data;


      this.filteredManagerItems = this.floorAddForm.controls.managerName.valueChanges.pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value?.label),

				map(name => name ? this._filterManager(name) : this.managerSelectedItems.slice())
      );
      
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
    console.log("çalıştı");
    
  
    this.floor.createdUserId=this.authService.getCurrentUserId();
    this.floor.orderName=this.selectedOrderNumber;

		this.floorService.addWashingControll_Floor(this.floor).subscribe(data => {
			this.floor = new WashingControll_Floor();
      jQuery('#order').modal('hide');
			this.clearFormGroup(this.floorAddForm);
      this.router.navigateByUrl("/floorcontroll");


		},(error)=>{
      this.alertifyService.error(error.error)
    })

	}
  save(){

		 if (this.floorAddForm.valid) {
		 	this.floor = Object.assign({}, this.floorAddForm.value)
      

				this.addFloor();

      
			
		}
    else{
      this.alertifyService.error("Boş olan verileri doldurun!")

    }

	}
  toggleJobRotation(event: any) {
    const isChecked = event.checked;
    console.log(event.checked);
    
  
    if (event.checked==true) {
      this.floorAddForm.get('jobRotation').setValue('Gündüz vardiyesi');
    } else {
      this.floorAddForm.get('jobRotation').setValue('Gece vardiyesi');
    }
  }


  
  

  createFloorAddForm() {
		this.floorAddForm = this.formBuilder.group({		
			id : [0],
      machineEmployee : ["", Validators.required],
orderNames : ["", Validators.required],
washingMachine: ["", Validators.required],
dryingMachine: ["", Validators.required],
squeezMachine: ["", Validators.required],
managerName : ["", Validators.required],
sumProductAmount:[0, Validators.required],
brendaNumber : ["", Validators.required],
jobRotation: ["false", Validators.required],
		})
	}


}
