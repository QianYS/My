import { Component, OnInit, Input, Injector } from '@angular/core';
import { ModalComponentBase } from '@shared/component-base/modal-component-base';
import {
  RoleDto,
  ListResultDtoOfPermissionDto,
  RoleServiceProxy,
  PermissionDto,
} from '@shared/service-proxies/service-proxies';
import { Validators } from '@angular/forms';
import {
  NzFormatEmitEvent,
  NzTreeNode,
  NzTreeNodeOptions,
} from 'ng-zorro-antd';

@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html',
  styles: [],
})
export class EditRoleComponent extends ModalComponentBase implements OnInit {
  @Input()
  id: number;
  permissions: ListResultDtoOfPermissionDto = null;
  role: RoleDto = null;
  permissionList: NzTreeNodeOptions[];
  array = [];
  tmpPermissions: string[] = [];
  constructor(injector: Injector, private _roleService: RoleServiceProxy) {
    super(injector);
  }

  ngOnInit() {
    this._roleService
      .get(this.id)
      .finally(() => {})
      .subscribe((result: RoleDto) => {
        this.role = result;

        this.fetchData();
      });
  }

  fetchData(): void {
    this._roleService
      .getAllPermissionsTree()
      .subscribe((permissions: ListResultDtoOfPermissionDto) => {
        this.permissions = permissions;
        this.permissions.items.forEach(item => {
          this.array.push({
            title: item.title,
            key: item.key,
            children: item.children,
          });
        });
        this.getChecked(this.array);
        this.permissionList = this.array;
      });
  }

  getChecked(data: any[]): void {
    data.forEach(item => {
      item.checked =
        this.role.permissions.indexOf(item.key) != -1 ? true : false;
      if (item.children) {
        this.getChecked(item.children);
      }
    });
  }

  checkPermission(permissionName: string): boolean {
    return this.role.permissions.indexOf(permissionName) != -1;
  }

  getSelectPermission(data: NzTreeNodeOptions): void {
    if (data.checked) {
      this.tmpPermissions.push(data.key);
    }
    if (data.children) {
      data.children.forEach(itemItem => {
        this.getSelectPermission(itemItem);
      });
    }
  }

  save(): void {
    this.saving = true;
    this.permissionList.forEach(item => {
      this.getSelectPermission(item);
    });
    this.role.permissions = this.tmpPermissions;
    this._roleService
      .update(this.role)
      .finally(() => {
        this.saving = false;
      })
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.success();
      });
  }
}
