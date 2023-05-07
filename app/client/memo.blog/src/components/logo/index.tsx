import { FC } from 'react';
import s from './index.module.scss';

const Index: FC = () => {
  return (
    <div className={s.logo}>
      <span>
        MEMOYU
        <small>'BLOG</small>
      </span>
    </div>
  );
};

export default Index;
