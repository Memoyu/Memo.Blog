import React, { useState } from 'react';
import classNames from 'classnames';
import s from './index.module.scss';
import CommentList from './commentList';

interface Props {
  content?: string;
}

const Comment: React.FC<Props> = ({ content }) => {
  return (
    <div>
      <div className={s.divider} />
      <CommentList />
    </div>
  );
};

export default Comment;
