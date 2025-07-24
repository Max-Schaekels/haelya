import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { DashboardComponent } from './pages/user/dashboard/dashboard.component';
import { ProfilComponent } from './pages/user/dashboard/profil/profil.component';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';
import { AdminLayoutComponent } from './layout/admin-layout/admin-layout.component';
import { PublicLayoutComponent } from './layout/public-layout/public-layout.component';

export const routes: Routes = [
    {
        path: '',
        component: PublicLayoutComponent,
        children: [
            {
                path: '',
                loadComponent: () =>
                    import('./pages/home/home.component').then((m) => m.HomeComponent),
            },
            {
                path: 'login',
                loadComponent: () =>
                    import('./pages/auth/login/login.component').then((m) => m.LoginComponent),
            },
            {
                path: 'register',
                loadComponent: () =>
                    import('./pages/auth/register/register.component').then((m) => m.RegisterComponent),
            },
            
            {
                path: 'dashboard',
                canActivate: [authGuard],
                component: DashboardComponent,
                children: [
                    { path: '', redirectTo: 'profil', pathMatch: 'full' },
                    {
                        path: 'profil',
                        loadComponent: () =>
                            import('./pages/user/dashboard/profil/profil.component').then((m) => m.ProfilComponent),
                    },
                ]
            },
        ]
    },

    {
        path: 'admin',
        canActivate: [adminGuard],
        component: AdminLayoutComponent,
        children: [
            {
                path: '',
                loadComponent: () =>
                    import('./pages/admin/admin-home/admin-home.component').then((m) => m.AdminHomeComponent),
            },
            {
                path: 'utilisateurs',
                loadComponent: () =>
                    import('./pages/admin/user-list/user-list.component').then(m => m.UserListComponent)
            },
            {
                path: 'utilisateurs/:id',
                loadComponent: () =>
                    import('./pages/admin/user-detail/user-detail.component').then(m => m.UserDetailComponent)
            }

        ]
    }
];
