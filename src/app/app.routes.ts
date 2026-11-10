import { Routes } from '@angular/router';
import { Login } from './features/login/login';
import { Otp } from './features/otp/otp';
import { MainLayoutComponent } from './layouts/main-layout/main-layout';
import { CustomersListComponent } from './features/customers/customers-list/customers-list';
import { CustomerCreateComponent } from './features/customers/customers-create/customers-create';

export const routes: Routes = [

  
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: Login
  },

  {
    path: 'verify-otp',
    component: Otp
  },

  {
  path: '',
      component: MainLayoutComponent,
      children: [

        {
          path: 'dashboard',
          loadComponent: () =>
            import('./features/dashboard/dashboard')
              .then(m => m.DashboardComponent)
        },

        {
          path: 'customers/list',
          loadComponent: () =>
            import('./features/customers/customers-list/customers-list')
              .then(m => m.CustomersListComponent)
        },

        {
          path: 'customers/create',
          loadComponent: () =>
            import('./features/customers/customers-create/customers-create')
              .then(m => m.CustomerCreateComponent)
        }

      ]
  }

];
