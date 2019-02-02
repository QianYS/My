import { Component, OnInit, Injector, Input } from '@angular/core';
import {
  AuditLogDetailDto,
  AuditLogServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { ModalComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-check-auditLog',
  templateUrl: './check-auditLog.component.html',
  styles: [],
})
export class CheckAuditLogComponent extends ModalComponentBase
  implements OnInit {
  @Input()
  key: number;
  auditLog: AuditLogDetailDto = new AuditLogDetailDto();

  constructor(
    injector: Injector,
    private _auditLogService: AuditLogServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit() {
    this.fetchData();
  }

  fetchData(): void {
    if (this.key) {
      this._auditLogService.getFroEdit(this.key).subscribe(result => {
        console.log(result);
        this.auditLog = result;
      });
    } else {
      this.notify.error('日志详情参数丢失！');
    }
  }

  toJson(jsonStr: string): string {
    let obj: any = JSON.parse(jsonStr);
    console.log(JSON.stringify(obj, null, 1000));
    return JSON.stringify(obj, null, 1000);
  }
}
