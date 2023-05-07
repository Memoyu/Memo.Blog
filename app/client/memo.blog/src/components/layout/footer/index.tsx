import { FC } from 'react';
import { Layout, Col, Row } from '@douyinfe/semi-ui';
import s from './index.module.scss';
import Logo from '@/components/logo';

const { Footer } = Layout;
type Item = {
  name: string;
  to: string;
};

const links: Array<Item> = [
  { name: 'Memoyu Github', to: 'https://github.com/Memoyu' },
  { name: 'Memoyu Cnblog', to: 'https://www.cnblogs.com/memoyu' }
];

const records: Array<Item> = [
  { name: '桂ICP备20005450号', to: 'https://beian.miit.gov.cn/#/Integrated/index' },
  { name: 'About Me', to: '/link' }
];

const Index: FC = () => {
  return (
    <Footer className={s.footer}>
      <Row>
        <Col span={7}>
          <div className={s.author}>
            <div className={s.logo}>
              <Logo />
            </div>
            <div className={s.github}>
              2022 - 2023 ©&nbsp;
              <a target="_blank" href="https://github.com/Memoyu" title="Memoyu">
                Memoyu
              </a>
            </div>
            <small className={s.info}>
              Made with ❤️ on .NET 6 & React <br /> Updated on 2023, 5月4日 06:40
            </small>
          </div>
        </Col>
        <Col span={10}>
          <div className={s.link}>
            {links.map((link, index) => (
              <div className={s.text}>
                <a target="_blank" href={link.to}>
                  {link.name}
                </a>
              </div>
            ))}
          </div>
        </Col>
        <Col span={7}>
          <div className={s.link}>
            {records.map((record, index) => (
              <div className={s.text}>
                <a target="_blank" href={record.to}>
                  {record.name}
                </a>
              </div>
            ))}
          </div>
        </Col>
      </Row>
    </Footer>
  );
};

export default Index;
