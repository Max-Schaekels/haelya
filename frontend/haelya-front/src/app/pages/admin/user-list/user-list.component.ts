import { Component, inject, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user.service';
import { User } from '../../../core/models/user';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent implements OnInit {
  private _userService: UserService = inject(UserService);
  private _router : Router = inject(Router);
  users: User[] = [];
  loading: boolean = false;
  errorMessage: string | null = null;



  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;
    this.errorMessage = null;

    this._userService.getAllUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Erreur lors du chargement des utilisateurs.';
        this.loading = false;
      }
    });
  }




}
