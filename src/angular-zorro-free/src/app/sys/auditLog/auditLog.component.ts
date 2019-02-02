import { Component, OnInit, Injector } from '@angular/core';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  PagedResultDtoOfLoginAttemptShowIndexDto,
  LoginAttemptServiceProxy,
  LoginAttemptShowIndexDto,
} from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-auditLog',
  templateUrl: './auditLog.component.html',
  styles: [],
})
export class auditLogComponent extends PagedListingComponentBase<
  LoginAttemptShowIndexDto
> {
  constructor(
    injector: Injector,
    private _loginAttempService: LoginAttemptServiceProxy,
  ) {
    super(injector);
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this._loginAttempService
      .getIndex('', request.skipCount, request.maxResultCount)
      .finally(() => {
        finishedCallback();
      })
      .subscribe((result: PagedResultDtoOfLoginAttemptShowIndexDto) => {
        this.dataList = result.items;
        this.totalItems = result.totalCount;
      });
  }
}
