export default [
  { name: '数据概览', path: '/dashboard', icon: 'smile', component: './dashboard' },
  {
    path: '/post',
    name: '博客管理',
    icon: 'crown',
    routes: [
      { name: '文章管理', icon: 'smile', path: '/post/list', component: './post' },
      { name: '文章编辑', hideInMenu: true, path: '/post/edit/:id', component: './post/edit' },
      { name: '文章分类', icon: 'smile', path: '/post/category', component: './post/category' },
      { name: '文章标签', icon: 'smile', path: '/post/tag', component: './post/tag' },
      { name: '评论管理', icon: 'smile', path: '/post/comment', component: './post/comment' },
    ],
  },
  {
    path: '/page',
    name: '页面管理',
    icon: 'crown',
    routes: [
      { name: '站点信息', icon: 'smile', path: '/page/site', component: './page/site' },
      { name: '友链管理', icon: 'smile', path: '/page/friends', component: './page/friends' },
      { name: '关于信息', icon: 'smile', path: '/page/about', component: './page/about' },
    ],
  },
  {
    path: '/user',
    name: '用户管理',
    icon: 'crown',
    routes: [{ name: '用户管理', icon: 'smile', path: '/user/list', component: './user' }],
  },
  {
    path: '/log',
    name: '日志管理',
    icon: 'crown',
    routes: [
      { name: '系统日志', icon: 'smile', path: '/logger/system', component: './logger/system' },
      { name: '访问日志', icon: 'smile', path: '/logger/access', component: './logger/access' },
    ],
  },
  { path: '/', redirect: '/dashboard' },
  { name: '登录', layout: false, path: '/user/login', component: './user/login' },
  { path: '*', layout: false, component: './noFound' },
];
