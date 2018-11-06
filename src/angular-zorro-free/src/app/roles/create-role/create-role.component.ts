import { Component, OnInit, Injector, Input, ViewChild } from '@angular/core';
import {
  ListResultDtoOfPermissionDto,
  CreateRoleDto,
  RoleServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalComponentBase } from '@shared/component-base';
import { Validators } from '@angular/forms';
import {
  NzFormatEmitEvent,
  NzTreeNode,
  NzTreeNodeOptions,
} from 'ng-zorro-antd';

@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styles: [],
})
export class CreateRoleComponent extends ModalComponentBase implements OnInit {
  @ViewChild('treeCom')
  treeCom;
  permissions: ListResultDtoOfPermissionDto = null;
  role: CreateRoleDto = new CreateRoleDto();
  permissionList: NzTreeNodeOptions[];
  tmpPermissions: string[] = [];

  constructor(injector: Injector, private _roleService: RoleServiceProxy) {
    super(injector);
  }

  ngOnInit() {
    this._roleService
      .getAllPermissionsTree()
      .subscribe((permissions: ListResultDtoOfPermissionDto) => {
        this.permissions = permissions;
        let array = [];
        this.permissions.items.forEach(item => {
          array.push({
            title: item.title,
            key: item.key,
            checked: false,
            children: item.children,
          });
        });
        this.permissionList = array;
      });
  }

  nzCheck(event: NzFormatEmitEvent): void {
    this.getChildNode(event.node);
  }

  getChildNode(data: NzTreeNode): void {
    data.isExpanded = true;
    if (data.children) {
      data.children.forEach(item => {
        this.getChildNode(item);
      });
    }
  }

  getCheckedNode(data: NzTreeNode[]): void {
    data.forEach(item => {
      if (item.isChecked) {
        this.tmpPermissions.push(item.key);
        if (item.children) {
          this.getCheckedNode(item.children);
        }
      }
    });
  }

  save(): void {
    this.saving = true;
    this.getCheckedNode(this.treeCom.getCheckedNodeList());
    this.role.permissions = this.tmpPermissions;
    this._roleService
      .create(this.role)
      .finally(() => {
        this.saving = false;
      })
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.success();
      });
  }
}
