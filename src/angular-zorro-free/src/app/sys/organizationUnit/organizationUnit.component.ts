import { Component, Injector, ViewChild, TemplateRef } from '@angular/core';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  OrganizationUnitServiceProxy,
  OrganizationUnitTreeDto,
  PagedResultDtoOfOrganizationUnitUserDto,
  OrganizationUnitUserDto,
} from '@shared/service-proxies/service-proxies';
import {
  NzDropdownContextComponent,
  NzDropdownService,
  NzFormatEmitEvent,
  NzTreeComponent,
  NzTreeNode,
} from 'ng-zorro-antd';
//import { Key } from 'protractor';
import { CreateOrganizationUnitComponent } from '@app/sys/organizationUnit/create-organizationUnit/create-organizationUnit.component';
import { LookupModelComponent } from '@app/layout/common/lookupModel/lookupModel.component';

@Component({
  selector: 'app-organizationUnit',
  templateUrl: './organizationUnit.component.html',
  styles: [],
})
export class OrganizationUnitComponent extends PagedListingComponentBase<
  OrganizationUnitUserDto
> {
  @ViewChild('treeCom')
  treeCom: NzTreeComponent;
  dropdown: NzDropdownContextComponent;
  // actived node
  activedNode: NzTreeNode = null;
  organizationUnitList: NzTreeNode[];
  requestThis: PagedRequestDto;
  pageNumberThis: number;
  finishedCallbackThis: Function;
  constructor(
    injector: Injector,
    private _organizationUnitService: OrganizationUnitServiceProxy, //private _userService: UserServiceProxy,
  ) {
    super(injector);
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this.requestThis = request;
    this.pageNumberThis = pageNumber;
    this.finishedCallbackThis = finishedCallback;
    this._organizationUnitService
      .getAllTree()
      .subscribe((organizationUnit: OrganizationUnitTreeDto[]) => {
        let array = [];
        organizationUnit.forEach(item => {
          array.push({
            title: item.title,
            key: item.key,
            children: item.children,
          });
        });
        this.organizationUnitList = array;
        this.finishedCallbackThis();
      });
  }

  activeNode(data: NzFormatEmitEvent): void {
    this.activedNode = data.node;
    this._organizationUnitService
      .getUserByOrganizationUnit(
        this.activedNode.key,
        this.requestThis.skipCount,
        this.requestThis.maxResultCount,
      )
      .finally(() => {
        this.finishedCallbackThis();
      })
      .subscribe((result: PagedResultDtoOfOrganizationUnitUserDto) => {
        console.log(result);
        this.activedNode = null;
        this.dataList = result.items;
        this.totalItems = result.totalCount;
      });
    // add selectedNodeList
    //this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode);
  }

  show(data: NzFormatEmitEvent): void {
    this.activedNode = data.node;
  }

  contextMenu($event: MouseEvent, template: TemplateRef<void>): void {
    this.dropdown = this.nzDropdownService.create($event, template);
  }

  createChild(): void {
    let key: string = '';
    if (this.activedNode) {
      key = this.activedNode.key;
    } else {
      key = null;
    }
    this.modalHelper
      .open(CreateOrganizationUnitComponent, { key: key }, 'md', {
        nzMask: true,
        nzClosable: false,
      })
      .subscribe(isSave => {
        this.activedNode = null;
        if (isSave) {
          this.refresh();
          this.nzDropdownService.close();
        }
      });
  }
  removeUser(userId: number, organizationUnitCode: string): void {
    //alert(id);
    //console.log('userId:' + userId);
    //console.log('organizationUnitId:' + organizationUnitId);
    this._organizationUnitService.removeUsers([userId], organizationUnitCode);
  }
  // lookup(): void {
  //   this.modalHelper
  //     .open(
  //       LookupModelComponent,
  //       {
  //         obj: {
  //           serviceMethod: this._roleService.getAllPermissionsTree,
  //           filter: '111',
  //         },
  //       },
  //       'md',
  //       {
  //         nzMask: true,
  //         nzClosable: false,
  //       },
  //     )
  //     .subscribe(isSave => {
  //       console.log(isSave);
  //       if (isSave) {
  //         this.refresh();
  //       }
  //     });
  //}

  // edit(item: UserDto): void {
  //   this.modalHelper
  //     .open(CreateOrganizationUnitComponent, { id: item.id }, 'md', {
  //       nzMask: true,
  //       nzClosable: false,
  //     })
  //     .subscribe(isSave => {
  //       if (isSave) {
  //         this.refresh();
  //       }
  //     });
  // }
}
