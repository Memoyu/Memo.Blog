import React, { FC } from 'react';
import { Layout } from '@douyinfe/semi-ui';
import s from './index.module.scss';

const { Header } = Layout;

const Index: FC = () => {
  return (
    <Header className={s.headerTop}>
      <div className={s.headerLeftMenu}>
        <a>文章</a>
        <a>文章</a>
        <a>文章</a>
      </div>
      <div className={s.headerRightMenu}>
        <div className={s.blogTool}>
          <span>MEMOYU BLOG</span>
        </div>
      </div>
    </Header>
  );
};

export default Index;
