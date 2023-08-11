module.exports = {
  'GET /api/currentUser': {
    data: {
      name: 'Serati Ma',
      avatar: 'https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png',
      userid: '00000001',
      email: 'antdesign@alipay.com',
      signature: '海纳百川，有容乃大',
      title: '交互专家',
      group: '蚂蚁金服－某某某事业群－某某平台部－某某技术部－UED',
      tags: [
        { key: '0', label: '很有想法的' },
        { key: '1', label: '专注设计' },
        { key: '2', label: '辣~' },
        { key: '3', label: '大长腿' },
        { key: '4', label: '川妹子' },
        { key: '5', label: '海纳百川' },
      ],
      notifyCount: 12,
      unreadCount: 11,
      country: 'China',
      geographic: {
        province: { label: '浙江省', key: '330000' },
        city: { label: '杭州市', key: '330100' },
      },
      address: '西湖区工专路 77 号',
      phone: '0752-268888888',
    },
  },
  // 'GET /api/post/list': {
  //   data: [
  //     {
  //       id: 8213123140,
  //       title: '这是后端文章的标题',
  //       category: { id: 232444255, name: '后端开发' },
  //       tags: [
  //         { id: 24234234, name: '.NET' },
  //         { id: 24234235, name: 'C#' },
  //         { id: 24234236, name: '后端' },
  //       ],
  //       status: 2,
  //       updatedAt: '2022-12-06T05:00:57.040Z',
  //       createdAt: '2022-12-06T05:00:57.040Z',
  //     },
  //     {
  //       id: 8213123140,
  //       title: '这是前端文章的标题',
  //       category: { id: 232444255, name: '前端开发' },
  //       tags: [
  //         { id: 24234234, name: 'React' },
  //         { id: 24234234, name: 'Typescript' },
  //         { id: 24234235, name: 'Javascript' },
  //         { id: 24234236, name: '前端' },
  //       ],
  //       status: 1,
  //       updatedAt: '2022-12-07T05:00:57.040Z',
  //       createdAt: '2022-12-08T05:00:57.040Z',
  //     },
  //   ],
  //   total: 100,
  //   success: true,
  //   pageSize: 20,
  //   current: 1,
  // },
  'POST /api/login/outLogin': { data: {}, success: true },
  'POST /api/login/account': {
    status: 'ok',
    type: 'account',
    currentAuthority: 'admin',
  },
};
