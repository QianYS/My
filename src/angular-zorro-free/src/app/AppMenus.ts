import { MenuItem } from '@yoyo/theme';

// 全局的左侧导航菜单
export class AppMenus {
  static Menus = [
    // 首页
    new MenuItem('HomePage', '', 'anticon anticon-home', '/app/home'),
    // 租户
    new MenuItem(
      'Tenants',
      'Pages.Sys.Tenants.ShowIndex',
      'anticon anticon-team',
      '/app/tenants',
    ),
    // 角色
    new MenuItem(
      'Roles',
      'Pages.Sys.Roles.ShowIndex',
      'anticon anticon-safety',
      '/app/roles',
    ),
    // 用户
    new MenuItem(
      'Users',
      'Pages.Sys.Users.ShowIndex',
      'anticon anticon-user',
      '/app/users',
    ),
    new MenuItem('系统管理', '', 'anticon anticon-setting', '', [
      // 登录记录
      new MenuItem(
        '登录记录',
        '',
        'anticon anticon-login',
        '/app/sys/loginAttempt',
      ),
      // 组织架构
      new MenuItem(
        '组织架构',
        '',
        'anticon anticon-team',
        '/app/sys/organizationUnit',
      ),
      // 日志管理
      new MenuItem('日志管理', '', 'anticon anticon-file', '/app/sys/auditLog'),
    ]),

    // 关于我们
    new MenuItem('About', '', 'anticon anticon-info-circle-o', '/app/about'),
  ];
}
