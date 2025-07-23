import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { DashboardComponent } from './pages/user/dashboard/dashboard.component';
import { ProfilComponent } from './pages/user/dashboard/profil/profil.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'login', component: LoginComponent},
    { path: 'register', loadComponent: () => import('./pages/auth/register/register.component').then(m => m.RegisterComponent) },
    {
    path: 'dashboard',
    canActivate: [authGuard],
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: 'profil', pathMatch: 'full' },
      { path: 'profil', loadComponent: () => import('./pages/user/dashboard/profil/profil.component').then(m => m.ProfilComponent) },     
    ]
    }
];
