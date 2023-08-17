import { Routes } from '@angular/router';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LogDtoComponent } from 'app/core/components/admin/log/logDto.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { FloorComponent } from 'app/core/components/admin/floor/floor.component';
import { OrderComponent } from 'app/core/components/admin/order/order.component';
import { MachineComponent } from 'app/core/components/admin/machine/machine.component';
import { ErrorComponent } from 'app/core/components/admin/error/error.component';
import { WashingControll_FloorControllComponent } from 'app/core/components/admin/washingControll_FloorControll/washingControll_FloorControll.component';





export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]},
    { path: 'floor',            component: FloorComponent,canActivate:[LoginGuard]},
    { path: 'order',            component: OrderComponent,canActivate:[LoginGuard]},
    { path: 'machine',            component: MachineComponent,canActivate:[LoginGuard]},
    { path: 'error',            component: ErrorComponent,canActivate:[LoginGuard]},
    { path: 'floorcontroll',            component: WashingControll_FloorControllComponent,canActivate:[LoginGuard]}



    
];
