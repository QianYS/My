import { Component, OnInit, Injector, Input } from '@angular/core';
import {
  CreateUserDto,
  UserServiceProxy,
  RoleDto,
} from '@shared/service-proxies/service-proxies';
import { ModalComponentBase } from '@shared/component-base';
// import {
//   PagedRequestDto,
// } from '@shared/component-base/paged-listing-component-base';
@Component({
  selector: 'lookupModel',
  templateUrl: './lookupModel.component.html',
  styles: [],
})
export class LookupModelComponent extends ModalComponentBase implements OnInit {
  @Input()
  obj: {
    serviceMethod: any;
    filter: string;
  };
  //request: PagedRequestDto;

  options = {
    serviceMethod: this.obj.serviceMethod, //Required
    title: '请选择',
    loadOnStartup: true,
    showFilter: true,
    filterText: '',
    skipCount: 0,
    //pageSize: this.request.defaultPageSize,
    callback: function(selectedItem) {
      //This method is used to get selected item
    },
    canSelect: function(item) {
      /* This method can return boolean or a promise which returns boolean.
         * A false value is used to prevent selection.
         */
      return true;
    },
  };

  confirmPassword: string = '';

  constructor(injector: Injector, private _userService: UserServiceProxy) {
    super(injector);
  }

  ngOnInit() {
    this.fetchData();
  }

  fetchData(): void {
    console.log(this.options);
    //this.serviceMethod();
  }
}
