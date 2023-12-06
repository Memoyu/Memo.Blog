export default [
  { name: '数据概览', path: '/dashboard', icon: 'LineChart', component: './dashboard' },
  {
    path: '/post',
    name: '博客管理',
    icon: 'FileWord',
    routes: [
      { name: '文章管理', path: '/post/list', component: './post' },
      { name: '文章编辑', hideInMenu: true, path: '/post/edit/:id', component: './post/edit' },
      { name: '文章分类', path: '/post/category', component: './post/category' },
      { name: '文章标签', path: '/post/tag', component: './post/tag' },
      { name: '评论管理', path: '/post/comment', component: './post/comment' },
    ],
  },
  {
    path: '/page',
    name: '页面管理',
    icon: 'FilePpt',
    routes: [
      { name: '站点信息', path: '/page/site', component: './page/site' },
      { name: '友链管理', path: '/page/friends', component: './page/friends' },
      { name: '关于信息', path: '/page/about', component: './page/about' },
    ],
  },
  {
    path: '/account',
    name: '用户管理',
    icon: 'User',
    routes: [{ name: '用户管理', path: '/account/list', component: './account' }],
  },
  {
    path: '/log',
    name: '日志管理',
    icon: 'Bug',
    routes: [
      { name: '系统日志', path: '/logger/system', component: './logger/system' },
      { name: '访问日志', path: '/logger/access', component: './logger/access' },
    ],
  },
  { path: '/', redirect: '/dashboard' },
  { name: '登录', layout: false, path: '/account/login', component: './account/login' },
  { path: '*', layout: false, component: './noFound' },
];
