import React from 'react';
import s from './index.module.scss';
import CommentItem from './commentItem';

interface Props {
  avatar?: string;
}

const CommentList: React.FC<Props> = ({ avatar }) => {
  return (
    <div className={s.commentList}>
      <div></div>
    </div>
  );
};

export default CommentList;
