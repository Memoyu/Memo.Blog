import { FC } from 'react';
import { BackTop } from '@douyinfe/semi-ui';
import { IconArrowUp } from '@douyinfe/semi-icons';
import s from './index.scss';

const Index: FC = () => {
  const style = {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    height: 40,
    width: 40,
    borderRadius: '100%',
    background: 'linear-gradient(90deg,#6457c1,#b84297)',
    color: '#fff',
    bottom: 80
  };

  return (
    <BackTop style={style}>
      <IconArrowUp />
    </BackTop>
  );
};

export default Index;
