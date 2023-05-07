import { Col, Row } from '@douyinfe/semi-ui';
import s from './index.module.scss';

const Home = () => {
  return (
    <div className={s.home}>
      <div className={s.container}>
        <div className={s.topPost}>
          <div className={s.list}>
            <div className={s.topForward}>
              <div style={{ height: '100%', display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <span style={{ fontSize: 40, fontWeight: 'bold' }}>OPEN</span>
                  <span style={{ fontSize: 25, fontWeight: 'bold', color: 'blue' }}>*TO THE WORLD</span>
                </div>
                <span style={{ fontSize: 40, fontWeight: 'bold' }}>YOURSELF</span>
              </div>
            </div>
            <div className={s.topForward}></div>
            <div className={s.topForward}></div>
            <div className={s.topBack}></div>
            <div className={s.topBack}></div>
          </div>
        </div>
        <div className={s.show}>
          <div className={s.image}></div>
        </div>
      </div>
    </div>
  );
};

export default Home;
