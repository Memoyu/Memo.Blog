import React, { lazy, Suspense } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Layout } from '@douyinfe/semi-ui';
import { RouterProvider } from 'react-router-dom';
import FallbackLoading from '@/components/fallback-loading';
import routes from '@/router/router';

const { Content } = Layout;
const Main: React.FC = () => (
  <Content className="layout-content">
    <Suspense fallback={<FallbackLoading message="正在加载中" />}>
      <RouterProvider router={routes} />
    </Suspense>
  </Content>
);

export default Main;
