import { Button } from 'antd';
import React, { useState } from 'react';

import s from './index.less';

export const About: React.FC = () => {
  const [title, setTitle] = useState<boolean>('关于信息');
  return (
    <div>
      {title}
      <Button className={s.styles}>按钮</Button>
    </div>
  );
};

export default About;
