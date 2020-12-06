import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfigService } from './app-config.service';
import { ITokenData } from '../models/token';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private _http: HttpClient) { }

  public authenticate(email: string, password: string)
  {
    let authenticationUrl = `${AppConfigService.settings.authenticationService.endpoint}/${AppConfigService.settings.authenticationService.authenticatonTokenEndpoint}`;
    return this._http.post<ITokenData>(authenticationUrl, `grant_type=password&username=${email}&password=${password}`);
  }
  public validate(token: string)
  {
    let authenticationUrl = `${AppConfigService.settings.authenticationService.endpoint}/${AppConfigService.settings.authenticationService.restful.validateToken}`;
    return this._http.post<boolean>(authenticationUrl, {token: token});
  }
}
