import { tokenName } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ITokenData } from '../models/token';
import { AuthenticationService } from '../services/authentification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private _email: string;
  private _password: string;
  public Token: string;
  public IsValid: boolean;

  constructor(private _authService: AuthenticationService) { }

  ngOnInit(): void {
  }

  updateEmail(e: Event) {
    this._email = (<HTMLInputElement>e.currentTarget).value;
  }

  updatePassword(e: Event) {
    this._password = (<HTMLInputElement>e.currentTarget).value;
  }

  authenticate(): void {
    this._authService.authenticate(this._email, this._password).subscribe((token: ITokenData) => {
      console.log(token);
      this.Token = token.access_token;
    },
      (error) => console.log(error));
  }

  validate(): void {
    this._authService.validate(this.Token).subscribe((isValid: boolean) => {
      console.log(isValid);
      this.IsValid = isValid;
    },
      (error) => console.log(error));
  }
}
