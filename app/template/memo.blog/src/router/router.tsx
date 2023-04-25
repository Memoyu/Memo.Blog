import { lazy } from 'react';
import { createBrowserRouter } from 'react-router-dom';

// 懒加载
const Home = lazy(() => import('@/pages/home'));
const Login = lazy(() => import('@/pages/login'));

let routers = createBrowserRouter([
  {
    path: '/',
    Component: Home,
    children: [
      // {
      //   path: 'todos',
      //   action: todosAction,
      //   loader: todosLoader,
      //   Component: TodosList,
      //   ErrorBoundary: TodosBoundary,
      //   children: [
      //     {
      //       path: ':id',
      //       loader: todoLoader,
      //       Component: Todo
      //     }
      //   ]
      // }
    ]
  },
  {
    path: '/login',
    Component: Login
  }
]);

export default routers;
