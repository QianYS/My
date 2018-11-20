import { Component, OnInit, Injector, Input, } from '@angular/core';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  PagedResultDtoOfNameValueDto,
  LookupModelUserInput,
  UserServiceProxy,
  NameValueDto,
} from '@shared/service-proxies/service-proxies';
import { CreateUserComponent } from '@app/users/create-user/create-user.component';
import { EditUserComponent } from '@app/users/edit-user/edit-user.component';

@Component({
  selector: 'app-lookupModel-users',
  templateUrl: './lookupModel-User-Component.html',
  styles: [],
})
export class LookupModelUserComponent extends PagedListingComponentBase<
NameValueDto
> {
  constructor(injector: Injector, private _userService: UserServiceProxy) {
    super(injector);
  }
  @Input() roleNameArray: string[];

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    let input: LookupModelUserInput = new LookupModelUserInput();
    input.roleNameArray = this.roleNameArray || [];
    input.filter = "";
    input.maxResultCount = request.maxResultCount;
    input.skipCount = request.skipCount;
    this._userService
      .lookupModelUser(input)
      .finally(() => {
        finishedCallback();
      })
      .subscribe((result: PagedResultDtoOfNameValueDto) => {
        this.dataList = result.items;
        this.totalItems = result.totalCount;
      });
  }
}
