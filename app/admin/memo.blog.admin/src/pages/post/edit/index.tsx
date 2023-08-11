import { Button } from 'antd';
import React, { useState } from 'react';

import s from './index.less';

export const PostEdit: React.FC = () => {
  const [title, setTitle] = useState<string>('文章编辑');
  return (
    <div>
      {title}
      <Button className={s.styles}>按钮</Button>
    </div>
  );
};

export default PostEdit;
