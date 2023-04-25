import React, { FC } from 'react';
import { IconSemiLogo } from '@douyinfe/semi-icons';
import { Layout } from '@douyinfe/semi-ui';

const { Header } = Layout;

const Index: FC = () => {
  return (
    <Header
      style={{
        display: 'flex',
        justifyContent: 'space-between',
        padding: '20px',
        color: 'var(--semi-color-text-2)',
        backgroundColor: 'rgba(var(--semi-grey-0), 1)'
      }}>
      <h3>Header</h3>
    </Header>
  );
};

export default Index;
