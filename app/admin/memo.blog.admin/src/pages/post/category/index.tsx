import { Button } from 'antd';
import React, { useState } from 'react';

import s from './index.less';

export const PostCategory: React.FC = () => {
  const [title, setTitle] = useState<string>('分类管理');
  return (
    <div>
      {title}
      <Button className={s.styles}>按钮</Button>
    </div>
  );
};

export default PostCategory;
