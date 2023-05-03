import React, { lazy, Suspense } from 'react';
import { RouterProvider } from 'react-router-dom';
import FallbackLoading from '@/components/fallback-loading';
import routes from '@/router/router';
import s from './index.module.scss';

const Main: React.FC = () => (
  <main className={s.main}>
    <div className={s.center}>
      <Suspense fallback={<FallbackLoading message="正在加载中" />}>
        <RouterProvider router={routes} />
      </Suspense>
    </div>
  </main>
);

export default Main;
