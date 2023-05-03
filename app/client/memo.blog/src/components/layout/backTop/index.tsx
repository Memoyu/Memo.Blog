import React, { FC } from 'react';
import { BackTop } from '@douyinfe/semi-ui';
import { IconArrowUp } from '@douyinfe/semi-icons';
import s from './index.scss';

const Index: FC = () => {
  return (
    <BackTop className={s.backTop}>
      <IconArrowUp />
    </BackTop>
  );
};

export default Index;
