import React, { FC } from 'react';
import { Layout } from '@douyinfe/semi-ui';
import s from './index.module.scss';

const { Header } = Layout;

const Index: FC = () => {
  return (
    <Header className={s.header}>
      <h3>Header</h3>
    </Header>
  );
};

export default Index;
