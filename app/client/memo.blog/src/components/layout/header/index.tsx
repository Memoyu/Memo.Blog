import React, { FC } from 'react';
import { Button, Layout } from '@douyinfe/semi-ui';
import { IconGithubLogo, IconPaperclip } from '@douyinfe/semi-icons';
import s from './index.module.scss';

const { Header } = Layout;

const Index: FC = () => {
  return (
    <Header className={s.headerTop}>
      <div className={s.headerLogo}>
        <span>
          MEMOYU
          <small>'BLOG</small>
        </span>
      </div>
      <div className={s.headerLeftMenu}>
        <a>文章</a>
        <a>友链</a>
        <a>留言</a>
        <a>关于</a>
      </div>
      <div className={s.headerRightMenu}>
        <div className={s.blogTool}>
          <div className={s.blogToolContent}>
            <IconGithubLogo className={s.toolIcon} />
            <IconPaperclip className={s.toolIcon} />
          </div>
        </div>
      </div>
    </Header>
  );
};

export default Index;
