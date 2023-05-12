import { lazy } from 'react';
import { useRoutes } from 'react-router-dom';

// 懒加载
const Home = lazy(() => import('@/pages/home'));
const TimeLine = lazy(() => import('@/pages/timeline'));
const Link = lazy(() => import('@/pages/link'));
const About = lazy(() => import('@/pages/about'));
const Login = lazy(() => import('@/pages/login'));
const PostDetail = lazy(() => import('@/pages/post/detail'));

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
    path: '/timeline',
    element: <TimeLine />
  },
  {
    path: '/link',
    element: <Link />
  },
  {
    path: '/about',
    element: <About />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/post/detail/:id',
    element: <PostDetail />
  }
];

export default routes;
