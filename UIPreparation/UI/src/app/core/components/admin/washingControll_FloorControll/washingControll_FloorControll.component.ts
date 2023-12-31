import { Component, AfterViewInit, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { LookUpService } from "app/core/services/LookUp.service";
import { AlertifyService } from "app/core/services/Alertify.service";

import { AuthService } from "../login/services/auth.service";
import { WashingControll_FloorControll } from "./models/washingcontroll_floorcontroll";
import { WashingControll_FloorControllService } from "./services/washingcontroll_floorcontroll.service";
import { FloorService } from "../floor/services/wscFloor.service";
import { WashingControll_Floor } from "../floor/models/wscFloor";
import { Error } from "../error/models/error";
import { ErrorService } from "../error/services/error.service";
import { error, log } from "console";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { OpenDialogComponent } from "../open-dialog/open-dialog.component";
import { element } from "protractor";
import { FloorControllError } from "../floorControllError/models/FloorControllError";
import { FloorControllErrorService } from "../floorControllError/services/FloorControllError.service";
import Swal from "sweetalert2";

declare var jQuery: any;

@Component({
  selector: "app-washingControll_FloorControll",
  templateUrl: "./washingControll_FloorControll.component.html",
  styleUrls: ["./washingControll_FloorControll.component.scss"],
})
export class WashingControll_FloorControllComponent
  implements AfterViewInit, OnInit
{
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = ["errorName", "amount", "percent", "delete"];

  washingControll_FloorControllList: WashingControll_FloorControll[];
  washingControll_FloorControll: WashingControll_FloorControll =
    new WashingControll_FloorControll();

  floorListLenght: number = 0;
  floorList: WashingControll_Floor[];


  errorList: Error[];
  errorList0: Error;
  errorListColor0: string;
  errorListisError0: Boolean;
  errorListName0: string;


  
  getTotalCountt: number = 0;

  selectedError: Error;
  selectedErrors: WashingControll_FloorControll[] = [];

  faultyProduct: number = 0;
  totalPercent: number = 0;
  sayacElem: HTMLElement | null = null; // initialize it with null

  orderId: number = 0;

  realSecond: number = 0;
  second: number = 0;
  interval: any;
  elapsedSeconds: number = 0;

  floorControllErrorList: FloorControllError[];
  floorControllError: FloorControllError = new FloorControllError();

  washingControll_FloorControllAddForm: FormGroup;

  washingControll_FloorControllId: number;

  lastFloor: number = 0;
  constructor(
    private errorService: ErrorService,
    private floorService: FloorService,
    private washingControll_FloorControllService: WashingControll_FloorControllService,
    private lookupService: LookUpService,
    private alertifyService: AlertifyService,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private floorControllErrorService: FloorControllErrorService,

    private dialog: MatDialog
  ) {
    this.faultyProduct = 0;
  }

  ngAfterViewInit(): void {}

  ngOnInit() {
    this.startTimer();
    this.getWashingControll_FloorControllList();
    this.getErrorList();
    this.createWashingControll_FloorControllAddForm();
    this.getFloorList();
    this.getTotalCount();
    this.calculateTotal();
    console.log("userid: "+this.authService.getCurrentUserId());
    
  }

  getWashingControll_FloorControllList() {
    this.washingControll_FloorControllService
      .getWashingControll_FloorControllList()
      .subscribe((data) => {
        this.washingControll_FloorControllList = data;
      });
  }

  sortErrorList() {
    return this.errorList.slice(1).sort((a, b) => a.rowNumber - b.rowNumber);
  }

  save() {
    this.washingControll_FloorControll = Object.assign(
      {},
      this.washingControll_FloorControllAddForm.value
    );
    this.washingControll_FloorControll.amount = this.getTotalCount();
    this.washingControll_FloorControll.percent = this.calculateTotal();
    this.washingControll_FloorControll.faultyProduct = this.faultyProduct;
    this.washingControll_FloorControll.orderId = this.orderId;
    this.washingControll_FloorControll.createdUserId=this.authService.getCurrentUserId();

    this.washingControll_FloorControll.controllTime = this.second;

    if (this.washingControll_FloorControll.id == 0)
      this.addWashingControll_FloorControll();
    else this.updateWashingControll_FloorControll();
  }

  getTotalCount(): number {
    return this.selectedErrors.reduce(
      (total, error) => total + error.amount,
      0
    );
  }

  calculateTotal(): number {
    let total = 0;
    this.dataSource.data.forEach((item: any) => {
      total += this.calculatePercent(item);
    });
    return total;
  }

  calculatePercent(element: any): number {
    if (element.isError) {
      const totalAmount = this.getTotalCount();
      const percent = Math.round((element.amount / totalAmount) * 100);
      this.totalPercent += percent;
      return percent;
    } else {
      return 0;
    }
  }

  addErrorToTable(error: Error) {
    const existingErrorIndex = this.selectedErrors.findIndex(
      (e) => e.errorName === error.errorName
    );

    if (existingErrorIndex === -1) {
      this.selectedErrors.push({ ...error, amount: 1 });
      this.floorControllError.amount = 1;
    } else {
      this.selectedErrors[existingErrorIndex].amount++;
    }

    this.floorControllError.errorName = error.errorName;
    this.floorControllError.amount = 1;
    this.floorControllError.wSHfloorControllId = this.lastFloor;
    this.floorControllError.createdUserId=this.authService.getCurrentUserId();
    this.floorControllErrorService
      .addFloorControllError(this.floorControllError)
      .subscribe((data) => {
        this.alertifyService.success(data);
      });
    this.dataSource = new MatTableDataSource(this.selectedErrors);
  }

  addFaulty(error: Error) {
    this.getTotalCountt = this.getTotalCount();

    if (
      this.getTotalCountt <
      this.floorList[this.floorListLenght].sumProductAmount
    ) {
      if (error.isError == true) {
        this.faultyProduct += 1;
      }
      this.addErrorToTable(error);
    } else {
      this.alertifyService.error(
        "Kontrol edilen ürün toplam üründen fazla olamaz!!"
      );
    }
  }

  confirmDelete(element: any): void {
    Swal.fire({
      title: "Emin misiniz?",
      text: "Bu kaydı silmek istediğinizden emin misiniz?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Evet, sil!",
      cancelButtonText: "İptal",
    }).then((result) => {
      if (result.isConfirmed) {
        this.deleteRow(element);
      }
    });
  }

  deleteRow(element: any): void {
    const index = this.dataSource.data.indexOf(element);

    if (index !== -1) {
      this.dataSource.data.splice(index, 1);
      Swal.fire("Silindi!", "Kayıt başarıyla silindi.", "success");
      this.floorControllErrorService
        .deleteFloorControllError(this.lastFloor, element.errorName)
        .subscribe((data) => {
          console.log(data);
        });
      this.dataSource._updateChangeSubscription();
    }
  }

  getErrorList() {
    this.errorService.getErrorList().subscribe((data) => {
      this.errorList = data;
      this.errorListColor0 = this.errorList[0].colorCode;
      this.errorListisError0 = this.errorList[0].isError;
      this.errorListName0 = this.errorList[0].errorName;
    });
  }

  getFloorList() {
    this.floorService.getWashingControll_FloorList().subscribe((data) => {
      this.floorList = data;

      this.floorListLenght = this.floorList.length - 1;
      this.orderId = this.floorList[this.floorListLenght].id;

      const maxIdItem = this.floorList.reduce((maxItem, currentItem) => {
        return currentItem.id > maxItem.id ? currentItem : maxItem;
      }, this.floorList[0]);
      this.lastFloor = maxIdItem.id;
    });
  }

  addWashingControll_FloorControll() {
    this.washingControll_FloorControllService
      .addWashingControll_FloorControll(this.washingControll_FloorControll)
      .subscribe(
        (data) => {
          this.getWashingControll_FloorControllList();
          this.washingControll_FloorControll = new WashingControll_FloorControll();
          jQuery("#washingcontroll_floorcontroll").modal("hide");
          this.alertifyService.success(data);
          this.clearFormGroup(this.washingControll_FloorControllAddForm);
          this.openSuccessPopup();
        },
        (error) => {
          this.alertifyService.error(error.error);
        }
      );
  }
  openSuccessPopup(): void {
    const dialogRef = this.dialog.open(OpenDialogComponent, {});
  }

  updateWashingControll_FloorControll() {
    this.washingControll_FloorControllService
      .updateWashingControll_FloorControll(this.washingControll_FloorControll)
      .subscribe((data) => {
        var index = this.washingControll_FloorControllList.findIndex(
          (x) => x.id == this.washingControll_FloorControll.id
        );
        this.washingControll_FloorControllList[index] =
          this.washingControll_FloorControll;
        this.dataSource = new MatTableDataSource(
          this.washingControll_FloorControllList
        );
        this.configDataTable();
        this.washingControll_FloorControll =
          new WashingControll_FloorControll();
        jQuery("#washingcontroll_floorcontroll").modal("hide");
        this.alertifyService.success(data);
        this.clearFormGroup(this.washingControll_FloorControllAddForm);
      });
  }
  setControllResultToControll() {
    this.finishStarter();

    this.washingControll_FloorControllAddForm
      .get("controllTime")
      ?.setValue(this.second);

    this.washingControll_FloorControllAddForm
      .get("controllResult")
      ?.setValue("Tamamlama Kontrole Gönder");
    this.save();
  }
  setControllResultToConsignment() {
    this.finishStarter();

    this.washingControll_FloorControllAddForm
      .get("controllTime")
      ?.setValue(this.second);

    this.washingControll_FloorControllAddForm
      .get("controllResult")
      ?.setValue("Sevk Et");
    this.save();
  }
  setControllResultToFix() {
    this.finishStarter();

    this.washingControll_FloorControllAddForm
      .get("controllTime")
      ?.setValue(this.second);
    this.washingControll_FloorControllAddForm
      .get("controllResult")
      ?.setValue("Tamir");
    this.save();
  }
  createWashingControll_FloorControllAddForm() {
    this.washingControll_FloorControllAddForm = this.formBuilder.group({
      id: [0],
      errorName: ["", Validators.required],
      amount: [0, Validators.required],
      faultyProduct: ["", Validators.required],
      controllTime: [0, Validators.required],
      controllResult: ["", Validators.required],
      managerReview: ["", Validators.required],
    });
  }

  deleteWashingControll_FloorControll(washingControll_FloorControllId: number) {
    this.washingControll_FloorControllService
      .deleteWashingControll_FloorControll(washingControll_FloorControllId)
      .subscribe((data) => {
        this.alertifyService.success(data.toString());
        this.washingControll_FloorControllList =
          this.washingControll_FloorControllList.filter(
            (x) => x.id != washingControll_FloorControllId
          );
        this.dataSource = new MatTableDataSource(
          this.washingControll_FloorControllList
        );
        this.configDataTable();
      });
  }

  getWashingControll_FloorControllById(
    washingControll_FloorControllId: number
  ) {
    this.clearFormGroup(this.washingControll_FloorControllAddForm);
    this.washingControll_FloorControllService
      .getWashingControll_FloorControllById(washingControll_FloorControllId)
      .subscribe((data) => {
        this.washingControll_FloorControll = data;
        this.washingControll_FloorControllAddForm.patchValue(data);
      });
  }

  clearFormGroup(group: FormGroup) {
    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach((key) => {
      group.get(key).setErrors(null);
      if (key == "id") group.get(key).setValue(0);
    });
  }

  checkClaim(claim: string): boolean {
    return this.authService.claimGuard(claim);
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

  startTimer() {
    if (!this.interval) {
      this.counter();
      this.interval = setInterval(this.counter.bind(this), 1000);
    }
  }

  counter() {
    this.second += 1;
  }

  finishStarter() {
    clearInterval(this.interval);
    this.interval = null;
  }
}
