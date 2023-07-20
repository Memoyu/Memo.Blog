import React from 'react';
import { useState, useEffect } from 'react';
import MarkDown from '../components/md';
import s from './index.module.scss';
import MarkDownFile from '@/assets/md/test-code.md';
import { Tag, Space } from '@douyinfe/semi-ui';

const PostDetail = () => {
  const [md, setMd] = useState('loading......');
  useEffect(() => {
    fetch(MarkDownFile)
      .then((resp) => resp.text())
      .then((txt) => setMd(txt));
  }, [md]);

  const data = {
    title: '文章的标题',
    date: '2023-07-21 19:30:40',
    desc: '前言：本次的教程与上次的基于 WePY 2.x 平台下使用 ECharts方式基本一致，毕竟目标平台都是微信小程序而已（别的平台未测试），只是就是多了一个参数而已。',
    tags: ['UNI-APP', 'echarts', 'mbill']
  };

  return (
    <div className={s.postDetail}>
      <div className={s.postTitle}>{data.title}</div>
      <Space wrap>
        <Tag color="indigo" size="large">
          作者：{data.title}
        </Tag>
        <Tag color="indigo" size="large">
          时间：{data.date}
        </Tag>
      </Space>
      <div className={s.postDesc}>{data.desc}</div>
      <Space wrap>
        {data.tags.map((item) => (
          <Tag color="light-green" key={item} size="large">
            {item}
          </Tag>
        ))}
      </Space>
      <div className={s.markDown}>
        <MarkDown content={md} />
      </div>
    </div>
  );
};

export default PostDetail;
