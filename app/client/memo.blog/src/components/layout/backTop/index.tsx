import { FC } from 'react';
import { FloatButton } from 'antd';

import s from './index.scss';

const CustBackTop: FC = () => {
  const style = {
    // display: 'flex',
    // alignItems: 'center',
    // justifyContent: 'center',
    // height: 40,
    // width: 40,
    // background: 'linear-gradient(90deg,#6457c1,#b84297)'
    // color: '#fff',
    // bottom: 80
  };

  return <FloatButton.BackTop style={style} visibilityHeight={0} />;
};

export default CustBackTop;
