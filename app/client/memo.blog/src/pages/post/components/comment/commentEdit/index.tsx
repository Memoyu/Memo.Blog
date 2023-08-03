import React, { useState } from 'react';
import { Tooltip, Input, TextArea, Button } from '@douyinfe/semi-ui';
import { IconEmoji, IconBrackets, IconSend } from '@douyinfe/semi-icons';
import classNames from 'classnames';
//import defaultAvatar from '@/assets/images/avatar/default.png';
import s from './index.module.scss';

const CommentEdit: React.FC = () => {
  const [avatar, setAvatar] = useState(require('@/assets/images/avatar/default.png'));
  const [avatarSelectVisible, setAvatarSelectVisible] = useState(false);
  const [qq, setQq] = useState('');

  const onQqInputBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    setQqAvatar();
    setAvatarSelectVisible(false);
  };

  const onQqInputEnterPress = () => {
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
              trigger="custom"
              visible={avatarSelectVisible}
              content={
                <div>
                  <Input
                    prefix="QQ头像："
                    showClear
                    value={qq}
                    onChange={(e) => setQq(e)}
                    onBlur={(e) => onQqInputBlur(e)}
                    onEnterPress={onQqInputEnterPress}></Input>
                </div>
              }>
              <img className={s.avatar} src={avatar} onClick={() => setAvatarSelectVisible(true)} />
            </Tooltip>
          </div>
          <Input className={s.inputInfo} placeholder={'昵称'}></Input>
          <Input className={s.inputInfo} placeholder={'邮箱(选填)'}></Input>
        </div>
        <TextArea className={s.commentTextArea} maxCount={100} showClear />
        <div className={s.commentBtns}>
          <div className={s.functionBtns}>
            <Button icon={<IconEmoji />} aria-label="emoji" />
          </div>
          <div className={s.operateBtns}>
            <Button icon={<IconBrackets />} className={s.itemBtn} aria-label="emoji">
              预览
            </Button>
            <Button icon={<IconSend />} className={s.itemBtn} aria-label="emoji">
              发送
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CommentEdit;
