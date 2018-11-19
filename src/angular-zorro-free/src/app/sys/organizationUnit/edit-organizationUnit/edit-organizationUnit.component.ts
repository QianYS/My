import { Component, OnInit, Injector, Input } from '@angular/core';
import {
  OrganizationUnitDto,
  OrganizationUnitServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-edit-organizationUnit',
  templateUrl: './edit-organizationUnit.component.html',
  styles: [],
})
export class EditOrganizationUnitComponent extends ModalComponentBase
  implements OnInit {
  @Input()
  key: string;
  organizationUnit: OrganizationUnitDto = new OrganizationUnitDto();

  constructor(
    injector: Injector,
    private _organizationUnitService: OrganizationUnitServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit() {
    this.fetchData();
  }

  fetchData(): void {
    if (this.key) {
      this._organizationUnitService
        .getOrganizationUnitByCode(this.key)
        .subscribe(result => {
          this.organizationUnit = result;
        });
    } else {
      this.notify.error('组织架构参数丢失！');
    }
  }

  save(): void {
    this._organizationUnitService
      .update(this.organizationUnit)
      .finally(() => {
        this.saving = false;
      })
      .subscribe(() => {
        this.notify.success('修改成功');
        this.success();
      });
  }
}
