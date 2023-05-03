import React, { FC } from 'react';
import { IconSemiLogo } from '@douyinfe/semi-icons';
import { Layout } from '@douyinfe/semi-ui';
import s from './index.module.scss';

const { Footer } = Layout;

const Index: FC = () => {
  return (
    <Footer className={s.footer}>
      <span className={s.auhtor}>
        <IconSemiLogo size="large" style={{ marginRight: '8px' }} />
        <span>Copyright ©2021 memoyu. All Rights Reserved. </span>
      </span>
      <span>
        <span style={{ marginRight: '24px' }}>Github地址</span>
        <span>反馈建议</span>
      </span>
    </Footer>
  );
};

export default Index;
