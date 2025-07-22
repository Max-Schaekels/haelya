import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { Register } from '../../../core/models/register';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  private _fb: FormBuilder = inject(FormBuilder);
  private _authService: AuthService = inject(AuthService);
  private _routeur: Router = inject(Router);
  private _toastService : ToastService = inject(ToastService);

  isSubmitting = false;
  errorMessage: string | null = null;

  registerForm: FormGroup;

  constructor() {
    this.registerForm = this._fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$/)]],
      confirm: [null, [Validators.required]],
      lastName: [null, [Validators.required, Validators.maxLength(55)]],
      firstName: [null, [Validators.required, Validators.maxLength(55)]],
      phoneNumber: [null],
      birthDate: [null, [this.dateNonFutureValidator()]],
      conditions: [false, Validators.requiredTrue]
    }, { validators: [this.confirmMdp()] });
  }

  confirmMdp(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      let password: string = control.get('password')?.value;
      let confirmPassword: string = control.get('confirm')?.value;

      //S'il y a une valeur dans pwd et dans confirm mais qu'ils ne sont pas égaux
      if (password && confirmPassword && password !== confirmPassword) {
        return { 'checkpassword': true };

      }
      return null;
    };

  }

  dateNonFutureValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const val = control.value;
      if (val && new Date(val) > new Date()) {
        return { futureDate: true };
      }
      return null;
    };
  }

  onSubmit(): void {
    if (this.registerForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;

    const dto: Register = {
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
      lastName: this.registerForm.value.lastName,
      firstName: this.registerForm.value.firstName,
      phoneNumber: this.registerForm.value.phoneNumber,
      birthDate: this.registerForm.value.birthDate
    };

    this._authService.register(dto).subscribe({
      next: () => {

        this.isSubmitting = false;
        this.registerForm.reset();
        this.errorMessage = null;        
        this._routeur.navigate(['login']);
        this._toastService.showToast('Inscription réussie ! Vous pouvez vous connecté !')
      },
      error: (err) => {

        this.isSubmitting = false;
        console.error('Erreur d’inscription :', err);

        this.errorMessage = err.error?.message || 'Une erreur est survenue pendant l’inscription.';
      }
    });
  }

}
