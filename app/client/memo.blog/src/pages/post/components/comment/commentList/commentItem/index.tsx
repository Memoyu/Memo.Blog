import React from 'react';
import s from './index.module.scss';

interface Props {
  avatar?: string;
}

const CommentItem: React.FC<Props> = ({ avatar }) => {
  return (
    <div className={s.commentItem}>
      <div></div>
    </div>
  );
};

export default CommentItem;
