import { Component, OnInit, Injector, Input } from '@angular/core';
import { PagedListChooseComponentBase } from '@shared/component-base/paged-list-choose-component-base';
import {
  PagedResultDtoOfNameValueDto,
  LookupModelUserInput,
  UserServiceProxy,
  NameValueDto,
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-lookupModel-users',
  templateUrl: './lookupModel-User-Component.html',
  styles: [],
})
export class LookupModelUserComponent
  extends PagedListChooseComponentBase<NameValueDto>
  implements OnInit {
  @Input()
  roleNameArray: string[];
  filter: string = '';

  constructor(injector: Injector, private _userService: UserServiceProxy) {
    super(injector);
  }

  protected fetchDataList(): void {
    this.getUserChooseList();
  }

  getUserChooseList(): void {
    let input: LookupModelUserInput = new LookupModelUserInput();
    input.roleNameArray = this.roleNameArray || [];
    input.filter = this.filter;
    input.maxResultCount = this.pageSize;
    input.skipCount = (this.pageNumber - 1) * this.pageSize;
    this._userService
      .lookupModelUser(input)
      .finally(() => {})
      .subscribe((result: PagedResultDtoOfNameValueDto) => {
        this.dataList = result.items;
        this.totalItems = result.totalCount;
        this.isTableLoading = false;
      });
  }

  choose(item: NameValueDto): void {
    this.success(item);
  }
}
