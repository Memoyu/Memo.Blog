import React from 'react';
import { useState, useEffect } from 'react';
import MarkDown from '../components/md';
import s from './index.module.scss';
import MarkDownFile from '@/assets/md/test-code.md';

const TimeLine = () => {
  const [md, setMd] = useState('loading......');
  useEffect(() => {
    fetch(MarkDownFile)
      .then((resp) => resp.text())
      .then((txt) => setMd(txt));
  }, [md]);

  return (
    <div className={s.markDown}>
      <MarkDown content={md} />
    </div>
  );
};

export default TimeLine;
