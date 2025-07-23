import { Component, inject, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user.service';
import { User } from '../../../core/models/user';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-detail',
  imports: [DatePipe, RouterModule],
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.scss'
})
export class UserDetailComponent implements OnInit {
  private _userService: UserService = inject(UserService);
  private _route: ActivatedRoute = inject(ActivatedRoute);
  user!: User | null;
  loading: boolean = false;
  errorMessage: string | null = null;



  ngOnInit(): void {
    this.loading = true;
    this.errorMessage = null;
    const id = Number(this._route.snapshot.paramMap.get('id'));

    if (!id || isNaN(id)) {
      this.errorMessage = 'ID utilisateur invalide.';
      this.loading = false;
      return;
    }

    this._userService.getUserById(id).subscribe({
      next: (data) => {
        this.user = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = "Impossible de charger l'utilisateur.";
        this.loading = false;
      }
    });
  }

  onTestClick(): void {
  console.log('Le bouton fonctionne !');
}
}
