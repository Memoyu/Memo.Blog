import { Button } from 'antd';
import React, { useState } from 'react';

import s from './index.less';

export const Friends: React.FC = () => {
  const [title, setTitle] = useState<string>('友链管理');
  return (
    <div>
      {title}
      <Button className={s.styles}>按钮</Button>
    </div>
  );
};

export default Friends;
