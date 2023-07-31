import React from 'react';
import s from './index.module.scss';
import type { CommentType } from '../../../data.d';
import CommentItem from './commentItem';

type Props = {
  comments?: Array<CommentType>;
};

const CommentList: React.FC<Props> = ({ comments }) => {
  return (
    <div className={s.commentList}>
      <div>
        {comments?.map((comment: CommentType) => {
          return <CommentItem comment={comment} />;
        })}
      </div>
    </div>
  );
};

export default CommentList;
