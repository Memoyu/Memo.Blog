import { Button } from 'antd';
import React, { useState } from 'react';

import s from './index.less';

export const PostComment: React.FC = () => {
  const [title, setTitle] = useState<boolean>('评论管理');
  return (
    <div>
      {title}
      <Button className={s.styles}>按钮</Button>
    </div>
  );
};

export default PostComment;
