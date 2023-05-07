import React, { lazy, Suspense } from 'react';
import { useRoutes } from 'react-router-dom';
import FallbackLoading from '@/components/fallback-loading';
import routes from '@/router';
import s from './index.module.scss';

const Main: React.FC = () => {
  const elements = useRoutes(routes);
  return (
    <main className={s.main}>
      <div className={s.center}>
        {/* fallback={<FallbackLoading message="正在加载中" />} */}
        <Suspense>{elements}</Suspense>
      </div>
    </main>
  );
};

export default Main;
