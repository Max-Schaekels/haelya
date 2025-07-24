import { Component, inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { UserService } from '../../../../core/services/user.service';
import { ToastService } from '../../../../core/services/toast.service';
import { User } from '../../../../core/models/user';
import { UpdateUser } from '../../../../core/models/update-user';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { trigger, transition, style, animate } from '@angular/animations';
import { ModalComponent } from "../../../../shared/components/modal/modal.component";

export const fadeInOut = trigger('fadeInOut', [
  transition(':enter', [style({ opacity: 0 }), animate('300ms', style({ opacity: 1 }))]),
  transition(':leave', [animate('200ms', style({ opacity: 0 }))])
]);

@Component({
  selector: 'app-profil',
  imports: [ReactiveFormsModule, CommonModule, ModalComponent],
  templateUrl: './profil.component.html',
  styleUrl: './profil.component.scss',
  animations: [fadeInOut]
})
export class ProfilComponent implements OnInit {
  private _fb: FormBuilder = inject(FormBuilder);
  private _authService: AuthService = inject(AuthService);
  private _userService: UserService = inject(UserService);
  private _toast: ToastService = inject(ToastService);
  private _routeur: Router = inject(Router);

  isSubmitting: boolean = false;
  errorMessage: string | null = null;
  isEditing: boolean = false;
  confirmDelete: boolean = false;
  isModalOpen = false;

  updateProfilForm!: FormGroup;
  user!: User;

  openModal() {
    this.isModalOpen = true;
  }


  ngOnInit(): void {
    this._userService.getprofile().subscribe({
      next: (data) => {
        this.user = data;
        this.errorMessage = null;
        this.updateProfilForm = this._fb.group({
          lastName: [this.user.lastName, [Validators.required, Validators.maxLength(55)]],
          firstName: [this.user.firstName, [Validators.required, Validators.maxLength(55)]],
          phoneNumber: [this.user.phoneNumber],
          birthDate: [this.user.birthDate, [this.dateNonFutureValidator()]]

        });

      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Une erreur est survenue pendant la récupération du profil';
      }
    });
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
    if (this.updateProfilForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;

    const dto: UpdateUser = {
      lastName: this.updateProfilForm.value.lastName,
      firstName: this.updateProfilForm.value.firstName,
      phoneNumber: this.updateProfilForm.value.phoneNumber,
      birthDate: this.updateProfilForm.value.birthDate
    };

    this._userService.updateProfil(dto).subscribe({
      next: () => {
        this._userService.getprofile().subscribe({
          next: (data) => {
            this.user = data;
            this.isSubmitting = false;
            this.isEditing = false;
            this.updateProfilForm.reset();
            this.errorMessage = null;
            this._toast.showToast('Mise à jour du profil réussie !');
          },
          error: () => {
            this.isSubmitting = false;
            this.isEditing = false;
            this._toast.showToast('Mise à jour réussie, mais une erreur est survenue lors du rafraîchissement des données.');
          }
        });


      },
      error: (err) => {
        this.isSubmitting = false;
        this.errorMessage = err.error?.message || 'Une erreur est survenue pendant la mise à jour.';
      }
    });

  }

  enableEdit(): void {
    this.isEditing = true;
  }

  cancelEdit(): void {
    this.isEditing = false;
    this.updateProfilForm.reset({
      lastName: this.user.lastName,
      firstName: this.user.firstName,
      phoneNumber: this.user.phoneNumber,
      birthDate: this.user.birthDate
    });
  }

  deleteAccount() {
    this.errorMessage = null;
    this._userService.deleteAccount().subscribe({
      next: () => {
        this._toast.showToast('Le compte a été supprimé avec succès !');
        this._authService.logout();
      },
      error: () => {
        this.errorMessage = 'Erreur lors de la suppression du compte.';
      }
    });
  }

}
