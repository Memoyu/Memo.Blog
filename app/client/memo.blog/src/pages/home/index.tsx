import { Image } from '@douyinfe/semi-ui';
import { motion } from 'framer-motion';
import s from './index.module.scss';

const Home = () => {
  const postListVar = {
    visible: {
      opacity: 1,
      transition: {
        when: 'beforeChildren',
        staggerChildren: 0.1
      }
    },
    hidden: {
      opacity: 0,
      transition: {
        when: 'afterChildren'
      }
    }
  };

  const postListItemVar = {
    visible: { opacity: 1, x: 0 },
    hidden: { opacity: 0, x: -100 }
  };

  const imgListVar = {
    visible: { opacity: 1, x: 0 },
    hidden: { opacity: 0, x: 100 }
  };
  return (
    <div className={s.home}>
      <div className={s.container}>
        <div className={s.topPost}>
          <motion.div className={s.list} initial="hidden" animate="visible" variants={postListVar}>
            <motion.div className={s.topForward} variants={postListItemVar}>
              <div style={{ height: '100%', display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <span style={{ fontSize: 40, fontWeight: 'bold' }}>OPEN</span>
                  <span style={{ fontSize: 25, fontWeight: 'bold', color: 'blue' }}>*TO THE WORLD</span>
                </div>
                <span style={{ fontSize: 40, fontWeight: 'bold' }}>YOURSELF</span>
              </div>
            </motion.div>
            <motion.div className={s.topForward} variants={postListItemVar}></motion.div>
            <motion.div className={s.topForward} variants={postListItemVar}></motion.div>
            <motion.div className={s.topBack} variants={postListItemVar}></motion.div>
            <motion.div className={s.topBack} variants={postListItemVar}></motion.div>
          </motion.div>
        </div>
        <motion.div className={s.show} initial="hidden" animate="visible" variants={imgListVar}>
          <div className={s.image}>
            {/* <Image className={s.img} src="https://w.wallhaven.cc/full/zy/wallhaven-zyqo8o.png"></Image> */}
          </div>
        </motion.div>
      </div>
    </div>
  );
};

export default Home;
