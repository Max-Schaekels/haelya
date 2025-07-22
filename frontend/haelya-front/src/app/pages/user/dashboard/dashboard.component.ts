import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { UserService } from '../../../core/services/user.service';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  private _userService : UserService = inject(UserService);
  private _router : Router = inject(Router);
  private _authService : AuthService = inject(AuthService);

  userfirstname : string | null = this._authService.getFirstName();

}
