import { FC } from 'react';
import s from './index.module.scss';

const Logo: FC = () => {
  return (
    <div className={s.logo}>
      <span>
        MEMOYU
        <small>'BLOG</small>
      </span>
    </div>
  );
};

export default Logo;
