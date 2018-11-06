import { Component, OnInit, Injector, Input } from '@angular/core';
import {
  OrganizationUnitDto,
  OrganizationUnitServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-create-organizationUnit',
  templateUrl: './create-organizationUnit.component.html',
  styles: [],
})
export class CreateOrganizationUnitComponent extends ModalComponentBase
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
          this.organizationUnit.parentId = result.id;
        });
    } else {
      this.organizationUnit.parentId = null;
    }
  }

  save(): void {
    this._organizationUnitService
      .create(this.organizationUnit)
      .finally(() => {
        this.saving = false;
      })
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.success();
      });
  }
}
