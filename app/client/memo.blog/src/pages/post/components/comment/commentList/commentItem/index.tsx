import React from 'react';
import { Tooltip } from 'antd';
import { NumberOutlined, MessageOutlined } from '@ant-design/icons';
import classNames from 'classnames';
import s from './index.module.scss';
import type { CommentType } from '../../../../data.d';

type Props = {
  comment: CommentType;
};

const CommentItem: React.FC<Props> = ({ comment }) => {
  return (
    <div className={classNames([s.commentItem, comment.isAutor ? s.rightCommentItem : s.leftCommentItem])}>
      <div className={s.avatarBox}>
        <img className={s.avatar} src={comment.avatar} />
      </div>
      <div className={s.commentBox}>
        <div className={s.commentInfo}>
          <div>#{comment.sort}楼</div>
          <div className={s.name}>{comment.name}</div>
          <div>{comment.date}</div>
          <Tooltip title={'回复'}>
            <NumberOutlined className={s.iconAt} />
          </Tooltip>
          <Tooltip title={'引用'}>
            <MessageOutlined />
          </Tooltip>
        </div>
        <div className={s.commentContent}>
          <div>{comment.content}</div>
        </div>
      </div>
    </div>
  );
};

export default CommentItem;
