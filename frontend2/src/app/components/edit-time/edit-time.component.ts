import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, finalize, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { EMAIL_REGEXP } from 'src/app/models/email';
import { MyTel } from 'src/app/models/phone';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-edit-time',
  templateUrl: './edit-time.component.html',
  styleUrls: ['./edit-time.component.css']
})
export class EditTimeComponent implements OnInit{
  public loading: boolean;
  public showPassword: boolean = false;

  private destroyed$ = new Subject<void>();

  selectedID!: string;
  
  constructor(
    private loginService: LoginService,
    private router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.selectedID = this.route.snapshot.paramMap.get('id')!;    
  }

  configurationForm: FormGroup = new FormGroup({
    phone: new FormControl(new MyTel('', '', ''), Validators.required)
  })


  form: FormGroup = new FormGroup({
    Email: new FormControl('', [
      Validators.required,
      Validators.email,
      Validators.pattern(EMAIL_REGEXP),
    ]),
    Name: new FormControl('', Validators.required),
    Password: new FormControl('', Validators.required),
    PhoneNumber: new FormControl(''),
    Role: new FormControl('', Validators.required),
  });


  submit() {
    let number: number = this.configurationForm.value['phone'].area + this.configurationForm.value['phone'].exchange + this.configurationForm.value['phone'].subscriber;
    this.form.get('PhoneNumber').setValue(number);
  
    this.loading = true;
    if (this.form.valid) {
      this.loginService
        .register(this.form.value)
        .pipe(
          takeUntil(this.destroyed$),
          finalize(() => (this.loading = false))
        )
        .subscribe({
          next: (response: any) => {
            this.router.navigate(['/', 'login']);
          },
          error: (error: HttpErrorResponse) => {
            throw new HttpErrorResponse(error);
          },
        });
    }
  }

  public togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
    this.destroyed$.complete();
  }
}
