import { lazy } from 'react';
import { useRoutes } from 'react-router-dom';

// 懒加载
const Home = lazy(() => import('@/pages/home'));
const Login = lazy(() => import('@/pages/login'));

interface Router {
  name?: string;
  path: string;
  children?: Array<Router>;
  element: any;
}

let routes: Array<Router> = [
  {
    path: '/',
    element: <Home />,
    children: [
      // { path: ":id", element: <Invoice /> },
      // { path: "/pending", element: <Pending /> },
      // { path: "/complete", element: <Complete /> },
    ]
  },
  {
    path: '/login',
    element: <Login />
  }
];

export default routes;
