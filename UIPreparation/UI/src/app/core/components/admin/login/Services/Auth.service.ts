import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from 'app/core/services/Alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { environment } from 'environments/environment';
import { LoginUser } from '../model/login-user';
import { TokenModel } from '../model/token-model';
import { SharedService } from 'app/core/services/shared.service';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  userName: string;
  userId: number;
  isLoggin: boolean;
  decodedToken: any;
  userToken: string;
  jwtHelper: JwtHelperService = new JwtHelperService();
  claims: string[];

  constructor(private httpClient: HttpClient, private storageService: LocalStorageService,
    private router: Router, private alertifyService: AlertifyService, private sharedService: SharedService) {
    this.setClaims();
  }

  login(loginUser: LoginUser) {

    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json")

    this.httpClient.post<TokenModel>(environment.getApiUrl + "/Auth/login", loginUser, { headers: headers }).subscribe(data => {


      if (data.success) {

        this.storageService.setToken(data.data.token);
        this.storageService.setItem("refreshToken", data.data.refreshToken)
        this.claims = data.data.claims;

        //login olan kullanıcının id sini getirmek için...
        var decode = this.jwtHelper.decodeToken(this.storageService.getToken());
        var propUserName = Object.keys(decode).filter(x => x.endsWith("/name"))[0];
        this.userName = decode[propUserName];
        this.getCurrentUserId();
        this.sharedService.sendChangeUserNameEvent();

        this.router.navigateByUrl("/dashboard");
      }
      else {
        this.alertifyService.warning(data.message);
      }

    }
    );
  }
  getUserName(): string {
    return this.userName;
  }

  async setClaims() {
    if ((this.claims == undefined || this.claims.length == 0) && this.storageService.getToken() != null && this.loggedIn()) {
      try {
        const data = await this.httpClient.get<string[]>("https://localhost:5001/api/v1/OperationClaim/cache").toPromise();
        this.claims = data;
        
        var token = this.storageService.getToken();
        var decode = this.jwtHelper.decodeToken(token);
  
        var propUserId = Object.keys(decode)?.filter(x => x.endsWith("/nameidentifier"))[0];
        this.userId = Number(decode[propUserId]);
      } catch (error) {
        console.error("Hatalı talep sonucu:", error);
      }
    }
  }
  

  logOut() {
    this.storageService.removeToken();
    this.storageService.removeItem("lang")
    this.storageService.removeItem("refreshToken");
    this.claims = [];
  }

  loggedIn(): boolean {

    let isExpired = this.jwtHelper.isTokenExpired(this.storageService.getToken(), -120);
    return !isExpired;
  }

  getCurrentUserId() {
    var decode = this.jwtHelper.decodeToken(this.storageService.getToken());
    var propUserId = Object.keys(decode)?.filter(x => x.endsWith("/nameidentifier"))[0];
    this.userId = Number(decode[propUserId]);
    return this.userId;
  }

  claimGuard(claim: string): boolean {
    if (!this.loggedIn())
      this.router.navigate(["/login"]);

    var check = this.claims.some(function (item) {
      return item == claim;
    })

    return check;
  }

}