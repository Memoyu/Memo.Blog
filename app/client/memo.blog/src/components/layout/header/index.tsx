import React, { FC } from 'react';
import { NavLink, useLocation } from 'react-router-dom';
import { Button, Layout } from '@douyinfe/semi-ui';
import { IconGithubLogo, IconPaperclip } from '@douyinfe/semi-icons';
import { motion } from 'framer-motion';
import s from './index.module.scss';

type Item = {
  name: string;
  to: string;
};

const items: Array<Item> = [
  { name: '首页', to: '/' },
  { name: '友链', to: '/link' },
  { name: '关于', to: '/about' }
];

const { Header } = Layout;

const Index: FC = () => {
  let location = useLocation();
  let pathname = location.pathname || '/';
  if (pathname === '/404' || pathname === '/_not-found') pathname = '/';
  console.log('p', pathname);
  return (
    <Header className={s.header}>
      <div className={s.headerNav}>
        <div className={s.container}>
          {items.map((item, index) => (
            <NavLink to={item.to} key={index}>
              {item.name}
              {item.to === pathname && (
                <motion.div
                  className={s.active}
                  layoutId="bar"
                  aria-hidden={true}
                  transition={{ type: 'spring', stiffness: 350, damping: 30 }}
                />
              )}
            </NavLink>
          ))}
        </div>
      </div>
    </Header>
  );
};

export default Index;
