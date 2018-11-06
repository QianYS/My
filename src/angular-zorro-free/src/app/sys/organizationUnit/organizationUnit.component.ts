import { Component, Injector, ViewChild, TemplateRef } from '@angular/core';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  OrganizationUnitServiceProxy,
  OrganizationUnitTreeDto,
  UserServiceProxy,
  UserDto,
  PagedResultDtoOfUserDto,
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
  UserDto
> {
  @ViewChild('treeCom')
  treeCom: NzTreeComponent;
  dropdown: NzDropdownContextComponent;
  // actived node
  activedNode: NzTreeNode;
  organizationUnitList: NzTreeNode[];
  opj: {};
  //nzDropdownService = this.nzDropdownService;
  constructor(
    injector: Injector,
    private _organizationUnitService: OrganizationUnitServiceProxy,
    private _userService: UserServiceProxy,
  ) {
    super(injector);
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    // this._userService
    //   .getAll(request.skipCount, request.maxResultCount)
    //   .finally(() => {
    //     finishedCallback();
    //   })
    //   .subscribe((result: PagedResultDtoOfUserDto) => {
    //     this.dataList = result.items;
    //     this.totalItems = result.totalCount;
    //   });
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
      });
  }

  // activeNode(data: NzFormatEmitEvent): void {
  //   console.log(data);
  //   if (this.activedNode) {
  //     // delete selectedNodeList(u can do anything u want)
  //     this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode);
  //   }
  //   data.node.isSelected = true;
  //   this.activedNode = data.node;
  //   // add selectedNodeList
  //   this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode);
  // }

  show(data: NzFormatEmitEvent): void {
    this.activedNode = data.node;
  }

  contextMenu($event: MouseEvent, template: TemplateRef<void>): void {
    this.dropdown = this.nzDropdownService.create($event, template);
  }

  createChild(): void {
    console.log(this.activedNode);
    this.modalHelper
      .open(
        CreateOrganizationUnitComponent,
        { key: this.activedNode.key },
        'md',
        {
          nzMask: true,
          nzClosable: false,
        },
      )
      .subscribe(isSave => {
        console.log(isSave);
        if (isSave) {
          this.refresh();
        }
      });
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
