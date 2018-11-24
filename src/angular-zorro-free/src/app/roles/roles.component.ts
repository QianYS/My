import { Component, OnInit, Injector } from '@angular/core';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  RoleDto,
  RoleServiceProxy,
  PagedResultDtoOfRoleDto,
} from '@shared/service-proxies/service-proxies';
import { EditRoleComponent } from '@app/roles/edit-role/edit-role.component';
import { CreateRoleComponent } from '@app/roles/create-role/create-role.component';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styles: [],
})
export class RolesComponent extends PagedListingComponentBase<RoleDto> {
  EditOrUpdate: boolean = abp.auth.isGranted('Pages.Sys.Roles.EditOrUpdate');
  ShowIndex: boolean = abp.auth.isGranted('Pages.Sys.Roles.ShowIndex');

  constructor(injector: Injector, private _rolesService: RoleServiceProxy) {
    super(injector);
  }

  filter: string = '';
  isTableLoading: boolean = true;

  protected fetchDataList(): void {
    this.getRoleIndexList();
  }

  search(): void {
    this.getRoleIndexList();
  }

  protected delete(entity: RoleDto): void {
    abp.message.confirm(
      "Remove Users from Role and delete Role '" + entity.displayName + "'?",
      'Permanently delete this Role',
      (result: boolean) => {
        if (result) {
          this._rolesService
            .delete(entity.id)
            .finally(() => {
              abp.notify.info('Deleted Role: ' + entity.displayName);
              this.search();
            })
            .subscribe(() => {});
        }
      },
    );
  }

  create(): void {
    this.modalHelper
      .open(CreateRoleComponent, {}, 'md', {
        nzMask: true,
      })
      .subscribe(isSave => {
        if (isSave) {
          this.search();
        }
      });
  }

  edit(item: RoleDto): void {
    this.modalHelper
      .open(EditRoleComponent, { id: item.id }, 'md', {
        nzMask: true,
      })
      .subscribe(isSave => {
        if (isSave) {
          this.search();
        }
      });
  }

  getRoleIndexList(): void {
    let maxResultCount = this.pageSize;
    let skipCount = (this.pageNumber - 1) * this.pageSize;
    this._rolesService
      .getRolesIndexList(this.filter, skipCount, maxResultCount)
      .finally(() => {})
      .subscribe((result: PagedResultDtoOfRoleDto) => {
        this.dataList = result.items;
        this.totalItems = result.totalCount;
        this.isTableLoading = false;
      });
  }
}
