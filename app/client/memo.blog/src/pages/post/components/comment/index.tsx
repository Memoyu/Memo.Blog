import React, { useState } from 'react';
import classNames from 'classnames';
import s from './index.module.scss';
import CommentList from './commentList';
import CommentEdit from './commentEdit';
import type { CommentType } from '../../data.d';

type Props = {
  comments?: Array<CommentType>;
};

const Comment: React.FC<Props> = ({ comments }) => {
  return (
    <div>
      <div className={s.divider} />
      <CommentList comments={comments} />
      <CommentEdit />
    </div>
  );
};

export default Comment;
