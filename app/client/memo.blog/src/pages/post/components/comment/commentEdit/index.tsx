import React, { useState } from 'react';
import { Tooltip, Input, Button } from 'antd';
import { SmileOutlined, FireOutlined, MessageOutlined } from '@ant-design/icons';
import classNames from 'classnames';
import s from './index.module.scss';

const { TextArea } = Input;

const CommentEdit: React.FC = () => {
  const [avatar, setAvatar] = useState(require('@/assets/images/avatar/default.png'));
  const [avatarSelectVisible, setAvatarSelectVisible] = useState(false);
  const [qq, setQq] = useState('');

  const onQqInputBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    setQqAvatar();
    setAvatarSelectVisible(false);
  };

  const onQqInputPressEnter = () => {
    setQqAvatar();
    setAvatarSelectVisible(false);
  };

  const setQqAvatar = () => {
    const regQq = /[1-9][0-9]{4,11}/;
    if (regQq.test(qq!)) {
      setAvatar(`https://q1.qlogo.cn/g?b=qq&nk=${qq}&s=100`);
    }
  };

  return (
    <div>
      <div className={s.commentEdit}>
        <div className={s.commentUser}>
          <div className={s.avatarBox}>
            <Tooltip
              style={{ backgroundColor: 'white' }}
              trigger="click"
              open={avatarSelectVisible}
              title={
                <div>
                  <Input
                    prefix="QQ头像："
                    value={qq}
                    onChange={(e) => setQq(e.target.value)}
                    onBlur={(e) => onQqInputBlur(e)}
                    onPressEnter={onQqInputPressEnter}></Input>
                </div>
              }>
              <img className={s.avatar} src={avatar} onClick={() => setAvatarSelectVisible(true)} />
            </Tooltip>
          </div>
          <Input className={s.inputInfo} placeholder={'昵称'}></Input>
          <Input className={s.inputInfo} placeholder={'邮箱(选填)'}></Input>
        </div>
        <TextArea className={s.commentTextArea} maxLength={100} />
        <div className={s.commentBtns}>
          <div className={s.functionBtns}>
            <Button icon={<SmileOutlined />} aria-label="emoji" />
          </div>
          <div className={s.operateBtns}>
            <Button icon={<FireOutlined />} className={s.itemBtn} aria-label="emoji">
              预览
            </Button>
            <Button icon={<MessageOutlined />} className={s.itemBtn} aria-label="emoji">
              发送
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CommentEdit;
