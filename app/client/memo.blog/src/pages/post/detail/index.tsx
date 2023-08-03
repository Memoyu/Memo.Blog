import React from 'react';
import { useState, useEffect } from 'react';
import MarkDown from '../components/markdown';
import Navigation from '../components/navigation';
import Comment from '../components/comment';
import s from './index.module.scss';
import MarkDownFile from '@/assets/md/test-code.md';
import { Tag, Space } from '@douyinfe/semi-ui';
import { CommentType } from '../data';

const PostDetail = () => {
  const [md, setMd] = useState('loading......');
  useEffect(() => {
    fetch(MarkDownFile)
      .then((resp) => resp.text())
      .then((txt) => setMd(txt));
  }, [md]);

  const data = {
    author: 'memoyu',
    title: '文章的标题',
    date: '2023-07-21 19:30:40',
    desc: '前言：本次的教程与上次的基于 WePY 2.x 平台下使用 ECharts方式基本一致，毕竟目标平台都是微信小程序而已（别的平台未测试），只是就是多了一个参数而已。',
    tags: ['UNI-APP', 'echarts', 'mbill']
  };

  const comments: Array<CommentType> = [
    {
      id: 1,
      isAutor: false,
      name: '小明',
      avatar: 'https://q1.qlogo.cn/g?b=qq&nk=904703311&s=100',
      content:
        '这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复这是回复',
      date: '2023-07-31 23:19',
      sort: 1
    },
    {
      id: 2,
      isAutor: true,
      name: '小红',
      avatar: 'https://q1.qlogo.cn/g?b=qq&nk=904703312&s=100',
      content: '这是回复红这是回复这是回复这是回复这是回复这是回复这是回复',
      date: '2023-07-31 23:19',
      sort: 2
    },
    {
      id: 3,
      isAutor: false,
      name: '小刚',
      avatar: 'https://q1.qlogo.cn/g?b=qq&nk=904703317&s=100',
      content: '这我是小刚这我是小刚这我是小刚这我是小刚这我是小刚这我是小刚这我是小刚这我是小刚',
      date: '2023-07-31 23:19',
      sort: 3
    }
  ];

  return (
    <div className={s.postContainer}>
      <div className={s.postHeader}>
        <div className={s.postTitle}>{data.title}</div>
        <Space wrap>
          <Tag color="indigo" size="large">
            作者：{data.author}
          </Tag>
          <Tag color="indigo" size="large">
            时间：{data.date}
          </Tag>
        </Space>
        <div className={s.postDesc}>{data.desc}</div>
      </div>
      <div className={s.postContent}>
        <Space wrap>
          {data.tags.map((item) => (
            <Tag color="light-green" key={item} size="large">
              {item}
            </Tag>
          ))}
        </Space>
        <div className={s.markDown}>
          <MarkDown content={md} />
          <Navigation content={md} />
        </div>
        <Comment comments={comments} />
      </div>
    </div>
  );
};

export default PostDetail;
