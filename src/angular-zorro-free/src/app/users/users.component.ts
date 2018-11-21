import { Component, OnInit, Injector } from '@angular/core';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  PagedResultDtoOfUserDto,
  UserServiceProxy,
  PagedResultDtoOfUserListDto,
  UserListDto,
  UserDto,
} from '@shared/service-proxies/service-proxies';
import { CreateUserComponent } from '@app/users/create-user/create-user.component';
import { EditUserComponent } from '@app/users/edit-user/edit-user.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styles: [],
})
export class UsersComponent extends PagedListingComponentBase<UserListDto> {
  constructor(injector: Injector, private _userService: UserServiceProxy) {
    super(injector);
  }
  filter: string = '';
  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this.getUserIndexList(finishedCallback);
    // this._userService
    //   .getUsers('', request.skipCount, request.maxResultCount)
    //   .finally(() => {
    //     finishedCallback();
    //   })
    //   .subscribe((result: PagedResultDtoOfUserListDto) => {
    //     this.dataList = result.items;
    //     this.totalItems = result.totalCount;
    //   });
  }

  search(finishedCallback: Function): void {
    this.getUserIndexList(finishedCallback);
  }

  protected delete(entity: UserDto): void {
    abp.message.confirm(
      "Delete user '" + entity.fullName + "'?",
      (result: boolean) => {
        if (result) {
          this._userService.delete(entity.id).subscribe(() => {
            abp.notify.info('Deleted User: ' + entity.fullName);
            this.refresh();
          });
        }
      },
    );
  }

  create(): void {
    this.modalHelper
      .open(CreateUserComponent, {}, 'md', {
        nzMask: true,
        nzClosable: false,
      })
      .subscribe(isSave => {
        if (isSave) {
          this.refresh();
        }
      });
  }

  edit(item: UserDto): void {
    this.modalHelper
      .open(EditUserComponent, { id: item.id }, 'md', {
        nzMask: true,
        nzClosable: false,
      })
      .subscribe(isSave => {
        if (isSave) {
          this.refresh();
        }
      });
  }

  getUserIndexList(finishedCallback: Function): void {
    let maxResultCount = this.pageSize;
    let skipCount = (this.pageNumber - 1) * this.pageSize;
    this._userService
      .getUsers(this.filter, skipCount, maxResultCount)
      .finally(() => {
        finishedCallback();
      })
      .subscribe((result: PagedResultDtoOfUserListDto) => {
        this.dataList = result.items;
        this.totalItems = result.totalCount;
      });
  }
}
