import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from '@app/home/home.component';
import { AppComponent } from '@app/app.component';
import { AboutComponent } from '@app/about/about.component';
import { TenantsComponent } from '@app/tenants/tenants.component';
import { RolesComponent } from '@app/roles/roles.component';
import { UsersComponent } from '@app/users/users.component';
import { LoginAttemptComponent } from '@app/sys/loginAttempt/loginAttempt.component';
import { OrganizationUnitComponent } from '@app/sys/organizationUnit/organizationUnit.component';
const routes: Routes = [
  {
    path: 'app',
    component: AppComponent,
    canActivate: [AppRouteGuard],
    canActivateChild: [AppRouteGuard],
    children: [
      {
        path: 'home',
        component: HomeComponent,
        canActivate: [AppRouteGuard],
      },
      {
        path: 'tenants',
        component: TenantsComponent,
        canActivate: [AppRouteGuard],
      },
      {
        path: 'roles',
        component: RolesComponent,
        canActivate: [AppRouteGuard],
      },
      {
        path: 'users',
        component: UsersComponent,
        canActivate: [AppRouteGuard],
      },
      {
        path: 'sys',
        //component: AppComponent,
        canActivate: [AppRouteGuard],
        canActivateChild: [AppRouteGuard],
        children: [
          {
            path: 'loginAttempt',
            component: LoginAttemptComponent,
            canActivate: [AppRouteGuard],
          },
          {
            path: 'organizationUnit',
            component: OrganizationUnitComponent,
            canActivate: [AppRouteGuard],
          },
        ],
      },
      {
        path: 'about',
        component: AboutComponent,
        canActivate: [AppRouteGuard],
      },
      {
        path: '**',
        redirectTo: 'home',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
