import React from 'react';
import classNames from 'classnames';
import s from './index.module.scss';
import type { CommentType } from '../../../../data.d';

type Props = {
  comment: CommentType;
};

const CommentItem: React.FC<Props> = ({ comment }) => {
  return (
    <div className={classNames(s.commentItem, { [s.reverseCommentItem]: comment.isAutor })}>
      <div className={s.avatarBox}>
        <img className={s.avatar} src={comment.avatar} />
      </div>
      <div className={s.commentBox}>
        <div className={s.commentInfo}>
          <div>#{comment.sort}楼</div>
          <div>{comment.name}</div>
          <div>{comment.date}</div>
        </div>
        <div className={s.commentContent}>
          <div>{comment.content}</div>
        </div>
      </div>
    </div>
  );
};

export default CommentItem;
