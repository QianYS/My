import { Component, OnInit, Injector } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  PagedResultDtoOfAuditLogShowIndexDto,
  AuditLogServiceProxy,
  AuditLogShowIndexDto,
} from '@shared/service-proxies/service-proxies';
import { unescapeIdentifier } from '@angular/compiler';
import { CheckAuditLogComponent } from '@app/sys/auditLog/check-auditLog/check-auditLog.component';

@Component({
  selector: 'app-auditLog',
  templateUrl: './auditLog.component.html',
  styles: [],
})
export class AuditLogComponent extends PagedListingComponentBase<
  AuditLogShowIndexDto
> {
  validateForm: FormGroup = this.fb.group({
    userName: '',
    status: undefined,
    serviceName: '',
    time: [[]],
  });

  constructor(
    injector: Injector,
    private _auditLogService: AuditLogServiceProxy,
    private fb: FormBuilder,
  ) {
    super(injector);
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this._auditLogService
      .getIndex(
        this.validateForm.value.status === null
          ? undefined
          : this.validateForm.value.status,
        this.validateForm.value.time[0],
        this.validateForm.value.time[1],
        this.validateForm.value.serviceName,
        this.validateForm.value.userName,
        request.skipCount,
        request.maxResultCount,
      )
      .finally(() => {
        finishedCallback();
      })
      .subscribe((result: PagedResultDtoOfAuditLogShowIndexDto) => {
        this.dataList = result.items;
        this.totalItems = result.totalCount;
      });
  }

  edit(item: AuditLogShowIndexDto): void {
    this.modalHelper
      .open(CheckAuditLogComponent, { key: item.id }, 'md', {
        nzMask: true,
        nzClosable: false,
      })
      .subscribe(isSave => {});
  }
}
