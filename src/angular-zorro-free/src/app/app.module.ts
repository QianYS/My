import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from '@app/app-routing.module';
import { AppComponent } from '@app/app.component';
import { AbpModule, LocalizationService } from '@yoyo/abp';
import { LayoutModule } from '@app/layout/layout.module';
import { HomeComponent } from '@app/home/home.component';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { AboutComponent } from '@app/about/about.component';
import { TenantsComponent } from '@app/tenants/tenants.component';
import { UsersComponent } from '@app/users/users.component';
import { RolesComponent } from '@app/roles/roles.component';
import { LoginAttemptComponent } from '@app/sys/loginAttempt/loginAttempt.component';
import { OrganizationUnitComponent } from '@app/sys/organizationUnit/organizationUnit.component';
import { AuditLogComponent } from '@app/sys/auditLog/auditLog.component';

import { CreateTenantComponent } from '@app/tenants/create-tenant/create-tenant.component';
import { EditTenantComponent } from '@app/tenants/edit-tenant/edit-tenant.component';
import { CreateRoleComponent } from '@app/roles/create-role/create-role.component';
import { EditRoleComponent } from '@app/roles/edit-role/edit-role.component';
import { CreateUserComponent } from '@app/users/create-user/create-user.component';
import { EditUserComponent } from '@app/users/edit-user/edit-user.component';
import { CreateOrganizationUnitComponent } from '@app/sys/organizationUnit/create-organizationUnit/create-organizationUnit.component';
import { EditOrganizationUnitComponent } from '@app/sys/organizationUnit/edit-organizationUnit/edit-organizationUnit.component';
import { CheckAuditLogComponent } from '@app/sys/auditLog/check-auditLog/check-auditLog.component';

import { MenuService } from '@yoyo/theme';

import { LookupModelUserComponent } from '@app/users/lookupModel-user/lookupModel-user-component';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    LayoutModule,
    SharedModule,
    AbpModule,
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    TenantsComponent,
    UsersComponent,
    RolesComponent,
    LoginAttemptComponent,
    OrganizationUnitComponent,
    AuditLogComponent,
    CreateTenantComponent,
    EditTenantComponent,
    CreateRoleComponent,
    EditRoleComponent,
    CreateUserComponent,
    EditUserComponent,
    CreateOrganizationUnitComponent,
    EditOrganizationUnitComponent,
    CheckAuditLogComponent,

    LookupModelUserComponent,
  ],
  entryComponents: [
    CreateTenantComponent,
    EditTenantComponent,
    CreateRoleComponent,
    EditRoleComponent,
    CreateUserComponent,
    EditUserComponent,
    CreateOrganizationUnitComponent,
    EditOrganizationUnitComponent,
    CheckAuditLogComponent,

    LookupModelUserComponent,
  ],
  providers: [LocalizationService, MenuService],
})
export class AppModule {}
