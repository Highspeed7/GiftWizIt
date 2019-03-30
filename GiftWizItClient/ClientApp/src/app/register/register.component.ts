import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AccountsService } from "../accounts.service";

import { RegisterModel } from '../models/register';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

    public registerForm: FormGroup;

    constructor(private accntSvc: AccountsService, private router: Router) { }

    ngOnInit() {
        this.registerForm = new FormGroup(
            {
                'email': new FormControl('', [Validators.required, Validators.email]),
                'password': new FormControl('', [Validators.required]),
                'confPass': new FormControl('', [Validators.required])
            }
        );
    }

    public onUsernameChange(elem: any) {
        // Check if username available
        console.log(elem);
    }

    public onRegisterSubmit() {
        var username = this.registerForm.controls.email.value;
        var password = this.registerForm.controls.password.value;
        var confPass = this.registerForm.controls.confPass.value;

        this.accntSvc.registerUser(username, password, confPass).subscribe((response) => {
            console.log(response);
            this.router.navigate(['login']);
        });
    }
}
