import React, { useState } from 'react';
import classNames from 'classnames';
import MarkdownNavbar from 'markdown-navbar';
import s from './index.module.scss';
import 'markdown-navbar/dist/navbar.css';

interface Props {
  content?: string;
  setNavShow?: Function;
}

const Navigation: React.FC<Props> = ({ content, setNavShow }) => {
  const [navVisible, setNavVisible] = useState(true);

  return (
    <div className={classNames(s.postNavBar, [navVisible ? s.postNavBarShow : s.postNavBarHide])}>
      <div
        className={s.toggleBtn}
        onClick={() => {
          setNavVisible(!navVisible);
        }}>
        {navVisible ? '→' : '←'}
      </div>
      <MarkdownNavbar
        source={content || ''}
        // headingTopOffset={15}
        // ordered={false}
        // updateHashAuto={false}
        //onNavItemClick={() => setNavShow?.(false)}
      />
    </div>
  );
};

export default Navigation;
