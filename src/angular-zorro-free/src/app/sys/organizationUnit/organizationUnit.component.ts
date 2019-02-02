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
  UserServiceProxy,
  UsersToOrganizationUnitInput,
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
import { EditOrganizationUnitComponent } from '@app/sys/organizationUnit/edit-organizationUnit/edit-organizationUnit.component';
import { LookupModelUserComponent } from '@app/users/lookupModel-user/lookupModel-user-component';
import { stringify } from '@angular/core/src/render3/util';

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
  activedNode: NzTreeNode;
  dropdown: NzDropdownContextComponent;
  // actived node
  selectedNodeKey: string = null; //（用于获取该组织架构中用户）
  activedNodeKey: string = null; //（用于判断新增组织架构级别，操作后清空）
  organizationUnitList: NzTreeNode[];
  requestThis: PagedRequestDto;
  pageNumberThis: number;
  finishedCallbackThis: Function;
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

  //选中组织架构节点
  activeNode(data: NzFormatEmitEvent): void {
    if (this.activedNode) {
      // delete selectedNodeList(u can do anything u want)
      this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode);
    }
    data.node.isSelected = true;
    this.activedNode = data.node;
    // add selectedNodeList
    this.treeCom.nzTreeService.setSelectedNodeList(this.activedNode);

    this.selectedNodeKey = data.node.key.toString(); //选中节点编号（用于获取该组织架构中用户）
    this.GetUser();
  }

  //出现组织架构右键菜单
  contextMenu(
    $event: MouseEvent,
    template: TemplateRef<void>,
    key: string,
  ): void {
    this.activedNodeKey = key.toString();
    this.dropdown = this.nzDropdownService.create($event, template);
  }

  //获取特定组织架构的用户
  GetUser(): void {
    this._organizationUnitService
      .getUserByOrganizationUnit(
        this.selectedNodeKey,
        this.requestThis.skipCount,
        this.requestThis.maxResultCount,
      )
      .finally(() => {
        this.finishedCallbackThis();
      })
      .subscribe((result: PagedResultDtoOfOrganizationUnitUserDto) => {
        console.log(result);
        this.dataList = result.items;
        this.totalItems = result.totalCount;
      });
  }

  //新增子组织架构 或 新增顶级组织架构
  createChild(): void {
    let key: string = this.activedNodeKey;
    this.modalHelper
      .open(CreateOrganizationUnitComponent, { key: key }, 'md', {
        nzMask: true,
        nzClosable: false,
      })
      .subscribe(isSave => {
        this.activedNodeKey = null;
        if (isSave) {
          this.refresh();
          this.nzDropdownService.close();
        }
      });
  }

  //修改组织架构显示名
  editChild(): void {
    let key: string = this.activedNodeKey;
    if (key == '') {
      abp.notify.error('组织架构参数丢失！');
    }
    this.modalHelper
      .open(EditOrganizationUnitComponent, { key: key }, 'md', {
        nzMask: true,
        nzClosable: false,
      })
      .subscribe(isSave => {
        this.activedNodeKey = null;
        if (isSave) {
          this.refresh();
          this.nzDropdownService.close();
        }
      });
  }

  //移除用户
  removeUser(userId: number, organizationUnitCode: string): void {
    this._organizationUnitService
      .removeUsers([userId], organizationUnitCode)
      .finally(() => {
        this.finishedCallbackThis();
      })
      .subscribe(() => {
        this.GetUser();
      });
  }

  lookup(): void {
    if (this.selectedNodeKey == null || this.selectedNodeKey == '') {
      abp.notify.warn('请先选择一个组织架构！');
    } else {
      this.modalHelper
        .open(
          LookupModelUserComponent,
          {
            roleNameArray: [],
          },
          'md',
          {
            nzMask: true,
            nzClosable: false,
          },
        )
        .subscribe(isSave => {
          let userIdArray: number[] = [];
          if (isSave) {
            userIdArray.push(Number(isSave.value));
            this.addUser(userIdArray);
          }
        });
    }
  }

  addUser(userIdArray: number[]): void {
    let input = new UsersToOrganizationUnitInput();
    input.userIds = userIdArray;
    input.organizationUnitCode = this.selectedNodeKey;
    console.log(input);
    this._organizationUnitService
      .addUsers(input)
      .finally(() => {})
      .subscribe(() => {
        this.GetUser();
      });
  }
}
