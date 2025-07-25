import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { Login } from '../../../core/models/login';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private _fb: FormBuilder = inject(FormBuilder);
  private authService: AuthService = inject(AuthService);
  private router: Router = inject(Router);
  errorMessage: string = "";


  loginForm: FormGroup;


  constructor() {
    this.loginForm = this._fb.group({
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required]]
    });

  }

  onSubmit() {
    if (this.loginForm.invalid) return;
    const userLogin: Login = {
      Email: this.loginForm.get('email')?.value,
      Password: this.loginForm.get('password')?.value
    };
    this.authService.login(userLogin).subscribe({
      next: ({ accessToken }) => {
        this.authService.saveAuth(accessToken);
        this.redirectAfterLogin(this.authService.getRole());

      },
      error: err => {
        console.error(err);
        const message = err.error?.error || "Erreur inconnue.";

        switch (err.status) {
          case 401:
            this.errorMessage = "Email ou mot de passe incorrect.";
            break;
          case 410:
            this.errorMessage = "Ce compte a été supprimé.";
            break;
          case 0:
            this.errorMessage = "Connexion au serveur impossible.";
            break;
          default:
            this.errorMessage = message;
        }

      }
    });
  }

  redirectAfterLogin(role: string | null) {
    if (role === 'admin') {
      this.router.navigate(['']);
    } else {
      this.router.navigate(['']);
    }
  }

}


